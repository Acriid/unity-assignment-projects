using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityPlacerAlgorithm : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _enemyObject;
    [SerializeField] private float _minDistance = 50f;

    public void PlaceEntities(List<BoundsInt> roomsList)
    {
        bool foundSpots = false;
        int currentIndex = 0;

        Vector3 playerPosition = Vector3.zero;
        Vector3 enemyPosition = Vector3.zero;


        while(!foundSpots && currentIndex < roomsList.Count)
        {
            BoundsInt currentRoom = roomsList[currentIndex];
            playerPosition = currentRoom.center;

            for(int i = currentIndex + 1 ; i < roomsList.Count ; i++)
            {
                currentRoom = roomsList[i];
                enemyPosition = currentRoom.center;

                float currentDistance = Vector3.Distance(playerPosition,enemyPosition);

                if(currentDistance >= _minDistance)
                {
                    foundSpots = true;
                    break;
                }
            }

        }


        _playerObject.transform.position = playerPosition;
        _enemyObject.transform.position = enemyPosition;
    }
}
