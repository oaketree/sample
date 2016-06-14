﻿using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;

namespace Gygl.BLL.Magazine.Service
{
    public  interface IGyglCategoryService : IRepository<GyglCategory>
    {
        List<CatalogViewModel> getCatalogByID(int gyglid);
    }
}
