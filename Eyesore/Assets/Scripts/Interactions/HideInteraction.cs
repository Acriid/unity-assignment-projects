using Unity.VisualScripting;
using UnityEngine;

public class HideInteraction : Interaction
{
    [SerializeField] private string _newTag;
    [SerializeField] private bool _isHidden = false;

    [SerializeField] private Enemy _enemy;
    private string _originalTag;
    private LayerMask _originalLayer;
    private GameObject _lightObject = null;
    private Player _playerComponent = null;
    private SpriteRenderer _playerRenderer = null;
    private BoxCollider2D _playerCollider = null;

    private bool _enemySaw = false;
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

        _playerComponent = player.GetComponent<Player>();
        _playerRenderer = player.GetComponent<SpriteRenderer>();
        _lightObject = _playerComponent.GetLightObject();
        _playerCollider = player.GetComponent<BoxCollider2D>();

        if (_enemy != null && _enemy.StateMachine.CurrentEnemyState == _enemy.ChaseState)
        {
            _enemySaw = true;
        }


        //Change tag
        if (!_enemySaw)
        {
            _originalTag = player.tag;
            _originalLayer = player.layer;
            
            player.tag = _newTag;
            player.layer = 0;

            


            //Remove actions
            _playerComponent.StopMove();
            _playerComponent.StopToggle();

            
            //Remove collider
            _playerCollider.enabled = false;
        }


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

        //Restore collider
        _playerCollider.enabled = true;

        _isHidden = false;
    }

    public override string GetPrompt()
    {
        return !_isHidden ? "Hide" : "Stop Hiding";
    }
}
