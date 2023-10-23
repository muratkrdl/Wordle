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

    [SerializeField] TextMeshProUGUI winLoseText;

    int _index; 

    void Start() 
    {
        inputField.onValueChanged.AddListener(OnUpdateContent);
        inputField.onSubmit.AddListener(onSubmit);
        winLoseText.gameObject.SetActive(false);
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
            Debug.Log("inadequate character");
            return;
        }

        var isWon = UpdateState();
        if(isWon) 
        {
            winLoseText.gameObject.SetActive(true);
            winLoseText.text = "You Won";
            inputField.enabled = false;
            return; 
        }

        _index++;

        if(_index == rows.Count)
        {
            winLoseText.gameObject.SetActive(true);
            winLoseText.text = "You Lose";
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
