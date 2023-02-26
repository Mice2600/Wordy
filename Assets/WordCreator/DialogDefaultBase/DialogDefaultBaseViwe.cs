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
    public class DialogDefaultBaseViwe : BaseListWithFillter<Dialog>
    {

        private protected override int IndexOf(Content content) => Contents.IndexOf((content as Dialog));
        public List<Dialog> WordsGenereted;

        public static List<Dialog> OflineData
        {
            get
            {
                if (_OflineData == null)
                {
                    _OflineData = new List<Dialog>();
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

        public override List<Dialog> AllContents
        {
            get
            {
                if (string.IsNullOrEmpty(SearchingString)) return WordsGenereted;
                return OflineData;
            }
        }

        public static List<Dialog> _OflineData;


        private protected override void Start()
        {
            LoadNew();
        }
        public void LoadNew()
        {
            LoadNewOffline();
            

            void LoadNewOffline()
            {

                TList<Dialog> O = OflineData;
                List<Dialog> gann = new List<Dialog>();
                for (int i = 0; i < 150; i++) gann.Add(O.RemoveRandomItem());
                Resulrat(gann);
            }
            void Resulrat(List<Dialog> words)
            {
                WordsGenereted = words;
                Lode(0);
                contentPattent.Childs().ForEach(child =>
                {
                    AddButton[] UIButtons = child.GetComponentsInChildren<AddButton>();
                    GameObject DD = child.gameObject;
                    for (int i = 0; i < UIButtons.Length; i++) UIButtons[i].onClick.AddListener(() => TryAdd(DD));
                });
            }
        }
        public void TryAdd(GameObject Content)
        {
            DialogBase.Dialogs.Add((Content.GetComponent<ContentObject>().Content as Dialog));
        }

        protected override TList<Dialog> SearchComand(TList<Dialog> AllContents, string SearchString) => Servises.Search.SearchAll<Dialog>(AllContents, SearchingString);
    }
}