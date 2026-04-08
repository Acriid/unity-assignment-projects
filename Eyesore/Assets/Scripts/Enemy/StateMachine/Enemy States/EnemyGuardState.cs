using UnityEngine;

public class EnemyGuardState : EnemyState
{
    public EnemyGuardState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        _enemy.EnemyGuardSOBaseInstance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();

        _enemy.EnemyGuardSOBaseInstance.DoExitLogic();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _enemy.EnemyGuardSOBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _enemy.EnemyGuardSOBaseInstance.DoPhysicsLogic();
    }
}
