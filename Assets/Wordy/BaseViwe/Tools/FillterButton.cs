using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IFillterUser
{
    void OnFiltere();
}

public class FillterButton : MonoBehaviour, IBaseTools
{


    public GameObject FillterViwe;
    private CanvasGroup CanvasGroup => _CanvasGroup ??= GetComponent<CanvasGroup>();
    private CanvasGroup _CanvasGroup;
    private void Update()
    {
        List<string> kk = new List<string>(TagFilterViwe.TagFillterValues.Keys);
        foreach (string k in kk)
        {
            if (TagFilterViwe.TagFillterValues[k]) 
            {
                CanvasGroup.alpha = 1f;
                return;
            }
        }
        CanvasGroup.alpha = .4f;
    }
    public void OnButton()
    {
        Instantiate(FillterViwe).GetComponent<TagFilterViwe>().OnDone += () =>
        {
            var Liss = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList().Where(a => a is IFillterUser).ToList();
            if (Liss.Count > 0) (Liss[0] as IFillterUser).OnFiltere();
        };
        
    }

    void IBaseTools.Refresh()
    {

        gameObject.SetActive(false);
        var Liss = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList().Where(a => a is IFillterUser).ToList();
        if (Liss.Count > 0) gameObject.SetActive(true);
    }
}
