using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SystemBox;
using TMPro;
using UnityEngine;

public class GameBaseViwe : MonoBehaviour, IBaseNameUser
{
    Sprite IBaseNameUser.BaseImage => BaseImage;
    [SerializeField, Required]
    private Sprite BaseImage;
    string IBaseNameUser.BaseName => BaseName;
    [SerializeField]
    private string BaseName;

    [SerializeField]
    private GameObject ContentParent;
    [SerializeField]
    private GameObject GameContent;
    

    private void Start()
    {
        ContentParent.ClearChilds();
        Dictionary<string, object> All = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
        List<string> Kays = new List<string>(All.Keys);
        TList<StudyContentData> gameObjects = new List<StudyContentData>();
        for (int i = 0; i < All.Count; i++)
            if (All[Kays[i]] != null && All[Kays[i]] is StudyContentData && (All[Kays[i]] as StudyContentData) != null)
                gameObjects.Add(All[Kays[i]] as StudyContentData);
        gameObjects.ForEach(x => 
        {
            GameObject D = Instantiate(GameContent, ContentParent.transform);
            D.GetComponentInChildren<TextMeshProUGUI>().text = x.SceneName;
            D.GetComponentInChildren<QuestStartSceneButton>().SceneName = x.SceneName;
            });










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
