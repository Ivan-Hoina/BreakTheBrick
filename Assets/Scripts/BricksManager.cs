using UnityEngine;
using UnityEngine.Events;

namespace BreakTheBrick
{
    public class BricksManager : MonoBehaviour
    {
        public static UnityEvent OnAllBricksDestroyed = new UnityEvent();

        private int _brokenBricks;
        [SerializeField] private BrickController[] _bricks;

        private void Awake()
        {
            _brokenBricks = 0;
            BrickController.OnBrickBrokenEvent.AddListener(OnBrickBroken);
        }

        private void OnBrickBroken()
        {
            _brokenBricks++;
            if (_brokenBricks == 3)
                ReCreateBricks();
        }

        private void ReCreateBricks()
        {
            _brokenBricks = 0;
            foreach(BrickController brickController in _bricks)
            {
                brickController.Restart();
            }
            OnAllBricksDestroyed?.Invoke();
        }
    }
}
