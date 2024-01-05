using Base;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordCreator.WordCretor 
{
    public class TagContentList : MonoBehaviour
    {
        [SerializeField, Required]
        private GameObject TagButtonPrefab;
        private Transform ConentParrent => transform;
        private Dictionary<string, bool> TagSituation;
        Content Content;
        private void Start()
        {
            if(TagSituation == null) TagSituation = new Dictionary<string, bool>();
            Content = GetComponentInParent<ContentObject>().Content;
            WordChanger wordChanger= GetComponentInParent<WordChanger>();
            if (wordChanger.OnApple != OnApplay) 
                wordChanger.OnApple += OnApplay;

            List<string> AllTages = TagSystem.GetAllTagIdes();
            List<string> BellongTages = TagSystem.GetBlongTags(Content.EnglishSource);

            Toggle[] toggle = GetComponentsInChildren<Toggle>();
            toggle.DestroyAll(true);
            AllTages.ForEach((a) =>
            {
                Toggle Toggel = Instantiate(TagButtonPrefab, ConentParrent).GetComponent<Toggle>();
                if (TagSituation.ContainsKey(a))
                    Toggel.isOn = TagSituation[a];
                else {
                    TagSituation.Add(a, BellongTages.Contains(a));
                    Toggel.isOn = TagSituation[a];
                } 
                Toggel.GetComponentInChildren<TextMeshProUGUI>().text = a;
                string Ass = a;
                Toggel.onValueChanged.AddListener((GG) => TagSituation[Ass] = GG);
                
            });
        }
        public void OnApplay() 
        {
            TagSituation.Keys.ToList().ForEach((a) => {
                if (TagSituation[a]) TagSystem.AddContent(a, Content.EnglishSource);
                else TagSystem.RemoveContent(a, Content.EnglishSource);
            });
        }//
        public void OnAddButton() => TagCreator.Open(Start);
    }
}

