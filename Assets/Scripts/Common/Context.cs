using System.Drawing;
using Asteroids.Data;
using NonUnity.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Asteroids.Common
{
    /// <summary>
    /// Главный контекст игры
    /// </summary>
    public sealed class Context : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField, Tooltip("Сцена интерфейса")]
        private Object uiScene;

        [SerializeField, Tooltip("Игровая камера")]
        private Camera gameCamera;

        [SerializeField, Tooltip("Фабрика визуализаторов")]
        private MonoBehaviour viewFactory;

        [Header("Data")]
        [SerializeField, Tooltip("Данные корабля")]
        private ShipData shipData;

        [SerializeField, Tooltip("Данные НЛО")]
        private UfoData ufoData;

        [SerializeField, Tooltip("Конфигурация астероидов")]
        private AsteroidData asteroidData;

        /// <summary>
        /// Имя сцены интерфейса
        /// </summary>
        [SerializeField, HideInInspector]
        private string uiSceneName;

        /// <summary>
        /// Завершение инициализации
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Текущее разрешение экрана
        /// </summary>
        private Vector2Int _resolution;

        /// <summary>
        /// Экземпляр объекта
        /// </summary>
        public static Context Instance;

        /// <summary>
        /// Объект геймплея
        /// </summary>
        public Game Game { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            if (scene.name == uiSceneName)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;

                _initialized = true;
            }
        }

        private void Start()
        {
            _resolution = new Vector2Int(Screen.width, Screen.height);

            RectangleF bounds = GetBounds();
            GameBuilder gameBuilder = new GameBuilder(bounds, (IViewFactory) viewFactory);

            if (shipData != null)
            {
                gameBuilder.SetShipSettings(in shipData.GetData());
            }

            if (ufoData != null)
            {
                gameBuilder.SetUfoSettings(in ufoData.GetData());
            }

            if (asteroidData != null)
            {
                gameBuilder.SetAsteroidSettings(in asteroidData.GetData());
            }

            Game = gameBuilder.Build();

            SceneManager.LoadSceneAsync(uiSceneName, LoadSceneMode.Additive);
        }

        private void Update()
        {
            if (!_initialized)
                return;

            if (_resolution.x != Screen.width || _resolution.y != Screen.height)
            {
                Game.Bounds = GetBounds();

                _resolution.x = Screen.width;
                _resolution.y = Screen.height;
            }

            Game.Update(Time.deltaTime);
        }

        /// <summary>
        /// Игровые границы
        /// </summary>
        private RectangleF GetBounds()
        {
            float z = gameCamera.gameObject.transform.position.z;
            Vector3 topRight = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, -z));
            Vector3 bottomLeft = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, -z));

            float width = Vector2.Distance(new Vector2(bottomLeft.x, 0), new Vector2(topRight.x, 0));
            float height = Vector2.Distance(new Vector2(0, topRight.y), new Vector2(0, bottomLeft.y));
            float offsetX = bottomLeft.x;
            float offsetY = topRight.y;

            return new RectangleF(offsetX, offsetY, width, height);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (viewFactory != null && !(viewFactory is IViewFactory))
            {
                IViewFactory match = viewFactory.GetComponent<IViewFactory>();
                viewFactory = match as MonoBehaviour;

                EditorUtility.SetDirty(this);
            }

            if (uiScene != null && uiScene is SceneAsset sceneAsset)
            {
                if (string.IsNullOrWhiteSpace(uiSceneName) || uiSceneName != sceneAsset.name)
                {
                    uiSceneName = sceneAsset.name;

                    EditorUtility.SetDirty(this);
                }
            }
            else if (!string.IsNullOrWhiteSpace(uiSceneName))
            {
                uiSceneName = string.Empty;

                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}