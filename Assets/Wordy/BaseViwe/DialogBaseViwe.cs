using Base;
using Base.Dialog;
using Base.Word;
using Servises;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace BaseViwe.DialogViwe
{
    public class DialogBaseViwe  : BaseListWithFillter, IGenrateUser, ICreatNewUser, IBaseNameUser
    {

        public List<Content> WordsGenereted;
        public override List<Content> AllContents 
        {
            get 
            {
                if(WordsGenereted == null) return new List<Content>(DialogBase.Dialogs);
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
            if (value) 
            {
                WordsGenereted = new List<Content>();
                TList<Content> O = new List<Content>(DialogBase.DefaultBase);
                List<Content> gann = new List<Content>();
                for (int i = 0; i < 150; i++) gann.Add(O.RemoveRandomItem());
                Resulrat(gann);
                void Resulrat(List<Content> words)
                {
                    WordsGenereted = words;
                    Lode(0);
                }
            }
            else WordsGenereted = null;
            Lode(0);
        }
        void ICreatNewUser.OnButton()
        {
            DialogChanger.StartChanging();
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
            SearchCoroutines.Add(StartCoroutine(Servises.Search.SearchAllEnumerator(new TList<Content>(DialogBase.Dialogs), SearchString, (l) => {

                FromMee = l;
                List<Content> N = new List<Content>(FromMee);
                N.AddRange(FromDefalt);
                SerchedContents = N;
                Refresh();
            })));
            SearchCoroutines.Add(StartCoroutine(Servises.Search.SearchAllEnumerator(new TList<Content>(DialogBase.DefaultBase), SearchString, (l) => {
                FromDefalt = l;

                List<Content> N = new List<Content>(FromMee);
                N.AddRange(FromDefalt);
                SerchedContents = N;
                Refresh();
            })));
            return new TList<Content>();
        }

    }
}