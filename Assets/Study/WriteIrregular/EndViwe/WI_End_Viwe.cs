using Base;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class WI_End_Viwe : MonoBehaviour
{
    public GameObject TrueSinglePrefab;
    public GameObject FalseSinglePrefab;
    public Transform ConParrent;
    private System.Action OnFinsh;
    public void Set(List<(Content content, bool isCurrect, string Answer, int Score)> Contents, System.Action OnFinsh) 
    {
        ConParrent.ClearChilds();
        this.OnFinsh = OnFinsh;
        for (int i = 0; i < Contents.Count; i++)
        {
            GameObject d = null;
            if (Contents[i].isCurrect) d = Instantiate(TrueSinglePrefab, ConParrent);
            else d = Instantiate(FalseSinglePrefab, ConParrent);
            d.GetComponent<ContentObject>().Content = Contents[i].content;
            if (Contents[i].isCurrect) d.GetComponent<WI_End_SingelTrue>().Set(Contents[i].Score);
            else d.GetComponent<WI_End_SingelFalse>().Set(Contents[i].Score, Contents[i].Answer);
        }
    }
    public void OnSelfDestroy() 
    {
        Destroy(gameObject);
        OnFinsh?.Invoke();
    }
    
}
