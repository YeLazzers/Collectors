using UnityEngine;

namespace YeLazzers.Core
{
    [RequireComponent(typeof(Camera))]
    public class RTSCamera : MonoBehaviour
    {
        [SerializeField] private InputReader _reader;

        [Header("Angle")]
        [SerializeField] private float _pitch = 55f;
        [SerializeField] private float _yaw;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 20f;

        [Header("Zoom")]
        [SerializeField] private float _zoomSpeed = 10f;
        [SerializeField] private float _minHeight = 5f;
        [SerializeField] private float _maxHeight = 30f;

        private Bounds _bounds;
        private bool _hasBounds;

        private void Awake()
        {
            ApplyAngle();
        }

        private void OnEnable()
        {
            _reader.HorizontalMoving += OnHorizontalMoving;
            _reader.VerticalMoving += OnVerticalMoving;
            _reader.Scrolled += OnScrolled;
        }

        private void OnDisable()
        {
            _reader.HorizontalMoving -= OnHorizontalMoving;
            _reader.VerticalMoving -= OnVerticalMoving;
            _reader.Scrolled -= OnScrolled;
        }

        private void OnValidate()
        {
            ApplyAngle();
        }

        public void SetBounds(Bounds bounds)
        {
            _bounds = bounds;
            _hasBounds = true;
        }

        public void CenterOn(Vector3 worldPosition)
        {
            Vector3 delta = worldPosition - GetLookPoint();
            delta.y = 0f;

            transform.position += delta;

            if (_hasBounds)
            {
                ClampLookPointToBounds();
            }
        }

        private void ApplyAngle()
        {
            transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        }

        private void OnHorizontalMoving(float value)
        {
            Vector3 right = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
            Move(right * value);
        }

        private void OnVerticalMoving(float value)
        {
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            Move(forward * value);
        }

        private void Move(Vector3 direction)
        {
            transform.position += direction * (_moveSpeed * Time.deltaTime);

            if (_hasBounds)
            {
                ClampLookPointToBounds();
            }
        }

        private void ClampLookPointToBounds()
        {
            Vector3 lookPoint = GetLookPoint();

            float clampedX = Mathf.Clamp(lookPoint.x, _bounds.min.x, _bounds.max.x);
            float clampedZ = Mathf.Clamp(lookPoint.z, _bounds.min.z, _bounds.max.z);

            transform.position += new Vector3(clampedX - lookPoint.x, 0f, clampedZ - lookPoint.z);
        }

        private Vector3 GetLookPoint()
        {
            Vector3 forward = transform.forward;
            if (Mathf.Approximately(forward.y, 0f))
                return transform.position;

            float distanceToGround = -transform.position.y / forward.y;
            return transform.position + (forward * distanceToGround);
        }

        private void OnScrolled(float value)
        {
            Vector3 direction = transform.forward;
            float step = value * _zoomSpeed;
            float targetHeight = Mathf.Clamp(transform.position.y + (direction.y * step), _minHeight, _maxHeight);

            step = (targetHeight - transform.position.y) / direction.y;
            transform.position += direction * step;
        }
    }
}
