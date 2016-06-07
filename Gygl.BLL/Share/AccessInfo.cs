using System.Collections.Generic;

namespace Gygl.BLL.Share
{
    public class AccessInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public IList<int> Authorise { get; set; }
    }
}
