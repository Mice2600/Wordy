using Base;
using Base.Word;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SynonymWordViwe : MonoBehaviour
{

    [SerializeField]
    private Color TitleColor = Color.red;
    void Start()
    {
        string Contents = "";
        SynonymSystem.SynonymOf(GetComponentInParent<ContentObject>().Content).ForEach(x => Contents += x + "   ");
        if (string.IsNullOrEmpty(Contents) || string.IsNullOrWhiteSpace(Contents))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        GetComponentInChildren<TMP_Text>().text = TextUtulity.Color("Synonym : ", TitleColor) + Contents;
        GetComponentInChildren<TMP_WordHightLighter>().OnClick.AddListener(OnWordCliced);
    }

    public void OnWordCliced(string Word)
    {
        if (WordBase.Wordgs.Contains(Word))
        {
            DiscretionObject.Show(WordBase.Wordgs.GetContent(Word));
        }
        else
        {
            int Indeee = WordBase.DefaultBase.IndexOf(new WordDefoult(Word, "", "", ""));
            if (Indeee != -1) DiscretionObject.Show(WordBase.DefaultBase[Indeee]);
        }
    }
}
