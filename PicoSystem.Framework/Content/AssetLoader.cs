using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PicoSystem.Framework.Graphics;
using PicoSystem.Framework.Platform.STB;
using PicoSystem.Framework.Serialization;

namespace PicoSystem.Framework.Content
{
    public static class AssetLoader
    {
        private const string EMBEDDED_CONTENT_NAMESPACE = "PicoSystem.Framework.EmbeddedContent";
        private const string EMBEDDED_FILES_NAMESPACE = EMBEDDED_CONTENT_NAMESPACE + ".Files.";
        private const string EMBEDDED_IMAGES_NAMESPACE = EMBEDDED_CONTENT_NAMESPACE + ".Images";
        private const char ASSET_ID_FOLDER_SEPARATOR = ':';
        private const string ASSETS_FOLDER = "Assets";


        public static Pixmap LoadPixmap(string assetId)
        {
            if (assetId.Contains("@"))
            {
                return LoadEmbeddedPixmap(assetId.Replace("@", ""));
            }

            if (!assetId.Contains(".png"))
            {
                assetId += ".png";
            }

            string assetRelativePath = assetId.Replace(ASSET_ID_FOLDER_SEPARATOR, Path.DirectorySeparatorChar);

            string assetFullPath = Path.Combine(ASSETS_FOLDER, assetRelativePath);

            if (File.Exists(assetFullPath))
            {
                Pixmap pixmap;

                using (FileStream stream = File.OpenRead(assetFullPath))
                {
                    pixmap = LoadPixmap(stream);
                }

                return pixmap;
            }

            throw new Exception($"[AssetLoader] File not found: {assetFullPath}");
        }

        public static AssetsPack LoadAssetsPack()
        {
            string assetPackFilePath = Path.Combine(ASSETS_FOLDER, "Assets.pck");

            if (File.Exists(assetPackFilePath))
            {
                var assetPackFileData = File.ReadAllBytes(assetPackFilePath);

                AssetsPack pack = BinarySerializer.Deserialize<AssetsPack>(assetPackFileData);

                return pack;
            }

            return null;
        }

        internal static List<string> LoadEmbeddedTextFile(string fileName)
        {
            var lines = new List<string>();

            using (var stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(EMBEDDED_FILES_NAMESPACE + fileName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                        {
                            lines.Add(line);
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            return lines;
        }

        private static Pixmap LoadPixmap(Stream fileStream)
        {
            ImageReader loader = new ImageReader();
            Image image = loader.Read(fileStream, Stb.STBI_rgb_alpha);
            byte[] imageData = image.Data;

            return new Pixmap(imageData, image.Width, image.Height);
        }

        private static Pixmap LoadEmbeddedPixmap(string fileName)
        {
            Pixmap pixmap = null;

            using (var stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(EMBEDDED_IMAGES_NAMESPACE + fileName))
            {
                if (stream != null)
                {
                    pixmap = LoadPixmap(stream);
                }
            }

            return pixmap;
        }

        
    }
}
