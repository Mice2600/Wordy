using Base;
namespace Servises.SmartText
{
    public class ContentScore : ContentText
    {
        public override void OnValueChanged(Content Object)
        {
            if (Object == null) MyTextContent = "";
            else MyTextContent = ((int)Object.Score).ToString();
        }
    }
}