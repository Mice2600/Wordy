using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;

public class WaterMelonContent : ContentObject
{
    [SerializeField, Required]
    private TextMeshPro Letter => GetComponentInChildren<TextMeshPro>();
    [SerializeField, Required]
    private Transform LetterParrents;

    public int MaxCount = 8;
    

    [Button, SerializeField]
    public void Sort() 
    {
        TList<Content> contents = new TList<Content>(WordBase.Wordgs);
        Sort(contents.Mix().FindAll(d => d.EnglishSource.Length < MaxCount + 1 && (d as IMultiTranslation).Translations.FindAll(x => d.RussianSource.Length <= MaxCount).Count > 0).RandomItem());
    }

    public void Sort(Content content)
    {
        Content = content;
        LetterParrents.ClearChilds();
        Debug.Log((Content as IMultiTranslation).Translations.FindAll(x => Content.RussianSource.Length <= MaxCount).Count);
        string RU = (Content as IMultiTranslation).Translations.FindAll(x => Content.RussianSource.Length <= MaxCount).RandomItem();

        

        Letter.text = content.EnglishSource; 
        

        /*

        string Word = Find();
        for (int i = 0; i < Word.Length; i++)
        {
            GameObject L = Instantiate(Letter.gameObject, LetterParrents);
            L.SetActive(true);
            L.transform.localPosition = Vector3.zero;
            L.transform.Rotate(0,0, -((360 / MaxCount) * i));
            L.transform.Translate(Vector3.up * Letter.transform.localPosition.y);
            L.GetComponent<TextMeshPro>().text = Word[i].ToString();
            if(i + 1 == Word.Length) L.GetComponent<TextMeshPro>().text += ".";
        }
        string Find()
        {



            string RU = (Content as IMultiTranslation).Translations.FindAll(x => Content.RussianSource.Length <= MaxCount && content.RussianSource.Length >= MinCount).RandomItem();
            if(Type != -1)if(Type == 0) return content.EnglishSource; else return RU;
            if (Random.Range(0, 100) > 50)
            {
                Type = 0;
                return content.EnglishSource;
            }
            else 
            {
                Type = 1;
                return RU;
            }

        }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.TryGetComponent<WaterMelonContent>(out WaterMelonContent contentObject)) 
        {
            if (contentObject.Content == Content)
            {
                    FindObjectOfType<Creator>().Creat(contentObject, this);
            }
        }
    }
}
