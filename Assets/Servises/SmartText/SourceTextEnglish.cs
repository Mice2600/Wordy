using Base;
namespace Servises.SmartText
{
    public class SourceTextEnglish : ContentText
    {
        public override void OnValueChanged(Content Object)
        {
            if (Object == null) MyTextContent = "";
            else MyTextContent = Object.EnglishSource;
        }
    }
}