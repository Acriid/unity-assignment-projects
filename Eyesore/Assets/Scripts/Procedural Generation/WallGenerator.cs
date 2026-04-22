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
            tilemapPainter.PaintSingleBasicWall(position);
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
}
