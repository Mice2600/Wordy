using Base.Antonym;
using Base.Dialog;
using Base.Synonym;
using Base.Word;
using Servises;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemBox;
using UnityEngine;

public class QuestStartSceneButton : SceneLodeButton
{
    
    public override void OpenScene()
    {

        Dictionary<string, object> All = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
        List<string> Kays = new List<string>(All.Keys);
        TList<StudyContentData> gameObjects = new List<StudyContentData>();
        for (int i = 0; i < All.Count; i++)
            if (All[Kays[i]] != null && All[Kays[i]] is StudyContentData && (All[Kays[i]] as StudyContentData) != null)
                gameObjects.Add(All[Kays[i]] as StudyContentData);
        StudyContentData QuestPrefab = gameObjects.Find(x => x.SceneName == SceneName);

        if (QuestPrefab is IQuestStarterWithWord && WordBase.Wordgs.Count < 2) return;
        else if (QuestPrefab is IQuestStarterWithDialog && DialogBase.Dialogs.Count < 2) return;
        else if (QuestPrefab is IQuestStarterWithIrregular && IrregularBase.Irregulars.Count < 2) return;
        else if (QuestPrefab is IQuestStarterWithWordList && WordBase.Wordgs.Count < (QuestPrefab as IQuestStarterWithWordList).MinimalCount) return;
        else if (QuestPrefab is IQuestStarterWithDialogList && DialogBase.Dialogs.Count < (QuestPrefab as IQuestStarterWithDialogList).MinimalCount) return;
        else if (QuestPrefab is IQuestStarterWithIrregularList && IrregularBase.Irregulars.Count < (QuestPrefab as IQuestStarterWithIrregularList).MinimalCount) return;
        else if (QuestPrefab is IQuestStarterWithIrregularList && IrregularBase.Irregulars.Count < (QuestPrefab as IQuestStarterWithIrregularList).MinimalCount) return;
        else if (QuestPrefab is IQuestStarterWithSynonymList && SynonymBase.Synonyms.UsebleCount < (QuestPrefab as IQuestStarterWithSynonymList).MinimalCount) return;
        else if (QuestPrefab is IQuestStarterWithAntonymList && AntonymBase.Antonyms.UsebleCount < (QuestPrefab as IQuestStarterWithAntonymList).MinimalCount) return;
        
        base.OpenScene();

        Dictionary<string, object> DictionaryFromType(object atype)
        {
            if (atype == null) return new Dictionary<string, object>();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (var prop in atype.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = prop.GetValue(atype);
                dict.Add(prop.Name, value);
            }
            return dict;
        }


        

    }
}
