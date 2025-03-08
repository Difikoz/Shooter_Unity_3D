using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Action", menuName = "Winter Universe/ALIFE/New Action")]
    public class ActionConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private int _cost = 1;
        [SerializeField] private bool _playAnimationOnStart;
        [SerializeField] private string _animationOnStart = "Eat";
        [SerializeField] private bool _completeOnAnimationEnd;
        [SerializeField] private bool _completeOnReachedDestination;
        [SerializeField] private bool _completeOnTimerEnd;
        [SerializeField] private float _timerDuration = 2f;
        [SerializeField] private bool _stopMovementOnAbort;
        [SerializeField] private bool _stopMovementOnComplete;
        [SerializeField] private List<StateCreator> _results = new();
        [SerializeField] private List<StateCreator> _conditionsToStart = new();
        [SerializeField] private List<StateCreator> _conditionsToAbort = new();
        [SerializeField] private List<StateCreator> _conditionsToComplete = new();
        [SerializeField] private List<StateCreator> _effectsOnStart = new();
        [SerializeField] private List<StateCreator> _effectsOnAbort = new();
        [SerializeField] private List<StateCreator> _effectsOnComplete = new();

        public string DisplayName => _displayName;
        public int Cost => _cost;
        public bool PlayAnimationOnStart => _playAnimationOnStart;
        public string AnimationOnStart => _animationOnStart;
        public bool CompleteOnAnimationEnd => _completeOnAnimationEnd;
        public bool CompleteOnReachedDestination => _completeOnReachedDestination;
        public bool CompleteOnTimerEnd => _completeOnTimerEnd;
        public float TimerDuration => _timerDuration;
        public bool StopMovementOnAbort => _stopMovementOnAbort;
        public bool StopMovementOnComplete => _stopMovementOnComplete;
        public List<StateCreator> Results => _results;
        public List<StateCreator> ConditionsToStart => _conditionsToStart;
        public List<StateCreator> ConditionsToAbort => _conditionsToAbort;
        public List<StateCreator> ConditionsToComplete => _conditionsToComplete;
        public List<StateCreator> EffectsOnStart => _effectsOnStart;
        public List<StateCreator> EffectsOnAbort => _effectsOnAbort;
        public List<StateCreator> EffectsOnComplete => _effectsOnComplete;
    }
}