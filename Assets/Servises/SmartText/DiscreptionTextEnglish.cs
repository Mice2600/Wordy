using Base;
namespace Servises.SmartText
{
    public class DiscreptionTextEnglish : ContentText
    {
        public override void OnValueChanged(IContent Object)
        {
            if (Object == null || Object is not IDiscreption) MyTextContent = "";
            else MyTextContent = (Object as IDiscreption).EnglishDiscretion;
        }
    }
}