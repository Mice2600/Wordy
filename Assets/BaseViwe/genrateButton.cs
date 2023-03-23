using Base;
using Newtonsoft.Json.Linq;
using Servises.BaseList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface IGenrateUser 
{
    public void OnValueChanged(bool value);
}
[RequireComponent(typeof(Button), typeof(CanvasGroup))]
public class genrateButton : MonoBehaviour, IBaseToolItem
{
    private CanvasGroup CanvasGroup => _CanvasGroup ??= GetComponent<CanvasGroup>();
    private CanvasGroup _CanvasGroup;
    private void Start()
    {
        Value = false;
        CanvasGroup.alpha = .5f;
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    IGenrateUser CorrentUser;
    public void OnNewViweOpend(BaseListViwe baseList)
    {
        gameObject.SetActive(baseList is IGenrateUser);
        CorrentUser = baseList as IGenrateUser;
        if (CorrentUser != null) CorrentUser.OnValueChanged(Value);
    }
    private bool Value;
    public void OnButton()
    {
        Value = !Value;
        CanvasGroup.alpha = (Value) ? 1f : .5f;
        if (CorrentUser != null) CorrentUser.OnValueChanged(Value);
        
    }
}
