using System.Configuration;
using System.Collections.Specialized;

namespace HeyCoder.Web.Mvc.Pager
{
    public class PagerConfig : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var settings = new NameValueSectionHandler();
            return settings.Create(parent, configContext, section) as NameValueCollection ?? new NameValueCollection();
        }
        #endregion
    }
}
