using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public interface IBaseTools 
{
    public void Refresh();
}
public interface IBaseNameUser 
{
    public string BaseName { get; }
}
public class BaseName : MonoBehaviour
{
    
    [SerializeField, Required]
    private GameObject Line;

    [SerializeField, Required]
    private GameObject ScenePrefab;
    [SerializeField, Required]
    private Transform ContentParrent;
    private List<(IBaseNameUser, GameObject)> BaseContent;

    private void Start()
    {
        ContentParrent.ClearChilds();
        BaseContent = new List<(IBaseNameUser, GameObject)>();
        Line.SetActive(false);
        StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            yield return null;
            yield return null;
            List<MonoBehaviour> MMS = Object.FindObjectsByType<MonoBehaviour>( FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList().Where(M => M is IBaseNameUser).ToList();

            

            for (int i = 0; i < MMS.Count; i++)
            {
                
                GameObject GG = Instantiate(ScenePrefab, ContentParrent);
                GG.SetActive(true);
                GG.GetComponentInChildren<TMP_Text>().text = (MMS[i] as IBaseNameUser).BaseName;
                (IBaseNameUser, GameObject) item = ((MMS[i] as IBaseNameUser), GG);
                BaseContent.Add(item);
                GG.GetComponentInChildren<Button>().onClick.AddListener(() => OnButton(item));
                yield return null;
            }

            


            Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList().
                Where(a => a is IBaseTools).ToList().ForEach(a => (a as IBaseTools).Refresh());

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);

            yield return new WaitForSeconds(1);
            Line.transform.position = ContentParrent.GetChild(0).transform.position;
            Line.SetActive(true);
            yield return new WaitForSeconds(1);
            Line.transform.position = ContentParrent.GetChild(0).transform.position;



            
        }
    }
    void OnButton((IBaseNameUser, GameObject) Item) 
    {

        BaseContent.ForEach(A => (A.Item1 as MonoBehaviour).gameObject.SetActive(false));
        (Item.Item1 as MonoBehaviour).gameObject.SetActive(true);
        // swich event
        StartCoroutine(Go());


        Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList().
            Where(a => a is IBaseTools).ToList().ForEach(a => (a as IBaseTools).Refresh());


        IEnumerator Go()
        {
            float Lerp = 0f;
            Vector3 StartPos = Line.transform.position;
            while (true)
            {
                Line.transform.position = Vector3.Lerp(StartPos, Item.Item2.transform.position, Lerp);
                Lerp += Time.deltaTime * 2;
                if (Lerp > 1) break;
                yield return null;
            }
            
        }
    }
}
