using UnityEngine;

namespace BreakTheBrick
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 defaultPosition;

        private void Awake()
        {
            defaultPosition = transform.position;

            InputSystem.OnSwipe.AddListener(OnSwipe);
            InputSystem.OnSwipeEnd.AddListener(OnSwipeEnd);
        }

        private void OnSwipe(Vector3 origin)
        {
            transform.position = (defaultPosition + origin);
        }

        private void OnSwipeEnd(Vector3 origin)
        {
            transform.position = defaultPosition;
        }
    }
}
