using NonUnity.Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Common
{
    /// <summary>
    /// Команды игры
    /// </summary>
    public sealed class Commands : MonoBehaviour
    {
        /// <summary>
        /// Геймплейный объект
        /// </summary>
        private Game _game;

        private void Awake()
        {
            _game = Context.Instance.Game;
        }

        /// <summary>
        /// Передвижение корабля
        /// </summary>
        /// <param name="callback"></param>
        public void Move(InputAction.CallbackContext callback)
        {
            Vector2 value = callback.ReadValue<Vector2>();

            _game.Command.Translate(value.y);
            _game.Command.Rotate(-value.x);
        }

        /// <summary>
        /// Выстрел корабля
        /// </summary>
        /// <param name="callback"></param>
        public void Fire(InputAction.CallbackContext callback)
        {
            bool pressed = callback.ReadValueAsButton();

            _game.Command.Shot(pressed);
        }

        /// <summary>
        /// Альтернативный выстрел корабля
        /// </summary>
        /// <param name="callback"></param>
        public void AltFire(InputAction.CallbackContext callback)
        {
            bool pressed = callback.ReadValueAsButton();

            _game.Command.AltShot(pressed);
        }
    }
}