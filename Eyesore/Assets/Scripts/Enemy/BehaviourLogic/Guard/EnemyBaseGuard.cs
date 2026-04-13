using UnityEngine;

[CreateAssetMenu(fileName = "Guard-Area", menuName = "Enemy Logic/Logic Guard/Guard Area")]
public class EnemyBaseGuard : EnemyGuardSOBase
{
    private Vector2 _guardPosition;
    
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
    //
    private Vector3 GetRandomPointInCircle()
    {
        if(_guardPosition == null)
        {
            Debug.Log("Going to 0,0");
            return _enemy.transform.position + (Vector3)Random.insideUnitCircle * _movementRange;
        }
        Vector3 result = _guardPosition + Random.insideUnitCircle * _movementRange;
        result.z = 0f;
        return result;
    }

    public override void SetGuardPosition(Vector2 guardPosition)
    {
        _guardPosition = guardPosition;
    }
}
