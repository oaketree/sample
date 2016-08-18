using Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class PageArticleViewModel:PageInfo<TitleViewModel>
    {
        public string Category { get; set; }
    }
}
