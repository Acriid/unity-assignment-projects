using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDirector : MonoBehaviour
{
    private static WaitForSecondsRealtime _waitForSecondsRealtime1 = new(1f);
    public GameObject Enemy;
    public GameObject Player;
    private Enemy _enemyComponent;
    [Header("Annoyance Distance")]
    [SerializeField] private float _maxDistance = 30f;
    [SerializeField] private float _minDistance = 5f;
    [Header("Annoyance Meter")]
    [SerializeField] private float _maxAnnoyance = 5f;
    [SerializeField] private float _currentAnnoyance = 2.5f;
    [SerializeField] private float _minAnnoyance = 0.5f;
    [SerializeField] private List<GoalObject> _goalList;
    private readonly float _annoyanceRateMin = -0.33f;
    private readonly float _annoyanceRateMax = 0.16f;
    private bool _alreadySent = false;

    void Awake()
    {
        _enemyComponent = Enemy.GetComponent<Enemy>();

        StartCoroutine(ChangeAnnoyance());

        if(!Enemy.activeSelf) return;
        _enemyComponent.EnemyGuardSOBaseInstance.EnteredGuard += SetGuardDestination;
    }
    void OnDisable()
    {
        if(Enemy != null && !Enemy.activeSelf) return;
        if(_enemyComponent == null) return;
        _enemyComponent.EnemyGuardSOBaseInstance.EnteredGuard -= SetGuardDestination;
    }
    private IEnumerator ChangeAnnoyance()
    {
        //Range -0.33 to 1.66 when chasing its instantly full.
        //When the meter is full it will take 30 seconds for the annoyance to empty thus it decreases at a rate of 1/3 per second
        //When the meter is empty it will take 1 second for the meter to fill if the enemy is chasing the player. If the player hides from the enemy it will take 20 seconds for the annoyance to cause the
        //enemy to leave the player
        //When the meter is empty it will take 30 seconds for the annoyance meter to fill when the enemy is almost right next to the player. 5/30 increase.
        
        while (true)
        {
            IncreaseAnnoyance(GetDistanceAnnoyance());
            yield return _waitForSecondsRealtime1;
        }
    }
    private float GetDistanceAnnoyance()
    {
        if(Player == null) return 0f;

        float distanceBetween = Vector2.Distance(Enemy.transform.position,Player.transform.position);

        float t = Mathf.InverseLerp(_minDistance,_maxDistance,distanceBetween);

        float annoyanceValue = Mathf.Lerp(_annoyanceRateMax,_annoyanceRateMin,t);

        return annoyanceValue;
    }
    private void IncreaseAnnoyance(float increaseValue)
    {
        if(!Enemy.activeSelf) return;
        if(_enemyComponent.StateMachine.CurrentEnemyState == _enemyComponent.GuardState) return;
        _currentAnnoyance += increaseValue;
        _currentAnnoyance = Mathf.Clamp(_currentAnnoyance,0.3f,5.2f);
        if(_currentAnnoyance >= _maxAnnoyance && !_alreadySent)
        {
            Debug.Log("sending enemy away");
            _currentAnnoyance -= 0.5f;
            _enemyComponent.ForceMoveEnemy(GetPointNearOrFarPlayer(50f));
        }
        else if(_currentAnnoyance <= _minAnnoyance && !_alreadySent)
        {
            Debug.Log("sending enemy closer");
            _currentAnnoyance += 0.5f;
            _enemyComponent.ForceMoveEnemy(GetPointNearOrFarPlayer(25f));
        }
    }

    private Vector2 GetPointNearOrFarPlayer(float distance)
    {
        int Testing = 0;
        while(Testing < 500)
        {
            Vector2 randomPosition = (Vector2)Player.transform.position +  UnityEngine.Random.insideUnitCircle * distance;


            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                _alreadySent = true;
                StartCoroutine(ResetSend());
                return hit.position;
            }

            Testing++;
        }
        
        _alreadySent = true;
        StartCoroutine(ResetSend());
        NavMesh.SamplePosition(Player.transform.position, out NavMeshHit fallback, 10f, NavMesh.AllAreas);
        return fallback.position;

    }

    private IEnumerator ResetSend()
    {
        yield return new WaitForSeconds(10f);
        _alreadySent = false;
    }

    private void SetGuardDestination()
    {
        float minDistance = Mathf.Infinity;
        GoalObject objectToGuard = null;
        foreach(GoalObject guardObject in _goalList)
        {
            if(!guardObject.GoalSO.GoalComplete)
            {
                float distance = Vector2.Distance(guardObject.GoalSO.GoalPosition, Enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    objectToGuard = guardObject;
                }
            }
        }
        if(objectToGuard == null) 
        {   
            Debug.Log("No goal Found");
            _enemyComponent.EnemyGuardSOBaseInstance.SetGuardPosition(Enemy.transform.position);
            return;
        }
        _enemyComponent.EnemyGuardSOBaseInstance.SetGuardPosition(objectToGuard.GoalSO.GoalPosition);
    }


    public EnemyState GetCurrentEnemyState()
    {
        return _enemyComponent.StateMachine.CurrentEnemyState;
    }
    public float GetCurrentEnemyAnnoyance()
    {
        return _currentAnnoyance;
    }
}
