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
    public class WordBaseViwe : BaseListWithFillter
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

        public void CreatNewContent() 
        {
            WordChanger.StartChanging();
        }

        private float CCSize;
        public override float GetSizeOfContent(Content content) 
        {
            if (CCSize == 0f) CCSize = content.ContentObject.GetComponent<RectTransform>().rect.height;
            return CCSize;
        }

        protected override TList<Content> SearchComand(TList<Content> AllContents, string SearchString)
        {
            TList < Content > FromMee =   base.SearchComand(new List<Content>(WordBase.Wordgs), SearchString);
            TList<Content> FromDefalt = base.SearchComand(new List<Content>(WordBase.DefaultBase), SearchString);
            for (int i = 0; i < FromDefalt.Count; i++)
                if(!FromMee.Contains(FromDefalt[i])) 
                    FromMee.Add(FromDefalt[i]);
            return FromMee;
        }

        public void OnGenerateButton()
        {
            if (WordsGenereted == null) LoadNew();
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



    }
}