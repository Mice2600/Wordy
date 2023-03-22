using Servises.BaseList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class BaseCountText : MonoBehaviour, IBaseToolItem
{
    private TMP_Text text => _text ??= GetComponent<TMP_Text>();
    private TMP_Text _text;
    public void OnNewViweOpend(BaseListViwe baseList)
    {
        int Count = 0;
        int ActiveCount = 0;
        new List<Content>((baseList as BaseListWithFillter).AllContents).ForEach((a) =>{Count++;
            if (a is IPersanalData && (a as IPersanalData).Active) ActiveCount++;
        });
        if (Count == 0) GetComponent<TMP_Text>().text = "";
        else if (ActiveCount > 0) GetComponent<TMP_Text>().text = ActiveCount + "/" +Count;
        else GetComponent<TMP_Text>().text = Count.ToString();
    }
}
