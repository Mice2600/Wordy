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
    }
}