using System.Collections.Generic;

namespace PicoSystem.Editor.Building.Script
{
    public class ScriptsCompileResult
    {
        public bool UpToDate { get; }

        public byte[] ScriptData { get; }

        public List<string> Errors { get; }

        public ScriptsCompileResult(byte[] scriptData, IEnumerable<string> errors)
        {
            ScriptData = scriptData;

            if (errors != null)
            {
                Errors = new List<string>();

                foreach (var error in errors)
                {
                    Errors.Add(error);
                }
            }
        }

        public ScriptsCompileResult()
        {
            UpToDate = true;

            ScriptData = null;

            Errors = null;
        }
        
    }
}
