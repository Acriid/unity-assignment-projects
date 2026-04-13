using UnityEngine;

[CreateAssetMenu(fileName = "Goal", menuName= "Player/Player Goal")]
public class GoalSO : ScriptableObject
{
    public bool GoalComplete;
    public Vector2 GoalPosition;
    public  void SetGoalPosition(Vector2 newPosition)
    {
        GoalPosition = newPosition;
    }
}
