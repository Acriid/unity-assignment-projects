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
    private DialogObject _objectInstance;
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

        //Mybe later dialogId changes form a save
    }

    private void StartDialog()
    {
        _objectInstance = _dialogPool.Get();
        _objectInstance.DialogSO = _dialogSOs.Find(listItem => listItem.DialogID == _currentDialogID);
        _objectInstance.ShowDialog();
    }
}
