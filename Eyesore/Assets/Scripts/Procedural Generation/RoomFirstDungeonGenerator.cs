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

        _puzzlePlacer.PlacePuzzleRoom(roomsList,4);
        if(_randomWalkRooms)
        {
            floor = CreateRoomsRandomWalk(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        

        


        //Corridors
        List<Vector2Int> roomCenters = new();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        
        _tilePainter.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor,_tilePainter);

        if(_placeLights)
        {
            _lightPlacer.ClearLights();


            HashSet<BoundsInt> roomsRandom = new();
            roomsRandom.UnionWith(roomsList);
            _lightPlacer.PlaceLights(roomsRandom);
        }


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
    private HashSet<Vector2Int> SecretConnectRooms(List<BoundsInt> rooms, HashSet<Vector2Int> currentConnections)
    {
        List<Vector2Int> roomCenters = new();

        foreach(BoundsInt room in rooms)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        

        return null;
    }
    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {

        //Currently Gets random room and then connects to other rooms from the random room
        HashSet<Vector2Int> corridors = new();
        var currentRoomCenter = roomCenters[Random.Range(0,roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);
    
        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter,roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter,closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
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
}



/*// Represents a directional connection between two rooms.
// StartingDestination updates to the last intermediate room if the corridor crosses one.
public struct RoomConnection
{
    public Vector2Int StartingDestination;
    public Vector2Int EndingDestination;
}

// All room connections established during generation
private List<RoomConnection> roomConnections = new();

// Connections that passed CanConnectRooms validation
private List<RoomConnection> validConnections = new();

private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
{
    //Currently Gets random room and then connects to other rooms from the random room
    HashSet<Vector2Int> corridors = new();
    var currentRoomCenter = roomCenters[Random.Range(0,roomCenters.Count)];
    roomCenters.Remove(currentRoomCenter);

    // Snapshot all room centers before any are consumed, used for through-room detection
    List<Vector2Int> allRoomCenters = new List<Vector2Int>(roomCenters) { currentRoomCenter };

    while (roomCenters.Count > 0)
    {
        Vector2Int closest = FindClosestPointTo(currentRoomCenter,roomCenters);
        roomCenters.Remove(closest);
        HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter,closest);
        // Capture the start before currentRoomCenter is overwritten
        Vector2Int corridorStart = currentRoomCenter;
        currentRoomCenter = closest;
        corridors.UnionWith(newCorridor);
        // Record the connection, shifting StartingDestination if the corridor crosses another room
        roomConnections.Add(BuildRoomConnection(corridorStart, closest, newCorridor, allRoomCenters));
    }
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

// Builds a RoomConnection and shifts StartingDestination to the last room the corridor crosses.
private RoomConnection BuildRoomConnection(Vector2Int start, Vector2Int end,
    HashSet<Vector2Int> corridor, List<Vector2Int> allRoomCenters)
{
    RoomConnection connection = new RoomConnection
    {
        StartingDestination = start,
        EndingDestination   = end
    };

    // Find any intermediate rooms the corridor walks through, ordered by traversal
    List<Vector2Int> intermediates = GetIntermediateRoomsInOrder(start, end, allRoomCenters, corridor);

    // Use the last crossed room as the effective start of this connection
    if (intermediates.Count > 0)
        connection.StartingDestination = intermediates[intermediates.Count - 1];

    return connection;
}

// Returns intermediate room centers the corridor walks through, in path order.
// Mirrors CreateCorridor's vertical-then-horizontal movement to preserve order.
private List<Vector2Int> GetIntermediateRoomsInOrder(Vector2Int start, Vector2Int end,
    List<Vector2Int> allRoomCenters, HashSet<Vector2Int> corridor)
{
    List<Vector2Int> intermediates = new();
    var position = start;

    // Vertical segment
    while (position.y != end.y)
    {
        position += (end.y > position.y) ? Vector2Int.up : Vector2Int.down;
        foreach (var room in allRoomCenters)
        {
            if (room != start && room != end && room == position && !intermediates.Contains(room))
                intermediates.Add(room);
        }
    }

    // Horizontal segment
    while (position.x != end.x)
    {
        position += (end.x > position.x) ? Vector2Int.right : Vector2Int.left;
        foreach (var room in allRoomCenters)
        {
            if (room != start && room != end && room == position && !intermediates.Contains(room))
                intermediates.Add(room);
        }
    }

    return intermediates;
}

// Returns true if the corridor passes through any room other than start or end.
private bool CorridorPassesThroughRoom(HashSet<Vector2Int> corridor, Vector2Int start,
    Vector2Int end, List<Vector2Int> allRoomCenters)
{
    foreach (var room in allRoomCenters)
    {
        if (room == start || room == end) continue;
        if (corridor.Contains(room)) return true;
    }
    return false;
}

// Returns true if roomA and roomB share a direct entry in roomConnections.
private bool AreRoomsDirectlyConnected(Vector2Int roomA, Vector2Int roomB)
{
    foreach (var connection in roomConnections)
    {
        bool forward  = connection.StartingDestination == roomA && connection.EndingDestination == roomB;
        bool backward = connection.StartingDestination == roomB && connection.EndingDestination == roomA;
        if (forward || backward) return true;
    }
    return false;
}

// Returns all rooms that share a direct connection with the given room.
private List<Vector2Int> GetConnectedRooms(Vector2Int room)
{
    List<Vector2Int> connected = new();
    foreach (var connection in roomConnections)
    {
        if (connection.StartingDestination == room && !connected.Contains(connection.EndingDestination))
            connected.Add(connection.EndingDestination);
        else if (connection.EndingDestination == room && !connected.Contains(connection.StartingDestination))
            connected.Add(connection.StartingDestination);
    }
    return connected;
}

// Checks whether two rooms can be validly connected without the corridor crossing another room.
// If a direct path is invalid, tries pairs of rooms adjacent to each target room instead.
// Valid connections are stored in validConnections.
private bool CanConnectRooms(Vector2Int roomA, Vector2Int roomB, List<Vector2Int> allRoomCenters)
{
    // No need to connect already-connected rooms
    if (AreRoomsDirectlyConnected(roomA, roomB))
        return false;

    // Try a direct corridor first
    HashSet<Vector2Int> directCorridor = CreateCorridor(roomA, roomB);
    if (!CorridorPassesThroughRoom(directCorridor, roomA, roomB, allRoomCenters))
    {
        validConnections.Add(new RoomConnection { StartingDestination = roomA, EndingDestination = roomB });
        return true;
    }

    // Direct path is blocked; check rooms neighbouring A and B for a clear indirect route
    List<Vector2Int> adjacentToA = GetConnectedRooms(roomA);
    List<Vector2Int> adjacentToB = GetConnectedRooms(roomB);

    foreach (var adjA in adjacentToA)
    {
        foreach (var adjB in adjacentToB)
        {
            // Skip identical or already-connected pairs
            if (adjA == adjB || AreRoomsDirectlyConnected(adjA, adjB)) continue;

            HashSet<Vector2Int> testCorridor = CreateCorridor(adjA, adjB);
            if (!CorridorPassesThroughRoom(testCorridor, adjA, adjB, allRoomCenters))
            {
                validConnections.Add(new RoomConnection { StartingDestination = adjA, EndingDestination = adjB });
                return true;
            }
        }
    }

    return false;
}*/
