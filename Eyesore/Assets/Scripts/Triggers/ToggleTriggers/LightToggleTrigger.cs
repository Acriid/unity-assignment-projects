using System.Collections.Generic;
using UnityEngine;

public class LightToggleTrigger : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private List<GameObject> _enableObjects;
    [SerializeField] private List<GameObject> _disableObjects;
    void OnEnable()
    {
        _inputReader.OnToggleLight += OnLightToggle;
    }
    void OnDisable()
    ///
    {
        _inputReader.OnToggleLight -= OnLightToggle;
    }
    private void OnLightToggle()
    {
        foreach(GameObject enableObject in _enableObjects)
        {
            enableObject.SetActive(!enableObject.activeSelf);
        }
        foreach(GameObject disableObjects in _disableObjects)
        {
            disableObjects.SetActive(!disableObjects.activeSelf);
        }
    }
}
