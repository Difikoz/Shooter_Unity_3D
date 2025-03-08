using UnityEngine;

namespace WinterUniverse
{
    public abstract class SwitchableBase : InteractableBase
    {
        [SerializeField] protected bool _canInteract;
        [SerializeField] protected bool _singleUse;
        [SerializeField] protected bool _switched;

        protected bool _used;

        private void Awake()
        {
            if (_switched)
            {
                SwitchOn();
            }
            else
            {
                SwitchOff();
            }
            _used = false;
        }

        public override bool CanInteract(PawnController pawn)
        {
            return _canInteract && !_used;
        }

        public override void Interact(PawnController pawn)
        {
            if (_singleUse)
            {
                _used = true;
            }
            Switch();
        }

        public void Switch()
        {
            if (_switched)
            {
                _switched = false;
                SwitchOff();
            }
            else
            {
                _switched = true;
                SwitchOn();
            }
        }

        public abstract void SwitchOn();
        public abstract void SwitchOff();
    }
}