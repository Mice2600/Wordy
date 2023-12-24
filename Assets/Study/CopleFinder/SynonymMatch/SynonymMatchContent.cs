using Base.Synonym;
using System.Linq;
using SystemBox;
using TMPro;

namespace Study.CopleFinder.SynonymMatch
{
    public class SynonymMatchContent : CopleFinderContent
    {
        protected override string Text
        {
            get
            {
                if (SideType)
                    return TextUtulity.UnderLine((Content as Synonym).attachments.RandomItem());
                

                else return Content.EnglishSource;

            }
        }
        protected override bool CanUseVoiceToBothSids => true;

        public override bool IsThereEqualnest()
        {
            string NText = SecondSellected.GetComponentInChildren<TMP_Text>(true).text;
            bool IsThere = false;
            (FirstSellected.Content as Synonym).attachments.ForEach(
                (ST) => { if (NText.Contains(NText, System.StringComparison.OrdinalIgnoreCase)) 
                        IsThere = true;
                });
            return IsThere;
        }

    }
}