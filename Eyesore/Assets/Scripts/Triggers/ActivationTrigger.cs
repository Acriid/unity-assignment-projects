using System.Collections.Generic;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    [SerializeField] private string _collisionTag;
    [SerializeField] private List<GameObject> _enableGameObject = new();
    [SerializeField] private List<GameObject> _disableGameObject = new();
    [SerializeField] private bool _disableSelf = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag))
        {
            if(_enableGameObject.Count != 0) EnableGameObjects();
            if(_disableGameObject.Count != 0) DisableGameObjects();
            if(_disableSelf) this.gameObject.SetActive(false);
        }
    }

    private void EnableGameObjects()
    {
        foreach(GameObject enableObject in _enableGameObject)
        {
            enableObject.SetActive(true);
        }
    }
    private void DisableGameObjects()
    {
        foreach(GameObject disableObject in _disableGameObject)
        {
            disableObject.SetActive(false);
        }
    }
}
