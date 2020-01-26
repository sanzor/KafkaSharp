comment=$0
git add .
git commit -m $comment
git push

dotnet publish -c Release -o D:/lsbuild
cp -R D:/lsbuild  C:/Leplace/adrianbuilds/ls
cd C:/Leplace/adrianbuilds && git add . && git commit -m "${comment}" && git push
