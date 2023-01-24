using Base;
using Base.Word;
using Servises;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Traonsletor;
using UnityEngine;
using UnityEngine.UIElements;

namespace WordCreator.WordGenerator
{
    public class WordGeneratorViwe : BaseListWithFillter<Word>
    {

        private protected override int IndexOf(Content content) => Contents.IndexOf((content as Word));
        public List<Word> WordsGenereted;
        [SerializeField]
        private GameObject LodingObject;

        public static List<Word> OflineData
        {
            get 
            {
                if (_OflineData == null) 
                {
                    _OflineData = new List<Word>();
                    Word[] DaaaTaa = new List<Word>(JsonHelper.FromJson<Word>(ProjectSettings.ProjectSettings.Mine.DefalultWords.text)).ToArray();
                    if (DaaaTaa.Length > 1) Array.Sort(DaaaTaa);
                    for (int i = 0; i < DaaaTaa.Length; i++)
                    {
                        Word dialog = DaaaTaa[i];
                        if (string.IsNullOrEmpty(dialog.EnglishSource)) continue;
                        _OflineData.Add(dialog);
                    }
                }
                return _OflineData;
            }
        }

        public override List<Word> AllContents 
        {
            get 
            {
                if (string.IsNullOrEmpty(SearchingString)) return WordsGenereted;
                return OflineData;
            }
        }

        public static List<Word> _OflineData;


        private protected override void Start()
        {
            LoadNew();
        }
        private bool InternetMode;
        
        public void OnInternetModeChanged(bool Value) 
        {
            InternetMode = Value;
            LoadNew();
        }

        [Button]
        public void LoadNew() 
        {
            LodingObject.SetActive(true);
            if (Application.internetReachability == NetworkReachability.NotReachable || !InternetMode) LoadNewOffline();
            else LoadNewOnline();

            void LoadNewOffline() 
            {
                
                TList<Word> O = OflineData;
                List<Word> gann = new List<Word>();
                for (int i = 0; i < 150; i++) gann.Add(O.RemoveRandomItem());
                Resulrat(gann);
            }
            void LoadNewOnline() 
            {
                Translator.GetRandomWord(Resulrat);
                contentPattent.ClearChilds();
                
            }
            void Resulrat(List<Word> words)
            {
                WordsGenereted = words;
                Lode(0);
                contentPattent.Childs().ForEach(child =>
                {
                    AddButton[] UIButtons = child.GetComponentsInChildren<AddButton>();
                    GameObject DD = child.gameObject;
                    for (int i = 0; i < UIButtons.Length; i++) UIButtons[i].onClick.AddListener(() => TryAdd(DD));
                });
                LodingObject.SetActive(false);
            }
        }
        public void TryAdd(GameObject Content)
        {
            WordBase.Wordgs.Add((Content.GetComponent<ContentObject>().Content as Word));
        }

    }
}