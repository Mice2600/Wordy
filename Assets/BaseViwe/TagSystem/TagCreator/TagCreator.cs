using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TagCreator : MonoBehaviour
{
    private TMPro.TMP_InputField textContainer => _textContainer ??= GetComponentInChildren<TMPro.TMP_InputField>();
    private TMPro.TMP_InputField _textContainer;
    public System.Action OnNewTagCreated;
    private string CurrentValue;
    private void Start()
    {
        textContainer.onValueChanged.AddListener(OnValuechanged);
    }
    public void OnValuechanged(string Value) 
    {
        Value = Value.Replace(" ", "").Replace("/", "").Replace(@"\", "").
            Replace(@"(", "").Replace(@")", "").Replace(@"{", "").Replace(@"}", "").
            Replace(@"[", "").Replace(@"]", "").Replace("@", "").Replace(@"#", "").
            Replace(@"!", "").Replace(@"$", "").Replace(@"%", "").Replace(@"^", "").
            Replace(@"&", "").Replace(@"*", "").Replace(@"-", "").Replace(@"_", "").
            Replace(@"=", "").Replace(@"+", "").Replace(@"`", "").Replace(@"~", "").
            Replace(@"|", "").Replace(@"'", "").Replace('"', ' ').Replace(@";", "").
            Replace(@":", "").Replace(@"?", "").Replace(@".", "").Replace(@",", "");
        Value = Value.Remove(15);
        textContainer.text = Value;
    }
    
    
    public void Creat() 
    {
        if (!TestText(out string Massage)) return;
        if (TagSystem.CreatTag(CurrentValue)) { Destroy(gameObject); OnNewTagCreated?.Invoke(); }
    }

    public bool TestText(out string Massage) 
    {
        if (string.IsNullOrEmpty(CurrentValue)) { Massage = "Write"; return false;}
        Massage = "Applay";
        return true;
    }
    public void OnUrself()
    {
        Destroy(gameObject);
    }

}
