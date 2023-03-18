using Base;
using Base.Dialog;
using Base.Word;
using Servises;
using Servises.BaseList;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace BaseViwe.DialogViwe
{
    public class DialogBaseViwe  : BaseListWithFillter{

        public List<Content> WordsGenereted;
        public override List<Content> AllContents 
        {
            get 
            {
                if(WordsGenereted == null) return new List<Content>(DialogBase.Dialogs);
                else return new List<Content>(WordsGenereted);
            }
        }
        

        public override void Lode(int From)
        {
            
            base.Lode(From);
        }

        public void CreatNewContent()
        {
            DialogChanger.StartChanging();
        }
        public void OnGenerateButton() 
        {
            if (WordsGenereted == null) LoadNew();
            else WordsGenereted = null;
            Lode(0);

            void LoadNew()
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

        }

    }
}