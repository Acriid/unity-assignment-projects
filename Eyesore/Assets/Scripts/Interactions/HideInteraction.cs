using Unity.VisualScripting;
using UnityEngine;

public class HideInteraction : Interaction
{
    [SerializeField] private string _newTag;
    [SerializeField] private bool _isHidden = false;
    private string _originalTag;
    private GameObject _lightObject = null;
    private Player _playerComponent = null;
    private SpriteRenderer _playerRenderer = null;
    public override void OnInteract(GameObject player)
    {
        if(!_isHidden)
        {
            Hide(player);
        }
        else
        {
            UnHide(player);
        }
    }

    private void Hide(GameObject player)
    {
        //Change tag
        _originalTag = player.tag;
        player.tag = _newTag;

        
        _playerComponent = player.GetComponent<Player>();
        _playerRenderer = player.GetComponent<SpriteRenderer>();
        _lightObject = _playerComponent.GetLightObject();

        //Remove actions
        _playerComponent.StopMove();
        _playerComponent.StopToggle();

        //Remove light
        _lightObject.SetActive(false);

        //Remove sprite
        _playerRenderer.enabled = false;


        _isHidden = true;
    }

    private void UnHide(GameObject player)
    {
        //Restore tag
        player.tag = _originalTag;

        //Restore actions
        _playerComponent.StartMove();
        _playerComponent.StartToggle();  

        //Restore light
        _lightObject.SetActive(true);  
        

        //Restore sprite
        _playerRenderer.enabled = true;

        _playerComponent = null;
        _lightObject = null;
        _playerRenderer = null;

        _isHidden = false;
    }

    public override string GetPrompt()
    {
        return !_isHidden ? "Hide" : "Stop Hiding";
    }
}
