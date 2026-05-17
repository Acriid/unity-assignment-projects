using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance {get; private set;}
    [SerializeField] private DialogSO _startDialog;
    [SerializeField] private GameObject _dialogPrefab;
    [SerializeField] private int _poolSize;


    private GenericPool<DialogObject> _dialogPool;
    private Dictionary<DialogSO,DialogObject> _objectDictionary = new();

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



        ShowDialog(_startDialog);
        //Mybe later dialogId changes from a save
    }
    void OnDisable()
    {
        _objectDictionary.Clear();
        _dialogPool.ReturnAll();
    }
    public void ShowDialog(DialogSO dialogSO)
    {
        if(dialogSO == null) return;
        
        List<DialogSO> checkList = new();
        dialogSO.GetNextDialogList(ref checkList);
        foreach(DialogSO checkDialog in checkList)
        {
            if(_objectDictionary.TryGetValue(checkDialog, out DialogObject NullTest)) return;
        }
        

        Debug.Log($"Showing Dialog {dialogSO.name}");

        _objectDictionary[dialogSO] = _dialogPool.Get();
        _objectDictionary[dialogSO].DialogSO = dialogSO;


        _objectDictionary[dialogSO].ShowDialog();
        _objectDictionary[dialogSO].OnTextFinished += RemoveDialog;
    }
    public void StopDialog(DialogSO dialogSO)
    {
        bool noMoreNext = false;
        while(!noMoreNext)
        {
            if(_objectDictionary.TryGetValue(dialogSO, out DialogObject dialogObject))
            {
                
            }
        }
    }

    private void RemoveDialog(DialogSO dialogSO)
    {
        if(!_objectDictionary.ContainsKey(dialogSO))
        {
            Debug.Log("Something went wrong with the dialog objects");
            return;
        }

        _objectDictionary[dialogSO].OnTextFinished -= RemoveDialog;

        if(dialogSO.NextDialog != null)
        {

            ShowDialog(dialogSO.NextDialog);

        }
        _dialogPool.Return(_objectDictionary[dialogSO]);

        _objectDictionary[dialogSO] = null;
        _objectDictionary.Remove(dialogSO);
    }

}

