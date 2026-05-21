using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReaderSO;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private GameObject _mapCamera;
    [SerializeField] private AudioClip _pingClip;
    [SerializeField] private EnemyDirector _enemyDirector;
    [SerializeField] private float _pingCooldown = 1f;
    [SerializeField] private Transform _playerTransform;
    private Coroutine _pingRoutine;

    void OnEnable()
    {
        _inputReaderSO.OnMapAction += ToggleMap;
        _inputReaderSO.EnableMapAction();
        _pingCooldown = _pingClip.length + 2f;
    }
    void OnDisable()
    {
        _inputReaderSO.OnMapAction -= ToggleMap;
        _inputReaderSO.DisableMapAction();
    }
    private void ToggleMap()
    {
        bool mapActive = !_mapCamera.activeSelf;

        if(mapActive)
        {
            _mapCamera.SetActive(mapActive);
            _pingRoutine = StartCoroutine(SendPing());

        }
        else
        {
            _mapCamera.SetActive(mapActive);
            _renderTexture.Release();
            if(_pingRoutine != null)
            {
                StopCoroutine(_pingRoutine);
                _pingRoutine = null;
            }
        }
    }

    private IEnumerator SendPing()
    {
        while(true)
        {
            SoundFXManager.Instance.PlaySoundFXClip(_pingClip,this.transform,1f);
            _enemyDirector.ForceEnemyMove(_playerTransform.position);
            yield return new WaitForSeconds(_pingCooldown);
        }
    }
}
