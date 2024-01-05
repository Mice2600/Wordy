using Newtonsoft.Json.Linq;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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
    [Required]
    public GameObject OpenButton;
    public void OnShearchValueChanged(string Value)
    {
        if (!SystemBox.Simpls.Comand.OneFream("Search")) return;
        InputField.text = Value.ToUpper();
        OnValueChanged?.Invoke(Value);
        if (CorrentUser != null) CorrentUser.OnValueChanged(Value);
    }

    private void OnEnable()
    {
        OnSearchStarted?.Invoke();
        if (CorrentUser != null) CorrentUser.OnSearchStarted();

        StartCoroutine(dd());
        IEnumerator dd() 
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(gameObject);
            InputField.OnSelect(new BaseEventData(EventSystem.current));
        }
        
    }
    private void OnDisable()
    {
        OnSearchEnded?.Invoke();
        if(CorrentUser != null) CorrentUser.OnSearchEnded();
    }
    ISearchUser CorrentUser;
    public void OnNewViweOpend(GameObject baseList)
    {
        var r = baseList.GetComponent<ISearchUser>();

        OpenButton.SetActive(r != null);

        if (r == null) 
        {
            gameObject.SetActive(false);
            InputField.text = "";
            return;
        }

        CorrentUser = r;
        if (CorrentUser != null && IsSearching) 
        {
            CorrentUser.OnSearchStarted();
            CorrentUser.OnValueChanged(InputField.text);
        }
        
        if (CorrentUser != null && !IsSearching) CorrentUser.OnSearchEnded();
    }
}
