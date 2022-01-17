using UnityEngine;
using UnityEngine.Events;

namespace BreakTheBrick
{
    public class InputSystem : MonoBehaviour
    {
        public static UnityEvent OnSwipeBegin = new UnityEvent();
        public static UnityEvent<Vector3> OnSwipe = new UnityEvent<Vector3>();
        public static UnityEvent<Vector3> OnSwipeEnd = new UnityEvent<Vector3>();

        [SerializeField] private float rayCastDistance = 100f;
        [SerializeField] private LayerMask swipeLayerMask;
        [SerializeField] private SwipeObject swipeObject;

        private bool isSwiping;
        private Vector2 swipeDelta;
        private Vector2 beginPosition;
        private Vector2 currentPosition;
        private Camera mainCamera;
        
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Ray ray = new Ray();
            RaycastHit hitInfo = new RaycastHit();

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                ray = mainCamera.ScreenPointToRay((Vector2)Input.mousePosition);
                if(Physics.Raycast(ray, out hitInfo, rayCastDistance, swipeLayerMask))
                {
                    if(hitInfo.transform.TryGetComponent(out swipeObject))
                    {
                        isSwiping = true;
                        beginPosition = hitInfo.textureCoord;
                        currentPosition = beginPosition;
                        OnSwipeBegin?.Invoke();
                    }
                    else
                        Debug.Log("Can't find Swipe Object");
                }
                else
                    Debug.Log("Raycast didn't hit any objects");
            }else if (Input.GetMouseButtonUp(0) && isSwiping)
            {
                ResetSwipe();
            }
#endif

#if UNITY_ANDROID
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    ray = mainCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hitInfo, rayCastDistance, swipeLayerMask))
                    {
                        if (hitInfo.transform.TryGetComponent(out swipeObject))
                        {
                            isSwiping = true;
                            beginPosition = hitInfo.textureCoord;
                            currentPosition = beginPosition;
                            OnSwipeBegin?.Invoke();
                        }
                        else
                            Debug.Log("Can't find Swipe Object");
                    }
                    else
                        Debug.Log("Raycast didn't hit any objects");

                }
                else if ((touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) && isSwiping)
                {
                    ResetSwipe();
                }
            }
#endif        
            CheckSwipe();
        }

        private void CheckSwipe()
        {
            swipeDelta = Vector3.zero;

            if (isSwiping)
            {
#if UNITY_EDITOR
                if (Input.GetMouseButton(0))
                    currentPosition = GetMousePosition();
#endif
#if UNITY_ANDROID
                if (Input.touchCount > 0)
                    currentPosition = GetTouchPosition(Input.GetTouch(0));
#endif
                swipeDelta = currentPosition - beginPosition;

                if (swipeDelta.magnitude != 0)
                    OnSwipe?.Invoke(FixVector(swipeDelta));
            }
        }

        private void ResetSwipe()
        {
            OnSwipeEnd?.Invoke(FixVector(swipeDelta));
            isSwiping = false;

            beginPosition = Vector2.zero;
            currentPosition = Vector2.zero;
        }

        private Vector2 GetMousePosition()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayCastDistance, swipeLayerMask))
            {
                if (hitInfo.transform.TryGetComponent(out SwipeObject swipeObject))
                    currentPosition = hitInfo.textureCoord;
            }
            return currentPosition;
        }

        private Vector2 GetTouchPosition(Touch touch)
        {
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayCastDistance, swipeLayerMask))
            {
                if (hitInfo.transform.TryGetComponent(out SwipeObject swipeObject))
                    currentPosition = hitInfo.textureCoord;
            }
            return currentPosition;
        }

        private Vector3 FixVector(Vector2 vector)
        {
            Vector3 newVector;
            newVector.x = -vector.x * swipeObject.Scale.z;
            newVector.z = -vector.y * swipeObject.Scale.x;
            newVector.y = 0;

            return newVector / 1.3f;
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}