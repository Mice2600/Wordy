using Base.Antonym;
using SystemBox;

namespace Study.CopleFinder.AntonymMatch
{
    public class AntonymMatchContent : CopleFinderContent
    {
        protected override string Text { get {
                if (SideType) return Content.EnglishSource;
                else return (Content as Antonym).attachments.RandomItem();
            } 
        }
        protected override bool CanUseVoiceToBothSids => true;
    }
}