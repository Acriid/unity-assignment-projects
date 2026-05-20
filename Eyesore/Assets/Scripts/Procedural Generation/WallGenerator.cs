using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilePainter tilemapPainter)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions,Direction2D.CardinalDirectionsList);
        foreach(var position in basicWallPositions)
        {   
            if(UnityEngine.Random.Range(1,101) == 19)
            {
                tilemapPainter.PaintSingleBasicWall(position);
                tilemapPainter.PaintSingleHidingSpot(position);
            }
            else
            {
                tilemapPainter.PaintSingleBasicWall(position);
            }
            
        }
    }

    public static void CreateWalls(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> hiddenFloorPositions ,TilePainter tilemapPainter )
    {
        var basicWallPositions = FindWallsInDirections(floorPositions,hiddenFloorPositions,Direction2D.CardinalDirectionsList);
        foreach(var position in basicWallPositions)
        {   
            if(UnityEngine.Random.Range(1,101) == 19)
            {
                tilemapPainter.PaintSingleBasicWall(position);
                tilemapPainter.PaintSingleHidingSpot(position);
            }
            else
            {
                tilemapPainter.PaintSingleBasicWall(position);
            }
            
        }

        var toggleWalls = FindToggleWallsInDirections(floorPositions,hiddenFloorPositions,Direction2D.CardinalDirectionsList);
        foreach(var position in toggleWalls)
        {
            tilemapPainter.PaintSingleToggleWall(position);
        }  
    }
    public static void CreateHiddenWalls(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> hiddenFloorPositions ,TilePainter tilemapPainter)
    {
        var basicWallPositions = FindWallsInDirections(hiddenFloorPositions,floorPositions,Direction2D.CardinalDirectionsList);
        foreach(var position in basicWallPositions)
        {   
            tilemapPainter.PaintSingleHiddenWall(position);
        }  
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if(!floorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> hiddenFloorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if(!floorPositions.Contains(neighbourPosition) && !hiddenFloorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
    private static HashSet<Vector2Int> FindToggleWallsInDirections(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> hiddenFloorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if(!floorPositions.Contains(neighbourPosition) && hiddenFloorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions; 
    }
}
