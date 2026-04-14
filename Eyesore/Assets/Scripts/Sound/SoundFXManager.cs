using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance {get; private set;}
    [SerializeField] private GameObject _soundFXObject;
    [SerializeField] private int _poolSize;
    private GenericPool<AudioSource> _audioPool;
    private Queue<AudioSource> _audioQueue = new();

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioPool = PoolManager.Instance.GetPool<AudioSource>(_soundFXObject,_poolSize);
        if(_audioPool == null)
        {
            Debug.LogError("Failed to load audio");
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = _audioPool.Get();
        _audioQueue.Enqueue(audioSource);

        audioSource.gameObject.transform.position = spawnTransform.position;

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        StartCoroutine(RemoveSound(clipLength));
    }

    public void PlaySoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        int randomChoice = UnityEngine.Random.Range(0,audioClip.Length);
        PlaySoundFXClip(audioClip[randomChoice],spawnTransform,volume);
    }
    private IEnumerator RemoveSound(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        _audioPool.Return(_audioQueue.Dequeue());
    }
}
