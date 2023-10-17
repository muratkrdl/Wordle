using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentController : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button temp;
    [SerializeField] List<RowController> rows;
    [SerializeField] WordManager wordManager;

    int _index; 

    void Start() 
    {
        inputField.onValueChanged.AddListener(OnUpdateContent);
        inputField.onSubmit.AddListener(onSubmit);
    }

    void OnUpdateContent(string msg)
    {
        var row = rows[_index];
        row.UpdateText(msg);
    }

    bool UpdateState()
    {
        var states = wordManager.GetStates(inputField.text);
        rows[_index].UpdateState(states);

        foreach(var item in states)
        {
            if(item != State.Correct) return false;
        }

        return true;
    }

    void onSubmit(string msg)
    {
        temp.Select();
        inputField.Select();

        if(IsEnough())
        {
            Debug.Log("yetersiz karakter"); return;
        }

        var isWon = UpdateState();
        if(isWon) 
        {
            Debug.Log("kazandınız");
            inputField.enabled = false;
            return; 
        }

        _index++;

        if(_index == rows.Count)
        {
            Debug.Log("kaybettin");
            inputField.enabled = false;
            return;
        }

        inputField.text = "";
    }

    bool IsEnough()
    {
        return inputField.text.Length < rows[_index].CellAmount;
    }

}
