using System.Collections;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    private static WaitForSecondsRealtime _waitForSecondsRealtime1 = new(1f);
    public GameObject Enemy;
    public GameObject Player;
    private Enemy _enemyComponent;
    [SerializeField] private float _maxDistance = 30f;
    [SerializeField] private float _currentDistance = 0f;
    [SerializeField] private float _minDistance = 5f;
    [SerializeField] private float _maxAnnoyance = 5f;
    private float _currentAnnoyance = 0f;
    [SerializeField] private float _minAnnoyance = 0.5f;
    private readonly float _annoyanceRateMin = -0.33f;
    private readonly float _annoyanceRateMax = 0.16f;
    private Coroutine _annoyanceChange;
    void Awake()
    {
        _enemyComponent = Enemy.GetComponent<Enemy>();

        StartCoroutine(ChangeAnnoyance());
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
        _currentAnnoyance += increaseValue;
        if(_currentAnnoyance >= _maxAnnoyance)
        {
            //Do something
        }
        else if(_currentAnnoyance <= _minAnnoyance)
        {
            //Do something 2
        }
    }

}
