using Base;
using Base.Dialog;
using Base.Word;
using Servises;
using Servises.BaseList;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace BaseViwe.DialogViwe
{
    public class DialogBaseViwe  : BaseListWithFillter<Dialog> 
    {
        public override List<Dialog> AllContents => DialogBase.Dialogs;
        private protected override int IndexOf(Content content) => base.Contents.IndexOf(content as Dialog);
        
        protected override TList<Dialog> SearchComand(TList<Dialog> AllContents, string SearchString) => Servises.Search.SearchAll<Dialog>(AllContents, SearchingString);
    }
}