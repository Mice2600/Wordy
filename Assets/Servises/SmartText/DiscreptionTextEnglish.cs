using Base;
namespace Servises.SmartText
{
    public class DiscreptionTextEnglish : ContentText
    {
        public override string GetValue(Content Object)
        {
            if (Object == null || Object is not IDiscreption) return "";
            else return (Object as IDiscreption).EnglishDiscretion;
        }
    }
}