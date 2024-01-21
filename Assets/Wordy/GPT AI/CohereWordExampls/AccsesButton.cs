using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccsesButton : MonoBehaviour
{
    // Start is called before the first frame update*
    void Start()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable ||
            GetComponentInParent<ContentObject>() == null || GetComponentInParent<ContentObject>().Content == null)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void OnButton() 
    {
        if (Application.internetReachability == NetworkReachability.NotReachable ||
            GetComponentInParent<ContentObject>() == null || GetComponentInParent<ContentObject>().Content == null)
        {//
            gameObject.SetActive(false);
            return;
        }
        WordExampls.CreatSentencs(GetComponentInParent<ContentObject>().Content);
    }
}
