﻿using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public interface IArticleService : IRepository<Article>
    {
        Task<List<TilteViewModel>> getTitle(int gyglid, int categoryid);
        PageArticleViewModel getArticleByCategory(int? year, int? period, int? category, string key, int pageSize, int page);
        Task<List<int>> getArticleList(int gyglId);
        Task updateHit(int aid);
        object getFirstPages(int pid);
    }
}
