using Servises.BaseList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface ICreatNewUser
{
    public void OnButton();
}
public class CreatNewButton : MonoBehaviour, IBaseToolItem
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    ICreatNewUser CorrentUser;
    public void OnNewViweOpend(GameObject baseList)
    {
        var r = baseList.GetComponent<ICreatNewUser>();
        gameObject.SetActive(r != null);
        CorrentUser = r;
    }
    public void OnButton()
    {
        if (CorrentUser != null) CorrentUser.OnButton();
    }
}
