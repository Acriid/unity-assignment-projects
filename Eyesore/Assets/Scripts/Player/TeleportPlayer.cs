using System;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 _teleportPosition;
    [SerializeField] private GameObject _teleportObject;
    [SerializeField] private InputReaderSO _inputReaderSO;
    private bool _canTeleport = true;
    private void OnEnable()
    {
        if(_inputReaderSO != null)
        _inputReaderSO.OnCrouch += OnCrouch;
    }
    private void OnDisable()
    {
        if(_inputReaderSO != null)
        _inputReaderSO.OnCrouch -= OnCrouch;
    }
    private void OnCrouch(bool obj)
    {
        _canTeleport = !obj;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_canTeleport) return;
        if(collision.CompareTag("Player"))
        {
            if(_teleportObject != null)
            {
                collision.transform.position = _teleportObject.transform.position;
            }
            else
            {
                collision.transform.position = _teleportPosition;
            }
            
        }
    }

    public void ChangeTeleportPosition(Vector2 newPosition)
    {
        _teleportPosition = newPosition;
    }
    public void ChangeTeleportPosition(GameObject newPosition)
    {
        _teleportObject = newPosition;
    }
}
