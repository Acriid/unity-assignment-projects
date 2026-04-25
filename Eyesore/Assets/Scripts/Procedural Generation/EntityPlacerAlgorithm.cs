using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityPlacerAlgorithm : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _enemyObject;
    public event Action PlacedEntities;

    public void PlaceEntities(List<BoundsInt> roomsList)
    {

        Vector3 enemyPosition = Vector3.zero;

        float bestDistance = 0f;


        BoundsInt currentRoom = roomsList[Random.Range(0,roomsList.Count -1)];
        Vector3 playerPosition = currentRoom.center;

        for(int i = 0 ; i < roomsList.Count ; i++)
        {
            currentRoom = roomsList[i];
            enemyPosition = currentRoom.center;

            float currentDistance = Vector3.Distance(playerPosition,enemyPosition);

            if(currentDistance >= bestDistance)
            {
                bestDistance = currentDistance;

            }
        }



        _playerObject.transform.position = playerPosition;
        _enemyObject.transform.position = enemyPosition;

        PlacedEntities?.Invoke();
    }
}
