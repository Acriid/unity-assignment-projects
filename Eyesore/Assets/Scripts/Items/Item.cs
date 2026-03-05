using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public ItemSO ItemSO;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Collider2D _collider;
    void OnEnable()
    {
        Initialize();
    }
    private void Initialize()
    {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody2D>();

        _rigidBody.gravityScale = 0;

        if(_collider == null) _collider = GetComponent<Collider2D>();
        
    }

    public void ChangeExcludeLayerMasks(LayerMask layerMask)
    {
        _collider.excludeLayers = layerMask;
    }


    public void KinematicRigidBody(RigidbodyType2D newBodyType)
    {
        _rigidBody.bodyType = newBodyType;
    }
}
