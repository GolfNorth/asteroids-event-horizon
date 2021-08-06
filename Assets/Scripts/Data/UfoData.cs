using Asteroids.Extensions;
using NonUnity.Game;
using UnityEngine;

namespace Asteroids.Data
{
    /// <summary>
    /// Данные НЛО
    /// </summary>
    [CreateAssetMenu(fileName = "UfoData", menuName = "Data/UfoData", order = 0)]
    public sealed class UfoData : SettingsData<UfoSettings>
    {
        [SerializeField, Tooltip("Вершины формы НЛО")]
        private Vector2[] shapeVertexes = new Vector2[0];

        public override ref UfoSettings GetData()
        {
            settings.ShapeVertexes = shapeVertexes.Convert();

            return ref settings;
        }

        public override void Default()
        {
            settings.Offset = UfoSettings.DefaultOffset;
            settings.SpawnDelay = UfoSettings.DefaultSpawnDelay;
            settings.LinearSpeed = UfoSettings.DefaultLinearSpeed;
            shapeVertexes = ShipSettings.DefaultShapeVertexes.Convert();
        }
    }
}