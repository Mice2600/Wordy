using EnhancedScrollerDemos.FlickSnap;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillterButton : MonoBehaviour, IBaseToolItem
{
    public void OnNewViweOpend(GameObject baseList)
    {
        gameObject.SetActive(!baseList.TryGetComponent(out GameBaseViwe gameBaseViwe));
    }

    private void Start()
    {
        
    }
}
