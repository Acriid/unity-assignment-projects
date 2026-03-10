using System.Collections;
using UnityEngine;

public class TileDamage : MonoBehaviour
{
    private Player _player;
    private Coroutine _coroutine;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;

        _player = collision.GetComponent<Player>();
        _coroutine = StartCoroutine(DamageTicks());

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    private IEnumerator DamageTicks()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(2f);
            _player.DamagePlayer();
        }

    }
}
