using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
{
    protected Enemy _enemy;

    public virtual void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    public virtual void DoEnterLogic() {}
    public virtual void DoExitLogic() {}
    public virtual void DoFrameUpdateLogic() {}
    public virtual void DoPhysicsLogic() {}
}