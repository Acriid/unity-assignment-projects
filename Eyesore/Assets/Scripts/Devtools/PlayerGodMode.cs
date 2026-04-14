using Unity.VisualScripting;
using UnityEngine;

public class PlayerGodMode : MonoBehaviour
{
    public GameObject Player;
    public InputReaderSO InputReaderSO;
    private Collider2D _playerCollider;
    private LayerMask _originalPlayerMask;
    private string _originalPlayerTag;

    public GameObject PlayerLight;
    private LayerMask _originalLightMask;
    private string _originalLightTag;
    bool _godMode = false;
    private void OnEnable()
    {
        if(!Player.TryGetComponent<Collider2D>(out _playerCollider)) return;
        _originalPlayerTag = Player.tag;
        _originalPlayerMask = Player.layer;

        _originalLightMask = PlayerLight.layer;
        _originalLightTag = PlayerLight.tag;

        InputReaderSO.OnGodModePlayer += OnGodMode;
    }
    private void OnDisable()
    {
        InputReaderSO.OnGodModePlayer -= OnGodMode;
    }
    private void OnGodMode()
    {
        _godMode = !_godMode;
        _playerCollider.enabled = !_godMode;

        string newTagPlayer = !_godMode? _originalPlayerTag : "Untagged";
        string newTagLight = !_godMode? _originalLightTag : "Untagged";

        LayerMask newLayerPlayer = !_godMode? _originalPlayerMask : 0;
        LayerMask newLayerLight = !_godMode? _originalLightMask : 0; 

        Player.tag = newTagPlayer;
        Player.layer = newLayerPlayer;

        PlayerLight.tag = newTagLight;
        PlayerLight.layer = newLayerLight;
    }
}
