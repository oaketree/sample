using Core.DAL;
using Gygl.Contract.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gygl.BLL.Magazine.ViewModels;
using Microsoft.Practices.Unity;
using Core.Utility;

namespace Gygl.BLL.Magazine.Service
{
    public class CommentService : RepositoryBase<Comment, WebDBContext>, ICommentService
    {
        [Dependency]
        public IArticleService ArticleService { get; set; }
        public async Task<PageCommentViewModel> getCommentByArticle(int aid, int pageSize, int page)
        {
            var pcv = new PageCommentViewModel();
            //var titleid = await ArticleService.GetAsync(aid);//有问题有冲突，改为同一地方获取
            //pcv.Title = titleid.Title;
            var fa = FindAll(n => n.ArticleID == aid).OrderByDescending(o => o.RegDate);
            var c = await Count(fa);
            pcv.Count = c;
            pcv.TotalItems = c;
            pcv.CurrentPage = page;
            pcv.ItemPerPage = pageSize;
            if (c != 0)
            {
                var fbp = FindByPageAsync(fa, pageSize, page, s => new CommentViewModel
                {
                    IP = s.IP,
                    Advice = s.Advice,
                    RegDate = s.RegDate
                });
                pcv.Entity = await fbp;
            }
            return pcv;

        }

        public async Task smtComment(int aid, string message)
        {
            var commnent = new Comment
            {
                IP = Utils.GetIP(),
                Advice = message,
                ArticleID = aid
            };
            await InsertAsync(commnent);
        }
    }
}
