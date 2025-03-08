using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform _heightRoot;
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private Transform _rotateRoot;
        [SerializeField] private float _rotateSpeed = 45f;
        [SerializeField] private float _minAngle = 15f;
        [SerializeField] private float _maxAngle = 75f;
        [SerializeField] private Transform _zoomRoot;
        [SerializeField] private float _minZoomDistance = 2f;
        [SerializeField] private float _maxZoomDistance = 10f;
        [SerializeField] private float _zoomStep = 1f;
        [SerializeField] private float _zoomSpeed = 4f;
        [SerializeField] private Transform _collisionRoot;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private float _collisionAvoidanceSpeed = 8f;

        private PawnController _player;
        private Vector2 _lookInput;
        private bool _clicked;
        private float _xRot;
        private float _requiredZoomDistance;
        private float _currentZoomDistance;
        private Vector3 _collisionCurrentOffset;
        private float _collisionDefaultOffset;
        private float _collisionRequiredOffset;
        private RaycastHit _collisionHit;

        public void OnLook(InputValue value)
        {
            _lookInput = value.Get<Vector2>();
        }

        public void OnZoom(InputValue value)
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _requiredZoomDistance = Mathf.Clamp(_requiredZoomDistance + value.Get<Vector2>().y * _zoomStep, -_maxZoomDistance, -_minZoomDistance);
        }

        public void OnRightClick(InputValue value)
        {
            _clicked = value.isPressed;
        }

        public void Initialize()
        {
            _xRot = _rotateRoot.eulerAngles.x;
            _currentZoomDistance = _zoomRoot.localPosition.z;
            _requiredZoomDistance = _zoomRoot.localPosition.z;
            _collisionDefaultOffset = _collisionRoot.localPosition.z;
            _player = GameManager.StaticInstance.PlayerManager.Pawn;
        }

        public void ResetComponent()
        {
            _player = null;
        }

        public void OnUpdate()
        {
            if (_player != null)
            {
                transform.position = Vector3.Lerp(transform.position, _player.transform.position, _followSpeed * Time.deltaTime);
            }
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (_clicked)
            {
                LookAround();
            }
            if (_currentZoomDistance != _requiredZoomDistance)
            {
                _currentZoomDistance = Mathf.MoveTowards(_currentZoomDistance, _requiredZoomDistance, _zoomSpeed * Time.deltaTime);
                _zoomRoot.localPosition = new(0f, 0f, _currentZoomDistance);
            }
            HandleCollision();
        }

        private void LookAround()
        {
            if (_lookInput.x != 0f)
            {
                transform.Rotate(Vector3.up * _lookInput.x * _rotateSpeed * Time.deltaTime);
            }
            if (_lookInput.y != 0f)
            {
                _xRot = Mathf.Clamp(_xRot - (_lookInput.y * _rotateSpeed * Time.deltaTime), _minAngle, _maxAngle);
                _rotateRoot.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }
        }

        private void HandleCollision()
        {
            _collisionRequiredOffset = _collisionDefaultOffset;
            Vector3 direction = (_collisionRoot.position - _heightRoot.position).normalized;
            if (Physics.SphereCast(_heightRoot.position, _collisionRadius, direction, out _collisionHit, Mathf.Abs(_collisionRequiredOffset), GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                _collisionRequiredOffset = -(Vector3.Distance(_heightRoot.position, _collisionHit.point) - _collisionRadius);
            }
            if (Mathf.Abs(_collisionRequiredOffset) < _collisionRadius)
            {
                _collisionRequiredOffset = -_collisionRadius;
            }
            _collisionCurrentOffset.z = Mathf.Lerp(_collisionRoot.localPosition.z, _collisionRequiredOffset, _collisionAvoidanceSpeed * Time.deltaTime);
            _collisionRoot.localPosition = _collisionCurrentOffset;
        }
    }
}