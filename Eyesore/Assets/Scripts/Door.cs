using UnityEditor.ShaderGraph;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Vertical Moves in intervals of 11
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Vector3 _cameraStart;
    [SerializeField] private Vector3 _cameraEnd;
    [SerializeField] private bool _top;
    [SerializeField] private bool _right;
    private Vector2 _playerInput;
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
        SubscribeToEvents();
    }
    void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        _inputReader.OnMove += GetPlayerInput;
    }
    private void UnsubscribeFromEvents()
    {
        _inputReader.OnMove -= GetPlayerInput;
    }
    private void GetPlayerInput(Vector2 newValue)
    {
        _playerInput = newValue;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        float playerx = _playerInput.x;
        float playery = _playerInput.y;
        bool forward = (_right && playerx > 0f) || (_top && playery > 0f);
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        if(forward)
        {
            _camera.transform.localPosition = _cameraEnd;
        }
        else
        {
            _camera.transform.localPosition = _cameraStart;
        } 
    }
}
