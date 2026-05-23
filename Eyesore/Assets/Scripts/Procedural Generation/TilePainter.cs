using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTileMap, _wallTileMap, _hideTileMap, _hiddenFloorTileMap, _hiddenWallsTileMap, _toggleWallTileMap;
    [SerializeField] private TileBase _floorTile, _wallTile, _hideTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions,_floorTileMap,_floorTile);
    }
    public void PaintHiddenFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions,_hiddenFloorTileMap,_floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap,tile,position);
        }
    }

    public void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }

    public void Clear()
    {
        _floorTileMap.ClearAllTiles();
        _wallTileMap.ClearAllTiles();
        _hideTileMap.ClearAllTiles();
        _hiddenFloorTileMap.ClearAllTiles();
        _hiddenWallsTileMap.ClearAllTiles();
        _toggleWallTileMap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(_wallTileMap,_wallTile,position);
    }
    internal void PaintSingleHiddenWall(Vector2Int position)
    {
        PaintSingleTile(_hiddenWallsTileMap,_wallTile,position);
    }
    internal void PaintSingleToggleWall(Vector2Int position)
    {
        PaintSingleTile(_toggleWallTileMap,_wallTile,position);
    }  
    public void PaintSingleHidingSpot(Vector2Int position)
    {
        PaintSingleTile(_hideTileMap,_hideTile,position);
    }
}
