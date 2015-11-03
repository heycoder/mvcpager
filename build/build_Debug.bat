set RootDir=%~dp0..\src\Code
SET BUILD=Debug

%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe "%RootDir%\HeyCoder.Web.Mvc.Pager.Net40\HeyCoder.Web.Mvc.Pager.Net40.csproj" /p:Configuration=%BUILD% /t:ReBuild

xcopy "%RootDir%\HeyCoder.Web.Mvc.Pager.Net40\bin\%BUILD%\HeyCoder.Web.Mvc.Pager.dll" %~dp0NET40\ /F /R /Y

pause

