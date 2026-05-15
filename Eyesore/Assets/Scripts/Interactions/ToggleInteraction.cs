using UnityEngine;

public class ToggleInteraction : Interaction
{
    [SerializeField] private bool isToggled = false;
    void Start()
    {
        InitialToggle(isToggled);
    }
    public override void OnInteract(GameObject player)
    {
        isToggled = !isToggled;
        EnableObjects(isToggled);
        DisableObjects(isToggled);
    }
    private void InitialToggle(bool toggleValue)
    {
        EnableObjects(toggleValue);
        DisableObjects(toggleValue);
    }
}
