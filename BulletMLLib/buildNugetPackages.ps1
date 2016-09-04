rm *.nupkg
nuget pack .\BulletMLLib.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg