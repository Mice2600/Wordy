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
    public abstract class BaseListWithFillter : BaseListViwe, ISearchList
    {
        [Required,SerializeField]
        private GameObject CloseSearchingButton;
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
        
        public bool OnlyActive;
        public void SetOnlyActive(bool Value) 
        {
            if (Value == false) ActiveSorted = null;
            else 
            {
                if (SerchedContents == null)
                    ActiveSorted = new TList<Content>(AllContents.Where(stringToCheck => stringToCheck.Active));
                else ActiveSorted = new TList<Content>(SerchedContents.Where(stringToCheck => stringToCheck.Active)); 
            }
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
                SerchedContents = Servises.Search.SearchAll<Content>(AllContents, SearchingString);
                if(OnlyActive) SerchedContents = new TList<Content>(SerchedContents.Where(stringToCheck => stringToCheck.Active));
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
                if (OnlyActive) SerchedContents = new TList<Content>(SerchedContents.Where(stringToCheck => stringToCheck.Active));
            }
            Lode(0);
        }
        protected virtual TList<Content> SearchComand(TList<Content> AllContents, string SearchString) => Servises.Search.SearchAll(AllContents, SearchString);
    }

}