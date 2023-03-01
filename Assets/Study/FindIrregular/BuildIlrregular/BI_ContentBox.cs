using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BI_ContentBox : ContentObject, IPointerDownHandler, IPointerUpHandler
{
    [Required]
    [SerializeField]
    private TextMeshProUGUI Text;
    [System.NonSerialized]
    public int IRRType = 0;
    private void Start()
    {
        if (IRRType == 0) Text.text = (Content as IIrregular).BaseForm;
        if (IRRType == 1) Text.text = (Content as IIrregular).SimplePast;
        if (IRRType == 2) Text.text = (Content as IIrregular).PastParticiple;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        isControlling = true;
        Ofsete = transform.position - new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        if (dd != null) 
        {
            dd.contentBox = null;
            dd = null;
        }
        if(StartPos == Vector3.zero) StartPos = transform.position;
        transform.SetAsLastSibling();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isControlling = false;
        BI_Place[] ALLS = GameObject.FindObjectsOfType<BI_Place>();
        float Diss = 9999;
        for (int i = 0; i < ALLS.Length; i++)
        {
            if (ALLS[i].IsEnpty)
            {
                float DISS = Vector3.Distance(ALLS[i].transform.position, transform.position);
                if (DISS < 100) 
                {
                    if (DISS < Diss)
                    {
                        Diss = DISS;
                        dd = ALLS[i];
                    }
                }
            }
        }
        if(dd != null) dd.contentBox = this;
    }
    private BI_Place dd = null;
    private bool isControlling;
    private Vector3 Ofsete;
    private Vector3 StartPos;

    private void Update()
    {
        if (isControlling) transform.position = Ofsete + new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        else if (dd != null) transform.position = Vector3.MoveTowards(transform.position, dd.transform.position, Time.deltaTime * 1000);
        else if(StartPos != Vector3.zero) transform.position = Vector3.MoveTowards(transform.position, StartPos, Time.deltaTime * 1000);
    }
}
