using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PicoSystem.Framework.Content;
using PicoSystem.Framework.Graphics;
using PicoSystem.Framework.Serialization;

namespace PicoSystem.Editor.Building.Asset
{
    public class AssetsBuilder
    {
        private DateTime _lastBuildTime;

        private readonly Dictionary<string, Task> _compilationTasks;

        public AssetsBuilder()
        {
            _compilationTasks = new Dictionary<string, Task>();

            _lastBuildTime = DateTime.MinValue;
        }

        public async Task<List<AssetCompileResult>> Build(GameProject project)
        {
            _compilationTasks.Clear();

            var assetsDictionary = project.Assets;

            List<AssetCompileResult> compileResults = null;

            foreach (var asset in assetsDictionary)
            {
                var assetRelativePath = asset.Value.Replace(':', Path.DirectorySeparatorChar);

                var assetPath = Path.Combine(project.AssetsDirectory, assetRelativePath) ;

                FileInfo assetFileInfo = new FileInfo(assetPath);

                
                if (assetFileInfo.Exists)
                {
                    if (assetFileInfo.LastWriteTime > _lastBuildTime)
                    {
                        if (compileResults == null)
                        {
                            compileResults = new List<AssetCompileResult>();
                        }

                        var assetData = new AssetData()
                        {
                            Name = asset.Key
                        };

                        switch (assetFileInfo.Extension)
                        {
                            case ".png":
                            {
                                assetData.Type = AssetDataType.Pixmap;
                                _compilationTasks.Add(assetData.Name, GetCompileTask(compileResults, assetFileInfo, assetData));
                                 break;
                            }
                        }
                    }
                }
            }

            if (_compilationTasks.Count > 0)
            {
                foreach (Task task in _compilationTasks.Values)
                {
                    task.Start();
                }

                await Task.WhenAll(_compilationTasks.Values);
            }

            _lastBuildTime = DateTime.Now;

            return compileResults;
        }

        private AssetCompileResult BuildAsset(FileInfo file, AssetData assetData)
        {
            switch (assetData.Type)
            {
                case AssetDataType.Pixmap:
                {
                    int imageWidth, imageHeight;

                    var imageData = ProcessImageFile(file, out imageWidth, out imageHeight);

                    if (imageData == null)
                    {
                        return new AssetCompileResult(assetData: null, error: $"Could not process image file: {file.Name}");
                    }

                    assetData.Data = imageData;
                    assetData.MetaData1 = imageWidth;
                    assetData.MetaData2 = imageHeight;

                    AssetCompileResult result = new AssetCompileResult(assetData: assetData, error: null);

                    return result;
                }
                default:
                {
                    return new AssetCompileResult(assetData: null, error: "Invalid asset type");
                }
            }
        }

        private byte[] ProcessImageFile(FileInfo file, out int imageWidth, out int imageHeight)
        {
            Pixmap pixmap = AssetLoader.LoadPixmap(file.FullName);

            imageWidth = pixmap.Width;
            imageHeight = pixmap.Height;

            var data = new byte[pixmap.ByteArray.Length];

            pixmap.ByteArray.CopyTo(data, 0);

            pixmap.Dispose();

            return data;
        }

        /// <summary>
        /// Save Assets Data File
        /// </summary>
        /// <param name="buildPath"></param>
        /// <param name="pack"></param>
        /// <returns>True if Overwrited existing file, otherwise False</returns>
        public void SaveAssetsDataFile(string buildPath, AssetsPack pack)
        {
            var bytes = BinarySerializer.Serialize(pack);

            var filePath = Path.Combine(buildPath, "Assets.pck");

            File.WriteAllBytes(filePath, bytes);
        }
       
        private Task GetCompileTask(List<AssetCompileResult> compilationResults, FileInfo file, AssetData assetData)
        {
            return new Task(() =>
            {
                compilationResults.Add(BuildAsset(file, assetData));
            });
        }
    }

}
