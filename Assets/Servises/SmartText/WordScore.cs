using Base;
namespace Servises.SmartText
{
    public class WordScore : ContentText
    {
        public override void OnValueChanged(IContent Object)
        {
            if (Object == null) MyTextContent = "";
            else MyTextContent = ((int)Object.Score).ToString();
        }
    }
}