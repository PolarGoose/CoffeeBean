Function Info($msg) {
    Write-Host -ForegroundColor DarkGreen "`nINFO: $msg`n"
}

Function Error($msg) {
    Write-Host `n`n
    Write-Error $msg
    exit 1
}

Function CheckReturnCodeOfPreviousCommand($msg) {
    if(-Not $?) {
        Error "${msg}. Error code: $LastExitCode"
    }
}

Function FindMsBuild() {
    $vswhereCommand = Get-Command -Name "${Env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"

    $msbuild = `
        & $vswhereCommand `
            -latest `
            -requires Microsoft.Component.MSBuild `
            -find MSBuild\**\Bin\MSBuild.exe `
          | select-object -first 1

    if(!$msbuild)
    {
        Error "Can't find MsBuild"
    }

    Info "MsBuild found: `n $msbuild"
    return $msbuild
}

Function CreateZipArchive($fileFullName, $archiveFullName) {
    Info "Create zip archive `n $archiveFullName from `n $fileFullName"
    Compress-Archive -Force -Path $fileFullName -DestinationPath $archiveFullName
}

Function CopyFile($file, $dstFolder) {
    Info "Copy `n '$file' to `n '$dstFolder'"
    New-Item -Force -ItemType "directory" $dstFolder > $null
    Copy-Item -Force -Path $file -Destination $dstFolder > $null
}

Function GetVersion() {
    $gitCommand = Get-Command -Name git

    $commitTag = & $gitCommand describe --exact-match --tags HEAD
    if(-Not $?) {
        Info "The commit is not tagged. Use 'v0.0-dev' as a version instead"
        $commitTag = "v0.0-dev"
    }

    $commitHash = & $gitCommand rev-parse --short HEAD
    CheckReturnCodeOfPreviousCommand "Failed to get git commit hash"

    return "$($commitTag.Substring(1))-$commitHash"
}

Function GetInstallerVersion($version) {
    return $version.Split("-")[0];
}

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$root = Resolve-Path "$PSScriptRoot/../.."
$publishDir = "$root/build/Release"
$projectName = "CoffeeBean"
$version = GetVersion
$installerVersion = GetInstallerVersion $version
$msbuild = FindMsBuild

Info "Version: '$version'. InstallerVersion: '$installerVersion'"

Info "Remove Publish directory `n $publishDir"
Remove-Item $publishDir  -Force  -Recurse -ErrorAction SilentlyContinue

Info "Build project"
& $msbuild `
    /property:RestorePackagesConfig=true `
    /property:MSBuildWarningsAsMessages=NU1503 `
    /property:Configuration=Release `
    /property:DebugType=None `
    /property:Version=$version `
    /property:InstallerVersion=$installerVersion `
    /target:"restore;build" `
    $root/$projectName.sln
CheckReturnCodeOfPreviousCommand "build failed"

CreateZipArchive "$publishDir/net461/${projectName}.exe" "$publishDir/${projectName}.zip"
