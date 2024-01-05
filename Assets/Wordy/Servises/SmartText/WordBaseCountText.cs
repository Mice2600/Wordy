using Base.Word;
namespace Servises.SmartText
{
    public class WordBaseCountText : SmartText
    {
        public override string MyText => WordBase.Wordgs.Count.ToString();
    }
}