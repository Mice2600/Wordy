using Base.Dialog;
namespace Servises.SmartText
{
    public class DialogBaseCountText : SmartText
    {
        public override string MyText => DialogBase.Dialogs.Count.ToString();
    }
}
