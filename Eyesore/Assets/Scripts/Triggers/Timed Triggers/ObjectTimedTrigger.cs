using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimedTrigger : TimedTrigger
{
    [SerializeField] private List<GameObject> _enableObjects = new();
    [SerializeField] private List<GameObject> _disableObjects = new();
    protected override void TimedTriggered()
    {
        foreach(GameObject enableObject in _enableObjects)
        {
            enableObject.SetActive(true);
        }

        foreach(GameObject disableObject in _disableObjects)
        {
            disableObject.SetActive(false);
        }

        base.TimedTriggered();
    }
    protected override void Reset()
    {
        foreach(GameObject enableObject in _enableObjects)
        {
            enableObject.SetActive(false);
        }

        foreach(GameObject disableObject in _disableObjects)
        {
            disableObject.SetActive(true);
        }
    }
}
