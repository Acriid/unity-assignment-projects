using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy _enemy;
    protected Transform _enemyTransform;
    protected GameObject _enemyObject;

    public virtual void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _enemyObject = _enemy.gameObject;
        _enemyTransform = _enemyObject.transform;
    }

    public virtual void DoEnterLogic() {}
    public virtual void DoExitLogic() {}
    public virtual void DoFrameUpdateLogic()
    {
        if(_enemy.GetCanSeePlayer())
        {
            _enemy.StateMachine.ChangeState(_enemy.ChaseState);
        }
    }
    public virtual void DoPhysicsLogic() {}
}
