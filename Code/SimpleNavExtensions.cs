using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Text;

namespace System.Web.Mvc.Html
{
    public static class SimpleNavExtensions
    {
        public static string SimpleNav(this HtmlHelper html, IEnumerable<SimpleNavItem> navItems)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string controller = html.ViewContext.RouteData.Values["controller"].ToString();
            string action = html.ViewContext.RouteData.Values["action"].ToString();

            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("clearfix");

            StringBuilder listBuilder = new StringBuilder();
            TagBuilder li = null;
            TagBuilder a = null;
            foreach (var item in navItems)
            {
                a = new TagBuilder("a");
                a.Attributes.Add("href", urlHelper.Action(item.Action, item.Controller));
                a.InnerHtml = item.Text;

                li = new TagBuilder("li");
                if (item.GetSelected != null && item.GetSelected(action, controller))
                    li.AddCssClass("sel");
                li.InnerHtml = a.ToString();

                listBuilder.Append(li.ToString());
            }

            ul.InnerHtml = listBuilder.ToString();

            return ul.ToString();
        }
    }

    public class SimpleNavItem
    {
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public Func<string, string, bool> GetSelected { get; set; }
    }
}