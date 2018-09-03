using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace PicoSystem.Editor.Building.Script
{
    public class ScriptsBuilder
    {

        private readonly HashSet<MetadataReference> _assemblies;

        private CSharpCompilationOptions _compilationOptions;

        private DateTime _lastBuildTime;


        public ScriptsBuilder()
        {
            _assemblies = new HashSet<MetadataReference>();

            _compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithUsings(

                "System",
                "PicoSystem.Framework.Common",
                "PicoSystem.Framework.Graphics",
                "PicoSystem.Framework.Scripting"

            ).WithPlatform(Platform.X64);

            _lastBuildTime = DateTime.MinValue;

            SetAssemblies();
        }

        public async Task<ScriptsCompileResult> Build(GameProject gameProject, FileInfo mainScript, bool releaseMode)
        {
            string scriptsPath = mainScript.DirectoryName;


            _compilationOptions =
                _compilationOptions.WithSourceReferenceResolver(
                    new SourceFileResolver(ImmutableArray<string>.Empty, scriptsPath));


            if (releaseMode)
            {
                _compilationOptions = _compilationOptions.WithOptimizationLevel(OptimizationLevel.Release);
            }

            if (!releaseMode && mainScript.LastWriteTime < _lastBuildTime)
            {
                return new ScriptsCompileResult();
            }

            string scriptCode = File.ReadAllText(mainScript.FullName);

            ScriptsCompileResult result = await Task.Run(() => GenerateGameAssembly(scriptCode));

            if (result.Errors == null)
            {
                _lastBuildTime = DateTime.Now;
            }

            return result;
        }

        public void SaveScriptsDataFile(GameProject project, byte[] scriptData)
        {
            var codeDataPath = Path.Combine(project.DistDirectory, "Game.dll");

            File.WriteAllBytes(codeDataPath, scriptData);
        }

        private ScriptsCompileResult GenerateGameAssembly(string code)
        {

            SyntaxTree[] syntaxTree =
            {
                CSharpSyntaxTree.ParseText(code, CSharpParseOptions.Default.WithKind(SourceCodeKind.Script))
            };

            CSharpCompilation compilation = CSharpCompilation.Create("GameAssembly", syntaxTree, _assemblies, _compilationOptions);


            using (var stream = new MemoryStream())
            {

                EmitResult result = compilation.Emit(stream);

                if (result.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] scriptBytes;

                    GenerateScriptBinaryData(stream, out scriptBytes);

                    return new ScriptsCompileResult(scriptBytes, errors: null);
                }

                IEnumerable<Diagnostic> failures =
                    result.Diagnostics.Where(diagnostic =>

                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error
                    );

                List<string> errors = new List<string>();

                foreach (var diagnostic in failures)
                {
                    errors.Add(diagnostic.GetMessage());
                }

                return new ScriptsCompileResult(scriptData: null, errors: errors);
            }

        }
     
        private void GenerateScriptBinaryData(Stream scriptDataStream, out byte[] scriptData)
        {
            scriptData = StreamToByteArray(scriptDataStream);
        }
        
        private byte[] StreamToByteArray(Stream input)
        {
            var buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        private void SetAssemblies()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _assemblies.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\PicoSystem.Framework.dll").Location));


        }
    }
}
