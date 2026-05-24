using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TeleporterPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _teleporterObject;
    [SerializeField] private GameObject _teleportDestinationObject;
    [SerializeField] private Tilemap _floorTileMap;
    [SerializeField] private int _destinationOffset;
    private RoomConnection _roomConnections = new();
    private List<GameObject> _teleporters = new();
    private List<GameObject> _destinations = new();
    public void PlaceTeleporters(List<BoundsInt> roomsList, int offset)
    {
        List<BoundsInt> copyList = new(roomsList);


        float bestDistance = 0f;

        for(int i = 0 ; i < copyList.Count -1 ; i++)
        {
            BoundsInt currentRoom = copyList[i];
            Vector3Int currentCenter = GetRoomCenter(currentRoom);
            for(int j = 0; j < copyList.Count ; j++)
            {
                BoundsInt checkingRoom = copyList[j];
                Vector3Int checkingRoomCenter = GetRoomCenter(checkingRoom);

                float currentDistance = Vector3Int.Distance(currentCenter,checkingRoomCenter);
                if(currentDistance > bestDistance)
                {
                    _roomConnections.StartRoom = currentRoom;
                    _roomConnections.EndRoom = checkingRoom;
                    bestDistance = currentDistance;
                }

            }
        }
  

        Vector3Int roomPosition = FindPositionInRoom(_roomConnections.StartRoom,offset);
        Vector3Int destinationPosition = FindPositionInRoom(_roomConnections.EndRoom,offset);
        PlaceTeleporters(roomPosition,destinationPosition);


        Vector3Int returnTeleport = FindPositionInRoom(_roomConnections.EndRoom,offset,_destinationOffset,destinationPosition);
        Vector3Int returnDestinationPosition = FindPositionInRoom(_roomConnections.StartRoom,offset,_destinationOffset,roomPosition);
        PlaceTeleporters(returnTeleport,returnDestinationPosition);

        copyList.Remove(_roomConnections.StartRoom);
        copyList.Remove(_roomConnections.EndRoom);

        bestDistance = 0f;
        for(int i = 0 ; i < copyList.Count -1 ; i++)
        {
            BoundsInt currentRoom = copyList[i];
            Vector3Int currentCenter = GetRoomCenter(currentRoom);
            for(int j = 0; j < copyList.Count ; j++)
            {
                BoundsInt checkingRoom = copyList[j];
                Vector3Int checkingRoomCenter = GetRoomCenter(checkingRoom);

                float currentDistance = Vector3Int.Distance(currentCenter,checkingRoomCenter);
                if(currentDistance > bestDistance)
                {
                    _roomConnections.StartRoom = currentRoom;
                    _roomConnections.EndRoom = checkingRoom;
                    bestDistance = currentDistance;
                }

            }
        }

        roomPosition = FindPositionInRoom(_roomConnections.StartRoom,offset);
        destinationPosition = FindPositionInRoom(_roomConnections.EndRoom,offset);
        PlaceTeleporters(roomPosition,destinationPosition);


        returnTeleport = FindPositionInRoom(_roomConnections.EndRoom,offset,_destinationOffset,destinationPosition);
        returnDestinationPosition = FindPositionInRoom(_roomConnections.StartRoom,offset,_destinationOffset,roomPosition);
        PlaceTeleporters(returnTeleport,returnDestinationPosition);








        for(int i = 0 ; i < _teleporters.Count ; i++)
        {
            _teleporters[i].GetComponent<TeleportPlayer>().ChangeTeleportPosition(_destinations[i]);
        }
    }
    public void ClearTeleporters()
    {
        foreach(GameObject gameObject in _teleporters)
        {
            Destroy(gameObject);
        }
        _teleporters.Clear();

        foreach(GameObject gameObject in _destinations)
        {
            Destroy(gameObject);
        }
        _destinations.Clear();
    }

    private void PlaceTeleporters(Vector3Int position, Vector3Int destination)
    {
        _teleporters.Add(Instantiate(_teleporterObject,position,Quaternion.identity,this.transform));
        _destinations.Add(Instantiate(_teleportDestinationObject,destination,Quaternion.identity,this.transform));
    }
    private Vector3Int GetRoomCenter(BoundsInt room)
    {
        return Vector3Int.RoundToInt(room.center);
    }
    private Vector3Int FindPositionInRoom(BoundsInt currentRoom,int offset)
    {
        bool onTile = false;
        Vector3Int puzzlePosition = Vector3Int.zero;

        while(!onTile)
        {
            int xPosition = Random.Range(currentRoom.xMin + offset + 1, currentRoom.xMax - offset - 1);
            int yPosition = Random.Range(currentRoom.yMin + offset + 1, currentRoom.yMax - offset - 1);

            puzzlePosition = new(xPosition,yPosition);

            if(_floorTileMap.HasTile(puzzlePosition))
            onTile = true;
        }
        return puzzlePosition;
    }
    private Vector3Int FindPositionInRoom(BoundsInt currentRoom,int offset,int positionOffset, Vector3Int positionToOffset)
    {
        bool onTile = false;
        Vector3Int puzzlePosition = Vector3Int.zero;

        while(!onTile)
        {
            int xPosition = Random.Range(currentRoom.xMin + offset + 1, currentRoom.xMax - offset - 1);
            int yPosition = Random.Range(currentRoom.yMin + offset + 1, currentRoom.yMax - offset - 1);

            puzzlePosition = new(xPosition,yPosition);
            
            float currentDistance = Vector3Int.Distance(puzzlePosition,positionToOffset);
            if(currentDistance < positionOffset)
            continue;
            
            if(_floorTileMap.HasTile(puzzlePosition))
            onTile = true;
        }
        return puzzlePosition;
    }
}
