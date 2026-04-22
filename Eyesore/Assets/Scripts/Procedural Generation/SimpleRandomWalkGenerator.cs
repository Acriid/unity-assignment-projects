using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{
    public RandomWalkSO RandomWalkSO;
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();

        foreach (var position in floorPositions)
        {
            Debug.Log(position);
        }
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = _startPosition;
        HashSet<Vector2Int> floorPositions = new();
        for(int i = 0; i < RandomWalkSO.Iterations; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, RandomWalkSO.WalkLength);

            floorPositions.UnionWith(path);

            if(RandomWalkSO.StartRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
