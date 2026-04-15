using UnityEngine;
using System;

public class GoalObject : ActivationTrigger
{
    public GoalSO GoalSO;
    public event Action<GoalObject> GoalComplete;

    protected void SendGoalComplete(GoalObject goalObject)
    {
        GoalComplete?.Invoke(goalObject);
    }
}
