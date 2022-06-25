using Core.GameConfigs;
using UnityEngine;

namespace Services.Providers.ConfigProvider
{
    public class GameConfigProvider : IGameConfigProvider
    {
        private const string PlayerConfigPath = "Configs/GameConfigs/PlayerConfig";
        private const string GameConfigPath = "Configs/GameConfigs/GameConfig";
        
        public PlayerConfig GetCurrentPlayerConfig() => 
            Resources.Load<PlayerConfig>(PlayerConfigPath);

        public GameConfig GetCurrentGameConfig() => 
            Resources.Load<GameConfig>(GameConfigPath);
    }
}