using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance {get; private set;}
    [SerializeField] private List<DialogSO> _dialogSOs;
    [SerializeField] private GameObject _dialogPrefab;
    [SerializeField] private GameObject _poolParent;
    [SerializeField] private int _poolSize;


    private GenericPool<DialogObject> _dialogPool;
    private Dictionary<int,DialogObject> _objectDictionary = new();
    private int _currentDialogID = 1;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _dialogPool = PoolManager.Instance.GetPool<DialogObject>(_dialogPrefab,_poolSize);
        if(_dialogPool == null)
        {
            Debug.Log("Failed to load pool");
        }
        DialogTester();
        //ShowDialog(_currentDialogID);
        //Mybe later dialogId changes from a save
    }
    private void DialogTester()
    {
        ShowDialog(1);
        ShowDialog(2);
    }
    private void ShowDialog(int dialogIndex)
    {
       _objectDictionary[dialogIndex] = _dialogPool.Get();
       _objectDictionary[dialogIndex].DialogSO = _dialogSOs[dialogIndex -1];
       _objectDictionary[dialogIndex].OnTextFinished += RemoveDialog;
       _objectDictionary[dialogIndex].ShowDialog();

    }

    private void RemoveDialog(int dialogIndex)
    {
        _objectDictionary[dialogIndex].OnTextFinished -= RemoveDialog;
        _objectDictionary[dialogIndex] = null;
        _objectDictionary.Remove(dialogIndex);
    }
}
