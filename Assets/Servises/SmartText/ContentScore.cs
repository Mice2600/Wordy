using Base;
namespace Servises.SmartText
{
    public class ContentScore : ContentText
    {
        public override string GetValue(Content Object)
        {
            if (Object == null) return "";
            else return ((int)Object.ScoreConculeated).ToString();
        }
    }
}