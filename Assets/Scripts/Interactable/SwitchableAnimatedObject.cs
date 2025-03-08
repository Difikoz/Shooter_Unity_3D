using UnityEngine;

namespace WinterUniverse
{
    public class SwitchableAnimatedObject : SwitchableBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationSpeed = 1f;

        public override void SwitchOn()
        {
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", true);
        }

        public override void SwitchOff()
        {
            _animator.SetFloat("Speed", _animationSpeed);
            _animator.SetBool("Switched", false);
        }
    }
}