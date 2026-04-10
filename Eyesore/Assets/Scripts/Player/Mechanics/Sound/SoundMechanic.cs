using UnityEngine;

public class SoundMechanic : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;

    [SerializeField] private float _timeBetweenSounds = 5f;
    [SerializeField] private SprintMechanic _sprintMechanic;
    private bool _sneaking;
    private float _timeTillNextSound = 0f;
    private Vector2 _moveValue;
    void OnEnable()
    {
        _timeTillNextSound = _timeBetweenSounds;
        EnableMove();
    }
    void OnDisable()
    {
        DisableMove();
    }
    void Update()
    {
        MakeSound(_moveValue);
    }
    private void ReadMove(Vector2 moveValue)
    {
        _moveValue = moveValue;
    }
    private void EnableMove()
    {
        _inputReader.OnMove += ReadMove;
    }
    private void DisableMove()
    {
        _inputReader.OnMove -= ReadMove;
    }
    private void MakeSound(Vector2 moveValue)
    {
        if(_sneaking) return;
        if(moveValue != Vector2.zero)
        {
            _timeTillNextSound -= Time.deltaTime;
        }

        if(_timeTillNextSound <= 0f)
        {
           SoundMechanicManager.Instance.MakeSound(this.transform.position);
           _timeTillNextSound = _timeBetweenSounds;
        }
    }
}