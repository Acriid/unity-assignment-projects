using UnityEngine;

public class GoalComplete : MonoBehaviour
{
    public GameObject EnableObject;
    public GameObject DisableObject;
    void OnEnable()
    {
        GoalManager.Instance.OnAllGoalsCompleted += OnGoalComplete;
    }
    void OnDisable()
    {
        GoalManager.Instance.OnAllGoalsCompleted -= OnGoalComplete;
    }
    private void OnGoalComplete()
    {
        if(EnableObject != null)
        EnableObject.SetActive(true);
        if(DisableObject != null)
        DisableObject.SetActive(false);
    }
}