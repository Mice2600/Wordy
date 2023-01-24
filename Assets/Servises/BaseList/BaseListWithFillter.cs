using Base;
using Base.Dialog;
using Servises;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
using UnityEngine.UIElements;

namespace Servises.BaseList
{
    public abstract class BaseListWithFillter<T> : BaseListViwe<T> where T : Content
    {
        [Required,SerializeField]
        private GameObject CloseSearchingButton;
        public abstract List<T> AllContents { get; }
        public override List<T> Contents 
        {
            get 
            {
                if (SerchedContents == null) return AllContents;
                return SerchedContents;
            }
        }
        private List<T> SerchedContents;
        
        public bool OnlyActive;
        public void SetOnlyActive(bool Value) 
        {
            OnlyActive = Value;
            Refresh();
        }
        public override void Refresh() 
        {
            if (string.IsNullOrEmpty(SearchingString)) { SerchedContents = null; }
            else 
            {
                SerchedContents = Servises.Search.SearchAll<T>(AllContents, SearchingString);
                if(OnlyActive) SerchedContents = new TList<T>(SerchedContents.Where(stringToCheck => stringToCheck.Active));
            }
            base.Refresh();
        }
        [ReadOnly]
        public string SearchingString;

        protected virtual void Update() 
        {
            CloseSearchingButton.SetActive(!string.IsNullOrEmpty(SearchingString));
        }

        public void OnShearchValueChanged(string Value) 
        {
            SearchingString = Value.ToUpper();
            if (string.IsNullOrEmpty(SearchingString)) { SerchedContents = null; }
            else
            {
                SerchedContents = Servises.Search.SearchAll<T>(AllContents, SearchingString);
                if (OnlyActive) SerchedContents = new TList<T>(SerchedContents.Where(stringToCheck => stringToCheck.Active));
            }
            Lode(0);
        }

    }

}