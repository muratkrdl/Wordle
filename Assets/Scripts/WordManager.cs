using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] string origin;

    public List<State> GetStates(string msg)
    {
        List<State> result = new List<State>();

        var arrayOrigin = origin.ToCharArray().ToList();
        var current = msg.ToCharArray().ToList();

        for(int i = 0; i < current.Count; i++)
        {
            char item = current[i];
            var contains = arrayOrigin.Contains(item);
            if(contains)
            {
                var index = arrayOrigin.FindIndex(x => x == item);
                var isCorrectt = index == i;
                result.Add(isCorrectt ? State.Correct : State.Exist);
            }
            else
            {
                result.Add(State.Fail);
            }
        }
        return result;
    }

}
