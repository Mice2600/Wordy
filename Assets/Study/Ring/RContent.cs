using Base;
using Sirenix.OdinInspector;
using Study.Ring;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RContent : ContentObject
{
    [SerializeField, Required]
    private GameObject Flayer;
    void Start()
    {
        if (Content == null) return;
        Flayer.transform.position = Vector3.left + Vector3.up * 9999f;
        Flayer.transform.transform.SetParent(transform.root);
        Flayer.GetComponentInChildren<TextMeshProUGUI>().text = Content.EnglishSource;
        FindObjectOfType<RingCreator>().OncontentFound += (C) =>
        {
            if (C == Content)
            {
                Flayer.transform.position = FindObjectOfType<Indecator>().transform.position;
                Flayer.transform.SetAsLastSibling();
                StartCoroutine(AA());
            }
        };
        ;
        IEnumerator AA()
        {
            yield return new WaitForSeconds(.5f);
            Flollow = true;

        }
    }

    bool Flollow = false;
    void Update()
    {
        if (!Flollow) return;
        Flayer.transform.position = Vector3.MoveTowards(Flayer.transform.position, transform.position, 1000f * Time.deltaTime);

    }
}
