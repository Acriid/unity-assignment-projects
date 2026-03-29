using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightWarning : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private Color _warningColour;
    [SerializeField] private float _warningRadius;
    [SerializeField] private CircleCollider2D _collider;
    private Color _baseColour;
    private void OnEnable()
    {
        _baseColour = _light.color;

        if(_collider != null)
        {
            _collider.radius = _warningRadius;
        }
        else
        {
            Debug.LogWarning("No collider for lightwarning");
        }


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            ChangeLightColour(_warningColour);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            ChangeLightColour(_baseColour);
        }
    }

    private void ChangeLightColour(Color newColour)
    {
        _light.color = newColour;
    }
}
