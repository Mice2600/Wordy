using Base;
namespace Servises.SmartText
{
    public class DiscreptionTextRussian : ContentText
    {
        public override string GetValue(Content Object)
        {
            if (Object == null || Object is not IDiscreption) return "";
            else return (Object as IDiscreption).RusianDiscretion;
        }
    }
}