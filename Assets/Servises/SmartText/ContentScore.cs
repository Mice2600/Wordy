using Base;
namespace Servises.SmartText
{
    public class ContentScore : ContentText
    {
        public override void OnValueChanged(IContent Object)
        {
            if (Object == null) MyTextContent = "";
            else MyTextContent = ((int)Object.Score).ToString();
        }
    }
}