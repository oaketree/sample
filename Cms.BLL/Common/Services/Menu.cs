using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services
{
    public class MenuItem
    {
        /// <summary>
        /// 菜单类
        /// </summary>
        public int MenuID { get; set; }
        public int PID { get; set; }
        public string MenuName { get; set; }// 菜单名称
        public string Url { get; set; }// URL地址
        public dynamic Childs { get; set; }//子菜单
    }
    public class MenuDistinct : IEqualityComparer<MenuItem>
    {
        public bool Equals(MenuItem x, MenuItem y)
        {
            return x.MenuID == y.MenuID;
        }
        public int GetHashCode(MenuItem obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    public static class HandleMenu
    {
        /// <summary>
        /// 根据子menuitem生成完整menuitem
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public static List<MenuItem> CreateMenuItem(List<MenuItem> menuItems, Func<int, MenuItem> getparentmenuitem)
        {
            var lm = new List<MenuItem>();
            Func<int, List<MenuItem>> addparentmenuitem = null;
            addparentmenuitem = m =>
             {
                 if (m != 0)
                 {
                     var nm = new MenuItem();
                     var pitem = getparentmenuitem(m);
                     if (!lm.Contains(pitem, new MenuDistinct())&&!menuItems.Contains(pitem,new MenuDistinct()))
                     {
                         nm.MenuID = pitem.MenuID;
                         nm.PID = pitem.PID;
                         nm.MenuName = pitem.MenuName;
                         nm.Url = pitem.Url;
                         lm.Add(nm);
                     }
                     addparentmenuitem(nm.PID);
                 }  
                 return lm;
             };
            foreach (var item in menuItems)
            {
                addparentmenuitem(item.PID);
            }
            menuItems.AddRange(lm.AsEnumerable());
            return menuItems;
        }



        /// <summary>
        /// 把列表转换为排好的顺序
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="topid"></param>
        /// <returns></returns>
        public static List<MenuItem> handleSubMenu(List<MenuItem> menuItem,int topid)
        {
            var lm = new List<MenuItem>();
            foreach (var item in menuItem)
            {

                if (item.PID == topid)
                {
                    var m = new MenuItem();
                    m.MenuID = item.MenuID;
                    m.PID = item.PID;
                    m.MenuName = item.MenuName;
                    m.Url = item.Url;
                    var hsm = handleSubMenu(menuItem, item.MenuID);
                    if (hsm.Count > 0)
                        m.Childs = hsm;
                    else
                        m.Childs = "";
                    lm.Add(m);
                }
            }
            return lm.OrderBy(o => o.MenuID).ToList();
        }
    }
   
}
