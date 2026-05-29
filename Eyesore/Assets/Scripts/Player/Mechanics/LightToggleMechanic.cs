using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightToggleMechanic : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Light2D _playerLight;
    [SerializeField] private Material _outLine;
    [SerializeField] private Sprite _circle;

    private Sprite _originalSprite;
    private Material _originalMaterial;
    private CircleCollider2D _lightCollider;
    private SpriteRenderer _spriteRenderer;
    private bool _enabled = true;

    void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalSprite = _spriteRenderer.sprite;
        _originalMaterial = _spriteRenderer.material;
        _lightCollider = _playerLight.gameObject.GetComponent<CircleCollider2D>();
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
        _enabled = !_enabled;
        _lightCollider.enabled = _enabled;
        _playerLight.enabled = _enabled;
        if(!_enabled)
        {
            _spriteRenderer.sprite = _circle;
            _spriteRenderer.material = _outLine;
        }
        else
        {
            _spriteRenderer.sprite = _originalSprite;
            _spriteRenderer.material = _originalMaterial;            
        }
    }
}
