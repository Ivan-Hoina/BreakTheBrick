using UnityEngine;
using UnityEngine.UI;

namespace BreakTheBrick
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Text pauseText;

        private void Awake()
        {
            DifficultyManager.OnDifficultyChange.AddListener(OnDifficultyChange);
        }

        private void OnDifficultyChange(int newLevel)
        {
            levelText.text = "Level: " + newLevel;
        }

        public void Play()
        {
            playButton.gameObject.SetActive(false);
            levelText.gameObject.SetActive(true);
            pauseButton.interactable = true;
            GameManager.Instance.UnPause();
        }

        public void Pause()
        {
            GameManager.Instance.SwitchPause();
            pauseText.gameObject.SetActive(GameManager.Instance.IsPaused);
        }
    }
}
