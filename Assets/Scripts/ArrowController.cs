using UnityEngine;

namespace BreakTheBrick
{
    [RequireComponent(typeof(LineRenderer))]
    public class ArrowController : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private Vector3 defaultPointPosition;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            defaultPointPosition = lineRenderer.GetPosition(1);

            InputSystem.OnSwipeBegin.AddListener(OnSwipeBegin);
            InputSystem.OnSwipe.AddListener(OnSwipe);
            InputSystem.OnSwipeEnd.AddListener(OnSwipeEnd);
        }

        private void OnSwipeBegin()
        {
            lineRenderer.enabled = true;
        }

        private void OnSwipe(Vector3 origin)
        {
            if (Time.timeScale == 0)
                return;

            lineRenderer.SetPosition(1, defaultPointPosition + origin * 1.5f);
        }

        private void OnSwipeEnd(Vector3 origin)
        {
            lineRenderer.SetPosition(1, defaultPointPosition);
            lineRenderer.enabled = false;
        }
    }
}