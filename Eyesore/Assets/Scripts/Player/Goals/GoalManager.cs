using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public static GoalManager Instance {get; private set;}
    [SerializeField] private List<GoalObject> _goalObjects;
    public event Action OnAllGoalsCompleted;
    public event Action<float> OnProgressChanged;

    private int _completedCount = 0;
    private bool _allCompleted = false;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        foreach(GoalObject goal in _goalObjects)
        {
            goal.GoalComplete += HandleGoalCompleted;
        }
    }

    void OnDisable()
    {
        foreach(GoalObject goal in _goalObjects)
        {
            goal.GoalComplete -= HandleGoalCompleted;
        } 
    }

    private void HandleGoalCompleted(GoalObject goalObject)
    {
        _completedCount ++;

        float progress = (float)_completedCount/ _goalObjects.Count;
        OnProgressChanged?.Invoke(progress);

        if(_completedCount >= _goalObjects.Count && !_allCompleted)
        {
            _allCompleted = true;
            OnAllGoalsCompleted?.Invoke();
            Debug.Log("All goals done!");
        }
    }
}
