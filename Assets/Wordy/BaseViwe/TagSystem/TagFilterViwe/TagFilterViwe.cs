using Base;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagFilterViwe : MonoBehaviour
{

    public static Dictionary<string, bool> TagFillterValues
    {
        get
        {
            if (_TagFillterValues == null)
            {
                _TagFillterValues = new Dictionary<string, bool>();

                Tagable.GetListOfTags().ForEach((a) => { _TagFillterValues.Add(a, false); });
            }
            Dictionary<string, bool> NewList = new Dictionary<string, bool>();
            Tagable.GetListOfTags().ForEach((a) => {
                NewList.Add(a, false);
                if (_TagFillterValues.ContainsKey(a)) 
                    NewList[a] = _TagFillterValues[a];
            });
            _TagFillterValues = NewList;
            return NewList;
        }
    }
    
    private static Dictionary<string, bool> _TagFillterValues;


    public System.Action OnDone;
    [SerializeField, Required]
    private TogelContent TagButtonPrefab;
    [SerializeField, Required]
    private Transform ConentParrent;
    private void Start()
    {
        Dictionary<string, bool> TagFillterValues = TagFilterViwe.TagFillterValues;
        List<string> AllTages = TagFillterValues.Keys.ToList();
        Toggle[] toggle = GetComponentsInChildren<Toggle>();
        toggle.DestroyAll(true);
        AllTages.ForEach((a) =>
        {
            TogelContent Toggel = Instantiate(TagButtonPrefab, ConentParrent);
            Toggel.isOn = TagFillterValues[a];
            Toggel.GetComponentInChildren<TextMeshProUGUI>().text = a + " ";// +  TagSystem.GetAllContentsFromTag(a).Count;
            string Ass = a;
            Toggel.OnBoolChanged += (GG) => { TagFillterValues[Ass] = GG; };
            Toggel.OnDestroyButton += () => TagDeleter.Delet(Start, Ass);
        });
    }
    public void DestroyUrself()
    {
        OnDone?.Invoke();
        Destroy(gameObject);
    }
    public void OnDefaultButton() 
    {
        List<string> kk = new List<string>(TagFillterValues.Keys);
        kk.ForEach(a => TagFillterValues[a] = false);
        new List<Toggle>(GetComponentsInChildren<Toggle>()).ForEach(a => a.isOn = false);
    }
}
