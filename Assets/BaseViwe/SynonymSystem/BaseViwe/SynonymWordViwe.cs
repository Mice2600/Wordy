using Base;
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
        if (string.IsNullOrEmpty(Contents))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        GetComponentInChildren<TMP_Text>().text = TextUtulity.Color("Synonym : ", TitleColor) + Contents;
    }
}
