using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TestQuave : OptimizedBehaver, IQueueUpdate
{

    public void TurnUpdate()
    {
        Debug.Log(transform.name);
    }
    
    
}
