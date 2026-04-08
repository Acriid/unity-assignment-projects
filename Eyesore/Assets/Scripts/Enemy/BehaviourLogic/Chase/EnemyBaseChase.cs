using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Base", menuName = "Enemy Logic/Logic Chase/Chase Direct")]
public class EnemyBaseChase : EnemyChaseSOBase
{
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        _enemy.ChasePlayer();
    }
}
