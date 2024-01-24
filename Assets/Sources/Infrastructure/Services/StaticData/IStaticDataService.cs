using Sources.StaticData;

namespace Sources.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        AnchorStaticData GetAnchorData();
    }
}