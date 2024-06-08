using Base.Antonym;
using Base.Dialog;
using Base.Synonym;
using Base.Word;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;

public class QuestSceneLoop : MonoBehaviour
{
    private void Awake()
    {
        Quest quest = FindObjectOfType<Quest>();
        if (quest == null) return;
        StudyContentData QuestPrefab = quest.QuestData;

        quest.gameObject.SetActive(false);
        quest.gameObject.ToDestroy();
        OnFinsh(); 
        void OnFinsh() 
        {
            GameObject D = null;
            if (QuestPrefab is IQuestStarter)
            {
                D = (QuestPrefab as IQuestStarter).CreatQuest();
            }
            else if (QuestPrefab is IQuestStarterWithWord)
            {
                D = (QuestPrefab as IQuestStarterWithWord).CreatQuest(WordBase.Wordgs.GetContnetList(1)[0]);
            }
            else if (QuestPrefab is IQuestStarterWithDialog)
            {
                D = (QuestPrefab as IQuestStarterWithDialog).CreatQuest(DialogBase.Dialogs.GetContnetList(1)[0]);
            }
            else if (QuestPrefab is IQuestStarterWithIrregular)
            {
                D = (QuestPrefab as IQuestStarterWithIrregular).CreatQuest(IrregularBase.Irregulars.GetContnetList(1)[0]);
            }
            else if (QuestPrefab is IQuestStarterWithWordList)
            {
                D = (QuestPrefab as IQuestStarterWithWordList).CreatQuest(WordBase.Wordgs.GetContnetList((QuestPrefab as IQuestStarterWithWordList).MinimalCount));
            }
            else if (QuestPrefab is IQuestStarterWithDialogList)
            {
                D = (QuestPrefab as IQuestStarterWithDialogList).CreatQuest(DialogBase.Dialogs.GetContnetList((QuestPrefab as IQuestStarterWithDialogList).MinimalCount));
            }
            else if (QuestPrefab is IQuestStarterWithIrregularList)
            {
                D = (QuestPrefab as IQuestStarterWithIrregularList).CreatQuest(IrregularBase.Irregulars.GetContnetList((QuestPrefab as IQuestStarterWithIrregularList).MinimalCount));
            }
            else if (QuestPrefab is IQuestStarterWithSynonymList)
            {
                TList<Synonym> synonyms = new List<Synonym>();
                List<Word> ActiveContents = Base.Word.WordBase.Wordgs.ActiveItems;
                ActiveContents.ForEach(A => {

                    if (SynonymBase.Synonyms.Contains(A)) 
                    {
                        synonyms.Add(SynonymBase.Synonyms[A]);
                    }
                });
                D = (QuestPrefab as IQuestStarterWithSynonymList).CreatQuest(synonyms.Take((QuestPrefab as IQuestStarterWithSynonymList).MinimalCount).ToList());
            }
            else if (QuestPrefab is IQuestStarterWithAntonymList)

            {
                TList<Antonym> Antonyms = new List<Antonym>();
                List<Word> ActiveContents = Base.Word.WordBase.Wordgs.ActiveItems;
                ActiveContents.ForEach(A => {

                    if (AntonymBase.Antonyms.Contains(A))
                    {
                        Antonyms.Add(AntonymBase.Antonyms[A]);
                    }
                });
                D = (QuestPrefab as IQuestStarterWithAntonymList).CreatQuest(Antonyms.Take((QuestPrefab as IQuestStarterWithAntonymList).MinimalCount).ToList());
            }
            else throw new System.Exception("Countinue the List");
            D.GetComponent<Quest>().OnFineshed += OnFinsh;
        }

    }

}
