dotnet publish ./app/MonthlyCardExtractFormatter.App -r linux-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish ./app/MonthlyCardExtractFormatter.App -r win-x64 -p:PublishSingleFile=true --self-contained true

ReleasePath="./Release"
ReleaseWinPath="$ReleasePath/Windows"
ReleaseLinuxPath="$ReleasePath/Linux"

if [ ! -d "$ReleasePath" ]; then
  mkdir "$ReleasePath"
  mkdir "$ReleaseWinPath"
  mkdir "$ReleaseLinuxPath"
fi

if [ ! -d "$ReleaseWinPath" ]; then
  mkdir "$ReleaseWinPath"
fi

if [ ! -d "$ReleaseLinuxPath" ]; then
  mkdir "$ReleaseLinuxPath"
fi 

cp ./app/MonthlyCardExtractFormatter.App/bin/Release/net8.0/win-x64/publish/MonthlyCardExtractFormatter.App.exe "${ReleaseWinPath}/MonthlyCardExtractFormatter.exe"
cp ./app/MonthlyCardExtractFormatter.App/bin/Release/net8.0/linux-x64/publish/MonthlyCardExtractFormatter.App "${ReleaseLinuxPath}/MonthlyCardExtractFormatter"

cp ./app/MonthlyCardExtractFormatter.App/appsettings.json "${ReleaseWinPath}/appsettings.json"
cp ./app/MonthlyCardExtractFormatter.App/appsettings.json "${ReleaseLinuxPath}/appsettings.json"
