using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services
{
    public class Menu
    {
        public int MenuID { get; set; }
        public int PID { get; set; }
        public string MenuName { get; set; }// 菜单名称
        public string Url { get; set; }// URL地址
        public List<Menu> Childs { get; set; }//子菜单
    }
    public class HandleMenu
    {
        public List<Menu> handleSubMenu(List<Menu> menuItem,int topid)
        {
            var lm = new List<Menu>();
            foreach (var item in menuItem)
            {

                if (item.PID == topid)
                {
                    var m = new Menu();
                    m.MenuID = item.MenuID;
                    m.PID = item.PID;
                    m.MenuName = item.MenuName;
                    m.Url = item.Url;
                    var hsm = handleSubMenu(menuItem, item.MenuID);
                    if (hsm.Count > 0)
                        m.Childs = hsm;
                    else
                        m.Childs = null;
                    lm.Add(m);
                    //lm.Add(new Menu
                    //{
                    //    MenuID = item.MenuID,
                    //    PID = item.PID,
                    //    MenuName = item.MenuName,
                    //    Url = item.Url,
                    //    Childs = handleSubMenu(menuItem, item.MenuID)
                    //});
                }
            }
            return lm;
        }
    }
   
}
