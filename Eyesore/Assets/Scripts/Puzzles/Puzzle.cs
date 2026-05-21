using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Puzzle : Interaction
{
    [Header("Puzzle")]
    [SerializeField] private List<PuzzleObject> _puzzleObjects;
    [SerializeField] private PuzzleType? puzzleType = null; 
    [SerializeField] private GameObject _canvasObject = null;
    [SerializeField] private TMP_InputField _puzzleInput = null;
    private string _puzzleSolution = "";
    public void AddToPuzzleList(PuzzleObject objectToAdd)
    {
        _puzzleObjects.Add(objectToAdd);
    }
    public void SetPuzzleType(PuzzleType newType)
    {
        puzzleType = newType;
    }
    public void InitializePuzzle()
    {
        if(puzzleType == null)
        {
            Debug.LogWarning("Tried to Initialize Puzzle without setting puzzle type.");
            return;
        }

        if(puzzleType.Value == PuzzleType.LockPuzzle)
        {
            GenerateLockPuzzle();
            if(_puzzleInput != null)
            _puzzleInput.onValueChanged.AddListener(CheckValue) ;
        }
    }

    private void GenerateLockPuzzle()
    {
        int order = 1;
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        foreach(PuzzleObject puzzleObject in _puzzleObjects)
        {
            char randomChar = chars[Random.Range(0,chars.Length)];
            _puzzleSolution += randomChar;
            puzzleObject.SetPuzzleValue(randomChar,order++);
        }
        Debug.Log(_puzzleSolution);
    }
    public override void OnInteract(GameObject player)
    {
        if(_canvasObject == null) return;
        int scaleChange = !_canvasObject.activeSelf? 0 : 1;
        _canvasObject.SetActive(!_canvasObject.activeSelf);
        Time.timeScale = scaleChange;
    }

    private void CheckValue(string text)
    {
        if(text.Trim().ToUpper() == _puzzleSolution)
        {
            Debug.Log("Puzzle Solved");
        }
    }
}

public enum PuzzleType
{
    LockPuzzle
}
