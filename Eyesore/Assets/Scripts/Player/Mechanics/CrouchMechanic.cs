using UnityEngine;

public class CrouchMechanic : MonoBehaviour
{
    public InputReaderSO InputReaderSO;
    private float _baseSpeed;
    [Range(0,1)]
    [SerializeField] private float _speedDecrease;
    [SerializeField] private Player _player;
    void OnEnable()
    {
        _baseSpeed = _player.GetSpeed();
       EnableCrouch();
    }
    void OnDisable()
    {
        DisableCrouch();
    }
    private void Crouching(bool newValue)
    {
        float newSpeed = newValue? _baseSpeed * (1 - _speedDecrease) : _baseSpeed;

        _player.ChangeSpeed(newSpeed);
        if(newValue)
        {
            InputReaderSO.DisableSprintAction();
        }
        else
        {
            InputReaderSO.EnableSprintAction();
        }
    }

    private void EnableCrouch()
    {
        InputReaderSO.EnableCrouchAction();
        InputReaderSO.OnCrouch += Crouching;
    }
    private void DisableCrouch()
    {
        InputReaderSO.DisableCrouchAction();
        InputReaderSO.OnCrouch -= Crouching;
    }
}
