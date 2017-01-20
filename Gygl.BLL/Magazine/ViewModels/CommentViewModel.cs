using Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class CommentViewModel
    {
        public string IP { get; set; }
        public string Advice { get; set; }
        public DateTime? RegDate { get; set; }
        //年月
        public string Year
        {
            get
            {
                return string.Format("{0:d}", RegDate);
            }
        }
        //详细时间
        public string Time
        {
            get
            {
                return string.Format("{0:T}", RegDate);
            }
        }
    }
    //评论分页
    public class PageCommentViewModel : PageInfo<CommentViewModel>
    {
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
