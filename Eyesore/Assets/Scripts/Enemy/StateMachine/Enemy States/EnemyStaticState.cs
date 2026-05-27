using UnityEngine;

public class EnemyStaticState : EnemyState
{
    public EnemyStaticState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        _enemy.EnemyStaticSoBaseInstance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();

        _enemy.EnemyStaticSoBaseInstance.DoExitLogic();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _enemy.EnemyStaticSoBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _enemy.EnemyStaticSoBaseInstance.DoPhysicsLogic();
    }
}
