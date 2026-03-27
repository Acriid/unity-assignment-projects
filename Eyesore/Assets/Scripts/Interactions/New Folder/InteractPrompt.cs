using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text _textHolder;
    public void ShowPrompt(string text, Vector2 position)
    {
        this.transform.position = position;
        _textHolder.text = text;
    }
}
