using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleRoomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject _puzzleObject;
    [SerializeField] private GameObject _puzzleSolutionObject;
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private int _offset = 2;
    private List<GameObject> _puzzleObjects = new();
    private List<GameObject> _puzzleSolutions = new();
    public void PlacePuzzleRoom(List<BoundsInt> roomsList, int puzzleCount)
    {
        List<BoundsInt> randomRooms = new();
        List<BoundsInt> cloneRoomsList = new(roomsList);
        for(int i = 0 ; i < puzzleCount ; i++)
        {
            
            BoundsInt randomElement = cloneRoomsList.ElementAt(Random.Range(0,cloneRoomsList.Count));
            cloneRoomsList.Remove(randomElement);
            randomRooms.Add(randomElement);

        }

        PlaceLockPuzzle(randomRooms);
        
    }

    private void PlaceLockPuzzle(List<BoundsInt> rooms)
    {
        List<Vector3Int> puzzlePositions = new();
        int count = -1;
        foreach(BoundsInt currentRoom in rooms)
        {
            count++;

            if(count == rooms.Count -1)
            {
                break;
            }

            int xPosition = Random.Range(currentRoom.min.x + _offset, currentRoom.max.x - _offset);
            int yPosition = Random.Range(currentRoom.min.y + _offset, currentRoom.max.y - _offset);

            Vector3Int puzzlePosition = new(xPosition,yPosition);
            
            if(puzzlePositions.Count > 0)
            {
                bool correctPosition = false;
                int failsafe = 0;
                while(!correctPosition && failsafe < 10000)
                {
                    correctPosition = true;
                    xPosition = Random.Range(currentRoom.min.x + _offset, currentRoom.max.x - _offset);
                    yPosition = Random.Range(currentRoom.min.y + _offset, currentRoom.max.y - _offset);

                    puzzlePosition = new(xPosition,yPosition);

                    

                    foreach(Vector3Int vector3Int in puzzlePositions)
                    {
                        float currentDistance = Vector3Int.Distance(puzzlePosition, vector3Int);
                        if (currentDistance < _minDistance)
                        {
                            correctPosition = false;
                        }
                    }
                    failsafe++;
                }

            }
            puzzlePositions.Add(puzzlePosition);
            if(_puzzleObjects == null)
            {
                Debug.Log("Uh OH");
            }
            _puzzleObjects.Add(Instantiate(_puzzleObject,puzzlePosition,Quaternion.identity,this.transform));

        }
        
        _puzzleSolutions.Add(Instantiate(_puzzleSolutionObject,rooms[count].center,Quaternion.identity,this.transform));

        if(!_puzzleSolutions[0].TryGetComponent<Puzzle>(out Puzzle puzzleSolutionComponent))
        {
            Debug.LogWarning("Something went wrong with puzzle component");
            return;
        }

        foreach(GameObject puzzleObject in _puzzleObjects)
        {
            if(puzzleObject.TryGetComponent<PuzzleObject>(out PuzzleObject puzzleComponent))
            {
                puzzleSolutionComponent.AddToPuzzleList(puzzleComponent);
            }
        }
        puzzleSolutionComponent.SetPuzzleType(PuzzleType.LockPuzzle);

        puzzleSolutionComponent.InitializePuzzle();

    }
}
