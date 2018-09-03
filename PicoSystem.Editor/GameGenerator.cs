using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace PicoSystem.Editor
{
    public static class GameGenerator
    {
        public static readonly string PROJECT_FILE_TEMPLATE = 
        @"
        {
            ""Name"": ""{{projectName}}"",
            ""MainScript"": ""Main.csx"",
            ""Assets"": {
        
                ""Logo"" : ""Images:Logo.png""
            }
        }";

        public static readonly string MAIN_SCRIPT_TEMPLATE =
        @"
        using System;
        using PicoSystem.Framework.Common;
        using PicoSystem.Framework.Graphics;
        using PicoSystem.Framework.Scripting;

        public class TestScript : PicoScript
        {
            public override void Init()
            {
        
            }

            public override void UpdateMouseState(float dt)
            {
        
            }

            public override void Draw(PicoRenderer renderer)
            {
                renderer.DrawText(""Hello World!"", 200, 200, Color.White, 2);
            }
        }";

        public static string Generate(string targetFolder, string name)
        {
            string targetPath = Path.Combine(targetFolder, name);

            try
            {
                var gameDir = Directory.CreateDirectory(targetPath);

                var projectFilePath = Path.Combine(targetPath, $"{name}.json");

                // Create Project File
                using (FileStream fs = File.Create(projectFilePath))
                {
                    Byte[] code = new UTF8Encoding(true).GetBytes(PROJECT_FILE_TEMPLATE.Replace("{{projectName}}", name));

                    fs.Write(code, 0, code.Length);

                }


                // Create Main Script
                var scriptsDir = gameDir.CreateSubdirectory("Scripts");
                var mainScriptPath = Path.Combine(scriptsDir.FullName, "Main.csx");
                using (FileStream fs = File.Create(mainScriptPath))
                {
                    Byte[] code = new UTF8Encoding(true).GetBytes(MAIN_SCRIPT_TEMPLATE);

                    fs.Write(code, 0, code.Length);
                }

                // Copy Default Image
                var outPutFolder = gameDir.CreateSubdirectory("Assets").CreateSubdirectory("Images");

                string outPuthPath = Path.Combine(outPutFolder.FullName, "Logo.png");

                using (var logoStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PicoSystem.Editor.Assets.PicoSystemLogo.png"))
                {
                    if (logoStream != null)
                    {
                        using (var logoFile = new FileStream(outPuthPath, FileMode.Create, FileAccess.Write))
                        {
                            logoStream.CopyTo(logoFile);
                        }
                    }
                }

                return projectFilePath;
            }
            catch (Exception e)
            {
                throw new Exception($"[GameGenerator] Generate : Error creating game template: {e.Message}");
            }

        }
    }
}
