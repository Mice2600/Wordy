using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSceneLoop : MonoBehaviour
{
    
    private void Start()
    {
        Quest quest = FindObjectOfType<Quest>();
        GameObject QuestPrefab = quest.QuestPrefab;
        quest.OnFineshed += OnFinsh; 
        void OnFinsh() 
        {
            GameObject D = Instantiate(QuestPrefab);
            D.GetComponent<Quest>().OnFineshed += OnFinsh;
        } 

    }

}
