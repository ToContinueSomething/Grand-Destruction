using Sources.Infrastructure.Services;
using Sources.Services;
using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
    }
}