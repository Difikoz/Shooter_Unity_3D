using System.Collections.Generic;

namespace WinterUniverse
{
    public class TaskNode
    {
        private TaskNode _parent;
        private float _cost;
        private StateHolder _stateHolder;
        //private Dictionary<string, bool> _states;
        private ActionBase _action;

        public TaskNode Parent => _parent;
        public float Cost => _cost;
        public StateHolder StateHolder => _stateHolder;
        //public Dictionary<string, bool> States => _states;
        public ActionBase Action => _action;

        public TaskNode(TaskNode newParent, float newCost, StateHolder summaryStateHolder, ActionBase newAction)
        {
            _parent = newParent;
            _cost = newCost;
            _stateHolder = new();
            foreach (KeyValuePair<string, bool> state in summaryStateHolder.States)
            {
                _stateHolder.SetState(state.Key, state.Value);
            }
            //_states = new Dictionary<string, bool>(allStates);
            _action = newAction;
        }

        public TaskNode(StateHolder worldStateHolder, StateHolder pawnStateHolder)
        {
            _stateHolder = new();
            foreach (KeyValuePair<string, bool> state in worldStateHolder.States)
            {
                _stateHolder.SetState(state.Key, state.Value);
            }
            foreach (KeyValuePair<string, bool> state in pawnStateHolder.States)
            {
                _stateHolder.SetState(state.Key, state.Value);
            }
            //_states = new();
            //foreach (KeyValuePair<string, bool> state in worldStates)
            //{
            //    _states.Add(state.Key, state.Value);
            //}
            //foreach (KeyValuePair<string, bool> state in pawnStates)
            //{
            //    _states.Add(state.Key, state.Value);
            //}
        }
    }
}