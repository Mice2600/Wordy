using Newtonsoft.Json.Linq;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ISearchUser 
{
    public void OnSearchStarted();
    public void OnSearchEnded();
    public void OnValueChanged(string Value);
}
public class SearchViwe : MonoBehaviour, IBaseToolItem
{
    public System.Action OnSearchStarted;
    public System.Action OnSearchEnded;
    public System.Action<string> OnValueChanged;
    [Required, SerializeField]
    private TMPro.TMP_InputField InputField;
    public bool IsSearching =>  gameObject.activeSelf;
    public string SearchingString => InputField.text;
    public void OnShearchValueChanged(string Value)
    {
        InputField.text = Value.ToUpper();
        OnValueChanged?.Invoke(Value);
        if (CorrentUser != null) CorrentUser.OnValueChanged(Value);
    }

    private void OnEnable()
    {
        OnSearchStarted?.Invoke();
        if (CorrentUser != null) CorrentUser.OnSearchStarted();
    }
    private void OnDisable()
    {
        OnSearchEnded?.Invoke();
        if(CorrentUser != null) CorrentUser.OnSearchEnded();
    }
    ISearchUser CorrentUser;
    public void OnNewViweOpend(BaseListViwe baseList)
    {
        CorrentUser = (baseList as ISearchUser);
        if (CorrentUser != null && IsSearching) 
        {
            CorrentUser.OnSearchStarted();
            CorrentUser.OnValueChanged(InputField.text);
        }
        
        if (CorrentUser != null && !IsSearching) CorrentUser.OnSearchEnded();
    }
}
