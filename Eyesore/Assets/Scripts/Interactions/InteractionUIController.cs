using TMPro;
using UnityEngine;

public class InteractionUIController : MonoBehaviour
{
    [SerializeField] private InteractMechanic _interactMechanic;
    [SerializeField] private KeyCode _interactKey = KeyCode.Space;

    [SerializeField] private TMP_Text _promptText;

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(_promptText == null) return;

        Interaction target = _interactMechanic.GetTargetInteraction();

        if(target != null && target.CanInteract())
        {
            _promptText.gameObject.SetActive(true);
            _promptText.text = $"[{_interactKey}] {target.GetPrompt()}";
        }
        else
        {
            _promptText.gameObject.SetActive(false);
        }
    }
}
