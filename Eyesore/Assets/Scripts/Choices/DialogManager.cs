using System.Collections.Generic;
using System.Linq;
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
    private Dictionary<int,bool> _flowingDialog = new();

    void Awake()
    {
        Time.timeScale = 1;
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
        _dialogSOs = _dialogSOs.OrderBy(dialog => dialog.DialogID).ToList();



        ShowDialog(_currentDialogID);
        //Mybe later dialogId changes from a save
    }
    void OnDisable()
    {
        _objectDictionary.Clear();
        _dialogPool.ReturnAll();
    }
    public void ShowDialog(int dialogIndex)
    {
        if(dialogIndex == 0) return;

       _objectDictionary[dialogIndex] = _dialogPool.Get();
       _objectDictionary[dialogIndex].DialogSO =_dialogSOs[dialogIndex -1];

       if(_objectDictionary[dialogIndex].DialogSO.FlowingDialog)
        {
            _flowingDialog[dialogIndex] = true;
            
        }

       
       _objectDictionary[dialogIndex].ShowDialog();
       _objectDictionary[dialogIndex].OnTextFinished += RemoveDialog;

    }

    private void RemoveDialog(int dialogIndex)
    {
        if(!_objectDictionary.ContainsKey(dialogIndex))
        {
            Debug.LogError("Something went wrong with the index");
            return;
        }
        _objectDictionary[dialogIndex].OnTextFinished -= RemoveDialog;
        if(_flowingDialog.TryGetValue(dialogIndex,out bool _flowing))
        {
            if(_flowing)
            {
                _flowingDialog[dialogIndex] = false;
                ShowDialog(_objectDictionary[dialogIndex].DialogSO.NextDialogID); 
            }
        }
        _dialogPool.Return(_objectDictionary[dialogIndex]);

        _objectDictionary[dialogIndex] = null;
        _objectDictionary.Remove(dialogIndex);
    
    }

    public void LoadData(DialogSaveData data)
    {
        _currentDialogID = data.StartDialogID;
    }
}

[System.Serializable]
public struct DialogSaveData
{
    public int StartDialogID;
}
