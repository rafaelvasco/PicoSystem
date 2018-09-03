using System;
using System.Collections.Generic;

namespace PicoSystem.Framework.Content
{
    [Serializable]
    public class AssetsPack
    {
        public Dictionary<string, AssetData> Assets { get; set; }

        public AssetsPack()
        {
            Assets = new Dictionary<string, AssetData>();
        }

        public void AddAssetData(string id, AssetData data)
        {
            Assets.Add(id, data);
        }
    }
}
