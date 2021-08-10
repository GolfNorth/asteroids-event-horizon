using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asteroids.Common;
using NonUnity.Game;
using UnityEngine;

namespace Asteroids.UI.ViewModels
{
    /// <summary>
    /// ViewModel игры
    /// </summary>
    public sealed class GameViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        /// <summary>
        /// Результат игры
        /// </summary>
        private int _score;

        /// <summary>
        /// Состояние игры
        /// </summary>
        private GameState _state;

        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Результат игры
        /// </summary>
        public int Score
        {
            get => _score;
            private set
            {
                if (_score == value)
                    return;

                _score = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Идет игра
        /// </summary>
        public bool IsPlay => _state == GameState.Play;

        /// <summary>
        /// Поражение
        /// </summary>
        public bool IsGameOver => _state == GameState.GameOver;

        /// <summary>
        /// Состояние игры
        /// </summary>
        private GameState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;

                OnPropertyChanged(nameof(IsPlay));
                OnPropertyChanged(nameof(IsGameOver));
            }
        }

        private void LateUpdate()
        {
            Score = Context.Instance.Game.Score;
            State = Context.Instance.Game.State;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}