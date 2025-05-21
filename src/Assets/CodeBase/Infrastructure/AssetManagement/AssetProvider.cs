using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}