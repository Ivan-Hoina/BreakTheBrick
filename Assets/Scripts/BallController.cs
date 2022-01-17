using System.Collections;
using UnityEngine;

namespace BreakTheBrick
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        #region Singleton
        public static BallController Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Debug.LogWarning("Ball Already Exist. Destroying!");
                Destroy(this);
            }
        }
        #endregion

        public Vector3 Position { get => transform.position; }

        private bool _isThrown = false;
        private float _speed = 10f;
        private float _lifeTime = 10f;
        private Vector3 _defaultPosition;
        private Rigidbody _rigidbody;

        private void Initialize()
        {
            _defaultPosition = transform.position;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = true;

            InputSystem.OnSwipeEnd.AddListener(OnSwipeEnd);
        }       

        public void Throw(Vector3 vector)
        {
            if (_isThrown)
                return;

            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

            // If the magnitude less than 0.55, set the minimum value
            float magnitude = vector.magnitude < 0.55f ? 0.55f : vector.magnitude;

            float force = magnitude * _speed;
            Debug.Log("Force: " + force);

            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(vector * force, ForceMode.VelocityChange);
            StartCoroutine(LifeCycle());
        }

        public void Respawn()
        {
            transform.position = _defaultPosition;
            _isThrown = false;
            _rigidbody.isKinematic = true;
            StopAllCoroutines();
        }

        private void OnSwipeEnd(Vector3 inputPositionInWorld)
        {
            Throw(inputPositionInWorld);
            _isThrown = true;
        }

        private IEnumerator LifeCycle()
        {
            yield return new WaitForSeconds(_lifeTime);
            Respawn();
        }
    }
}
