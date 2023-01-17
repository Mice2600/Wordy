using Base;
using Base.Dialog;
using Servises;
using Servises.BaseList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseViwe.DialogViwe
{
    public class DialogBaseViwe : BaseListViwe<Dialog>
    {
        public override List<Dialog> Contents => DialogBase.Dialogs;
        private protected override int IndexOf(IContent content) => DialogBase.Dialogs.IndexOf((content as Dialog?).Value);
    }
}