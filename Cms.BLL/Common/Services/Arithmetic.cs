using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services
{
    public class Arithmetic
    {
        public static int i = 0;
        //1：不允许使用循环语句、条件语句，在控制台中打印出1-200这200个数。
        public bool doPrint()
        {
            //var a = new int[200];
            //Array.ForEach(a,)
            Console.WriteLine(++i);
            return i>=200|| doPrint();
        }
        // 2：有5个Aspx页面，分别为"Page_1.aspx","Page_10.aspx","Page_100.aspx","Page_11.aspx","Page_111.aspx",请编写代码，让5个Aspx页面按下面的顺序出:
        public void order()
        {
            var pageList = new[] { "Page_1.aspx", "Page_10.aspx", "Page_100.aspx", "Page_11.aspx", "Page_111.aspx" };
            pageList = pageList.OrderBy(o => int.Parse(Regex.Match(o, @"\d+").Value)).ToArray();
            Array.ForEach(pageList, Console.WriteLine);
        }

        public string RepeatString(string str, int repeatCount)
        {
            var source = str.ToCharArray();
            var dest = new char[source.Length * repeatCount];
            for (int i = 0; i < repeatCount; i++)
            {
                Buffer.BlockCopy(source, 0, dest, source.Length * i * 2, source.Length * 2);
                //System.Text.Encoding.Unicode.GetBytes
                //Array.Copy()
            }
            return  new string(dest);
        }

    }
}
