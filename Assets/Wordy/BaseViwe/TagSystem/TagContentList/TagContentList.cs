using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Required]
        public GameObject TagContentListViwe;
    }
}
public class TagContentList : ContentObject
{
    public static void Open(Content content, System.Action OnDone) => Instantiate(ProjectSettings.ProjectSettings.Mine.TagContentListViwe).GetComponent<TagContentList>().Set(content, OnDone);
    [SerializeField, Required]
    private GameObject TagButtonPrefab;
    [SerializeField, Required]
    private Transform ConentParrent;

    public System.Action OnDone;
    public void Set(Content content, System.Action OnDone)
    {
        this.Content = content;
        this.OnDone = OnDone;
    }
    private void Start()
    {
        List<string> AllTages= TagSystem.GetAllTagIdes();
        List<string> BellongTages= TagSystem.GetBlongTags(Content.EnglishSource);
        
        Toggle[] toggle = GetComponentsInChildren<Toggle>();
        toggle.DestroyAll(true);
        AllTages.ForEach((a) => 
        {
            Toggle Toggel = Instantiate(TagButtonPrefab, ConentParrent).GetComponent<Toggle>();
            Toggel.isOn = BellongTages.Contains(a);
            Toggel.GetComponentInChildren<TextMeshProUGUI>().text = a;
            string Ass = a;
            Toggel.onValueChanged.AddListener((GG) => { OnCliced(GG, Ass); });
        });

        void OnCliced(bool Value, string Tag) 
        {
            if(Value) TagSystem.AddContent(Tag, Content.EnglishSource);
            else TagSystem.RemoveContent(Tag, Content.EnglishSource);
        }
    }
    public void OnAddButton() => TagCreator.Open(Start);
    public void DestroyUrself()
    {
        OnDone?.Invoke();
        Destroy(gameObject);
    }
}
