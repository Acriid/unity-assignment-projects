using UnityEngine;

public class SoundMechanic : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;

    [SerializeField] private float _timeBetweenSounds = 5f;
    [SerializeField] private AudioClip[] _footStepSounds;
    private float _defualtTime;
    private float _timeTillNextSound = 0f;
    private Vector2 _moveValue;
    private bool _crouching = false;
    void OnEnable()
    {
        _timeTillNextSound = _timeBetweenSounds;
        _defualtTime = _timeBetweenSounds;
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
        _inputReader.OnSprint += OnSprint;
        _inputReader.OnCrouch += OnCrouch;
    }
    private void DisableMove()
    {
        _inputReader.OnMove -= ReadMove;
        _inputReader.OnSprint -= OnSprint;
        _inputReader.OnCrouch -= OnCrouch;
    }
    private void MakeSound(Vector2 moveValue)
    {
        if(_crouching) return;
        if(moveValue != Vector2.zero)
        {
            _timeTillNextSound -= Time.deltaTime;
        }

        if(_timeTillNextSound <= 0f)
        {
            if(_footStepSounds.Length != 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_footStepSounds,this.transform,1f); 
            }
            SoundMechanicManager.Instance.MakeSound(this.transform.position);
           _timeTillNextSound = _timeBetweenSounds;
        }
    }
    private void OnSprint(bool newValue)
    {
        _timeBetweenSounds = newValue? 0.5f : _defualtTime;
        _timeTillNextSound = _timeBetweenSounds;
    }
    private void OnCrouch(bool newValue)
    {
        _crouching = newValue;
    }
}