using Core.Cache;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Log
{
    public class LogException
    {
        private ILog logger;
        public LogException()
        {
            var logInfo = CacheHelper.Get("logInfo") as FileInfo;
            if (logInfo == null)
            {
                logInfo = new FileInfo(HttpContext.Current.Server.MapPath("Log4Net.config"));
                CacheHelper.Set("logInfo", logInfo);
            }
            log4net.Config.XmlConfigurator.Configure(logInfo);//要修改为服务器端文件
            logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //logger = LogManager.GetLogger("exLogging");//logger name
        }

        public void logInfo(string message)
        {
            logger.Info(message);
        }
        public void logError(string message,Exception ex)
        {
            logger.Error(message, ex);
        }

        public void logWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
