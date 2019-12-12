cd SeleniumWebdriver
msbuild.exe SeleniumWebdriver.csproj
cd ../packages\NUnit.ConsoleRunner.3.10.0\tools
nunit3-console.exe --testparam:browser=Chrome --testparam:environment=dev "../../../SeleniumWebdriver/bin/Debug/SeleniumWebdriver.dll"
