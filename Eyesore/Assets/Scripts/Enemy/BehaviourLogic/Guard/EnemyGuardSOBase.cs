using System;
using System.Collections;
using UnityEngine;

public class EnemyGuardSOBase : ScriptableObject
{
    protected Enemy _enemy;
    [SerializeField] protected float _guardTime = 0f;
    public event Action EnteredGuard;
    public virtual void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    public virtual void DoEnterLogic()
    {
        EnteredGuard?.Invoke();
        if(!Mathf.Approximately(_guardTime, 0f))
        {
            _enemy.TimeState(_guardTime, _enemy.IdleState);
        }
    }
    public virtual void DoExitLogic() {}
    public virtual void DoFrameUpdateLogic()
    {
        if(_enemy.GetCanSeePlayer())
        {
            _enemy.StateMachine.ChangeState(_enemy.ChaseState);
        }
    }
    public virtual void DoPhysicsLogic() {}

    public virtual void SetGuardPosition(Vector2 guardPosition) {}
}
