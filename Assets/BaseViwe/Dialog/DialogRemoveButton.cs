using Base;
using Base.Dialog;
using BaseViwe.DialogViwe;
using UnityEngine;
using UnityEngine.UI;

namespace BaseViwe.DialogViwe
{
    public class DialogRemoveButton : Button
    {
        protected override void Start()
        {
            base.Start();
            ContentObject IDialogContent = transform.GetComponentInParent<ContentObject>();
            onClick.AddListener(() =>
            {
                DialogBase.Dialogs.Remove(IDialogContent.Content as Dialog);
                FindObjectOfType<DialogBaseViwe>().Refresh();
            });
        }
    }
}