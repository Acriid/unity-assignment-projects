using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilePainter _tilePainter = null;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;
    [SerializeField] protected LightPlacingAlgorithm _lightPlacer = null;

    public void GenerateDungeon()
    {
        _tilePainter.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();

}
