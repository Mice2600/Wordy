using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class linecodeaa : MonoBehaviour
{
    public Image img;
    Vector3 firstPos;
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                img.transform.position = Input.mousePosition;
                firstPos = Input.mousePosition;
            }
            float dis = Vector2.Distance(firstPos, Input.mousePosition);
            img.transform.localScale = new Vector3(dis, 0.2f, 0f);
            img.transform.position = (Input.mousePosition + firstPos) / 2f;
            img.transform.right = firstPos - img.transform.position;
        }
    }
}
