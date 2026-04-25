using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;

public class GoalPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _keyObject;
    [SerializeField] private GameObject _goalObject;
    [SerializeField] private GameObject _playerObject;
    public void PlaceKey(List<BoundsInt> roomsList)
    {
        Vector3 randomKeyPosition = roomsList[Random.Range(0,roomsList.Count -1)].center;

        _keyObject.transform.position = randomKeyPosition;

        PlaceObjective(roomsList);
    }

    private void PlaceObjective(List<BoundsInt> roomsList)
    {


        List<BoundsInt> checkList = roomsList;

        
        Vector3 playerPosition = _playerObject.transform.position;
        Vector3 bestPosition = playerPosition;
        Vector3 keyPosition = _keyObject.transform.position;
        BoundsInt bestRoom = new();

        float bestDistance = 0f;


        int failSafe = 0;

        while(bestPosition == playerPosition && checkList.Count > 0 && failSafe < 1000)
        {
            for(int i = 0 ; i < checkList.Count ; i++)
            {
                BoundsInt currentRoom = checkList[i];
                Vector3 goalPosition = currentRoom.center;

                float currentDistance = Vector3.Distance(goalPosition,keyPosition);

                if(currentDistance >= bestDistance)
                {
                    bestDistance = currentDistance;
                    bestPosition = goalPosition;
                    bestRoom = currentRoom;
                }
            }
            checkList.Remove(bestRoom);
            failSafe++;
        }



        _goalObject.transform.position = bestPosition;
    }
}
