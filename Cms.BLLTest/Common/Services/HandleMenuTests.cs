﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Core.Utility;

namespace Cms.BLL.Common.Services.Tests
{
    [TestClass()]
    public class HandleMenuTests
    {
        [TestMethod()]
        public void handleSubMenuTest()
        {
            var a = new List<CmsMenu>();
            a.Add(new CmsMenu { MenuID = 1, PID = 0, MenuName = "a", Url = "ua" });
            a.Add(new CmsMenu { MenuID = 2, PID = 0, MenuName = "b", Url = "ub" });
            a.Add(new CmsMenu { MenuID = 3, PID = 0, MenuName = "c", Url = "uc" });
            a.Add(new CmsMenu { MenuID = 4, PID = 1, MenuName = "d", Url = "ud" });
            a.Add(new CmsMenu { MenuID = 5, PID = 2, MenuName = "e", Url = "ue" });
            a.Add(new CmsMenu { MenuID = 6, PID = 3, MenuName = "f", Url = "uf" });
            a.Add(new CmsMenu { MenuID = 7, PID = 4, MenuName = "g", Url = "uf" });
            a.Add(new CmsMenu { MenuID = 8, PID = 5, MenuName = "h", Url = "uf" });
            a.Add(new CmsMenu { MenuID = 9, PID = 6, MenuName = "i", Url = "uf" });
            a.Add(new CmsMenu { MenuID = 10, PID = 9, MenuName = "j", Url = "uj" });
            a.Add(new CmsMenu { MenuID = 11, PID = 6, MenuName = "l", Url = "uk" });


            var b = new List<CmsMenu>();
            b.Add(new CmsMenu { MenuID = 6, PID = 3, MenuName = "f", Url = "uf" });
            b.Add(new CmsMenu { MenuID = 7, PID = 4, MenuName = "g", Url = "uf" });
            b.Add(new CmsMenu { MenuID = 8, PID = 5, MenuName = "h", Url = "uf" });
            b.Add(new CmsMenu { MenuID = 9, PID = 6, MenuName = "i", Url = "uf" });
            b.Add(new CmsMenu { MenuID = 10, PID = 9, MenuName = "j", Url = "uj" });


            var cm = HandleMenu<CmsMenu>.CreateMenuItem(b, m =>
            {
                return a.FirstOrDefault(n => n.MenuID == m);
            });

            //Console.Write(JsonConvert.SerializeObject(cm));
            var c = HandleMenu<CmsMenu>.SubMenu(cm, 0).Where(s=>s.PID==0);

            
            //Console.Write(JsonConvert.SerializeObject(c));
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(c));
        }
    }
}