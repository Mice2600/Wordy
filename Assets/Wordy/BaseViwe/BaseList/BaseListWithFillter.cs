using Base;
using Base.Dialog;
using Servises;
using Servises.BaseList;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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




        #region FilterViwe
        private protected override void Start()
        {
            base.Start();
        }

        public TList<Content> Filtered;//

        public void OnFilter() => Refresh();
        #endregion

        public abstract List<Content> AllContents { get; }
        public override List<Content> Contents 
        {
            get 
            {
                if (SerchedContents == null) 
                {
                    if (Filtered.Count == 0) 
                    {
                        return AllContents; 
                    }
                    return Filtered; 
                }
                return SerchedContents;
            }
        }
        private protected List<Content> SerchedContents;

        public void RefreshWithSearch() 
        {
            if (!searchViwe.IsSearching) SerchedContents = null;
            else if (SerchedContents.Count == 0) SerchedContents = Servises.Search.SearchAll<Content>(AllContents, searchViwe.SearchingString);
            Refresh();
        }
        public override void Refresh() 
        {
            if (!searchViwe.IsSearching) 
            {

                SerchedContents = null;

                Filtered = new List<Content>();
                TagSystem.GetAllTagIdes().Where((s) => TagFilterViwe.TagFillterValues[s]).
                    ForEach((id) => {
                        List<string> BlongsContents = TagSystem.GetAllContentsFromTag(id);
                        List<Content> CorrentContents = AllContents;
                        Filtered.AddRange(CorrentContents.Where(er => BlongsContents.Contains(er.EnglishSource)));
                    });
            }
            
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
            StopSearching();
        }

        public virtual void StopSearching(){}

        void ISearchUser.OnValueChanged(string Value) 
        {
            if (string.IsNullOrEmpty(Value)) 
            {
                SerchedContents = null;
            }else SerchedContents = SearchComand(AllContents, Value);
            Lode(0);
        }
        protected virtual TList<Content> SearchComand(TList<Content> AllContents, string SearchString) => Servises.Search.SearchAll(AllContents, SearchString);
    }

}