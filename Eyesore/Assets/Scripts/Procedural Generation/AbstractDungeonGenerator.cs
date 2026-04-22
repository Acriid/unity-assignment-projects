using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilePainter _tilePainter = null;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        _tilePainter.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();

}
