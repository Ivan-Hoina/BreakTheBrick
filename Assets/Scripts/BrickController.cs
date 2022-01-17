using UnityEngine;
using UnityEngine.Events;

namespace BreakTheBrick
{
    public class BrickController : MonoBehaviour
    {
        public static UnityEvent OnBrickBrokenEvent = new UnityEvent();

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out BallController ballController))
            {
                gameObject.SetActive(false);
                ballController.Respawn();
                OnBrickBrokenEvent?.Invoke();
            }
        }

        public void Restart()
        {
            gameObject.SetActive(true);
        }
    }
}
