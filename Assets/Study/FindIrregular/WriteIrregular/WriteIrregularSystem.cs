using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class WriteIrregularSystem : MonoBehaviour
{
    [Required]
    public Quest questWriteWord;
    [Required]
    public Transform ContentParrent;
    [Required]
    public GameObject ContentPrefab;
    public GameObject WI_End_ViwePrefab;
    private void Start()
    {
        ContentParrent.ClearChilds();
        TList<Irregular> irregulars = questWriteWord.NeedIrregularList;
        for (int i = 0; i < questWriteWord.NeedIrregularList.Count; i++)
            Instantiate(ContentPrefab, ContentParrent).GetComponent<WI_Conttent>().Content = irregulars.RemoveRandomItem();
    }
    public bool TrayToComplate()
    {
        WI_Conttent[] Lis = ContentParrent.GetComponentsInChildren<WI_Conttent>();
        List<(Content content, bool isCurrect, string Answer, int Score)> Contents = new List<(Content content, bool isCurrect, string Answer, int Score)>();
        for (int i = 0; i < Lis.Length; i++)
        {
            if (Lis[i].IsEvrethingCurrect)
            {
                questWriteWord.OnIrregularWin?.Invoke(Lis[i].Content as Irregular);
                Contents.Add((Lis[i].Content, true, "", (questWriteWord.QuestData as IIrregularScorer).AddScorIrregular));
            }
            else
            {
                questWriteWord.OnIrregularLost?.Invoke(Lis[i].Content as Irregular);
                Contents.Add((Lis[i].Content, false, Lis[i].WrotenText, (questWriteWord.QuestData as IIrregularScorer).RemoveScoreIrregular));
            }
        }
        Instantiate(WI_End_ViwePrefab).GetComponent<WI_End_Viwe>().Set(Contents, onFinsh);
        questWriteWord.OnGameWin?.Invoke();


        return false;
        void onFinsh()
        {
            Destroy(gameObject);
            questWriteWord.OnFineshed();
        }
    }
}
