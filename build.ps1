Function Info($msg) {
  Write-Host -ForegroundColor DarkGreen "`nINFO: $msg`n"
}

Function Error($msg) {
  Write-Host -ForegroundColor Red "`n`nERROR: $msg"
  exit 1
}

Function CheckReturnCodeOfPreviousCommand($msg) {
  if(-Not $?) {
    Error "${msg}. Error code: $LastExitCode"
  }
}

Function GetVersion() {
  $commitTag = git describe --exact-match --tags HEAD
  if(-Not $?) {
    Info "The commit is not tagged. Use 'v0.0-dev' as a tag instead"
    $commitTag = "v0.0-dev"
  }
  return "$($commitTag.Substring(1))"
}

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$root = Resolve-Path $PSScriptRoot
$buildDir = "$root/build"
$version = GetVersion

Info "Version: '$version'"

Info "Build project"
dotnet build --nologo `
             --configuration Release `
             -verbosity:minimal `
             /property:DebugType=None `
             /property:Version=$version `
             $root/CoffeeBean.slnx
CheckReturnCodeOfPreviousCommand "build failed"

Compress-Archive -LiteralPath "$buildDir/AnyCPU/Release/CoffeeBean/net472/CoffeeBean.exe" `
                 -DestinationPath "$buildDir/CoffeeBean.zip" `
                 -Force
