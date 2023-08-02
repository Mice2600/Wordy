using Base;
using Sirenix.OdinInspector;
using Study.Ring;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SContent : ContentObject
{
    [SerializeField, Required]
    private GameObject Flayer;
    void Start()
    {
        if (Content == null) return;
        Flayer.transform.position = Vector3.left + Vector3.up * 9999f;
        FindObjectOfType<Indecator>().OnFound += (C) =>
        {
            if (C == Content.EnglishSource)
            {
                Flayer.transform.position = FindObjectOfType<Indecator>().transform.position;
                StartCoroutine(AA());
            }
        };
        ;
        IEnumerator AA()
        {
            yield return new WaitForSeconds(.1f);
            Flollow = true;
        }
    }

    bool Flollow = false;
    void Update()
    {
        if (!Flollow) return;
        Flayer.transform.position = Vector3.MoveTowards(Flayer.transform.position, transform.position, 10f * Time.deltaTime);

    }
}
