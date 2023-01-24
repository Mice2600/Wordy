using Base;
namespace Servises.SmartText
{
    public class SourceTextRussian : ContentText
    {
        public override void OnValueChanged(Content Object)
        {
            if (Object == null) MyTextContent = "";
            else MyTextContent = Object.RussianSource;
        }
    }
}