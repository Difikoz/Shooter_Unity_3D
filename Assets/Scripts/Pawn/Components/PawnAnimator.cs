using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _eyesPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;

        public Transform HeadPoint => _headPoint;
        public Transform EyesPoint => _eyesPoint;
        public Transform BodyPoint => _bodyPoint;
        public float Height => _height;
        public float Radius => _radius;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void ResetComponent()
        {

        }

        public void OnUpdate()
        {
            //_animator.SetFloat("Velocity", _pawn.Locomotion.Velocity);
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.StateHolder.CompareStateValue("Is Perfoming Action", isPerfomingAction);
            _animator.CrossFade(name, fadeTime);
        }

        public void ResetState()
        {
            if (_pawn != null && _pawn.StateHolder.HasState("Is Perfoming Action"))
            {
                _pawn.StateHolder.SetState("Is Perfoming Action", false);
            }
        }
    }
}