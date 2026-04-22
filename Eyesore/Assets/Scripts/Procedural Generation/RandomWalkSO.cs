using UnityEngine;

[CreateAssetMenu(fileName = "AlgorithmSettings", menuName = "AlgorithmSettings/RandomWalk")]
public class RandomWalkSO : ScriptableObject
{
    public int Iterations = 10;
    public int WalkLength = 10;
    public bool StartRandomlyEachIteration = true;
}
