using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using NUnit.Framework.Constraints;


public class LightPlacingAlgorithm : MonoBehaviour
{
    [SerializeField] private GameObject _lightPrefab;
    [SerializeField] private Transform _lightParent;
    [Range(0,1)] 
    [SerializeField] private float _lightToRoomRatio = 0.7f;
    [SerializeField] private int _lightsPerRoom = 2;
    

    private List<GameObject> _lightList = new();

    public void PlaceLights(HashSet<BoundsInt> roomsToLight, int offset)
    {
        int numberOfLights = Random.Range(Mathf.RoundToInt(roomsToLight.Count * _lightToRoomRatio),roomsToLight.Count);
        Queue<BoundsInt> roomsQueue = new();
        HashSet<BoundsInt> cloneRoomsList = new(roomsToLight);

        foreach(BoundsInt boundsInt in cloneRoomsList)
        {
            //Debug.Log($"Min Before: {boundsInt.min}, Max Before: {boundsInt.max}");
            boundsInt.SetMinMax(new(boundsInt.xMin + offset,boundsInt.yMin + offset),
            new(boundsInt.xMax - offset,boundsInt.yMax - offset));
            //Debug.Log($"Min After: {boundsInt.min}, Max After: {boundsInt.max}");
        }


        foreach(BoundsInt bounds in cloneRoomsList)
        {
            roomsQueue.Enqueue(bounds);
        }


        for(int i = 0 ; i < numberOfLights ; i++)
        {
            if(roomsQueue.Count > 0)
            {
                BoundsInt currentRoom = roomsQueue.Dequeue();
                int xOffset = Mathf.RoundToInt(currentRoom.size.x/5f);
                int yOffset = Mathf.RoundToInt(currentRoom.size.y/5f);
                for(int j = 0; j < _lightsPerRoom ; j++)
                {
                    int xPosition = Random.Range(currentRoom.min.x + xOffset, currentRoom.max.x - xOffset);
                    int yPosition = Random.Range(currentRoom.min.y + yOffset, currentRoom.max.y - yOffset);

                    Vector3Int lightPosition = new(xPosition,yPosition,0);

                    _lightList.Add(Instantiate(_lightPrefab,lightPosition,Quaternion.identity,_lightParent));                     
                }

            }
            else
            {
                break;
            }

        }


    }

    public void ClearLights()
    {
        foreach(GameObject light in _lightList)
        {
            Destroy(light);
        }
    }
}
