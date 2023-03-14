using Base.Dialog;
using Base;
using Servises.BaseList;
using Servises;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Traonsletor;
using UnityEngine;


namespace WordCreator.DialogDefaultBase
{
    public class DialogDefaultBase_Viwe : BaseListWithFillter
    {
        public List<Content> WordsGenereted;
        public static List<Content> OflineData
        {
            get
            {
                if (_OflineData == null)
                {
                    _OflineData = new List<Content>();
                    Dialog[] DaaaTaa = new List<Dialog>(JsonHelper.FromJson<Dialog  >(ProjectSettings.ProjectSettings.Mine.DefalultDialogs.text)).ToArray();
                    if (DaaaTaa.Length > 1) Array.Sort(DaaaTaa);
                    for (int i = 0; i < DaaaTaa.Length; i++)
                    {
                        Dialog dialog = DaaaTaa[i];
                        if (string.IsNullOrEmpty(dialog.EnglishSource)) continue;
                        _OflineData.Add(dialog);
                    }
                }
                return _OflineData;
            }
        }

        public override List<Content> AllContents
        {
            get
            {
                if (string.IsNullOrEmpty(SearchingString)) return WordsGenereted;
                return OflineData;
            }
        }

        public static List<Content> _OflineData;


        private protected override void Start()
        {
            LoadNew();
        }
        public void LoadNew()
        {
            LoadNewOffline();
            void LoadNewOffline()
            {

                TList<Content> O = OflineData;
                List<Content> gann = new List<Content>();
                for (int i = 0; i < 150; i++) gann.Add(O.RemoveRandomItem());
                Resulrat(gann);
            }
            void Resulrat(List<Content> words)
            {
                WordsGenereted = words;
                Lode(0);
                
            }
        }
    }
}