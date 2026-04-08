using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
    private Enemy _enemyComponent;
    void Awake()
    {
        _enemyComponent = Enemy.GetComponent<Enemy>();

        _enemyComponent.MoveEnemy(new(0f,0f));
    }
}
