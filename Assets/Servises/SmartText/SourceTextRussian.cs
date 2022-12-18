using Base;
namespace Servises.SmartText
{
    public class SourceTextRussian : ContentText
    {
        public override void OnValueChanged(IContent Object)
        {
            if (Object == null) MyTextContent = "";
            else MyTextContent = Object.RussianSource;
        }
    }
}