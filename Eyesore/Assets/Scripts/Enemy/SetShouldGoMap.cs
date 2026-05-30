using UnityEngine;

public class SetShouldGoMap : MonoBehaviour
{
    public EnemyDirector EnemyDirector;
    void Start()
    {
        EnemyDirector.SetShouldGoToMap(true);
    }
}
