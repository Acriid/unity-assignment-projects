using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_5 = new(0.2f);
    [SerializeField] protected NavMeshAgent _navAgent;
    [SerializeField] protected Rigidbody2D _enemyRigidBody;
    [SerializeField] protected List<FieldOfView> _fovs;
    [SerializeField] protected GameObject _playerRef;



    #region StateMachine Variables
    [SerializeField] protected EnemyIdleSOBase _enemyIdleSOBase;
    [SerializeField] protected EnemyGuardSOBase _enemyGuardSOBase;
    [SerializeField] protected EnemyChaseSOBase _enemyChaseSOBase;
    

    public EnemyIdleSOBase EnemyIdleSOBaseInstance {get; set;}
    public EnemyGuardSOBase EnemyGuardSOBaseInstance {get; set;}
    public EnemyChaseSOBase EnemyChaseSOBaseInstance {get; set;}

    #endregion

    #region  ScriptableObject StateMachine
    public EnemyStateMachine StateMachine {get; set;}
    public EnemyIdleState IdleState {get; set;}
    public EnemyChaseState ChaseState {get; set;}
    public EnemyGuardState GuardState {get; set;}
    #endregion

    private bool _showCooldown;
    [SerializeField] private GameObject _showText;

    void Awake()
    {
        _navAgent = _navAgent != null ? _navAgent : GetComponent<NavMeshAgent>();
        _enemyRigidBody = _enemyRigidBody != null ? _enemyRigidBody : GetComponent<Rigidbody2D>();


        InitializeStateMachine();


    }

    
    public bool GetCanSeePlayer()
    {
        bool result = false;
        foreach (FieldOfView fov in _fovs)
        {
            if(fov.CanSeePlayer)
                result = true;
        }
        return result;
    }


    void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }





    public bool MoveEnemy(Vector2 destination)
    {

        NavMeshPath path = new();
        if(!_navAgent.CalculatePath(destination,path)) return false;
        if(path.status != NavMeshPathStatus.PathComplete) return false;

        _navAgent.SetDestination(destination); 
        return true;    

        
    }
    public void ForceMoveEnemy(Vector2 destination)
    {
        _navAgent.ResetPath();
        _navAgent.SetDestination(destination);
    }

    public bool GetNavPath()
    {
        return _navAgent.hasPath;
    }

    private void InitializeStateMachine()
    {
        StateMachine = new();

        IdleState = new(this,StateMachine);
        ChaseState = new(this,StateMachine);
        GuardState = new(this,StateMachine);

        EnemyIdleSOBaseInstance  = Instantiate(_enemyIdleSOBase);
        EnemyGuardSOBaseInstance = Instantiate(_enemyGuardSOBase);
        EnemyChaseSOBaseInstance = Instantiate(_enemyChaseSOBase);


        EnemyIdleSOBaseInstance.Initialize(this);
        EnemyGuardSOBaseInstance.Initialize(this);
        EnemyChaseSOBaseInstance.Initialize(this);


        StateMachine.Initialize(IdleState);
    }

    public void ChasePlayer()
    {

        _navAgent.SetDestination(_playerRef.transform.position);  

    }

    public bool ValidPath(Vector2 destination)
    {
        NavMeshPath path = new();
        return _navAgent.CalculatePath(destination,path);
    }


    public void ShowRun()
    {
        if(!_showCooldown)
        {
            _showCooldown = true;
            _showText.SetActive(true);
            StartCoroutine(RunCoolDown());
        }
    }

    private IEnumerator RunCoolDown()
    {
        yield return new WaitForSeconds(3f);
        _showCooldown = false;
        _showText.SetActive(false);
    }

}
