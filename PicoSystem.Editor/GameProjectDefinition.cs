using System.Collections.Generic;

namespace PicoSystem.Editor
{
    public class GameProjectDefinition
    {
        public string Name { get; set; }

        public string MainScript { get; set; }

        public Dictionary<string, string> Assets { get; set; }
    }
}
