using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface ICreatNewUser
{
    public void OnButton();
}
public class CreatNewButton : MonoBehaviour, IBaseTools
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    
    public void OnButton()
    {
        var Liss = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList().Where(a => a is ICreatNewUser).ToList();
        if(Liss.Count > 0) (Liss[0] as ICreatNewUser).OnButton();
    }

    void IBaseTools.Refresh()
    {

        gameObject.SetActive(false);
        var Liss = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList().Where(a => a is ICreatNewUser).ToList();
        if (Liss.Count > 0) gameObject.SetActive(true);
    }

}
