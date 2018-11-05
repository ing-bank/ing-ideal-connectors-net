//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////

#tool "nuget:?package=GitVersion.CommandLine.DotNetCore&version=4.0.0"
#addin nuget:?package=Cake.Json
#addin nuget:?package=Newtonsoft.Json&version=9.0.1

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var projects = "./src/**/*.csproj";
var testProjects = "./test/**/*.csproj";

var allProjectFiles = GetFiles(projects) + GetFiles(testProjects);

var packFiles = "./src/iDealAdvancedConnector/*.csproj";
var buildArtifacts = "./artifacts";

public class GitVersionCustom
{
    public string NuGetVersion { get; set; }
    public string PreReleaseLabel { get; set; } 
    public string BranchName { get; set; }
    public string Sha { get; set; } 
}

GitVersionCustom gitVersionInfo;
string nugetVersion;

Setup(context =>
{

    //We don't use the build-in GitVersion() currently since it doesn't support
    //.NET Core version of  GitVersion. Instead, we invoke GitVersion manually here
    string gitVersionTool = Context.Tools.Resolve("GitVersion.dll").ToString();
    var gitVersionSettings = new ProcessSettings
        {
            Arguments = gitVersionTool,
            RedirectStandardOutput = true
        };

    using(var process = StartAndReturnProcess("dotnet", gitVersionSettings))
    {
        string gitVersionJson = string.Join("", process.GetStandardOutput());
        process.WaitForExit();
        gitVersionInfo = DeserializeJson<GitVersionCustom>(gitVersionJson);
    }

    nugetVersion = $"{gitVersionInfo.NuGetVersion}+{gitVersionInfo.Sha}";
    Information("Building iDealAdvancedConnector v{0} with configuration {1}", nugetVersion, configuration);
});

Task("__Clean")
    .Does(() => 
    {
        CleanDirectories(buildArtifacts);
    });

Task("__Restore")
    .Does(() =>
    {      
        foreach (var projectFile in allProjectFiles)
        {
            DotNetCoreRestore(projectFile.ToString());
        }
    });

Task("__Build")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
        };
        
        foreach (var projectFile in allProjectFiles)
        {
            DotNetCoreBuild(projectFile.ToString(), settings);
        }
    });

Task("__Test")
    .Does(() =>
    {
        foreach (var projectFile in GetFiles(testProjects))
        {
            DotNetCoreRun(projectFile.ToString());
        }
    });

Task("__Pack")
    .Does(() => 
    {
        var settings = new DotNetCorePackSettings 
        {
            Configuration = configuration,
            OutputDirectory = buildArtifacts,
            NoBuild = true,
            ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
        };
        
        foreach (var projectFile in GetFiles(packFiles))
        {
            DotNetCorePack(projectFile.ToString(), settings); 
        }
    });

Task("__PublishNuget")
    .WithCriteria(() => ShouldPublish(Context))
    .Does(() => 
    {
        // Resolve the API key.
        var apiKey = EnvironmentVariable("NUGET_API_KEY");
        if(string.IsNullOrEmpty(apiKey)) {
            throw new InvalidOperationException("Could not resolve NuGet API key.");
        }

        // Resolve the API url.
        var apiUrl = EnvironmentVariable("NUGET_API_URL");
        if(string.IsNullOrEmpty(apiUrl)) {
            throw new InvalidOperationException("Could not resolve NuGet API url.");
        }

        foreach(var package in GetFiles("./artifacts/*.nupkg"))
        {
            // Push the package.
            DotNetCoreNuGetPush(package.ToString(), new DotNetCoreNuGetPushSettings  {
                ApiKey = apiKey,
                Source = apiUrl
            });
        }
    });


private static bool ShouldPublish(ICakeContext context)
{
    var buildSystem = context.BuildSystem();
    return buildSystem.TravisCI.IsRunningOnTravisCI 
        && !string.IsNullOrWhiteSpace(buildSystem.TravisCI.Environment.Build.Tag);
}

Task("Build")
    .IsDependentOn("__Clean")
    .IsDependentOn("__Restore")
    .IsDependentOn("__Build")
    //.IsDependentOn("__Test")
    .IsDependentOn("__Pack");

Task("Default")
    .IsDependentOn("Build");

Task("Deploy")
    .IsDependentOn("Build")
    .IsDependentOn("__PublishNuget");

RunTarget(target);