using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : Interaction
{
    [Header("Puzzle")]
    [SerializeField] private List<PuzzleObject> _puzzleObjects;
    [SerializeField] private PuzzleType? puzzleType = null; 
    [SerializeField] private GameObject _canvasObject = null;
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
    }
}

public enum PuzzleType
{
    LockPuzzle
}
