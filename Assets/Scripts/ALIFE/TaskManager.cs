using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public static class TaskManager
    {
        public static Queue<ActionBase> GetPlan(List<ActionBase> allActions, StateHolder pawnStateHolder, StateHolder goalConditions)
        {
            if (GoalAchieved(goalConditions, GameManager.StaticInstance.WorldManager.StateHolder, pawnStateHolder))// already compared pawn and world states to goal conditions
            {
                return null;
            }
            List<ActionBase> usableActions = new();
            foreach (ActionBase action in allActions)
            {
                usableActions.Add(action);
            }
            List<TaskNode> leaves = new();
            TaskNode startNode = new(GameManager.StaticInstance.WorldManager.StateHolder, pawnStateHolder);
            bool success = BuildGraph(startNode, leaves, usableActions, goalConditions);
            if (!success)
            {
                return null;
            }
            TaskNode cheapestNode = null;
            foreach (TaskNode leaf in leaves)
            {
                if (cheapestNode == null)
                {
                    cheapestNode = leaf;
                }
                else if (leaf.Cost < cheapestNode.Cost)
                {
                    cheapestNode = leaf;
                }
            }
            List<ActionBase> result = new();
            TaskNode n = cheapestNode;
            while (n != null)
            {
                if (n.Action != null)
                {
                    result.Insert(0, n.Action);
                }
                n = n.Parent;
            }
            Queue<ActionBase> queue = new();
            foreach (ActionBase a in result)
            {
                queue.Enqueue(a);
            }
            return queue;
        }

        private static bool BuildGraph(TaskNode parent, List<TaskNode> leaves, List<ActionBase> usableActions, StateHolder goalConditions)
        {
            bool foundPath = false;
            foreach (ActionBase action in usableActions)
            {
                if (action.IsAchievable(parent.StateHolder.States))
                {
                    StateHolder currentStateHolder = new();
                    foreach (KeyValuePair<string, bool> state in parent.StateHolder.States)
                    {
                        currentStateHolder.SetState(state.Key, state.Value);
                    }
                    foreach (KeyValuePair<string, bool> effect in action.Effects)
                    {
                        currentStateHolder.SetState(effect.Key, effect.Value);
                    }
                    TaskNode node = new(parent, parent.Cost + action.Config.Cost, currentStateHolder, action);
                    if (GoalAchieved(goalConditions, currentStateHolder))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<ActionBase> subset = ActionSubset(usableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goalConditions);
                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }
            return foundPath;
        }

        private static List<ActionBase> ActionSubset(List<ActionBase> usableActions, ActionBase currentAction)
        {
            List<ActionBase> subset = new();
            foreach (ActionBase action in usableActions)
            {
                if (action.gameObject != currentAction.gameObject)// rework?
                {
                    subset.Add(action);
                }
            }
            return subset;
        }

        private static bool GoalAchieved(StateHolder goalConditions, StateHolder currentStates)
        {
            foreach (KeyValuePair<string, bool> condition in goalConditions.States)
            {
                if (!currentStates.CompareStateValue(condition.Key, condition.Value))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool GoalAchieved(StateHolder goalConditions, StateHolder worldStates, StateHolder pawnStates)
        {
            foreach (KeyValuePair<string, bool> condition in goalConditions.States)
            {
                if (pawnStates.HasState(condition.Key) && !pawnStates.CompareStateValue(condition.Key, condition.Value))
                {
                    return false;
                }
                if (worldStates.HasState(condition.Key) && !worldStates.CompareStateValue(condition.Key, condition.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}