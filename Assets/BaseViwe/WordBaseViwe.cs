using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using SystemBox.Engine;
using UnityEngine.SceneManagement;
using Base.Word;
using Servises;
using Servises.BaseList;
using Base;
using Base.Dialog;

namespace BaseViwe.WordViwe
{
    public class WordBaseViwe : BaseListWithFillter, IGenrateUser, ICreatNewUser, IBaseNameUser
    {
        public List<Content> WordsGenereted;
        public override List<Content> AllContents
        {
            get
            {
                if (WordsGenereted == null) return new List<Content>(WordBase.Wordgs);
                else return new List<Content>(WordsGenereted);
            }
        }

        Sprite IBaseNameUser.BaseImage => BaseImage;
        [SerializeField, Required]
        private Sprite BaseImage;
        string IBaseNameUser.BaseName => BaseName;
        [SerializeField]
        private string BaseName;

        void IGenrateUser.OnValueChanged(bool value)
        {
            if (value) LoadNew();
            else WordsGenereted = null;
            Lode(0);

            void LoadNew()
            {
                WordsGenereted = new List<Content>();
                TList<Content> O = new List<Content>(WordBase.DefaultBase);
                List<Content> gann = new List<Content>();
                for (int i = 0; i < 150; i++) gann.Add(O.RemoveRandomItem());
                Resulrat(gann);

                void Resulrat(List<Content> words)
                {
                    WordsGenereted = words;
                    Lode(0);

                }

            }
        }
        void ICreatNewUser.OnButton()
        {
            WordChanger.StartChanging();
        }
        private float CCSize;
        public override float GetSizeOfContent(Content content) 
        {
            if (CCSize == 0f) CCSize = content.ContentObject.GetComponent<RectTransform>().rect.height;
            return CCSize;
        }

        List<Coroutine> SearchCoroutines = new List<Coroutine>();
        public override void StopSearching()
        {
            for (int i = 0; i < SearchCoroutines.Count; i++)
                if (SearchCoroutines[i] != null) StopCoroutine(SearchCoroutines[i]);
            SearchCoroutines = new List<Coroutine>();
        }
        protected override TList<Content> SearchComand(TList<Content> AllContents, string SearchString)
        {
            StopSearching();
            TList<Content> FromMee = new TList<Content>();
            TList<Content> FromDefalt = new TList<Content>();
            if (Servises.Search.IsThereKays(SearchString))
            {
                SearchCoroutines.Add(StartCoroutine(Servises.Search.SmartSearch(new TList<Content>(WordBase.Wordgs), SearchString, this, (l) => {
                    FromMee = l;
                    List<Content> N = new List<Content>(FromMee);
                    N.AddRange(FromDefalt);
                    SerchedContents = N;
                    Refresh();
                })));
                SearchCoroutines.Add(StartCoroutine(Servises.Search.SmartSearch(new TList<Content>(WordBase.DefaultBase), SearchString, this, (l) => {
                    FromDefalt = l;
                    List<Content> N = new List<Content>(FromMee);
                    N.AddRange(FromDefalt);
                    SerchedContents = N;
                    Refresh();
                })));
            }
            else 
            {
                SearchCoroutines.Add(StartCoroutine(Servises.Search.SearchAllEnumerator(new TList<Content>(WordBase.Wordgs), SearchString, (l) => {

                    FromMee = l;
                    List<Content> N = new List<Content>(FromMee);
                    N.AddRange(FromDefalt);
                    SerchedContents = N;
                    Refresh();
                })));
                SearchCoroutines.Add(StartCoroutine(Servises.Search.SearchAllEnumerator(new TList<Content>(WordBase.DefaultBase), SearchString, (l) => {
                    FromDefalt = l;

                    List<Content> N = new List<Content>(FromMee);
                    N.AddRange(FromDefalt);
                    SerchedContents = N;
                    Refresh();
                })));
            }
            
            return new TList<Content>();
        }






    }
}