using System.Configuration;

namespace Core.Cache
{
    public class GetConn
    {
        private string key;
        public GetConn(string key)
        {
            this.key = key;
        }
        public string Conn()
        {
            var conn = CacheHelper.Get(key);
            if (conn != null)
                return conn.ToString();
            else
            {
                conn = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                CacheHelper.Set(key, conn);
                return conn.ToString();
            }
        }
    }
}
