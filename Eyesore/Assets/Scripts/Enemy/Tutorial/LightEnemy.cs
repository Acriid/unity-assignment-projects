using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LightEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navAgent;
    [SerializeField] private Vector2 _resetDestination;
    [SerializeField] private GameObject _playerResetDestination;
    [SerializeField] private List<GameObject> _enableObjects;
    [SerializeField] private List<GameObject> _disableObjects;
    private GameObject _player;

    void Start()
    {
        _resetDestination = transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            ResetEncounter();
        }
        else if (collision.CompareTag("Player Light"))
        {
            _player = collision.transform.parent.gameObject;
            ResetEncounter();
        }
    }

    private void ResetEncounter()
    {
        _navAgent.ResetPath();
        this.gameObject.transform.position = _resetDestination;
        _player.transform.position = _playerResetDestination.transform.position;

        if(_player.TryGetComponent<Player>(out Player playerComponent))
        {
            if(playerComponent.HoldingItem)
            {
                playerComponent.ForceInteract();
            }
        }

        foreach(GameObject enableObjects in _enableObjects)
        {
            enableObjects.SetActive(true);
        }
        foreach(GameObject disableObjects in _disableObjects)
        {
            disableObjects.SetActive(false);
        }
    }
    public void SetNavAgentDestination(Vector2 destination)
    {
        _navAgent.SetDestination(destination);
    }
}
