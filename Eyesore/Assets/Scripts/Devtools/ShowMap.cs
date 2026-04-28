using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour
{
    public InputReaderSO InputReaderSO;
    public CinemachineCamera CinemachineCamera;
    private int _originalSize;
    public int NewSize = 100;
    public SprintMechanic SprintMechanic;
    public CrouchMechanic CrouchMechanic;
    public GameObject Lights;
    public GameObject GlobalLight;
    void OnEnable()
    {
        InputReaderSO.OnMapShow += OnMapShow;
    }

    private void OnMapShow()
    {
        if(CinemachineCamera.Lens.OrthographicSize == NewSize)
        {
            SprintMechanic.shouldUpdateZoom = true;
            CrouchMechanic.shouldUpdateZoom = true;
            Lights.SetActive(true);
            GlobalLight.SetActive(false);
            CinemachineCamera.Lens.OrthographicSize = _originalSize;
            return;
        }
        Lights.SetActive(false);
        GlobalLight.SetActive(true);
        SprintMechanic.shouldUpdateZoom = false;
        CrouchMechanic.shouldUpdateZoom = false;
        CinemachineCamera.Lens.OrthographicSize = NewSize;
    }

    void OnDisable()
    {
        InputReaderSO.OnMapShow -= OnMapShow;
    }
}
