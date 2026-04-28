using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTileMap, _wallTileMap, _hideTileMap;
    [SerializeField] private TileBase _floorTile, _wallTile, _hideTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions,_floorTileMap,_floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap,tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }

    public void Clear()
    {
        _floorTileMap.ClearAllTiles();
        _wallTileMap.ClearAllTiles();
        _hideTileMap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(_wallTileMap,_wallTile,position);
    }
    public void PaintSingleHidingSpot(Vector2Int position)
    {
        PaintSingleTile(_hideTileMap,_hideTile,position);
    }
}
