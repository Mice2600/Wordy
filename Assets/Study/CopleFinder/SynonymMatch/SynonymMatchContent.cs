using Base.Synonym;
using SystemBox;

namespace Study.CopleFinder.SynonymMatch
{
    public class SynonymMatchContent : CopleFinderContent
    {
        protected override string Text
        {
            get
            {
                if (SideType) return Content.EnglishSource;
                else return (Content as Synonym).attachments.RandomItem();
            }
        }
        protected override bool CanUseVoiceToBothSids => true;
    }
}