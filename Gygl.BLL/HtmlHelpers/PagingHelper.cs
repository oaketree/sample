using Core.DAL;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Gygl.BLL.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLink<T>(this HtmlHelper html, PageInfo<T> pagingInfo, Func<int, string> pageUrl)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            if (pagingInfo.CurrentPage == 1)
            {
                writer.AddAttribute("class", "prev-page");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderEndTag();
            }
            else
            {
                writer.AddAttribute("class", "prev-page");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("上一页");
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            int begin = 0;
            int end = 0;

            if (pagingInfo.CurrentPage + 3 < pagingInfo.TotalPages)
            {
                end = pagingInfo.CurrentPage + 3;
            }
            else
            {
                end = pagingInfo.TotalPages;
            }

            if (pagingInfo.CurrentPage - 3 > 1)
            {
                begin = pagingInfo.CurrentPage - 3;
            }
            else
            {
                begin = 1;
            }

            if (begin > 1)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", pageUrl(1));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(1);
                writer.RenderEndTag();
                writer.RenderEndTag();


                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(" ... ");
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            for (int i = begin; i <= end; i++)
            {

                if (i == pagingInfo.CurrentPage)
                {
                    writer.AddAttribute("class", "active");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(i);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                else
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute("href", pageUrl(i));
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(i);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }

            if (end < pagingInfo.TotalPages)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(" ... ");
                writer.RenderEndTag();
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", pageUrl(pagingInfo.TotalPages));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(pagingInfo.TotalPages);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }



            if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
            {
                writer.AddAttribute("class", "next-page");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderEndTag();
            }
            else
            {
                writer.AddAttribute("class", "prev-page");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write("下一页");
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            return MvcHtmlString.Create(sw.ToString());
        }

    }
}
