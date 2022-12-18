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
            Transform ToTest = transform;
            for (int i = 0; i < 20; i++)
            {
                if (ToTest.TryGetComponent<ContentObject>(out ContentObject IDialogContent))
                {
                    onClick.AddListener(() =>
                    {
                        DialogBase.Dialogs.Remove((IDialogContent.Content as Dialog?).Value);
                        FindObjectOfType<DialogBaseViwe>().Refresh();
                    });
                    break;
                }
                ToTest = ToTest.parent;
                if (ToTest == null) break;
            }
        }
    }
}