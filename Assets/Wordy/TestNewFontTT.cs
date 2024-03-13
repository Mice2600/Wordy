using Base.Word;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestNewFontTT : MonoBehaviour
{

    public Color DeaActiveColor;
    public TextMeshProUGUI Outdd;
    public TextMeshProUGUI TTE;
    [Button]
    public void Refresh()
    {
        Refresh(WordBase.Wordgs);
    }

    [Button]
    public void Search(string GA)
    {

        Refresh(SearchComand(WordBase.Wordgs, GA));
    }

    public void GoToIndex() { }
    public void GoToContent() { }
    public void PupContent(string EnglishID) { }

    protected TList<Word> SearchComand(TList<Word> AllContents, string SearchString)
    {
        TList<Word> SerchedContents = new List<Word>();
        if (Servises.Search.IsThereKays(SearchString))
        {
            StartCoroutine(Servises.Search.SmartSearch<Word>(AllContents, SearchString, this, (l) => {
                SerchedContents = l;
                Refresh(l);
                Debug.Log(l.Count);
            }));
        }
        else
        {
            SerchedContents = Servises.Search.SearchAll<Word>(AllContents, SearchString);
            Debug.Log(SerchedContents.Count);
        }

        return SerchedContents;
    }



    private void Refresh(TList<Word> contents)
    {
        List<string> Tags = TagSystem.GetAllTagIdes().Where((s) => TagFilterViwe.TagFillterValues[s]).ToList();
        TList<Content> Filtered = new TList<Content>();

        if (Tags.IsEnpty())
        {
            Filtered = contents.Convert<Content>(a => a as Content).ToList();
        }
        else 
        {

            List<Content> contentsConverted = contents.Convert<Content>(a => a as Content).ToList();
            Tags.ForEach(tagg => {
                List<Content> Sorted = TagSystem.WhereTegHasContent(tagg, contentsConverted);
                Sorted.ForEach(T => Filtered.AddIfDirty(T));
            });
        }






        Outdd.text = "";
        TTE.text = "";

        //int FontSize = (int)TTE.fontSize;
        int FontSize = 23;

        int width = Screen.width;

#if UNITY_EDITOR

        if (!Application.isPlaying) width = (int)UnityEditor.Handles.GetMainGameViewSize().x;
#endif

        

        int IndexOn = 0;

        int LineCC = 0;

        while (IndexOn < Filtered.Count)
        {
            if (LineCC + (Filtered[IndexOn].EnglishSource.Length * FontSize + FontSize) > width) 
            {
                Outdd.text += "\n";
                TTE.text += "\n";
                LineCC = 0;
            }
            LineCC += (int)(Filtered[IndexOn].EnglishSource.Length * FontSize + FontSize);

            string s = Filtered[IndexOn].EnglishSource;
            string NSs = "A";
            for (int SS = 1; SS < s.Length - 1; SS++) NSs += "B";
            NSs += "C";

            if (!(Filtered[IndexOn] as IPersanalData).Active) 
            {
                NSs = TextUtulity.Color(NSs, Color.Lerp(Outdd.color, Color.black, .5f));
                s = TextUtulity.Color(s, Color.Lerp(TTE.color, Color.black, .5f)); 
            }
            
            Outdd.text += NSs + " ";
            TTE.text += s + " ";
            IndexOn++;

        }
            
        Vector2 ds = Outdd.GetRenderedValues(true);
        ds /= 2;

        Vector2 ResoltOfsetSize = ds;

        RectTransform MRT = GetComponent<RectTransform>();

        ResoltOfsetSize.x = TTE.rectTransform.offsetMax.x;
        TTE.rectTransform.offsetMax = ResoltOfsetSize;
        Outdd.rectTransform.offsetMax = ResoltOfsetSize;

        ResoltOfsetSize.x = MRT.offsetMax.x;
        MRT.offsetMax = ResoltOfsetSize;

        Vector2 ResoltoffsetMin = -ds;
        ResoltoffsetMin.x = TTE.rectTransform.offsetMin.x;
        TTE.rectTransform.offsetMin = ResoltoffsetMin;
        Outdd.rectTransform.offsetMin = ResoltoffsetMin;

        ResoltoffsetMin.x = MRT.offsetMin.x;
        MRT.offsetMin = ResoltoffsetMin;

        Outdd.rectTransform.anchoredPosition = Vector3.zero;
        TTE.rectTransform.anchoredPosition = Vector3.zero;

        MRT.anchoredPosition = Vector3.zero;

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);


    }


    public void OnCliced(string Word) 
    {
        Debug.Log(Word);
    }

}
