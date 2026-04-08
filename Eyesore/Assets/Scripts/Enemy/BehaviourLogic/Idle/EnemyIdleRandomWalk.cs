using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-RandomWalk", menuName = "Enemy Logic/Logic Idle/Idle Walk")]
public class EnemyIdleRandomWalk : EnemyIdleSOBase
{
    [SerializeField] private float _movementRange = 5f;
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        MoveEnemy();
    }
    private void MoveEnemy()
    {

        if(!_enemy.GetNavPath())
            _enemy.MoveEnemy(GetRandomPointInCircle());
    }
    private Vector2 GetRandomPointInCircle()
    {
        return (Vector2)_enemyTransform.position + Random.insideUnitCircle * _movementRange;
    }
}
