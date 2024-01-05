using EnhancedScrollerDemos.FlickSnap;
using EnhancedUI.EnhancedScroller;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FillterButton : MonoBehaviour, IBaseToolItem
{
    public void OnNewViweOpend(GameObject baseList)
    {
        gameObject.SetActive(baseList.TryGetComponent(out BaseListWithFillter ListWithFillter));
        this.ListWithFillter = ListWithFillter;
    }
    BaseListWithFillter ListWithFillter;
    public GameObject FillterViwe;
    private CanvasGroup CanvasGroup => _CanvasGroup ??= GetComponent<CanvasGroup>();
    private CanvasGroup _CanvasGroup;
    private void Update()
    {
        List<string> kk = new List<string>(BaseListWithFillter.TagFillterValues.Keys);
        foreach (string k in kk)
        {
            if (BaseListWithFillter.TagFillterValues[k]) 
            {
                CanvasGroup.alpha = 1f;
                return;
            }
        }
        CanvasGroup.alpha = .4f;
    }
    public void OnButton()
    {
        if (ListWithFillter == null) return;
        Instantiate(FillterViwe).GetComponent<TagFilterViwe>().OnDone += ListWithFillter.OnFilter;
    }
}
