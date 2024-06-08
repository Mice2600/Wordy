using Base;
using Base.Antonym;
using Base.Synonym;
using Base.Word;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalWordViewer : MonoBehaviour
{

    public Color SynonimTitleColorText;
    public Color SynonimTitleColor;

    public Color SynonimColorText;
    public Color SynonimColor;
    
    public Color AntonymTitleColorText;
    public Color AntonymTitleColor;

    public Color AntonymColorText;
    public Color AntonymColor;

    public TextMeshProUGUI OutlineText;
    public TextMeshProUGUI Text;

    private void Start()
    {
        ContentObject Gt = GetComponentInParent<ContentObject>();
        if (Gt == null) return;

        TList<string> Sny = new TList<string>();
        if (SynonymBase.Synonyms.Contains(Gt.Content))
            Sny = SynonymBase.Synonyms[Gt.Content].attachments;
        TList<string> Any = new TList<string>();
        if (AntonymBase.Antonyms.Contains(Gt.Content))
            Any = AntonymBase.Antonyms[Gt.Content].attachments;

        Refresh(Sny, Any);
    }
    private void Refresh(TList<string> Synonyms, TList<string> Antonyms)
    {
        OutlineText.text = "";
        Text.text = "";

        //int FontSize = (int)TTE.fontSize;
        int FontSize = 23;

        int width = Screen.width;

#if UNITY_EDITOR
        width = (int)UnityEditor.Handles.GetMainGameViewSize().x;
#endif
        width -= 10;
        if (Synonyms.Count > 0) 
        {

            (string Out, string Ins) SNS = GetListText(("Synonyms", SynonimTitleColor, SynonimTitleColorText), (Synonyms, SynonimColor, SynonimColorText));

            OutlineText.text += SNS.Out;
            Text.text += SNS.Ins;

            OutlineText.text += "\n";
            Text.text += "\n";
        }
        if (Antonyms.Count > 0) 
        {
            (string Out, string Ins) ANS = GetListText(("Antonyms", AntonymTitleColor, AntonymTitleColorText), (Antonyms, AntonymColor, AntonymColorText));

            OutlineText.text += ANS.Out;
            Text.text += ANS.Ins;

            OutlineText.text += "\n";
            Text.text += "\n";
        }

        

        (string Out, string Ins) GetListText((string TitleText, Color BackGraundColor, Color TColor) Title, (List<string> strings, Color BackGraundColor, Color TColor) Content)
        {
            
            int LineCC = 0;
            string NText = "";
            string NOutText = "";

            var TV =OneText(Title.TitleText, Title.TColor, Title.BackGraundColor);
            NOutText += TV.Out + " ";
            NText += TV.Ins + " "; 
            Content.strings.ForEach(A => {

                var V = OneText(A, Content.TColor, Content.BackGraundColor);
                NOutText += V.Out + " ";
                NText += V.Ins + " ";
                
            });
            

            (string Out, string Ins) OneText(string NeedString, Color TextColor, Color Backcolor) 
            {
                if (LineCC + (NeedString.Length * FontSize + FontSize) > width)
                {
                    NOutText += "\n";
                    NText += "\n";
                    LineCC = 0;
                }
                LineCC += (int)(NeedString.Length * FontSize + FontSize);

                string s = NeedString.ToUpper();
                string NSs = "A";
                for (int SS = 1; SS < s.Length - 1; SS++) NSs += "B";
                NSs += "C";

                NSs = TextUtulity.Color(NSs, Backcolor);
                s = TextUtulity.Color(s, TextColor);
                return (NSs, s);
            }
            return (NOutText, NText);
        }








    }

}
