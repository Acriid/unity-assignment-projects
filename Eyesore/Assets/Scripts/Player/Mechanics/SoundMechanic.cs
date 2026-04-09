using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundMechanic : MonoBehaviour
{
    public SoundMechanic Instance {get; private set;}
    [SerializeField] private int _poolSize;
    [SerializeField] private GameObject _soundPrefab;

    private GenericPool<SoundObject> _soundPool;
    private Queue<SoundObject> _soundQueue = new();
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _soundPool = PoolManager.Instance.GetPool<SoundObject>(_soundPrefab,_poolSize);
        if(_soundPool == null)
        {
            Debug.LogError("Failed to load soundPool");
        }

    }
    void OnDisable()
    {
        _soundPool.ReturnAll();
    }

    public void MakeSound()
    {
        SoundObject soundObject = _soundPool.Get();

        soundObject.MakeSound();

        soundObject.OnSoundFinished += RemoveSound;


        _soundQueue.Enqueue(soundObject);


    }

    public void RemoveSound()
    {
        SoundObject soundObject = _soundQueue.Dequeue();

        soundObject.OnSoundFinished -= RemoveSound;



        _soundPool.Return(soundObject);
    }
}
