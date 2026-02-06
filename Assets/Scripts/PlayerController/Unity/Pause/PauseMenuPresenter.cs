using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;
using Primitives.Unity.Infrastructure;
using Primitives.Execution;
using UnityEngine;
using Primitives.Common.Execution;

namespace PlayerController.Unity.Pause
{
    public class PauseMenuPresenter : MonoBehaviour, IInitializable<IGameContext>
    {
        [SerializeField] private GameObject _root;
        public int Priority => 1;
        private IGameStateEvents _gameStateEvents;
        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            SceneInitializationRegistry.Register(this);
        }
        public void Initialize(IGameContext context)
        {
            _gameStateEvents = context.GameStateServices.GameStateEvents;
            _gameStateEvents.OnGameStateChanged += HandleStateChange;
        }
        public void PostInitialize(IGameContext context)
        {
            // Should I break up these initialize and post initialize methods?
        }

        private void OnDestroy()
        {
            _gameStateEvents.OnGameStateChanged -= HandleStateChange;
        }

        private void HandleStateChange(GameState state)
        {
            if (state == GameState.Paused)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show() => _root.SetActive(true);
        private void Hide() => _root.SetActive(false);


    }
}
