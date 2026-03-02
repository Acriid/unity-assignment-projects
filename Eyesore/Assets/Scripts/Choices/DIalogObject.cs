using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DialogObject : MonoBehaviour
{
//Will be used to hold the dialog within the worlds
    public DialogSO DialogSO;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private bool _instantText = false;
    void Awake()
    {
        if(_tmpText == null)
            _tmpText = GetComponent<TMP_Text>();
    }
    public void ShowDialog()
    {
        if(_instantText)
            _tmpText.text = DialogSO.DialogText;

    }
    private IEnumerator DialogTyper()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
