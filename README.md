## 介绍（Description）
ASP.NET(C#) mvcpager for mvc 3.0+

## 安装（Install）

1. nuget：Install-Package HeyCoder.MvcPager
2. https://www.nuget.org/packages/HeyCoder.MvcPager/

## 配置（Config）
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="HeyCoder.MvcPager" type="HeyCoder.Web.Mvc.Pager.PagerConfig,HeyCoder.Web.Mvc.Pager" />
  </configSections>
  <!--HeyCoder.MvcPager全局配置文件-->
  <HeyCoder.MvcPager>
    <add key="PageSize" value="10" />
    <add key="PageButtonCount" value="5" />
    <add key="PagerClassName" value="pager" />
    <add key="CurrentPageClassName" value="active" />
    <add key="PrePageButtonText" value="&lt;" />
    <add key="NextPageButtonText" value="&gt;" />
    <add key="PreGroupButtonText" value="&lt;&lt;" />
    <add key="NextGroupButtonText" value="&gt;&gt;" />
    <add key="FirstPageButtonText" value="首页" />
    <add key="LastPageButtonText" value="尾页" />
    <add key="ShowPagerStatus" value="true" />
    <add key="ShowPreNextPageButton" value="true" />
    <add key="ShowFirstLastPageButton" value="false" />
    <add key="ShowGotoPanel" value="false" />
    <add key="ShowDataCount" value="false" />
    <add key="CurrentPageInCenter" value="true" />
    <add key="DataCountTextFormat" value="共{0}条记录" />
    <add key="PagerStatusTextFormat" value="共{1}页" />
  </HeyCoder.MvcPager>
  
</configuration>

```
 wiki:https://github.com/heycoder/mvcpager/wiki


## 使用示例（Demo）

1. Theme:Default
```
<link href="//cdn.ken.io/plugin/mvcpager/theme/default-1.0.css" rel="stylesheet"/>

@Html.Pager(new {p = Guid.NewGuid().ToString()}, new PagerOption(100, Model.PageIndex, 10))
```
2. Theme:BootStrap
```
<link href="//cdn.bootcss.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet" />
<link href="//cdn.ken.io/plugin/mvcpager/theme/bootstrap-1.0.css" rel="stylesheet" />

@Html.Pager(new {p = Guid.NewGuid().ToString()}, new PagerOption(100, Model.PageIndex, 10))
```
