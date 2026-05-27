using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundTrigger : MonoBehaviour
{
    [SerializeField] private bool _activeMechanic = true;
    [SerializeField] private bool _toggleMechanic = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<SoundMechanic>().SetActiveBool(_activeMechanic);
            if(_toggleMechanic)
            _activeMechanic = !_activeMechanic;
        }
    }
}
