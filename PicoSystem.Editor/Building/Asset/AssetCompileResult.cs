using PicoSystem.Framework.Content;

namespace PicoSystem.Editor.Building.Asset
{
    public class AssetCompileResult
    {
        public AssetData AssetData { get; }

        public string Error { get; }

        public AssetCompileResult(AssetData assetData, string error)
        {
            AssetData = assetData;

            Error = error;
        }
    }
}
