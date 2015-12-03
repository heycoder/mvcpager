using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;


namespace HeyCoder.Web.Mvc.Pager
{
    public static class HtmlPager
    {

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="html"></param>
        /// /// <param name="option">分页选项</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper html, PagerOption option)
        {
            var routeValue = new RouteValueDictionary();
            return html.Pager(option.RouteName, routeValue, option);
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="html"></param>
        /// <param name="routeValues">使用的路由的值</param>
        /// <param name="option">分页选项</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper html, object routeValues, PagerOption option)
        {
            var routeValue = new RouteValueDictionary();
            if (routeValues != null) routeValue = GetDictionary<RouteValueDictionary>(routeValues, option);
            return html.Pager(option.RouteName, routeValue, option);
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="html"></param>
        /// <param name="routeName">使用的路由名称</param>
        /// /// <param name="option">分页选项</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper html, string routeName, PagerOption option)
        {
            var routeValue = new RouteValueDictionary();
            return html.Pager(routeName, routeValue, option);
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="html"></param>
        /// <param name="routeName">使用的路由名称</param>
        /// <param name="routeValues">使用的路由的值</param>
        /// <param name="option">分页选项</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper html, string routeName, object routeValues, PagerOption option)
        {
            var routeValue = new RouteValueDictionary();
            if (routeValues != null) routeValue = GetDictionary<RouteValueDictionary>(routeValues, option);
            return html.Pager(routeName, routeValue, option);
        }


        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="html"></param>
        /// <param name="routeName">使用的路由名称</param>
        /// <param name="routeValues">使用的路由的值</param>
        /// <param name="option">分页选项</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper html, string routeName, RouteValueDictionary routeValues, PagerOption option)
        {
            //初始化
            var routeData = html.RouteCollection.GetRouteData(html.ViewContext.HttpContext);
            if (routeData == null) return MvcHtmlString.Empty;
            var requestContext = new RequestContext(html.ViewContext.HttpContext, routeData);
            var url = new UrlHelper(requestContext);
            if (routeValues == null) routeValues = new RouteValueDictionary();
            //计算分页信息
            var pageCount = option.DataCount / option.PageSize;
            if (option.DataCount % option.PageSize != 0) pageCount += 1;
            var strs = new StringBuilder();
            if (option.PageIndex < 1) option.PageIndex = 1;
            else if (option.PageIndex > pageCount) option.PageIndex = pageCount;
            if (pageCount <= 1) return MvcHtmlString.Empty;
            strs.AppendFormat("<div class='{0}'>", option.PagerClassName);
            if (option.ShowDataCount)
            {
                strs.AppendFormat("<span class='{1}'>{0}</span>", string.Format(option.DataCountTextFormat, option.DataCount), option.PagerStatusClassName);
            }
            //添加分页状态
            if (option.ShowPagerStatus)
            {
                strs.AppendFormat("<span class='{0}'>{1}</span>", option.PagerStatusClassName, string.Format(option.PagerStatusTextFormat, option.PageIndex, pageCount));
            }
            //判断路由值中是否包含页码参数,不存在就添加
            if (routeValues.ContainsKey(option.PageIndexParamName))
            {
                routeValues[option.PageIndexParamName] = 1;
            }
            else
            {
                routeValues.Add(option.PageIndexParamName, 1);
            }
            //添加首页按钮
            if (option.ShowFirstLastPageButton)
            {
                if (option.PageIndex != 1)
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), option.FirstPageButtonText, option.FirstPageButtonClassName);
                }
                else
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", "javascript:void(0)", option.FirstPageButtonText, option.FirstPageButtonClassName);
                }
            }
            //计算按钮分组
            int btnGroupNum = pageCount / option.PageButtonCount;//按钮分组的个数
            if (pageCount % option.PageButtonCount != 0) btnGroupNum += 1;
            int btnGroupIndex = option.PageIndex / option.PageButtonCount;//当前按钮分组的索引
            if (option.PageIndex % option.PageButtonCount != 0) btnGroupIndex += 1;
            //添加上一组按钮
            if (option.ShowPreNextGroupButton)
            {
                if (btnGroupIndex > 1)
                {
                    int preGroupIndex = btnGroupIndex - 1;
                    int preGroupBtnStart = preGroupIndex * option.PageButtonCount;
                    routeValues[option.PageIndexParamName] = preGroupBtnStart;
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), option.PreGroupButtonText, option.PreGroupButtonClassName);
                }
                else
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", "javascript:void(0)", option.PreGroupButtonText, option.PreGroupButtonClassName);
                }
            }
            //添加上一页按钮
            if (option.ShowPreNextPageButton)
            {
                if (option.PageIndex > 1)
                {
                    routeValues[option.PageIndexParamName] = option.PageIndex > 1 ? option.PageIndex - 1 : option.PageIndex;
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), option.PrePageButtonText, option.PrePageButtonClassName);
                }
                else
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", "javascript:void(0)", option.PrePageButtonText, option.PrePageButtonClassName);
                }
            }

            //添加数字按钮
            var btnStart = option.PageIndex - option.PageButtonCount / 2;
            if (btnStart <= 0)
            {
                btnStart = 1;
            }
            if (!option.CurrentPageInCenter)
            {
                btnStart = ((btnGroupIndex - 1) * option.PageButtonCount) + 1;
            }
            var btnEnd = btnStart + option.PageButtonCount;

            for (int i = btnStart; i < btnEnd; i++)
            {
                if (i <= pageCount)
                {
                    routeValues[option.PageIndexParamName] = i;
                    if (i == option.PageIndex)
                    {
                        strs.AppendFormat("<a href='{0}' class='{2}'>{1}</a>", url.RouteUrl(routeName, routeValues), i, option.CurrentPageClassName);
                    }
                    else
                    {
                        strs.AppendFormat("<a href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), i);
                    }
                }
            }

            //添加下一页按钮
            if (option.ShowPreNextPageButton)
            {
                if (option.PageIndex < pageCount)
                {
                    routeValues[option.PageIndexParamName] = option.PageIndex + 1 > pageCount ? pageCount : option.PageIndex + 1;
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), option.NextPageButtonText, option.NextPageButtonClassName);
                }
                else
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", "javascript:void(0)", option.NextPageButtonText, option.NextPageButtonClassName);
                }
            }
            //添加下一组按钮
            if (option.ShowPreNextGroupButton)
            {
                if (btnGroupIndex < btnGroupNum)
                {
                    int nextGroupIndex = btnGroupIndex + 1;
                    int nextGroupBtnStart = ((nextGroupIndex - 1) * option.PageButtonCount) + 1;
                    routeValues[option.PageIndexParamName] = nextGroupBtnStart;
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), option.NextGroupButtonText, option.NextGroupButtonClassName);
                }
                else
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", "javascript:void(0)", option.NextGroupButtonText, option.NextGroupButtonClassName);
                }
            }
            //添加尾页按钮
            if (option.ShowFirstLastPageButton)
            {
                if (option.PageIndex < pageCount)
                {
                    routeValues[option.PageIndexParamName] = pageCount;
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", url.RouteUrl(routeName, routeValues), option.LastPageButtonText, option.LastPageButtonClassName);
                }
                else
                {
                    strs.AppendFormat("<a class='{2}' href='{0}'>{1}</a>", "javascript:void(0)", option.LastPageButtonText, option.LastPageButtonClassName);
                }
            }
            //添加跳转区域
            if (option.ShowGotoPanel)
            {
                string txtID = Guid.NewGuid().ToString();
                string funcName = "func_" + Guid.NewGuid().ToString().Replace("-", "");
                strs.AppendFormat("<input class='{0}' id='{1}'/>", option.GotoPageInputClassName, txtID);
                strs.AppendFormat("<a class='{0}' href='javascript:{2}();'>{1}</a>", option.GotoPageInputClassName, option.GotoPageBtuttonText, funcName);
                string js = @"<script type='text/javascript'>
                                        function funcName()
                                        {
                                          var pageIndex=document.getElementById('{0}').value;
                                          var url=location.toString().toLowerCase();
                                          if (url.indexOf('{1}')<0) {
  		                                        if (url.indexOf('?')<0) {
  			                                        url+='?{1}='+pageIndex;
  		                                        }
  		                                        else {
  			                                        url+='&{1}='+pageIndex;
  		                                        }
                                          }
                                          else
                                          {
  	                                        var reg=/{1}=-?[0-9]+/;
  	                                        url=url.replace(reg,'{1}='+pageIndex);
                                          }
                                           location=url;
                                        }
                                        </script>";
                js = js.Replace("funcName", funcName).Replace("{0}", txtID).Replace("{1}", option.PageIndexParamName.ToLower());
                strs.Append(js);
            }
            //结束
            strs.Append("</div>");
            return MvcHtmlString.Create(strs.ToString());
        }

        static TDictionary GetDictionary<TDictionary>(Object obj, PagerOption option) where TDictionary : IDictionary<string, object>, new()
        {
            var t = new TDictionary();
            foreach (var item in obj.GetType().GetProperties())
            {
                var value = item.GetValue(obj, null);
                if (value == null) continue;
                var name = item.Name;
                switch (option.ParamNameFormat.ToLower())
                {
                    case "lower":
                        name = name.ToLower();
                        break;
                    case "upper":
                        name = name.ToUpper();
                        break;
                }
                t.Add(name, value);
            }
            return t;
        }

    }
}
