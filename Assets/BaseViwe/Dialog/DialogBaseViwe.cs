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
    public class DialogBaseViwe  : BaseListWithFillter{
        public override List<Content> AllContents => new List<Content>(DialogBase.Dialogs);
    }
}