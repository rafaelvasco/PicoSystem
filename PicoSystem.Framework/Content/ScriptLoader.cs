using System;
using System.Linq;
using System.Reflection;
using PicoSystem.Framework.Scripting;

namespace PicoSystem.Framework.Content
{
    public static class ScriptLoader
    {
        public static PicoScript Load(PicoGame game, byte[] scriptData)
        {
            PicoScript script = null;

            Assembly gameAssembly = Assembly.Load(scriptData);

            Type picoScriptType = gameAssembly.GetTypes().First(

                myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(PicoScript))
            );

            if (picoScriptType != null)
            {
                script = (PicoScript)Activator.CreateInstance(picoScriptType);
                script.__SetGameReference(game);

            }

            return script;
        }
    }
}
