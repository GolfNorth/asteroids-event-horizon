using System.ComponentModel;
using Asteroids.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    /// <summary>
    /// Интерфейс игры
    /// </summary>
    public sealed class GameUI : MonoBehaviour
    {
        [SerializeField, Tooltip("ViewModel игры")]
        private GameViewModel viewModel;

        [SerializeField, Tooltip("Текстовый компонент для счета")]
        private Text scoreText;

        [SerializeField, Tooltip("Объект окончания игры")]
        private GameObject gameOverObject;

        [SerializeField, Tooltip("Объект начала игры")]
        private GameObject playObject;

        private void OnEnable()
        {
            UpdateAll();

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
                case "Score":
                    UpdateScore();
                    break;
                case "IsPlay":
                    UpdateScore();
                    UpdatePlay();
                    break;
                case "IsGameOver":
                    UpdateGameOver();
                    break;
            }
        }

        /// <summary>
        /// Обновить все поля
        /// </summary>
        private void UpdateAll()
        {
            UpdateScore();
            UpdateGameOver();
            UpdatePlay();
        }

        /// <summary>
        /// Обновить поле счета
        /// </summary>
        private void UpdateScore()
        {
            if (!viewModel.IsPlay && !viewModel.IsGameOver)
            {
                scoreText.gameObject.SetActive(false);
            }
            else if (!scoreText.gameObject.activeInHierarchy)
            {
                scoreText.gameObject.SetActive(true);
            }

            scoreText.text = $"Score: {viewModel.Score}";
        }

        /// <summary>
        /// Обновить объект окончания игры
        /// </summary>
        private void UpdateGameOver()
        {
            gameOverObject.SetActive(viewModel.IsGameOver);
        }

        /// <summary>
        /// Обновить объект начала игры
        /// </summary>
        private void UpdatePlay()
        {
            playObject.SetActive(!viewModel.IsPlay);
        }
    }
}