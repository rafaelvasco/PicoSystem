using System.Collections.Generic;
using System.IO;

namespace PicoSystem.Editor
{
    public class GameProject
    {
        public const string SCRIPTS_DIR_NAME = "Scripts";

        public const string ASSETS_DIR_NAME = "Assets";

        public const string BUILD_DIR_NAME = "Build";

        public const string DIST_DIR_NAME = "Dist";

        public string Name { get; }

        public string MainScript { get; }

        public Dictionary<string, string> Assets { get; }

        public string ScriptsDirectory { get; }

        public string AssetsDirectory { get; }

        public string BuildDirectory { get; set; }

        public string DistDirectory { get; }


        public GameProject(string gameProjectPath, GameProjectDefinition projectDefinition)
        {
            Name = projectDefinition.Name;

            MainScript = projectDefinition.MainScript;

            Assets = projectDefinition.Assets;

            ScriptsDirectory = Path.Combine(gameProjectPath, SCRIPTS_DIR_NAME);

            AssetsDirectory = Path.Combine(gameProjectPath, ASSETS_DIR_NAME);

            BuildDirectory = Path.Combine(gameProjectPath, "Build");

            DistDirectory = Path.Combine(gameProjectPath, DIST_DIR_NAME);
        }
    }
}
