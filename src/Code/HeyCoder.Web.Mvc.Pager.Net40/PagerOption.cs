using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace HeyCoder.Web.Mvc.Pager
{
    public class PagerOption
    {


        /// <summary>
        /// 数字按钮的个数
        /// </summary>
        public int PageButtonCount { get; set; }

        /// <summary>
        /// 当前页码参数名
        /// </summary>
        public string PageIndexParamName { get; set; }

        /// <summary>
        /// 分页控件div的伪类名
        /// </summary>
        public string PagerClassName { get; set; }

        /// <summary>
        /// 分页状态的伪类名
        /// </summary>
        public string PagerStatusClassName { get; set; }

        /// <summary>
        /// 当前页页码的伪类名
        /// </summary>
        public string CurrentPageClassName { get; set; }

        /// <summary>
        /// 首页按钮伪类名称
        /// </summary>
        public string FirstPageButtonClassName { get; set; }


        /// <summary>
        /// 尾页按钮伪类名称
        /// </summary>
        public string LastPageButtonClassName { get; set; }

        /// <summary>
        /// 上一组按钮伪类名称
        /// </summary>
        public string PreGroupButtonClassName { get; set; }

        /// <summary>
        /// 下一组按钮伪类名称
        /// </summary>
        public string NextGroupButtonClassName { get; set; }

        /// <summary>
        /// 上一页按钮伪类名称
        /// </summary>
        public string PrePageButtonClassName { get; set; }


        /// <summary>
        /// 下一页按钮伪类名称
        /// </summary>
        public string NextPageButtonClassName { get; set; }


        /// <summary>
        /// 页面跳转的文本框伪类名称
        /// </summary>
        public string GotoPageInputClassName { get; set; }

        /// <summary>
        /// 页面跳转的按钮伪类名称
        /// </summary>
        public string GotoPageButtonClassName { get; set; }


        /// <summary>
        /// 页面跳转的按钮伪类名称
        /// </summary>
        public string GotoPageInputId { get; set; }



        /// <summary>
        /// 首页按钮显示文本
        /// </summary>
        public string FirstPageButtonText { get; set; }

        /// <summary>
        /// 尾页按钮显示文本
        /// </summary>
        public string LastPageButtonText { get; set; }

        /// <summary>
        /// 上一组按钮显示文本
        /// </summary>
        public string PreGroupButtonText { get; set; }

        /// <summary>
        /// 下一组按钮显示文本
        /// </summary>
        public string NextGroupButtonText { get; set; }


        /// <summary>
        /// 上一页按钮显示文本
        /// </summary>
        public string PrePageButtonText { get; set; }


        /// <summary>
        /// 下一页按钮显示文本
        /// </summary>
        public string NextPageButtonText { get; set; }

        /// <summary>
        /// 页面跳转的按钮显示文本
        /// </summary>
        public string GotoPageBtuttonText { get; set; }

        /// <summary>
        /// 是否显示首页尾页按钮
        /// </summary>
        public bool ShowFirstLastPageButton { get; set; }

        /// <summary>
        /// 是否显示上一组下一组按钮按钮
        /// </summary>
        public bool ShowPreNextGroupButton { get; set; }


        /// <summary>
        /// 是否显示上一页下一页按钮
        /// </summary>
        public bool ShowPreNextPageButton { get; set; }


        /// <summary>
        /// 是否显示分页状态
        /// </summary>
        public bool ShowPagerStatus { get; set; }

        /// <summary>
        /// 是否显示数据总数
        /// </summary>
        public bool ShowDataCount { get; set; }

        /// <summary>
        /// 是否显示跳转页码区域
        /// </summary>
        public bool ShowGotoPanel { get; set; }


        /// <summary>
        /// 每页显示的数据个数
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        /// 数据总个数
        /// </summary>
        public int DataCount { get; set; }


        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 参数名格式化方式
        /// </summary>
        public string ParamNameFormat { get; set; }

        /// <summary>
        /// 数据个数显示
        /// </summary>
        public string DataCountTextFormat { get; set; }

        /// <summary>
        /// 分页状态显示
        /// </summary>
        public string PagerStatusTextFormat { get; set; }

        /// <summary>
        /// 当前页码居中
        /// </summary>
        public bool CurrentPageInCenter { get; set; }

        /// <summary>
        /// MVC路由名字
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 初始化分页配置信息
        /// </summary>
        /// <param name="dataCount">数据总个数</param>
        /// <param name="pageIndex">当前页码</param>
        public PagerOption(int dataCount, int pageIndex)
        {
            InitOption();
            DataCount = dataCount;
            PageIndex = pageIndex > 0 ? pageIndex : 1;
        }
        /// <summary>
        /// 初始化分页配置信息
        /// </summary>
        /// <param name="dataCount">数据总个数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据个数</param>
        public PagerOption(int dataCount, int pageIndex, int pageSize)
        {
            InitOption();
            DataCount = dataCount;
            PageIndex = pageIndex > 0 ? pageIndex : 1;
            PageSize = pageSize;
        }

        private void InitOption()
        {
            CurrentPageInCenter = true;
            ShowPreNextPageButton = true;
            var valueCollection = ConfigurationManager.GetSection("HeyCoder.MvcPager") as NameValueCollection ?? new NameValueCollection();
            var propertyArray = GetType().GetProperties();
            foreach (var propertyInfo in propertyArray)
            {
                if (!valueCollection.AllKeys.Contains(propertyInfo.Name)) continue;
                var collectionValue = valueCollection[propertyInfo.Name];
                if (string.IsNullOrEmpty(collectionValue)) continue;
                var value = Convert.ChangeType(collectionValue, propertyInfo.PropertyType);
                propertyInfo.SetValue(this, value, null);
            }
            if (PageSize <= 0) PageSize = 10;
            if (PageButtonCount <= 0) PageButtonCount = 10;
            if (string.IsNullOrEmpty(PageIndexParamName)) PageIndexParamName = "pageindex";
            if (string.IsNullOrEmpty(PagerClassName)) PagerClassName = "pager";
            if (string.IsNullOrEmpty(PagerStatusClassName)) PagerStatusClassName = "pagerstatus";
            if (string.IsNullOrEmpty(CurrentPageClassName)) CurrentPageClassName = "currentpage";
            if (string.IsNullOrEmpty(FirstPageButtonClassName)) FirstPageButtonClassName = "firstPage";
            if (string.IsNullOrEmpty(LastPageButtonClassName)) FirstPageButtonClassName = "lastPage";
            if (string.IsNullOrEmpty(PreGroupButtonClassName)) PreGroupButtonClassName = "pregroup";
            if (string.IsNullOrEmpty(NextGroupButtonClassName)) NextGroupButtonClassName = "nextgroup";
            if (string.IsNullOrEmpty(PrePageButtonClassName)) PrePageButtonClassName = "prepage";
            if (string.IsNullOrEmpty(NextPageButtonClassName)) NextPageButtonClassName = "nextpage";
            if (string.IsNullOrEmpty(GotoPageInputClassName)) GotoPageInputClassName = "txtpage";
            if (string.IsNullOrEmpty(GotoPageButtonClassName)) GotoPageButtonClassName = "btngotopage";
            if (string.IsNullOrEmpty(GotoPageInputId)) GotoPageInputId = "txtpage";
            if (string.IsNullOrEmpty(FirstPageButtonText)) FirstPageButtonText = "首页";
            if (string.IsNullOrEmpty(LastPageButtonText)) LastPageButtonText = "尾页";
            if (string.IsNullOrEmpty(PreGroupButtonText)) PreGroupButtonText = "上一组";
            if (string.IsNullOrEmpty(NextGroupButtonText)) NextGroupButtonText = "下一组";
            if (string.IsNullOrEmpty(PrePageButtonText)) PreGroupButtonText = "上一页";
            if (string.IsNullOrEmpty(NextPageButtonText)) NextGroupButtonText = "下一页";
            if (string.IsNullOrEmpty(GotoPageBtuttonText)) GotoPageBtuttonText = "跳转";
            if (string.IsNullOrEmpty(ParamNameFormat)) ParamNameFormat = "normal";
            if (string.IsNullOrEmpty(DataCountTextFormat)) DataCountTextFormat = "共{0}条数据";
            if (string.IsNullOrEmpty(PagerStatusTextFormat)) PagerStatusTextFormat = "当前：{0}/{1}";
            if (string.IsNullOrEmpty(RouteName)) RouteName = "default";
        }
    }
}
