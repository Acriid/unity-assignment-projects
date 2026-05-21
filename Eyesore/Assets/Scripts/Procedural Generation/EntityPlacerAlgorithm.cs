using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EntityPlacerAlgorithm : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _enemyObject;
    [SerializeField] private InputReaderSO _inputReaderSO;
    [SerializeField] private Tilemap _floorTileMap;
    public event Action PlacedEntities;
    private Vector3Int enemyPosition = Vector3Int.zero;
    private Vector3Int playerPosition = Vector3Int.zero;
    private bool _placedEntities = false;
    private List<BoundsInt> _copyList;
    void OnEnable()
    {
        _inputReaderSO.OnToggleLight += CheckOutOfBounds;
    }
    void OnDisable()
    {
        _inputReaderSO.OnToggleLight -= CheckOutOfBounds;
    }
    public void PlaceEntities(List<BoundsInt> roomsList)
    {
        if(_enemyObject.activeSelf) _enemyObject.SetActive(false);
        Vector3Int bestPosition = Vector3Int.zero;

        float bestDistance = 0f;

        _copyList = new(roomsList);

        BoundsInt currentRoom = _copyList[Random.Range(0,_copyList.Count -1)];
        playerPosition = Vector3Int.RoundToInt(currentRoom.center);


        for(int i = 0 ; i < _copyList.Count ; i++)
        {
            currentRoom = _copyList[i];
            enemyPosition = Vector3Int.RoundToInt(currentRoom.center);

            float currentDistance = Vector3.Distance(playerPosition,enemyPosition);

            if(currentDistance >= bestDistance)
            {
                bestDistance = currentDistance;
                bestPosition = enemyPosition;
            }
        }



        _playerObject.transform.position = playerPosition;
        _enemyObject.transform.position = bestPosition;

        if(!_enemyObject.activeSelf) _enemyObject.SetActive(true);

        PlacedEntities?.Invoke();
        _placedEntities = true;
    }

    private void CheckOutOfBounds()
    {
        if(!_placedEntities) return;

        Vector3Int cellPositionPlayer = _floorTileMap.WorldToCell(_playerObject.transform.position);

        if(!_floorTileMap.HasTile(cellPositionPlayer))
        {
            _playerObject.transform.position = playerPosition;
        }

        Vector3Int cellPositionEnemy = _floorTileMap.WorldToCell(_enemyObject.transform.position);

        if(!_floorTileMap.HasTile(cellPositionEnemy))
        {
            _enemyObject.transform.position = enemyPosition;
        }
    }
}
