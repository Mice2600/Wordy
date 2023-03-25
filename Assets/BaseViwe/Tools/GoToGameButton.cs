using EnhancedScrollerDemos.FlickSnap;
using EnhancedUI.EnhancedScroller;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToGameButton : MonoBehaviour, IBaseToolItem
{

    [Required, SerializeField]
    private EnhancedScroller scroller;
    [Required, SerializeField]
    private FlickSnap flic;

    public void OnNewViweOpend(GameObject baseList)
    {
        gameObject.SetActive(!baseList.TryGetComponent(out GameBaseViwe gameBaseViwe));
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { flic.JumpToDataIndex(100);});
    }
}
