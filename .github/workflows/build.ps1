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

Function CreateZipArchive($dir) {
    Info "Create zip archive `n ${dir}.zip"
    Compress-Archive -Force -Path "$dir/*.exe" -DestinationPath "${dir}.zip"
}

Function CopyFile($file, $dstFolder) {
    Info "Copy `n '$file' to `n '$dstFolder'"
    New-Item -Force -ItemType "directory" $dstFolder > $null
    Copy-Item -Force -Path $file -Destination $dstFolder > $null
}

Function GetVersion() {
    $gitCommand = Get-Command -Name git

    $nearestTag = & $gitCommand describe --exact-match --tags HEAD
    if(-Not $?) {
        Info "The commit is not tagged. Use 'v0.0-dev' as a version instead"
        $nearestTag = "v0.0-dev"
    }

    $commitHash = & $gitCommand rev-parse --short HEAD
    CheckReturnCodeOfPreviousCommand "Failed to get git commit hash"

    return "$($nearestTag.Substring(1))-$commitHash"
}

Function Publish($slnFile, $version, $outDir) {
    Info "Run 'dotnet publish' command: `n slnFile=$slnFile `n version='$version' `n outDir=$outDir"

    $Env:DOTNET_NOLOGO = "true"
    $Env:DOTNET_CLI_TELEMETRY_OPTOUT = "true"
    dotnet publish `
        --runtime win-x86 `
        --configuration Release `
        --output $outDir `
        /property:DebugType=None `
        /property:Version=$version `
        $slnFile
    CheckReturnCodeOfPreviousCommand "'dotnet publish' command failed"

    CreateZipArchive $outDir
}

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$root = Resolve-Path "$PSScriptRoot/../.."
$publishDir = "$root/Build/Publish"
$projectName = "CoffeeBean"

Info "Remove Publish directory `n $publishDir"
Remove-Item $publishDir  -Force  -Recurse -ErrorAction SilentlyContinue

Publish `
    -slnFile $root/$projectName.sln `
    -version (GetVersion) `
    -outDir "$publishDir/$projectName"
