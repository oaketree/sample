using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cms.BLL.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cms.BLL.Common.Services.Tests
{
    [TestClass()]
    public class HandleMenuTests
    {
        [TestMethod()]
        public void handleSubMenuTest()
        {
            var a = new List<Menu>();
            a.Add(new Menu { MenuID = 1, PID = 0, MenuName = "a", Url = "ua" });
            a.Add(new Menu { MenuID = 2, PID = 0, MenuName = "b", Url = "ub" });
            a.Add(new Menu { MenuID = 3, PID = 0, MenuName = "c", Url = "uc" });
            a.Add(new Menu { MenuID = 4, PID = 1, MenuName = "d", Url = "ud" });
            a.Add(new Menu { MenuID = 5, PID = 2, MenuName = "e", Url = "ue" });
            a.Add(new Menu { MenuID = 6, PID = 3, MenuName = "f", Url = "uf" });
            a.Add(new Menu { MenuID = 7, PID = 4, MenuName = "g", Url = "uf" });
            a.Add(new Menu { MenuID = 8, PID = 5, MenuName = "h", Url = "uf" });
            a.Add(new Menu { MenuID = 9, PID = 6, MenuName = "i", Url = "uf" });
            a.Add(new Menu { MenuID = 10, PID = 9, MenuName = "j", Url = "uj" });
            a.Add(new Menu { MenuID = 11, PID = 6, MenuName = "l", Url = "uk" });

            var b = new HandleMenu().handleSubMenu(a,0);
            Console.Write(JsonConvert.SerializeObject(b));

        }
    }
}