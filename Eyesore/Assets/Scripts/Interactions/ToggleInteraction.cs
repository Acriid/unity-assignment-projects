using Unity.VisualScripting;
using UnityEngine;

public class ToggleInteraction : Interaction
{
    [SerializeField] private bool isToggled = false;
    [SerializeField] private string toggledPrompt;
    private string originalPrompt;
    void Start()
    {
        InitialToggle(isToggled);
        originalPrompt = interactionPrompt;
    }
    public override void OnInteract(GameObject player)
    {
        isToggled = !isToggled;
        EnableObjects(isToggled);
        DisableObjects(isToggled);
        if(!isToggled)
        {
            ChangeInteractionPrompt(originalPrompt);
        }
        else
        {
            ChangeInteractionPrompt(toggledPrompt);
        }
        ShowDialog();
    }
    private void InitialToggle(bool toggleValue)
    {
        EnableObjects(toggleValue);
        DisableObjects(!toggleValue);
    }
}
