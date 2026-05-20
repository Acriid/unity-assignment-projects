using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleObject : Interaction
{
    [Header("Puzzle")]
    [SerializeField] private char _puzzleValue;
    [SerializeField] private int _puzzleOrder = -1;
    [SerializeField] private GameObject _puzzleCanvas = null;
    [SerializeField] private TMP_Text _puzzleText = null;
    public void SetPuzzleValue(char newValue,int puzzleOrder)
    {
        _puzzleValue = newValue;
        _puzzleOrder = puzzleOrder;

        if(_puzzleText == null ) return;
        _puzzleText.text = $"{_puzzleOrder}. {_puzzleValue}";
    }
    public override void OnInteract(GameObject player)
    {
        if(_puzzleCanvas == null)
        {
            Debug.LogWarning($"{gameObject.name} has no puzzleCanvas");
            return;
        }
        int newScale = !_puzzleCanvas.activeSelf? 0 : 1;
        _puzzleCanvas.SetActive(!_puzzleCanvas.activeSelf);
        Time.timeScale = newScale;
    }
}
