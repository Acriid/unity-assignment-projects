using System.Collections;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SprintMechanic : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds2 = new(2f);



    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Player _player;
    [Range(0,1)]
    [SerializeField] private float _speedIncrease; //percentage
    [SerializeField] private float _maxSprintTime = 3f;
    private float _sprintTimeLeft;
    private bool _sprinting = false;
    private float _baseSpeed;
    private Vector2 _moveValue;
    private Coroutine _sprintReset;


    [SerializeField] private Slider _sprintMeter;
    [SerializeField] private CinemachineCamera _camera;


    void OnEnable()
    {
        _sprintTimeLeft = _maxSprintTime;
        _baseSpeed = _player.GetSpeed();
        EnableSprint();
    }
    void OnDisable()
    {
        DisableSprint();
    }

    void Update()
    {
        RemoveSprint(_moveValue);
    }

    public void EnableSprint()
    {
        _inputReader.OnMove += ReadMove;

        _inputReader.EnableSprintAction();
        _inputReader.OnSprint += SprintStart;

        _sprintReset = null;
    }
    public void DisableSprint()
    {
        _inputReader.OnMove -= ReadMove;

        _inputReader.DisableSprintAction(); 
        _inputReader.OnSprint -= SprintStart;

    }

    private void SprintStart(bool givenValue)
    {
        float increaseFloat = 1 + _speedIncrease;
        _sprinting = givenValue;
        if(_sprinting)
        {
            _player.ChangeSpeed(_baseSpeed * increaseFloat);

        }
        else
        {
            _player.ChangeSpeed(_baseSpeed);
            _sprintReset ??= StartCoroutine(ShowSprintReset(5f));
        }
    }

    private void ReadMove(Vector2 moveValue)
    {
        _moveValue = moveValue;
    }

    private void RemoveSprint(Vector2 moveValue)
    {
        if(moveValue != Vector2.zero && _sprinting && _sprintTimeLeft > 0)
        {

            _sprintTimeLeft -= Time.deltaTime;
            _sprintMeter.value = _sprintTimeLeft;

            if(_sprintReset != null)
            {
                StopCoroutine(_sprintReset);
                _sprintReset = null; 
            }
        }
        else if (moveValue == Vector2.zero && _sprinting)
        {
            _sprintReset ??= StartCoroutine(ShowSprintReset(5f));
        }
        else if (_sprintTimeLeft <= 0)
        {
            SprintStart(false);
        }
    }


    private IEnumerator ShowSprintReset(float waitValue)
    {
        float smallWait = _maxSprintTime/waitValue/50f;
        yield return _waitForSeconds2;
        while(_sprintTimeLeft < _maxSprintTime)
        {
            _sprintTimeLeft += smallWait;
            _sprintMeter.value = _sprintTimeLeft;
            yield return new WaitForSeconds(smallWait);
        }
    }

    public bool GetSprinting()
    {
        return _sprinting;
    }
}
