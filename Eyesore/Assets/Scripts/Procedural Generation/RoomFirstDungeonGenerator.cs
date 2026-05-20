using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField] private int _minRoomWidth = 4, _minRoomHeight = 4;
    [SerializeField] private int _dungeonWidth = 20, _dungeonHeight = 20;
    [Range(0,10)]
    [SerializeField] private int _offset = 1;
    [SerializeField] private bool _randomWalkRooms = false;
    [SerializeField] private bool _placeLights = false;
    private List<BoundsInt> roomsList = new();

    private HashSet<RoomConnection> _corridorConnections = new();
    public struct RoomConnection
    {
        public BoundsInt StartRoom;
        public BoundsInt EndRoom;

        public RoomConnection(BoundsInt start = default, BoundsInt end = default)
        {
            StartRoom = start;
            EndRoom = end;  
        }
    }
    protected override void RunProceduralGeneration()
    {

        _entityPlacer.PlacedEntities += PlaceGoals;

        CreateRooms();
    }

    private void CreateRooms()
    {
        roomsList = ProceduralGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)_startPosition,
         new Vector3Int(_dungeonWidth,_dungeonHeight, 0)), _minRoomWidth, _minRoomHeight);


        HashSet<Vector2Int> floor = new();




    
        if(_randomWalkRooms)
        {
            floor = CreateRoomsRandomWalk(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        

        


        //Corridors
        HashSet<Vector2Int> corridors = ConnectRooms(roomsList);
        floor.UnionWith(corridors);

        
        _tilePainter.PaintFloorTiles(floor);

        HashSet<Vector2Int> seceretFloor = SecretConnectRooms(roomsList,_corridorConnections);
        _tilePainter.PaintHiddenFloorTiles(seceretFloor);

        WallGenerator.CreateWalls(floor,seceretFloor,_tilePainter);
        WallGenerator.CreateHiddenWalls(floor,seceretFloor,_tilePainter);

        if(_placeLights)
        {
            _lightPlacer.ClearLights();


            HashSet<BoundsInt> roomsRandom = new();
            roomsRandom.UnionWith(roomsList);
            _lightPlacer.PlaceLights(roomsRandom,_offset);
        }

        _puzzlePlacer.ClearPuzzle();
        _puzzlePlacer.PlacePuzzleRoom(roomsList,4,_offset);



        BakeNavMesh();
        _entityPlacer.PlaceEntities(roomsList);

        
    }

    private HashSet<Vector2Int> CreateRoomsRandomWalk(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new();
        for(int i = 0; i<roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(RandomWalkSO,roomCenter);
            foreach(var position in roomFloor)
            {
                bool xCorrect = position.x >= (roomBounds.xMin + _offset) && position.x <= (roomBounds.xMax - _offset);
                bool yCorrect = position.y >= (roomBounds.yMin + _offset) && position.y <= (roomBounds.yMax - _offset);
                if(xCorrect && yCorrect)
                {
                    floor.Add(position);
                }
            }

        }
        return floor;
    }
    private HashSet<Vector2Int> SecretConnectRooms(List<BoundsInt> rooms, HashSet<RoomConnection> currentConnections)
    {

        HashSet<Vector2Int> corridors = new();

        List<BoundsInt> roomsCopy = new(rooms);
        
        HashSet<RoomConnection> copyConnections = new(currentConnections);

        Dictionary<BoundsInt,BoundsInt> lookupDictionary = new();

        foreach(RoomConnection bounds in copyConnections)
        {
            lookupDictionary.Add(bounds.StartRoom,bounds.EndRoom);
        }



        for(int i = 0 ; i < 1 ; i++)
        {

            
            BoundsInt currentRoom = roomsCopy[Random.Range(0,roomsCopy.Count)];
            roomsCopy.Remove(currentRoom);


            bool foundConnection = false;
            while(!foundConnection && roomsCopy.Count > 0)
            {
                BoundsInt closest = BoundsFindClosestPointTo(currentRoom,roomsCopy);
                roomsCopy.Remove(closest);
                RoomConnection currentConnection = new()
                {
                    StartRoom = currentRoom,
                    EndRoom = closest
                };


                if (
                    lookupDictionary.TryGetValue(currentRoom, out var currentLookup) &&
                    lookupDictionary.TryGetValue(closest, out var closestLookup) &&
                    currentLookup != closest &&
                    closestLookup != currentRoom
                )
                {                 

                    Vector2Int currentRoomCenter = GetBoundsCenter(currentRoom);
                    Vector2Int closestRoomCenter = GetBoundsCenter(closest);

                    HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter,closestRoomCenter);

                    if(!GoesThroughRoom(currentConnection,rooms,newCorridor))
                    {
                        foundConnection = true;
                        corridors.UnionWith(newCorridor);
                        currentConnections.Add(currentConnection);

                    }
                    

                }



                currentRoom = closest;
            }
        }

        return corridors;
    }
    private bool GoesThroughRoom(RoomConnection roomConnection, List<BoundsInt> allRooms, HashSet<Vector2Int> corridor)
    {
        foreach(BoundsInt currentRoom in allRooms)
        {
            if(currentRoom.Equals(roomConnection.StartRoom) || currentRoom.Equals(roomConnection.EndRoom)) continue;
            
            foreach(Vector2Int vector2Int in corridor)
            {
                if(currentRoom.Contains(new Vector3Int(vector2Int.x, vector2Int.y, 0)))
                {
                    Debug.Log("WOW");
                    return true;
                }
            }
        }
        return false;
    }
    private HashSet<Vector2Int> ConnectRooms(List<BoundsInt> allRooms)
    {
        List<RoomConnection> roomConnections = new();


        List<BoundsInt> roomsCopy = new(allRooms);

        Dictionary<Vector2Int,BoundsInt> _roomLookup = new();
        
        List<Vector2Int> roomCenters = new();
        foreach (var room in roomsCopy)
        {
            Vector2Int currentCenter = (Vector2Int)Vector3Int.RoundToInt(room.center);

            _roomLookup.Add(currentCenter,room);

            roomCenters.Add(currentCenter);
        }

        //Currently Gets random room and then connects to other rooms from the random room
        HashSet<Vector2Int> corridors = new();
        var currentRoomCenter = roomCenters[Random.Range(0,roomCenters.Count)];


        roomCenters.Remove(currentRoomCenter);
    
        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter,roomCenters);
            roomCenters.Remove(closest);

            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter,closest);

            if(_roomLookup.TryGetValue(currentRoomCenter, out BoundsInt startRoom) && _roomLookup.TryGetValue(closest,out BoundsInt endRoom))
            {
                RoomConnection newConnection = new()
                {
                    StartRoom = startRoom,
                    EndRoom = endRoom
                };

                roomConnections.Add(newConnection);
            }


            currentRoomCenter = closest;

            corridors.UnionWith(newCorridor);
        }

        _corridorConnections = new(roomConnections);

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new();
        var position = currentRoomCenter;
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
            corridor.Add(position += Vector2Int.left);
            corridor.Add(position += Vector2Int.right);
        }
        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else
            {
                position += Vector2Int.left;
            }
            corridor.Add(position); 
            corridor.Add(position += Vector2Int.up);
            corridor.Add(position += Vector2Int.down);           
        }
        return corridor;
    }
    private HashSet<Vector2Int> CreateCorridor(BoundsInt currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new();
        var position = (Vector2Int)Vector3Int.RoundToInt(currentRoomCenter.center);
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
            corridor.Add(position += Vector2Int.left);
            corridor.Add(position += Vector2Int.right);
        }
        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else
            {
                position += Vector2Int.left;
            }
            corridor.Add(position); 
            corridor.Add(position += Vector2Int.up);
            corridor.Add(position += Vector2Int.down);           
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach(var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position,currentRoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private BoundsInt BoundsFindClosestPointTo(BoundsInt currentRoomCenter, List<BoundsInt> roomCenters)
    {
        Vector2Int currentCenter = (Vector2Int)Vector3Int.RoundToInt(currentRoomCenter.center);


        Dictionary<Vector2Int,BoundsInt> _lookupDictionary = new();

        foreach(BoundsInt bounds in roomCenters)
        {
            _lookupDictionary.Add((Vector2Int)Vector3Int.RoundToInt(bounds.center),bounds);
        }

        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;


        foreach(var position in _lookupDictionary)
        {
            float currentDistance = Vector2.Distance(position.Key,currentCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = position.Key;
            }
        }

        return _lookupDictionary[closest];
    }


    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new();
        foreach(var room in roomsList)
        {
            for (int column = _offset; column < room.size.x - _offset; column++)
            {
                for(int row = _offset; row < room.size.y - _offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column,row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private void PlaceGoals()
    {
        _entityPlacer.PlacedEntities -= PlaceGoals;

        _goalPlacer.PlaceKey(roomsList);
    }

    private Vector2Int GetBoundsCenter(BoundsInt boundsToChange)
    {
        return (Vector2Int)Vector3Int.RoundToInt(boundsToChange.center);
    }
}

