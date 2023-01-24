using Base;
namespace Servises.SmartText
{
    public class DiscreptionTextRussian : ContentText
    {
        public override void OnValueChanged(Content Object)
        {
            if (Object == null || Object is not IDiscreption) MyTextContent = "";
            else MyTextContent = (Object as IDiscreption).RusianDiscretion;
        }
    }
}