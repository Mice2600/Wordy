using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class OutLineFixer : MonoBehaviour
{
    public GameObject Left, Right, Up, Down;

    void Update()
    {
        Down.SetActive(false);
        Up.SetActive(false);
        Left.SetActive(false);
        Right.SetActive(false);
        transform.parent.Childs().ForEach(T => {

            if (transform == T) return;

            if ((int)T.transform.localPosition.x == (int)transform.localPosition.x) 
            {
                if ((int)T.transform.localPosition.y + 1 == (int)transform.localPosition.y) Down.SetActive(true);
                if ((int)T.transform.localPosition.y - 1 == (int)transform.localPosition.y) Up.SetActive(true);
            }
            if ((int)T.transform.localPosition.y == (int)transform.localPosition.y)
            {
                if ((int)T.transform.localPosition.x + 1 == (int)transform.localPosition.x) Left.SetActive(true);
                if ((int)T.transform.localPosition.x - 1 == (int)transform.localPosition.x) Right.SetActive(true);
            }
        });
    }
}
