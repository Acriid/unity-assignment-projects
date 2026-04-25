using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class InteractMechanic : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float _detectionRadius = 3f;
    [SerializeField] private LayerMask _interactionLayer;

    private List<Interaction> _interactionsInRange = new();
    private Interaction _targetInteraction;
    private CircleCollider2D _detectionTrigger;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateTargetInteraction(transform);
    }

    private void Initialize()
    {
        _detectionTrigger = GetComponent<CircleCollider2D>();
        _detectionTrigger.radius = _detectionRadius;
        _detectionTrigger.isTrigger = true;
    }

    private void UpdateTargetInteraction(Transform player)
    {

        _interactionsInRange.RemoveAll(interaction => interaction == null);
        
        _targetInteraction = null;

        if (_interactionsInRange.Count == 0) return;

        float bestScore = float.MaxValue;

        foreach (Interaction interaction in _interactionsInRange)
        {
            float distance = Vector3.Distance(player.position, interaction.transform.position);

            if (distance < bestScore)
            {
                bestScore = distance;
                _targetInteraction = interaction;
            }
        }
    }


    public bool Interact(GameObject player)
    {
        if (_targetInteraction == null) return false;


        _targetInteraction.OnInteract(player);
        return true;
    }


    public Interaction GetTargetInteraction()
    {
        return _targetInteraction;
    }


    public bool HasInteraction()
    {
        return _targetInteraction != null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _interactionLayer) == 0)
            return;

        Interaction interaction = collision.GetComponent<Interaction>();

        if (interaction != null && !_interactionsInRange.Contains(interaction))
            _interactionsInRange.Add(interaction);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interaction>(out var interaction))
        {
            _interactionsInRange.Remove(interaction);
            if (_targetInteraction == interaction)
                _targetInteraction = null;
        }
    }
}
