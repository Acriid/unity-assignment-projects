using UnityEngine;

public class VisionProvider : MonoBehaviour
{
    [SerializeField] private float visionRange = 3f;

    // The child quad on the VisionProviders layer
    [SerializeField] private Transform visionQuad;

    void OnValidate() => ApplyScale();
    void Awake()      => ApplyScale();

    // Call this at runtime to update vision radius dynamically
    public float VisionRange
    {
        get => visionRange;
        set { visionRange = value; ApplyScale(); }
    }

    private void ApplyScale()
    {
        if (visionQuad == null) return;
        // Quad fills its UV space, so diameter = visionRange * 2 in world units
        float diameter = visionRange * 2f;
        visionQuad.localScale = new Vector3(diameter, diameter, 1f);
    }
}