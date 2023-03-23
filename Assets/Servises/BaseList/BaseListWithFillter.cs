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
    
    public abstract class BaseListWithFillter : BaseListViwe, ISearchUser
    {
        private SearchViwe searchViwe => _searchViwe ??= GameObject.FindObjectOfType<SearchViwe>(true);
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
        }

        private bool OnlyActive;
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


        void ISearchUser.OnSearchStarted() 
        {
            SerchedContents = new List<Content>();
            Lode(0);
        }
        void ISearchUser.OnSearchEnded() 
        {
            SerchedContents = null;
            Lode(0);
        }
        void ISearchUser.OnValueChanged(string Value) 
        {
            SerchedContents = SearchComand(AllContents, Value);
            Lode(0);
        }
        protected virtual TList<Content> SearchComand(TList<Content> AllContents, string SearchString) => Servises.Search.SearchAll(AllContents, SearchString);
    }

}