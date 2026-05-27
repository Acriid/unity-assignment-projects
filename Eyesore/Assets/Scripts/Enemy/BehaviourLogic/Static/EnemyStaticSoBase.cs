using UnityEngine;

[CreateAssetMenu(fileName = "Static-Enemy", menuName = "Enemy Logic/Logic Static/Static Enemy")]
public class EnemyStaticSoBase : ScriptableObject
{
    protected Enemy _enemy;
    protected Transform _enemyTransform;
    protected GameObject _enemyObject;
    protected bool _shouldGoToMap = false;
    public virtual void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _enemyObject = _enemy.gameObject;
        _enemyTransform = _enemyObject.transform;
    }

    public virtual void DoEnterLogic() {}
    public virtual void DoExitLogic() {}
    public virtual void DoFrameUpdateLogic() {}
    public virtual void DoPhysicsLogic() {}
    public virtual void ResetTime() {}
    public virtual bool GetShouldGoToMap()
    {
        return _shouldGoToMap;
    }
    public virtual void SetShouldGoToMap(bool newValue)
    {
        _shouldGoToMap = newValue;
    }
}
