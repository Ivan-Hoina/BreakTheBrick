using UnityEngine;

namespace BreakTheBrick
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Debug.LogWarning("Game Manager Already Exists. Destroying");
                Destroy(this);
            }
        }
        #endregion

        [SerializeField] private InputSystem inputSystem;

        public bool IsPaused { get => _isPaused; }

        private bool _isPaused;

        private void Initialize()
        {
            _isPaused = true;
            Time.timeScale = 0;            
        }
        
        public void SwitchPause()
        {
            if (_isPaused)
                UnPause();
            else
                Pause();
        }

        public void UnPause()
        {
            Time.timeScale = 1f;
            _isPaused = false;
            inputSystem.Enable();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            _isPaused = true;
            inputSystem.Disable();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}