using Core.GameConfigs;
using Infrastructure.Container;

namespace Services.Providers.ConfigProvider
{
    public interface IGameConfigProvider : IService
    {
        PlayerConfig GetCurrentPlayerConfig();
        GameConfig GetCurrentGameConfig();
    }
}