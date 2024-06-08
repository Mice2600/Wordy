using Base;
using Base.Word;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class WordListView : MonoBehaviour, ISearchUser, ICreatNewUser, IBaseNameUser, IFillterUser, IRemoveButtonUser
{

    public Color DeaActiveColor;
    public TextMeshProUGUI Outdd;
    public TextMeshProUGUI TTE;
    public WordScrollBar wordScrollBar;
    private void Start()
    {
        Refresh();
        wordScrollBar.scrollBar.OnBeginDraging += () => IsBeingDraged = true;
        wordScrollBar.scrollBar.OnEndDraging += () => { IsBeingDraged = false; VisualRefresh(); };
    }

    bool IsBeingDraged;

    private void Update()
    {
        if(IsBeingDraged) VisualUpdate();

    }
    [Button]
    public void Refresh()
    {
        Refresh(WordBase.Wordgs);

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
        Dictionary<string, bool> TagFillterValues = TagFilterViwe.TagFillterValues;
        List<string> Tags = TagFillterValues.Keys.Where((s) => TagFilterViwe.TagFillterValues[s]).ToList();
        Filtered = new TList<Word>();

        if (Tags.IsEnpty())
        {
            Filtered = contents.Convert<Word>(a => a as Word).ToList();
        }
        else 
        {
            Filtered = contents.Convert<Word>(a => a as Word).Where(A =>
            {
                if (!(A as Tagable).IsAnyThereTag()) return false;
                for (int i = 0; i < Tags.Count; i++) if ((A as Tagable).IsThereTag(Tags[i])) return true;
                return false;
            }).ToList();
        }
        VisualRefresh();
        wordScrollBar.RefreshScrollbar();
    }
    [NonSerialized]
    public TList<Word> Filtered;
    private void VisualRefresh()
    {
        Outdd.text = "";
        TTE.text = "";

        //int FontSize = (int)TTE.fontSize;
        int FontSize = 23;

        int width = Screen.width;

#if UNITY_EDITOR

        width = (int)UnityEditor.Handles.GetMainGameViewSize().x;
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

            string s = Filtered[IndexOn].EnglishSource.ToUpper();
            string NSs = "A";
            for (int SS = 1; SS < s.Length - 1; SS++) NSs += "B";
            NSs += "C";

            NSs = TextUtulity.Color(NSs, GetBackgraundColor(Filtered[IndexOn]));
            s = TextUtulity.Color(s, GetContentColor(Filtered[IndexOn]));



            Outdd.text += NSs + " ";
            TTE.text += s + " ";
            IndexOn++;

        }

        if (Outdd.GetRenderedValues(true).y < 0)
        {
            StartCoroutine(FixFream());
            IEnumerator FixFream()
            {
                yield return null;
                Refresh(Filtered);
            }
            return;
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

        //TTE.rectTransform.rect = TTE.rectTransform.rect;

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);

        Outdd.rectTransform.anchoredPosition = Vector3.zero;
        TTE.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnCliced(string Word) 
    {
        Debug.Log(Word);
        Word word = Filtered.Find(W => W.EnglishSource.ToUpper() == Word.ToUpper());
        if (word == null) return;
        if (word is not ISpeeker) return;
        EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            EasyTTSUtil.SpeechAdd((word as ISpeeker).SpeekText);
        else Debug.Log(word.EnglishSource + " Speeking");
    }
    public void OnLongCliced(string Word) 
    {
        Word word = Filtered.Find(W => W.EnglishSource.ToUpper() == Word.ToUpper());
        if (word != null) 
        {
            DiscretionObject.Show(word);
             
        }


    }

    private protected List<Word> SerchedContents;
    [Button]
    void ISearchUser.OnSearchStarted()
    {
        //SerchedContents = new List<Word>();
       // Refresh(SerchedContents);
    }
    [Button]
    void ISearchUser.OnSearchEnded()
    {
        SerchedContents = null;
        Refresh();
        StopSearching();
    }
    
    public virtual void StopSearching() { }
    [Button]
    void ISearchUser.OnValueChanged(string Value)
    {
        if (string.IsNullOrEmpty(Value))
        {
            SerchedContents = null;
            Refresh();
        }
        else SerchedContents = SearchComand(WordBase.Wordgs, Value);
        Refresh(SerchedContents);
    }




    char LastChar;

    void VisualUpdate() 
    {
        
        if (wordScrollBar.scrollBar.IsDraging && LastChar != wordScrollBar.MoveChar) 
        {
            VisualRefresh();
            LastChar = wordScrollBar.MoveChar;
        }
    }

    public Color GetBackgraundColor(Content content) 
    {
        Color color = Outdd.color;

        if (wordScrollBar.scrollBar.IsDraging)
        {


            if (content.EnglishSource.ToUpper()[0] != wordScrollBar.MoveChar)
                color = Color.Lerp(color, Color.black, .5f);
        }
        else 
        {
            if (!(content as IPersanalData).Active)
                color = Color.Lerp(color, Color.black, .5f);
        }


        
        return color;
    }
    public Color GetContentColor(Content content) 
    {
        Color color = Color.white;

        if (wordScrollBar.scrollBar.IsDraging)
        {
            if (content.EnglishSource.ToUpper()[0] != wordScrollBar.MoveChar)
                color = Color.Lerp(color, Color.black, .5f);
        }
        else 
        {
            if (!(content as IPersanalData).Active)
                color = Color.Lerp(color, Color.black, .5f);
        }

        
        return color;
    }
    void ICreatNewUser.OnButton()
    {
        WordCreator.WordCretor.WordChanger.StartChanging(OnFinsh: () => { Refresh(); });
    }

    public void OnFiltere()
    {
        Refresh();//
    }

    void IRemoveButtonUser.OnRemoveButton(Content content)
    {
        Refresh();
    }

    string IBaseNameUser.BaseName => BaseName;
    [SerializeField]
    private string BaseName;

}
