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
    private List<DialogObject> _objectInstance;
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
        StartDialog();
        //Mybe later dialogId changes from a save
    }
    private void ShowDialog(int dialogIndex)
    {
       // abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ;
    }
    private void StartDialog()
    {
        _objectInstance[0] = _dialogPool.Get();
        _objectInstance[0].DialogSO = _dialogSOs.Find(listItem => listItem.DialogID == _currentDialogID);
        //_objectInstance.SetInstantText(true);

        _objectInstance[0].ShowDialog();
        _objectInstance[0].OnTextFinished += RemoveDialog;
    }
    private void RemoveDialog()
    {
        _objectInstance[0].OnTextFinished -= RemoveDialog;
        _dialogPool.Return(_objectInstance[0]);
    }
}
