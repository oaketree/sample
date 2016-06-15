using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services
{
    public class MenuItem
    {
        public int MenuID { get; set; }
        public int PID { get; set; }
        public string MenuName { get; set; }// 菜单名称
        public string Url { get; set; }// URL地址
        public List<MenuItem> Childs { get; set; }//子菜单
    }
    public static class HandleMenu
    {
        /// <summary>
        /// 把列表转换为拍好的顺序
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
                        m.Childs = null;
                    lm.Add(m);
                }
            }
            return lm;
        }
    }
   
}
