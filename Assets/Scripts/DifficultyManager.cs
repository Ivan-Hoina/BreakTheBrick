using UnityEngine;
using UnityEngine.Events;

namespace BreakTheBrick
{
    public class DifficultyManager : MonoBehaviour
    {
        public static UnityEvent<int> OnDifficultyChange = new UnityEvent<int>();

        private int _level;

        private void Awake()
        {
            _level = 1;
            BricksManager.OnAllBricksDestroyed.AddListener(OnAllBricksDestroyed);
        }

        private void OnAllBricksDestroyed()
        {
            _level++;
            OnDifficultyChange?.Invoke(_level);
        }
    }
}
