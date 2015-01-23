IF EXIST .\opencover (
  rmdir .\opencover /S /Q
)

mkdir .\opencover
mkdir .\opencover\codecoverage
mkdir .\opencover\codecoveragereport
mkdir .\opencover\testresults

.\packages\OpenCover.4.5.3522\OpenCover.Console.exe -register:user -target:".\runTests.cmd" -output:.\opencover\codecoverage\coveragereport.xml -log:All -filter:"+[*]* -[ExchangeOofScheduler.*.Tests]* -[ExchangeOofScheduler.Tests]*"
.\packages\ReportGenerator.2.0.4.0\ReportGenerator.exe -reports:.\opencover\codecoverage\coveragereport.xml -targetdir:.\opencover\codecoveragereport