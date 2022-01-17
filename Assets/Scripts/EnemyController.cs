using UnityEngine;

namespace BreakTheBrick
{
    public class EnemyController : MonoBehaviour
    {
        private float _moveStep;
        private BallController _ballController;
        private void Start()
        {
            _moveStep = 0.01f;
            _ballController = BallController.Instance;
            DifficultyManager.OnDifficultyChange.AddListener(OnDifficultyChange);
        }

        private void FixedUpdate()
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y, _ballController.Position.z);
            Vector3 newPosition = Vector3.Lerp(transform.position, target, _moveStep);

            transform.position = newPosition;
        }

        private void OnDifficultyChange(int newLevel)
        {
            _moveStep *= 1.5f;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out BallController ballController))
            {
                ballController.Respawn();
            }
        }
    }
}
