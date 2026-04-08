using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navAgent;
    [SerializeField] protected Rigidbody2D _enemyRigidBody;

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
    void Awake()
    {
        _navAgent = _navAgent != null ? _navAgent : GetComponent<NavMeshAgent>();
        _enemyRigidBody = _enemyRigidBody != null ? _enemyRigidBody : GetComponent<Rigidbody2D>();


        InitializeStateMachine();
    }

    void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }





    public void MoveEnemy(Vector2 destination)
    {
        if(_navAgent.hasPath) return;
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
    }
}
