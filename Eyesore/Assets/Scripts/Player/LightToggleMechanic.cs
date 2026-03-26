using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightToggleMechanic : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Light2D _playerLight;

    void OnEnable()
    {
        EnableLightToggle();
    }
    void OnDisable()
    {
        DisableLightToggle();
    }
    public void EnableLightToggle()
    {
        _inputReader.EnableToggleLightAction();
        _inputReader.OnToggleLight += ToggleLight;
    }
    public void DisableLightToggle()
    {
        _inputReader.DisableToggleLightAction();
        _inputReader.OnToggleLight -= ToggleLight;
    }

    private void ToggleLight()
    {
        _playerLight.enabled = !_playerLight.enabled;
    }
}
