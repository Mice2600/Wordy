using Base.Dialog;
using Base.Word;

namespace Servises.SmartText
{
    public class DialogBaseActiveCountText : SmartText
    {
        public override string MyText => SevedString;
        private protected override int PerUpdateTime => 10;
        string SevedString;
        protected override void PerUpdate() => SevedString = DialogBase.Dialogs.ActiveItems.Count.ToString();

    }
}
