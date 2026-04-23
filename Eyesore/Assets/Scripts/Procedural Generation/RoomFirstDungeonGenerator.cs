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
    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)_startPosition,
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

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
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
        //For decoration save each room in a separate HashSet
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
}
