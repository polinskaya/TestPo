pushd Framework2
pushd build
nuget.exe restore ../SeleniumWebdriver.sln
popd
cd SeleniumWebdriver
"c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MsBuild.exe" SeleniumWebdriver.csproj /p:OutputPath=d:\GOCD_ARTIFACTS\package /t:rebuild
cd ../packages\NUnit.ConsoleRunner.3.10.0\tools
nunit3-console.exe --testparam:browser=Chrome --testparam:environment=dev "../../../SeleniumWebdriver/bin/Debug/SeleniumWebdriver.dll"
popd

