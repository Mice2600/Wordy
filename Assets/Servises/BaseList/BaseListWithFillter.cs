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

namespace Servises.BaseList
{
    
    public abstract class BaseListWithFillter : BaseListViwe
    {
        private SearchViwe searchViwe => _searchViwe ??= GameObject.FindObjectOfType<SearchViwe>();
        private SearchViwe _searchViwe;
        public abstract List<Content> AllContents { get; }
        public override List<Content> Contents 
        {
            get 
            {
                if (SerchedContents == null) 
                {
                    if(ActiveSorted == null) return AllContents;
                    return ActiveSorted; 
                }
                return SerchedContents;
            }
        }
        private List<Content> SerchedContents;
        private List<Content> ActiveSorted;

        private protected override void Start()
        {
            base.Start();

            searchViwe.OnSearchEnded += OnSerchEnded;
            searchViwe.OnSearchStarted += OnSerchStarted;
            searchViwe.OnValueChanged += OnShearchValueChanged;
        }

        public bool OnlyActive;
        public void SetOnlyActive(bool Value) 
        {
            if (Value == false) ActiveSorted = null;
            else 
            {
                if (SerchedContents == null)
                    ActiveSorted = new TList<Content>(AllContents.Where(stringToCheck => (stringToCheck as IPersanalData).Active));
                else ActiveSorted = new TList<Content>(SerchedContents.Where(stringToCheck => (stringToCheck as IPersanalData).Active)); 
            }
            OnlyActive = Value;
            Refresh();
        }
        public override void Refresh() 
        {
            if (!searchViwe.IsSearching) 
                SerchedContents = null; 
            else  SerchedContents = Servises.Search.SearchAll<Content>(AllContents, searchViwe.SearchingString);
            base.Refresh();
        }


        public void OnSerchStarted() 
        {
            SerchedContents = new List<Content>();
            Lode(0);
        }
        public void OnSerchEnded() 
        {
            SerchedContents = null;
            Lode(0);
        }
        public void OnShearchValueChanged(string Value) 
        {
            SerchedContents = SearchComand(AllContents, Value);
            Lode(0);
        }
        protected virtual TList<Content> SearchComand(TList<Content> AllContents, string SearchString) => Servises.Search.SearchAll(AllContents, SearchString);
    }

}