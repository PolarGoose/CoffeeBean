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

Function CreateZipArchive($file, $archiveFullName) {
    Info "Create zip archive: `n '$archiveFullName' from file `n '$file'"
    Compress-Archive -Force -Path $file -DestinationPath $archiveFullName
}

Function CopyFile($file, $dstFolder) {
    Info "Copy `n '$file' to `n '$dstFolder'"
    New-Item -Force -ItemType "directory" $dstFolder > $null
    Copy-Item -Force -Path $file -Destination $dstFolder > $null
}

Function GetInformationalVersion() {
    $gitCommand = Get-Command -ErrorAction Stop -Name git

    $tag = & $gitCommand describe --exact-match --tags HEAD
    if(-Not $?) {
        Info "The commit is not tagged. Use 'v0.0.0-dev' as a version instead"
        $tag = "v0.0.0-dev"
    }

    $commitHash = & $gitCommand rev-parse --short HEAD
    CheckReturnCodeOfPreviousCommand "Failed to get git commit hash"

    return "$($tag.Substring(1))~$commitHash"
}

Function ExtractVersion($informationalVersion) {
    if($informationalVersion -match "^([0-9]+\.[0-9]+\.[0-9])\.*") {
        return $Matches[1]
    }
    Error "Failed to extract version from '$informationalVersion'"
}

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$root = Resolve-Path "$PSScriptRoot/../.."
$projectName = "CoffeeBean"
$publishDir = "$root/Build/Publish"
$slnFile = "$root/$projectName.sln"
$msbuild = Get-Command "C:\Program Files (x86)\Microsoft Visual Studio\2019\*\MSBuild\Current\Bin\MSBuild.exe"
$informationalVersion = GetInformationalVersion
$version = ExtractVersion $informationalVersion

Info "Build project using MSBuild"
& $msbuild `
    /property:Version=$version `
    /property:InformationalVersion=$informationalVersion `
    /property:DebugType=None `
    /property:Configuration=Release `
    /property:Platform=x86 `
    $slnFile
CheckReturnCodeOfPreviousCommand "Failed to build the project"

CopyFile $root/Build/Release/${projectName}.exe $publishDir
CreateZipArchive $publishDir/${projectName}.exe $publishDir/${projectName}

if ($LastExitCode -ne 0) {
    Error "LastExitCode is $LastExitCode at the end of the script. Should be 0"
}
