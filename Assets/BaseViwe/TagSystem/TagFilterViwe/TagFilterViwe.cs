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
    private GameObject TagButtonPrefab;
    [SerializeField, Required]
    private Transform ConentParrent;
    private void Start()
    {
        List<string> AllTages = TagSystem.GetAllTagIdes();
        Toggle[] toggle = GetComponentsInChildren<Toggle>();
        toggle.DestroyAll(true);
        AllTages.ForEach((a) =>
        {
            Toggle Toggel = Instantiate(TagButtonPrefab, ConentParrent).GetComponent<Toggle>();
            Toggel.isOn = TagViwe.TagFillterValues[a];
            Toggel.GetComponentInChildren<TextMeshProUGUI>().text = a + " " +  TagSystem.GetAllContentsFromTag(a);
            string Ass = a;
            Toggel.onValueChanged.AddListener((GG) => { TagViwe.TagFillterValues[Ass] = GG; });
        });
    }
    public void OnAddButton() => TagCreator.Open(Start);
    public void DestroyUrself()
    {
        OnDone?.Invoke();
        Destroy(gameObject);
    }
}
