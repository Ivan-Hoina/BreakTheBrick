using UnityEngine;
using UnityEngine.Events;

namespace BreakTheBrick
{
    public class BricksManager : MonoBehaviour
    {
        public static UnityEvent OnAllBricksDestroyed = new UnityEvent();

        private int brokenBricks;
        [SerializeField] private BrickController[] bricks;

        private void Awake()
        {
            brokenBricks = 0;
            BrickController.OnBrickBrokenEvent.AddListener(OnBrickBroken);
        }

        private void OnBrickBroken()
        {
            brokenBricks++;
            if (brokenBricks == 3)
                ReCreateBricks();
        }

        private void ReCreateBricks()
        {
            brokenBricks = 0;
            foreach(BrickController brickController in bricks)
            {
                brickController.Restart();
            }
            OnAllBricksDestroyed?.Invoke();
        }
    }
}
