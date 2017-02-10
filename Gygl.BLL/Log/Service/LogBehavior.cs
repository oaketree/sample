using Core.Log;
using Core.Utility;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Log.Service
{
    public class LogBehavior : ICallHandler
    {
        //[Dependency]
        //public ILogConfig myLog { get; set; }//特性无法采用注入形式
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var myLog = new LogConfig();
            //执行前
            //if (input.MethodBase.Name == "getTitle")
            //{
            //    myLog.logInfo("正在查询文章名称");
            //}
            string message = string.Format("class:{0},function:{1},ip:{2}", input.Target.ToString(), input.MethodBase.Name, Utils.GetIP());
            myLog.logInfo(message);
            //myLog.logWarn(input.MethodBase.Name);
            //开始执行了
            IMethodReturn retvalue = getNext()(input, getNext);
            if (retvalue.Exception != null)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.Append(string.Format("发生异常对象及方法{0}:{1},", input.Target.ToString(), input.MethodBase.Name));
                for (int i = 0; i < input.Arguments.Count; i++)
                {
                    var parameter = input.Arguments[i];
                    errorMessage.Append(string.Format("第{0}个参数值为:{1}", i + 1, parameter.ToString()));
                }
                errorMessage.Append(string.Format("异常IP:{0}", Utils.GetIP()));
                myLog.logError(errorMessage.ToString(), retvalue.Exception);
                retvalue.Exception = null;
            }
            return retvalue;
        }

        public int Order { get; set; }
    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class LogHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogBehavior();//返回日志
        }
    }
}
