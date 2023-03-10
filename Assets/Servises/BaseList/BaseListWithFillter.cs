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
    public interface ISearchList 
    {
        public void OnShearchValueChanged(string Value);
        public string SearchingString { get; }
    }
    public abstract class BaseListWithFillter<T> : BaseListViwe<T>, ISearchList where T : Content
    {
        [Required,SerializeField]
        private GameObject CloseSearchingButton;
        public abstract List<T> AllContents { get; }
        public override List<T> Contents 
        {
            get 
            {
                if (SerchedContents == null) 
                {
                    if (OnlyActive) return  new TList<T>(AllContents.Where(stringToCheck => stringToCheck.Active));
                    return AllContents; 
                }
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
            if (string.IsNullOrEmpty(SearchingString)) 
            {
                SerchedContents = null; 
            }
            else 
            {
                SerchedContents = Servises.Search.SearchAll<T>(AllContents, SearchingString);
                if(OnlyActive) SerchedContents = new TList<T>(SerchedContents.Where(stringToCheck => stringToCheck.Active));
            }
            base.Refresh();
        }
        
        public string SearchingString { get; set; }

        protected virtual void Update() 
        {
            if(CloseSearchingButton != null) CloseSearchingButton.SetActive(!string.IsNullOrEmpty(SearchingString));
        }

        public void OnShearchValueChanged(string Value) 
        {
            SearchingString = Value.ToUpper();
            if (string.IsNullOrEmpty(SearchingString)) { SerchedContents = null; }
            else
            {
                SerchedContents = SearchComand(AllContents, SearchingString);
                //SerchedContents = Servises.Search.SearchAll<T>(AllContents, SearchingString);
                if (OnlyActive) SerchedContents = new TList<T>(SerchedContents.Where(stringToCheck => stringToCheck.Active));
            }
            Lode(0);
        }
        protected abstract TList<T> SearchComand(TList<T> AllContents, string SearchString);
    }

}