using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagFilterViwe : MonoBehaviour
{
    public System.Action OnDone;
    [SerializeField, Required]
    private TogelContent TagButtonPrefab;
    [SerializeField, Required]
    private Transform ConentParrent;
    private void Start()
    {
        List<string> AllTages = TagSystem.GetAllTagIdes();
        Toggle[] toggle = GetComponentsInChildren<Toggle>();
        toggle.DestroyAll(true);
        AllTages.ForEach((a) =>
        {
            Debug.Log("ss");
            TogelContent Toggel = Instantiate(TagButtonPrefab, ConentParrent);
            Toggel.isOn = BaseListWithFillter.TagFillterValues[a];
            Toggel.GetComponentInChildren<TextMeshProUGUI>().text = a + " ";// +  TagSystem.GetAllContentsFromTag(a).Count;
            string Ass = a;
            Toggel.OnBoolChanged += (GG) => { BaseListWithFillter.TagFillterValues[Ass] = GG; };
            Toggel.OnDestroyButton += () => TagDeleter.Delet(Start, TagSystem.GetTag(Ass));
        });
    }
    public void OnAddButton() => TagCreator.Open(Start);
    public void DestroyUrself()
    {
        OnDone?.Invoke();
        Destroy(gameObject);
    }
    public void OnDefaultButton() 
    {
        List<string> kk = new List<string>(BaseListWithFillter.TagFillterValues.Keys);
        kk.ForEach(a => BaseListWithFillter.TagFillterValues[a] = false);
        new List<Toggle>(GetComponentsInChildren<Toggle>()).ForEach(a => a.isOn = false);
    }
}
