using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-RandomWalk", menuName = "Enemy Logic/Logic Idle/Idle Walk")]
public class EnemyIdleRandomWalk : EnemyIdleSOBase
{
    [SerializeField] private float _movementRange = 5f;
    private float _waitTime = 2f;
    private float _currentTime = 0f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _currentTime = _waitTime;
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        _currentTime += Time.deltaTime;
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        
        if(!_enemy.GetNavPath())
        {
            if(_currentTime >= _waitTime)
            {
                if(_enemy.MoveEnemy(GetRandomPointInCircle()))
                {
                    _currentTime = 0f;
                }
            }
              
        }
    }
    private Vector3 GetRandomPointInCircle()
    {
        Vector3 result = _enemyTransform.position + (Vector3)Random.insideUnitCircle * _movementRange;
        result.z = 0f;
        return result;
    }
}
