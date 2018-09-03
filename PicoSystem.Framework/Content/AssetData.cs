using System;

namespace PicoSystem.Framework.Content
{
    public enum AssetDataType
    {
        Pixmap
    }

    [Serializable]
    public class AssetData
    {
        public AssetDataType Type { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public int MetaData1 { get; set; }

        public int MetaData2 { get; set; }

    }
}
