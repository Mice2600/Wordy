using Base;
using Base.Dialog;
using Base.Word;
using Sirenix.OdinInspector;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActivePasiveButton : MonoBehaviour
{
    public UnityEvent OnActive;
    public UnityEvent OnPassive;
    private ContentObject contentObject;
    private void Start()
    {
        contentObject = gameObject.GetComponentInParent<ContentObject>();
        if(contentObject == null )throw new System.ArgumentNullException(typeof(ContentObject).Name + " not found", transform.name + "'s patent dosnt exits " + typeof(ContentObject).Name);
        if (contentObject.Content == null) return;
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    private void Update()
    {
        if (contentObject == null) throw new System.ArgumentNullException(typeof(ContentObject).Name + " not found", transform.name + "'s patent dosnt exits " + typeof(ContentObject).Name);
        if (contentObject.Content == null) return;
        if ((contentObject.Content as IPersanalData).Active)OnActive?.Invoke();
        else OnPassive?.Invoke();
    }
    public void OnButton() 
    {
        (contentObject.Content as IPersanalData).Active = !(contentObject.Content as IPersanalData).Active;
    }
}
