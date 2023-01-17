using Base;
using Base.Dialog;
using Base.Word;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActivePasiveButton : MonoBehaviour
{
    public UnityEvent OnActive;
    public UnityEvent OnPassive;
    private void Start()
    {
        ContentObject contentObject = gameObject.GetComponentInParent<ContentObject>();
        if(contentObject == null )throw new System.ArgumentNullException(typeof(ContentObject).Name + " not found", transform.name + "'s patent dosnt exits " + typeof(ContentObject).Name);
        if (contentObject.Content == null) return;
        contentObject.OnValueChanged +=
        (IContent C) => {
            if (C.Active) OnActive?.Invoke();
            else OnPassive?.Invoke();
        };
        if (contentObject.Content.Active) OnActive?.Invoke();
        else OnPassive?.Invoke();
        if (contentObject.Content is Word) 
            GetComponent<Button>().onClick.AddListener(() => {
                Word Old = (contentObject.Content as Word?).Value;
                Word NEW = new Word(Old.EnglishSource, Old.RussianSource, Old.Score, !Old.Active, Old.EnglishDiscretion, Old.EnglishDiscretion);
                contentObject.Content = NEW;
                if (WordBase.Wordgs.Contains(Old)) { WordBase.Wordgs[Old] = NEW; Debug.Log("Waa"); }
            });
        else if (contentObject.Content is Dialog) 
            GetComponent<Button>().onClick.AddListener(() => {
                Dialog Old = (contentObject.Content as Dialog?).Value;
                Dialog NEW = new Dialog(Old.EnglishSource, Old.RussianSource, Old.Score, !Old.Active);
                contentObject.Content = NEW;
                if (DialogBase.Dialogs.Contains(Old)) DialogBase.Dialogs[Old] = NEW;
            });
    }
}
