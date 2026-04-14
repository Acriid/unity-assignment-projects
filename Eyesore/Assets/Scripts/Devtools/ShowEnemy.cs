using Unity.Cinemachine;
using UnityEngine;

public class ShowEnemy : MonoBehaviour
{
    public InputReaderSO InputReaderSO;
    public GameObject EnemyObject;
    public GameObject PlayerObject;
    public CinemachineCamera CinemachineCamera;
    private bool _following = false;
    void OnEnable()
    {
        InputReaderSO.OnShowEnemyAction += OnShowEnemy;
        InputReaderSO.EnableDevtools();
    }
    void OnDisable()
    {
        InputReaderSO.OnShowEnemyAction -= OnShowEnemy;
        InputReaderSO.DisableDevtools();
    }
    private void OnShowEnemy()
    {
        _following = !_following;
        Transform followTarget = _following? PlayerObject.transform : EnemyObject.transform;
        CinemachineCamera.Follow = followTarget;
    }
}
