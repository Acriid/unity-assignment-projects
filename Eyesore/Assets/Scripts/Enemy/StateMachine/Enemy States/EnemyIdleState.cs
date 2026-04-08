using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        _enemy.EnemyIdleSOBaseInstance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();

        _enemy.EnemyIdleSOBaseInstance.DoExitLogic();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _enemy.EnemyIdleSOBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _enemy.EnemyIdleSOBaseInstance.DoPhysicsLogic();
    }
}
