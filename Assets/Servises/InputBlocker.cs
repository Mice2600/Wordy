using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBlocker : MonoBehaviour
{
    public static List<InputBlocker> Blockers = new List<InputBlocker>();
    private void Awake()
    {
        Blockers.Add(this);
    }
    private void OnDestroy()
    {
        Blockers.Remove(this);
    }
}
