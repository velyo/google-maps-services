REM Generate the project docs website
copy README.md .\src\docfx\index.md
cd src\docfx
docfx metadata
docfx build
cd ..\..