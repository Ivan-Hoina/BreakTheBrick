using UnityEngine;

namespace BreakTheBrick
{
    [RequireComponent(typeof(LineRenderer))]
    public class ArrowController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Vector3 _defaultPointPosition;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
            _defaultPointPosition = _lineRenderer.GetPosition(1);

            InputSystem.OnSwipeBegin.AddListener(OnSwipeBegin);
            InputSystem.OnSwipe.AddListener(OnSwipe);
            InputSystem.OnSwipeEnd.AddListener(OnSwipeEnd);
        }

        private void OnSwipeBegin()
        {
            _lineRenderer.enabled = true;
        }

        private void OnSwipe(Vector3 origin)
        {
            if (Time.timeScale == 0)
                return;

            _lineRenderer.SetPosition(1, _defaultPointPosition + origin * 1.5f);
        }

        private void OnSwipeEnd(Vector3 origin)
        {
            _lineRenderer.SetPosition(1, _defaultPointPosition);
            _lineRenderer.enabled = false;
        }
    }
}