using UnityEngine;
using Unity.Cinemachine;

public class CrouchMechanic : MonoBehaviour
{
    public InputReaderSO InputReaderSO;
    private float _baseSpeed;
    [Range(0,1)]
    [SerializeField] private float _speedDecrease;
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineCamera _camera;

    private float _crouchCameraSize = 0f;
    private float _originalSize = 0f;
    private float _zoomSpeed = 20f;

    private bool _crouching = false;
    void OnEnable()
    {
        _baseSpeed = _player.GetSpeed();
        _originalSize = _camera.Lens.OrthographicSize;
        _crouchCameraSize = _originalSize * 0.7f;
       EnableCrouch();
    }
    void OnDisable()
    {
        DisableCrouch();
    }

    void Update()
    {
        UpdateCameraZoom();
    }
    private void Crouching(bool newValue)
    {
        _crouching = newValue;
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

    private void UpdateCameraZoom()
    {
        float targetSize = _crouching ? _crouchCameraSize : _originalSize;
        
        _camera.Lens.OrthographicSize = Mathf.Lerp(
            _camera.Lens.OrthographicSize,
            targetSize,
            _zoomSpeed * Time.deltaTime
        );
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
