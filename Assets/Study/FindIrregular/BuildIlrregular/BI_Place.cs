using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BI_Place : MonoBehaviour
{
    public bool IsEnpty => contentBox == null;
    [System.NonSerialized]
    public BI_ContentBox contentBox;
}
