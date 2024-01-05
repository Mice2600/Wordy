using Base;
namespace Servises.SmartText
{
    public class SourceTextRussian : ContentText
    {
        public override string GetValue(Content Object)
        {
            if (Object == null) return "";
            else return Object.RussianSource;
        }
    }
}