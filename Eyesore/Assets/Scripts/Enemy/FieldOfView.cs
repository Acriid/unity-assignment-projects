using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [Range(0,360)]
    [SerializeField] private float _angle;

    [SerializeField] private GameObject _playerRef;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    public bool CanSeePlayer;

    void Start()
    {
        if(_playerRef == null)
        {
            _playerRef = GameObject.FindGameObjectWithTag("Player");
        }
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
       Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position,_radius,_targetMask);

        if(rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if(Vector2.Angle(transform.up,directionToTarget) < _angle/2f)
            {
                float distanceToTarget = Vector2.Distance(transform.position,target.position);


                if(!Physics2D.Raycast(transform.position,directionToTarget,distanceToTarget,_obstructionMask))
                {
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if(CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }

    public float GetRadius()
    {
        return _radius;
    }

    public float GetAngle()
    {
        return _angle;
    }

    public GameObject GetPlayerReference()
    {
        return _playerRef;
    }
}
