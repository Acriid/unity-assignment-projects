using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        _enemy.EnemyChaseSOBaseInstance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();

        _enemy.EnemyChaseSOBaseInstance.DoExitLogic();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _enemy.EnemyChaseSOBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _enemy.EnemyChaseSOBaseInstance.DoPhysicsLogic();
    }
}
