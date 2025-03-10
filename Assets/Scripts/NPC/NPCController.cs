using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class NPCController : MonoBehaviour
    {
        private PawnController _pawn;
        private NavMeshAgent _agent;
        private ActionBase _currentAction;
        private GoalHolder _currentGoal;
        private List<ActionBase> _actions = new();
        private Dictionary<GoalHolder, int> _goals = new();
        private Queue<ActionBase> _actionQueue;
        private float _remainingDistance;
        private bool _reachedDestination;

        [SerializeField] private float _proccessingPlanDelay = 1f;

        public PawnController Pawn => _pawn;
        public ActionBase CurrentAction => _currentAction;
        public GoalHolder CurrentGoal => _currentGoal;
        public List<ActionBase> Actions => _actions;
        public Dictionary<GoalHolder, int> Goals => _goals;
        public float RemainingDistance => _remainingDistance;
        public bool ReachedDestination => _reachedDestination;

        public void Initialize(PawnData pawnData, NPCData npcData)
        {
            InitializePawn(pawnData);
            LoadData(npcData);
        }

        public void InitializePawn(PawnData data)
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            _pawn.Create(data);
        }

        public void LoadData(NPCData data)
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.height = _pawn.Animator.Height;
            _agent.radius = _pawn.Animator.Radius;
            _agent.updateRotation = false;
            ActionBase[] actions = GetComponentsInChildren<ActionBase>();
            foreach (ActionBase action in actions)
            {
                _actions.Add(action);
                action.Initialize();
            }
            foreach (GoalCreator creator in GameManager.StaticInstance.ConfigsManager.GetGoalHolder(data.GoalHolder).GoalsToAdd)
            {
                _goals.Add(new(creator.Config), creator.Priority);
            }
            var sortedGoals = from entry in _goals orderby entry.Value descending select entry;
            _goals = new(sortedGoals);
            StartCoroutine(ProccessPlan());
        }

        public void ResetComponent()
        {
            _pawn.ResetComponents();
        }

        public void OnUpdate()
        {
            _pawn.Input.MoveDirection = _agent.desiredVelocity;
            _pawn.OnUpdate();
            transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
        }

        private IEnumerator ProccessPlan()
        {
            WaitForSeconds delay = new(_proccessingPlanDelay);
            while (true)
            {
                while (_currentAction != null)
                {
                    if (_currentAction.CanAbort())
                    {
                        _currentAction.OnAbort();
                        ResetPlan();
                    }
                    else if (_currentAction.CanComplete())
                    {
                        _currentAction.OnComplete();
                        _currentAction = null;
                    }
                    else
                    {
                        _currentAction.OnUpdate(_proccessingPlanDelay);
                    }
                    yield return delay;
                }
                if (_actionQueue != null)
                {
                    if (_actionQueue.Count > 0)
                    {
                        _currentAction = _actionQueue.Dequeue();
                        if (_currentAction.CanStart())
                        {
                            _currentAction.OnStart();
                        }
                        else
                        {
                            ResetPlan();
                        }
                    }
                    else if (_currentGoal != null)
                    {
                        if (!_currentGoal.Config.Repeatable)
                        {
                            _goals.Remove(_currentGoal);
                        }
                        ResetPlan();
                    }
                    yield return delay;
                }
                while (_actionQueue == null)
                {
                    foreach (KeyValuePair<GoalHolder, int> goal in _goals)
                    {
                        _actionQueue = TaskManager.GetPlan(_actions, _pawn.StateHolder, goal.Key.RequiredStates);
                        if (_actionQueue != null)
                        {
                            _currentGoal = goal.Key;
                            string planText = $"Plan for [{_currentGoal.Config.DisplayName}] is:";
                            foreach (ActionBase a in _actionQueue)
                            {
                                planText += $" {a.Config.DisplayName}, ";
                            }
                            planText += "END";
                            _currentAction = _actionQueue.Dequeue();
                            if (_currentAction.CanStart())
                            {
                                Debug.Log(planText);
                                _currentAction.OnStart();
                                break;
                            }
                            else
                            {
                                ResetPlan();
                            }
                        }
                        else
                        {
                            Debug.Log($"No plan for {goal.Key.Config.DisplayName}");
                            ResetPlan();
                        }
                        yield return delay;
                    }
                }
                yield return null;
            }
        }

        private void ResetPlan()
        {
            _currentAction = null;
            _currentGoal = null;
            _actionQueue = null;
        }

        public void SetDestination(Vector3 position)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                StopMovement();
                return;
            }
            for (int i = 1; i < 5; i++)
            {
                if (NavMesh.SamplePosition(position, out NavMeshHit hit, i * 5f, NavMesh.AllAreas))
                {
                    _remainingDistance = Vector3.Distance(transform.position, _agent.destination);
                    _agent.SetDestination(hit.position);
                    _reachedDestination = false;
                    break;
                }
            }
        }

        public void SetDestinationAroundSelf(float minRange, float maxRange)
        {
            SetDestinationInRange(transform.position, minRange, maxRange);
        }

        public void SetDestinationAroundSelf(float radius)
        {
            SetDestinationInRange(transform.position, radius);
        }

        public void SetDestinationInRange(Vector3 position, float minRange, float maxRange)
        {
            if (Random.value > 0.5f)
            {
                position.x += Random.Range(minRange, maxRange);
            }
            else
            {
                position.x -= Random.Range(minRange, maxRange);
            }
            if (Random.value > 0.5f)
            {
                position.z += Random.Range(minRange, maxRange);
            }
            else
            {
                position.z -= Random.Range(minRange, maxRange);
            }
            SetDestination(position);
        }

        public void SetDestinationInRange(Vector3 position, float radius)
        {
            radius /= 2f;
            position += Vector3.right * Random.Range(-radius, radius);
            position += Vector3.forward * Random.Range(-radius, radius);
            SetDestination(position);
        }

        public void StopMovement()
        {
            _agent.ResetPath();
            _reachedDestination = true;
        }
    }
}