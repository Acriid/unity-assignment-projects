using System.Collections;
using UnityEngine;

public class TimedTrigger : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private float _triggerTime;
    private Coroutine _timeRoutine;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_tag))
        {
            Reset();
            _timeRoutine ??= StartCoroutine(TriggerTimer(_triggerTime));
        }
    }

    protected virtual IEnumerator TriggerTimer(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        TimedTriggered();
    }
    protected virtual void TimedTriggered()
    {
        _timeRoutine = null;
    }
    protected virtual void Reset()
    {
        
    }
    public void StopTimer()
    {
        if(_timeRoutine != null)
        {
            StopCoroutine(_timeRoutine);
        }
    }
}
