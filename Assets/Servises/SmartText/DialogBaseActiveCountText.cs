using Base.Dialog;
namespace Servises.SmartText
{
    public class DialogBaseActiveCountText : SmartText
    {
        public override string MyText => DialogBase.Dialogs.ActiveItems.Count.ToString();
    }
}
