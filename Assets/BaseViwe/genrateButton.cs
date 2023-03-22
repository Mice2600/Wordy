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
    private CanvasGroup CanvasGroup;
    private void Start()
    {
        Value = false;
        CanvasGroup.alpha = .5f;
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    
    public void OnNewViweOpend(BaseListViwe baseList)
    {
        gameObject.SetActive(baseList is IGenrateUser);
    }
    private bool Value;
    public void OnButton()
    {
        Value = !Value;
        CanvasGroup.alpha = (Value) ? 1f : .5f;
        new List<IGenrateUser>(FindObjectsOfType<MonoBehaviour>(true).OfType<IGenrateUser>()).ForEach((a) => a.OnValueChanged(Value));
    }

}
