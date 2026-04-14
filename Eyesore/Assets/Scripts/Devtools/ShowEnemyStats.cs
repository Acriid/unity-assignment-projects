using UnityEngine;

public class ShowEnemyStats : MonoBehaviour
{
    public InputReaderSO InputReaderSO;
    public GameObject StatsCanvas;

    void OnEnable()
    {
        InputReaderSO.OnShowEnemyStats += OnShowStats;
    }

    void OnDisable()
    {
        InputReaderSO.OnShowEnemyStats -= OnShowStats;
    }
    private void OnShowStats()
    {
        StatsCanvas.SetActive(!StatsCanvas.activeSelf);
    }
}