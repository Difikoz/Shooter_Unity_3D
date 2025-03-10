using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform _heightRoot;
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private Transform _rotateRoot;
        [SerializeField] private float _rotateSpeed = 45f;
        [SerializeField] private float _minAngle = 45f;
        [SerializeField] private float _maxAngle = 45f;
        [SerializeField] private Transform _collisionRoot;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private float _collisionAvoidanceSpeed = 8f;

        private PlayerInputActions _inputActions;
        private PawnController _player;
        private Camera _camera;
        private Vector2 _lookInput;
        private float _xRot;
        private Vector3 _collisionCurrentOffset;
        private float _collisionDefaultOffset;
        private float _collisionRequiredOffset;
        private RaycastHit _collisionHit;
        private RaycastHit _cameraHit;

        public Camera Camera => _camera;

        public void Initialize()
        {
            _inputActions = new();
            _inputActions.Enable();
            _player = GameManager.StaticInstance.PlayerManager.Pawn;
            _camera = GetComponentInChildren<Camera>();
            _xRot = _rotateRoot.eulerAngles.x;
            _collisionDefaultOffset = _collisionRoot.localPosition.z;
        }

        public void ResetComponent()
        {
            _inputActions.Disable();
            _player = null;
        }

        public void OnUpdate()
        {
            if (_player != null)
            {
                transform.position = Vector3.Lerp(transform.position, _player.transform.position, _followSpeed * Time.deltaTime);
                _player.Input.LookAngle = _xRot;
            }
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _lookInput = _inputActions.Camera.Look.ReadValue<Vector2>();
            LookAround();
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
                _xRot = Mathf.Clamp(_xRot - (_lookInput.y * _rotateSpeed * Time.deltaTime), -_minAngle, _maxAngle);
                _rotateRoot.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }
        }

        private void HandleCollision()
        {
            _collisionRequiredOffset = _collisionDefaultOffset;
            Vector3 direction = (_collisionRoot.position - _rotateRoot.position).normalized;
            if (Physics.SphereCast(_rotateRoot.position, _collisionRadius, direction, out _collisionHit, Mathf.Abs(_collisionRequiredOffset), GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                _collisionRequiredOffset = -(Vector3.Distance(_rotateRoot.position, _collisionHit.point) - _collisionRadius);
            }
            if (Mathf.Abs(_collisionRequiredOffset) < _collisionRadius)
            {
                _collisionRequiredOffset = -_collisionRadius;
            }
            _collisionCurrentOffset.z = Mathf.Lerp(_collisionRoot.localPosition.z, _collisionRequiredOffset, _collisionAvoidanceSpeed * Time.deltaTime);
            _collisionRoot.localPosition = _collisionCurrentOffset;
        }

        public Vector3 GetHitPoint()
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _cameraHit, 1000f, GameManager.StaticInstance.LayerManager.DetectableMask))
            {
                return _cameraHit.point;
            }
            else
            {
                return _camera.transform.position + _camera.transform.forward * 1000f;
            }
        }
    }
}