param([Parameter(Mandatory = $true)][string] $BuildFolder)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$iconsFolder = "$BuildFolder/Icons"
New-Item -ItemType Directory -Path $iconsFolder -Force > $null
if (Get-ChildItem -LiteralPath $iconsFolder -Filter "*.ico" -File | Select-Object -First 1) {
  return
}

$imageMagickFolder = "$BuildFolder/ImageMagick"
$magickExe = "$imageMagickFolder/magick.exe"

if (!(Test-Path -LiteralPath $magickExe -PathType Leaf)) {
  $archivePath = "$imageMagickFolder.7z"
  New-Item -ItemType Directory -Path $imageMagickFolder -Force > $null
  Write-Host "Downloading ImageMagick"
  Invoke-WebRequest -UseBasicParsing `
                    -Uri "https://github.com/ImageMagick/ImageMagick/releases/download/7.1.2-35/ImageMagick-7.1.2-35-portable-Q16-HDRI-x64.7z" `
                    -OutFile $archivePath
  tar -xf $archivePath -C $imageMagickFolder
}

$iconResolutions = 16,20,24,32,40,48,64,256
$svgFolder = "$PSScriptRoot/Icons"
foreach ($svg in Get-ChildItem -LiteralPath $svgFolder -Filter "*.svg" -File) {
  $iconPath = Join-Path $iconsFolder "$($svg.BaseName).ico"
  Write-Host "Generating $($svg.BaseName).ico..."
  & $magickExe -background none `
         -size 256x256 $svg.FullName `
         -depth 8 `
         -define "icon:auto-resize=$($iconResolutions -join ',')" `
         $iconPath
  if ($LASTEXITCODE -ne 0) {
    throw "Failed to generate '$iconPath' (exit code $LASTEXITCODE)."
  }
}
