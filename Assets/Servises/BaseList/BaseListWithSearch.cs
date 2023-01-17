using Base;
using Base.Dialog;
using Servises;
using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Servises.BaseList
{
    public abstract class BaseListWithSearch<T> : BaseListViwe<T> where T : IContent
    {
        [Required,SerializeField]
        private GameObject CloseSearchingButton;
        public abstract List<T> AllContents { get; }
        public override List<T> Contents => SerchedContents ??= new List<T>(AllContents);
        private List<T> SerchedContents;

        [ReadOnly]
        public string SearchingString;
        public override void Refresh() 
        {
            if (string.IsNullOrEmpty(SearchingString)) { SerchedContents = AllContents; }
            else SerchedContents = Servises.Search.SearchAll<T>(AllContents, SearchingString);
            base.Refresh();
        }
        public void OnShearchValueChanged(string Value) 
        {
            CloseSearchingButton.SetActive(!string.IsNullOrEmpty(SearchingString));
            SearchingString = Value.ToUpper();

            if (string.IsNullOrEmpty(SearchingString)) { SerchedContents = AllContents; }
            else SerchedContents = Servises.Search.SearchAll<T>(AllContents, SearchingString);

            Lode(0);

            
        }
    }
}