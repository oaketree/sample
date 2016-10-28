using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    public class MenuItem
    {
        /// <summary>
        /// 菜单类
        /// </summary>
        public int MenuID { get; set; }
        public int PID { get; set; }
        public string MenuName { get; set; }// 菜单名称
        public dynamic Childs { get; set; }//子菜单

    }
    public class MenuDistinct<T> : IEqualityComparer<T>
        where T: MenuItem
    {
        public bool Equals(T x, T y)
        {
            return x.MenuID == y.MenuID;
        }
        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
    public static class HandleMenu<T>
        where T : MenuItem
    {
        /// <summary>
        /// 根据子menuitem生成完整menuitem
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public static List<T> CreateMenuItem(List<T> menuItems, Func<int, T> getparentmenuitem)
        {
            var lm = new List<T>();
            var addparentmenuitem = Combitator.Fix<int, List<T>>(f => m =>
            {
                if (m != 0)
                {
                    var pitem = getparentmenuitem(m);
                    if (!lm.Contains(pitem, new MenuDistinct<T>()) && !menuItems.Contains(pitem, new MenuDistinct<T>()))
                    {
                        lm.Add(pitem);
                    }
                    f(pitem.PID);
                }
                return lm;
            });
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
        //public static List<T> handleSubMenu(List<T> menuItem, int topid)
        //{
        //    var lm = new List<T>();
        //    foreach (var item in menuItem)
        //    {
        //        if (item.PID == topid)
        //        {
        //            var hsm = handleSubMenu(menuItem, item.MenuID);
        //            if (hsm.Count > 0)
        //                item.Childs = hsm;
        //            else
        //                item.Childs = "";
        //            lm.Add(item);
        //        }
        //    }
        //    return lm.OrderBy(o => o.MenuID).ToList();
        //}

        /// <summary>
        /// 把列表转换为排好的顺序
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="topid"></param>
        /// <returns></returns>
        public static List<T> SubMenu(List<T> menuItem, int topid)
        {
            var handleSubMenu = Combitator.Fix<List<T>, int, List<T>>(f=>(m,t)=> 
            {
                var lm = new List<T>();
                foreach (var item in m)
                {
                    if (item.PID ==t)
                    {
                        var hsm = f(m, item.MenuID);
                        if (hsm.Count > 0)
                            item.Childs = hsm;
                        else
                            item.Childs = "";
                        lm.Add(item);
                    }
                }
                return lm;
            });
            return handleSubMenu(menuItem, topid).OrderBy(o => o.MenuID).ToList();
        }
    }

}
