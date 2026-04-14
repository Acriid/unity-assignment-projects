using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public event Action OnSoundFinished;
    public PlayerSoundSO PlayerSoundSO;

    [SerializeField] private CircleCollider2D _collider;

    public void MakeSound(Vector2 position)
    {
        this.transform.position = position;

        _collider = _collider != null ? _collider : GetComponent<CircleCollider2D>();

        StartCoroutine(StartSound(PlayerSoundSO.SoundTravelTime,PlayerSoundSO.SoundRadius,_collider,PlayerSoundSO.SoundLingerDuration));

        
    }

    public void StopSound()
    {
        _collider.radius = 0f;
    }

    private IEnumerator StartSound(float totalTime, float soundSize, CircleCollider2D circleCollider,float lingerTime)
    {
        float waitTime = totalTime / 5f;
        float totalTimeDone = 0f;
        float increaseAmount = soundSize/6f;


        while(totalTimeDone <= totalTime)
        {
            yield return new WaitForSeconds(waitTime);

            totalTimeDone += waitTime;

            circleCollider.radius += increaseAmount;
        }

        yield return new WaitForSeconds(lingerTime);
        OnSoundFinished?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            
            if(collision.gameObject.TryGetComponent<Enemy>(out Enemy component))
            {
                component.ForceMoveEnemy(this.transform.position);
                this.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if(collision.gameObject.TryGetComponent<Enemy>(out Enemy component))
            {
                component.ForceMoveEnemy(PlayerSoundSO.SoundPosition);
                this.gameObject.SetActive(false);
            }
        }
    }
}