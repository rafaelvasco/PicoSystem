using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PicoSystem.Editor.Building.Asset;
using PicoSystem.Editor.Building.Script;
using PicoSystem.Framework.Content;

namespace PicoSystem.Editor.Building
{
    public class BuildResult {
        
        public bool Ok { get; set; }

        public bool AssetsInvalidated { get; set; }
        
        public string ResultMessage { get; set; }

        public byte[] ScriptData { get; set; }

        public BuildResult()
        {
            ResultMessage = string.Empty;
        }
    }

    public class GameBuilder
    {
        private readonly ScriptsBuilder _scriptsBuilder;
        private readonly AssetsBuilder _assetsBuilder;

        private readonly string[] exportDLLNames = {
            "PicoSystem.Framework.dll",
            "SDL2.dll",
            "soloud_x64.dll"
        };

        #if DEBUG

        private readonly string[] exportDebugDLLNames =
        {
            
        };

        #endif

        public GameBuilder()
        {
            _scriptsBuilder = new ScriptsBuilder();
            _assetsBuilder = new AssetsBuilder();
        }

        public async Task<BuildResult> Build(GameProject gameProject, bool releaseMode=false)
        {
            BuildResult result = new BuildResult();

            string targetDirectory = releaseMode ? gameProject.DistDirectory : gameProject.BuildDirectory;

            StringBuilder messageBuilder = new StringBuilder();

            FileInfo mainScriptFile = new FileInfo(Path.Combine(gameProject.ScriptsDirectory, gameProject.MainScript));

            if (mainScriptFile.Exists)
            {

                ScriptsCompileResult scriptsCompileResult = await _scriptsBuilder.Build(gameProject, mainScriptFile, releaseMode);

                bool scriptsOk = scriptsCompileResult.Errors == null;

                if (scriptsOk)
                {
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    result.ScriptData = scriptsCompileResult.ScriptData;
                    
                    bool assetsOk = true;

                    result.AssetsInvalidated = false;

                    // Build Resource Pack
                    List<AssetCompileResult> assetCompileResults = await _assetsBuilder.Build(gameProject);

                    if (assetCompileResults != null && assetCompileResults.Count > 0)
                    {
                        result.AssetsInvalidated = true;

                        foreach (var assetCompileResult in assetCompileResults)
                        {
                            if (!string.IsNullOrEmpty(assetCompileResult.Error))
                            {
                                if (assetsOk)
                                {
                                    messageBuilder.AppendLine("Error building assets:");
                                }

                                assetsOk = false;

                                messageBuilder.AppendLine($"In Resource: {assetCompileResult.AssetData.Name} : {assetCompileResult.Error}");
                            }
                        }

                        AssetsPack assetPack = new AssetsPack();

                        foreach (var assetCompileResult in assetCompileResults)
                        {
                            assetPack.AddAssetData(assetCompileResult.AssetData.Name, assetCompileResult.AssetData);
                        }

                        _assetsBuilder.SaveAssetsDataFile(targetDirectory, assetPack);

                    }

                    if (assetsOk)
                    {
                        result.Ok = true;
                    }
                    else
                    {
                        result.ResultMessage = messageBuilder.ToString();
                    }

                    return result;
                }
                
                // Build Error:

                messageBuilder.AppendLine("Error compiling scripts:");

                foreach (var error in scriptsCompileResult.Errors)
                {
                    messageBuilder.AppendLine(error);
                }

                result.ResultMessage = messageBuilder.ToString();
                result.Ok = false;
                return result;

            }
            else
            {
                result.ResultMessage = "Missing Main Script!";
                result.Ok = false;
                return result;

            }
            
        }

        public async Task<BuildResult> Export(GameProject gameProject)
        {
            BuildResult result = await Build(gameProject, releaseMode: true);

            if (result.Ok)
            {
                // Copy Game Script DLL
                _scriptsBuilder.SaveScriptsDataFile(gameProject, result.ScriptData);

                // Copy Game Executable

                var srcExePath = Path.Combine(Directory.GetCurrentDirectory(), "PicoSystem.Player.exe");
                var targetExePath = Path.Combine(gameProject.DistDirectory, $"{gameProject.Name}.exe");

                File.Copy(srcExePath, targetExePath);

                // Copy dependencies DLL's

                foreach (var exportDllName in exportDLLNames)
                {
                    string dllPath = Path.Combine(Directory.GetCurrentDirectory(), exportDllName);
                    string targetPath = Path.Combine(gameProject.DistDirectory, exportDllName);

                    File.Copy(dllPath, targetPath);

                }

#if DEBUG

                foreach (var exportDllName in exportDebugDLLNames)
                {
                    string dllPath = Path.Combine(Directory.GetCurrentDirectory(), exportDllName);
                    string targetPath = Path.Combine(gameProject.DistDirectory, exportDllName);

                    File.Copy(dllPath, targetPath);

                }
#endif

            }

            return result;
        }

        public void Cleanup(GameProject gameProject)
        {

            if (Directory.Exists(gameProject.BuildDirectory))
            {
                Array.ForEach(Directory.GetFiles(gameProject.BuildDirectory), File.Delete );
            }
        }
    }
}
