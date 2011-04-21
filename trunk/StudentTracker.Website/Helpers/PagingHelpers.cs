using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StudentTracker.Website.Helpers {
    public static class PagingHelpers {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl) {
            var result = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPages; i++) {
                var tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.AppendLine(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}