using TMPro;
using UnityEngine;

public class ShowDirectorStats : MonoBehaviour
{
    public EnemyDirector EnemyDirector;
    public TMP_Text CurrentEnemyState;
    public TMP_Text CurrentEnemyAnnoyance;
    void Update()
    {
        CurrentEnemyState.text = "Current Enemy State:" + EnemyDirector.GetCurrentEnemyState().ToString();
        CurrentEnemyAnnoyance.text = "Current Enemy Annoyance: " + EnemyDirector.GetCurrentEnemyAnnoyance().ToString();
    }
}
