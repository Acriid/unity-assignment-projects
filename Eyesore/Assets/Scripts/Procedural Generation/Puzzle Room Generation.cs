using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleRoomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject _puzzleObject;
    [SerializeField] private GameObject _puzzleSolutionObject;
    [SerializeField] private Tilemap _floorTileMap;
    private List<GameObject> _puzzleObjects = new();
    private List<GameObject> _puzzleSolutions = new();
    public void PlacePuzzleRoom(List<BoundsInt> roomsList, int puzzleCount, int offset)
    {
        List<BoundsInt> randomRooms = new();
        List<BoundsInt> cloneRoomsList = new(roomsList);

        for(int i = 0 ; i < puzzleCount ; i++)
        {
            
            BoundsInt randomElement = cloneRoomsList[Random.Range(0,cloneRoomsList.Count)];
            cloneRoomsList.Remove(randomElement);
            randomRooms.Add(randomElement);

        }

        PlaceLockPuzzle(randomRooms,offset);
        
    }

    private void PlaceLockPuzzle(List<BoundsInt> rooms, int offset)
    {
        List<Vector3Int> puzzlePositions = new();
        int count = -1;
        foreach(BoundsInt currentRoom in rooms)
        {
            count++;
            Vector3Int puzzlePosition = Vector3Int.zero;
                
            if(count == rooms.Count -1)
            {
                break;
            }
            bool onTile = false;
            while(!onTile)
            {
                int xPosition = Random.Range(currentRoom.xMin + offset, currentRoom.xMax - offset);
                int yPosition = Random.Range(currentRoom.yMin + offset, currentRoom.yMax - offset);

                puzzlePosition = new(xPosition,yPosition);

                if(_floorTileMap.HasTile(puzzlePosition))
                onTile = true;
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

    public void ClearPuzzle()
    {
        foreach(GameObject gameObject in _puzzleObjects)
        {
            Destroy(gameObject);
        }
        _puzzleObjects.Clear();
        foreach(GameObject gameObject in _puzzleSolutions)
        {
            Destroy(gameObject);
        }
        _puzzleSolutions.Clear();
    }
}
