using System.ComponentModel;
using Asteroids.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    /// <summary>
    /// Интерфейс показателей корабля
    /// </summary>
    public sealed class ShipUI : MonoBehaviour
    {
        [SerializeField, Tooltip("ViewModel корабля")]
        private ShipViewModel viewModel;

        [SerializeField, Tooltip("Текстовый компонент для позиции")]
        private Text positionText;

        [SerializeField, Tooltip("Текстовый компонент для поворота")]
        private Text rotationText;

        [SerializeField, Tooltip("Текстовый компонент для скорости")]
        private Text speedText;

        [SerializeField, Tooltip("Текстовый компонент для количества зарядов")]
        private Text laserChargesText;

        [SerializeField, Tooltip("Текстовый компонент для отсчета готовности заряда")]
        private Text laserNextFireText;

        private void OnEnable()
        {
            if (viewModel.Spawned)
            {
                Toggle(true);
                UpdateAll();
            }
            else
            {
                Toggle(false);
            }

            viewModel.PropertyChanged += OnPropertyChanged;
        }

        private void OnDisable()
        {
            viewModel.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Position":
                    UpdatePosition();
                    break;
                case "Rotation":
                    UpdateRotation();
                    break;
                case "Speed":
                    UpdateSpeed();
                    break;
                case "LaserCharges":
                    UpdateLaserCharges();
                    break;
                case "LaserNextFire":
                    UpdateLaserNextFire();
                    break;
                case "Spawned":
                    Toggle(viewModel.Spawned);
                    UpdateAll();
                    break;
            }
        }

        /// <summary>
        /// Переключить состояние полей
        /// </summary>
        /// <param name="active">Активность полей</param>
        private void Toggle(bool active)
        {
            positionText.gameObject.SetActive(active);
            rotationText.gameObject.SetActive(active);
            speedText.gameObject.SetActive(active);
            laserChargesText.gameObject.SetActive(active);
            laserNextFireText.gameObject.SetActive(active);
        }

        /// <summary>
        /// Обновить все поля
        /// </summary>
        private void UpdateAll()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateSpeed();
            UpdateLaserCharges();
            UpdateLaserNextFire();
        }

        /// <summary>
        /// Обновить поле позиции
        /// </summary>
        private void UpdatePosition()
        {
            positionText.text = $"Position: {viewModel.Position.x:F1}:{viewModel.Position.y:F1}";
        }

        /// <summary>
        /// Обновить поле поворота
        /// </summary>
        private void UpdateRotation()
        {
            rotationText.text = $"Rotation: {viewModel.Rotation:F0}";
        }

        /// <summary>
        /// Обновить поле скорости
        /// </summary>
        private void UpdateSpeed()
        {
            speedText.text = $"Speed: {viewModel.Speed:F1}";
        }

        /// <summary>
        /// Обновить поле зарядов
        /// </summary>
        private void UpdateLaserCharges()
        {
            laserChargesText.text = $"Charges: {viewModel.LaserCharges}";
        }

        /// <summary>
        /// Обновить поле следующего выстрела
        /// </summary>
        private void UpdateLaserNextFire()
        {
            laserNextFireText.text = $"Cooldown: {viewModel.LaserNextFire:F2}";
        }
    }
}