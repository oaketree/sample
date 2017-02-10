using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log
{
    public class ExceptionLogBehavior : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var myLog = new LogException();
            IMethodReturn retvalue = getNext()(input, getNext);
            if (retvalue.Exception != null)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.Append(string.Format("异常方法名：{0}", input.MethodBase.Name));
                for (int i = 0; i < input.Arguments.Count; i++)
                {
                    var parameter = input.Arguments[i];
                    errorMessage.Append(string.Format("第{0}个参数值为:{1}", i + 1, parameter.ToString()));
                }
                myLog.logError(errorMessage.ToString(),retvalue.Exception);
                retvalue.Exception = null;
            }
            //myLog.Log(string.Format("方法名：{0}开始执行",input.MethodBase.Name));
            ////Console.WriteLine("执行前");
            //#region 参数部分
            //myLog.Log("-------------参数内容---------------");
            //for (int i = 0; i < input.Arguments.Count; i++)
            //{
            //    var parameter = input.Arguments[i];
            //    myLog.Log(string.Format("第{0}个参数值为:{1}", i + 1, parameter.ToString()));
            //}
            //myLog.Log("------------------------------------");
            //#endregion
            return retvalue;
        }

        public int Order { get; set; }
    }
    public class LogErrorHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ExceptionLogBehavior();//返回出错日志
        }
    }
}
