using NonUnity.Game;
using UnityEngine;

namespace Asteroids.Data
{
    /// <summary>
    /// Данные астероидов
    /// </summary>
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "Data/AsteroidData", order = 0)]
    public sealed class AsteroidData : SettingsData<AsteroidSettings>
    {
        public override void Default()
        {
            settings.SpawnDelay = AsteroidSettings.DefaultSpawnDelay;
            settings.SizeSettings = AsteroidSettings.DefaultSizeSettings;
        }
    }
}