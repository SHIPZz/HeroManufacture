using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        T LoadAsset<T>(string path) where T : Object;
    }
}