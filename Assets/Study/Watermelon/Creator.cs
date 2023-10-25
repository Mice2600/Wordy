using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using SystemBox;
using TMPro;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField, Required]
    private TList<GameObject> WaterMelonContentPrefabs;
    private TList<Content> Contents;
    private TList<WaterMelonContent> Created;
    private void Start()
    {
        Created = new TList<WaterMelonContent>();
        TList<Content> contents = new TList<Content>(WordBase.Wordgs);
        Contents = new TList<Content>();
        WaterMelonContentPrefabs.ForEach(G =>
        {
            Contents.Add(Find(G.GetComponent<WaterMelonContent>(), contents));
            contents.Remove(Contents.Last);
        });
        Content Find(WaterMelonContent waterMelonContent, TList<Content> contents) 
        {
            TList<Content> NeedConttents= contents.FindAll(C => 
            {
                if (C.EnglishSource.Length <= waterMelonContent.MaxCount && C.EnglishSource.Length >= waterMelonContent.MinCount) 
                {
                    bool Isthere = false;
                    (C as IMultiTranslation).Translations.ForEach(x => { if (C.RussianSource.Length <= waterMelonContent.MaxCount && C.RussianSource.Length >= waterMelonContent.MinCount) Isthere = true; });
                    return Isthere;
                }
                return false;
            });
            if (NeedConttents.Count == 0) return null;
            return NeedConttents.Mix().RandomItem;
        }
    }

    private List<WaterMelonContent> DontUse;
    float Offcet;
    public void Creat(WaterMelonContent Left, WaterMelonContent Right) 
    {
        if (Left == null) return;
        if (Right == null) return;
        if (DontUse == null) DontUse = new List<WaterMelonContent>();
        if (DontUse.Contains(Left) || DontUse.Contains(Right)) return;
        GameObject W = Instantiate(WaterMelonContentPrefabs[(Contents.IndexOf(Right.Content) + 1), ListGetType.Loop], transform);
        W.transform.position = (Right.transform.position + Left.transform.position) / 2;
        W.GetComponent<WaterMelonContent>().Sort(Contents[Contents.IndexOf(Right.Content) + 1, ListGetType.Loop]);
        Created.Add(W.GetComponent<WaterMelonContent>());
        DontUse.Add(Right);
        DontUse.Add(Left);


        Destroy(Left.gameObject);
        Destroy(Right.gameObject);
        //Right.Content
    }

    public GameObject OnControll;
    public TextMeshPro NexText;

    void Update()
    {

        Offcet += Time.deltaTime;
        if (Offcet < 1) return;
        if (OnControll == null) Creat();
        OnControll.transform.position = new Vector3(0, 5.5f, 0f);
        if (TInput.GetMouseButton(0)) 
        {
            OnControll.transform.position = new Vector3(TInput.mouseWorldPoint(0).x, 5.5f, 0f);
        }
        if (TInput.GetMouseButtonUp(0)) 
        {
            OnControll.transform.position = new Vector3(TInput.mouseWorldPoint(0).x, 5.5f, 0f);
            OnControll = null;
            Offcet = 0;
        }
    }
    private void OnDrawGizmos()
    {
        
    }
    public void Creat() 
    {


        List<Vector3> Rays = new List<Vector3>() { new(-3f, 4.5f, 0f), new(-2.5f, 4.5f, 0f), new(-2f, 4.5f, 0f), new(-1.5f, 4.5f, 0f), new(-1f, 4.5f, 0f), new(-1f, 4.5f, 0f), new(-.5f, 4.5f, 0f), new(0f, 4.5f, 0f),
        new(3f, 4.5f, 0f), new(2.5f, 4.5f, 0f), new(2f, 4.5f, 0f), new(1.5f, 4.5f, 0f), new(1f, 4.5f, 0f), new(1f, 4.5f, 0f), new(.5f, 4.5f, 0f), new(0f, 4.5f, 0f)};


        TList<(Content, int)> NeedContents = new List<(Content, int)>();

        Rays.ForEach(Pos => {

            Rigidbody2D FirsRB = null;
            Physics2D.RaycastAll(Pos, Vector3.down, 15).ToList().ForEach(A => 
            {
                if (FirsRB == null)
                    if (A.rigidbody != null)
                        FirsRB = A.rigidbody;
            });
            if (FirsRB != null) 
            {
                if (FirsRB.TryGetComponent<WaterMelonContent>(out WaterMelonContent CC)) 
                {
                    NeedContents.AddIfDirty((CC.Content, CC.Type == 0 ? 1 : 0));
                }
            }
        });

        Created.RemoveNulls();
        int MaxIndexes = 3;
        Created.ForEach(i => {
            if (i != null)
            {
                int N = Contents.IndexOf(i.GetComponent<WaterMelonContent>().Content);
                if (N > MaxIndexes) MaxIndexes = N;
            }
        });
        //int NewIndex = Random.Range(0, (MaxIndexes > 4) ? 5 : 3);

        (Content, int)? NeedContent = null;

        if (NeedContents.Count > 0 && Random.Range(0, 100) > 40) 
        {
            NeedContents = NeedContents.FindAll(DD => { if (Contents.IndexOf(DD.Item1) > 3) return false; return true; });
            if (NeedContents.Count > 0) NeedContent = NeedContents.RandomItem; 
        
        }
        


        



        //int NewIndex = Random.Range(0, (MaxIndexes > 4) ? 5 : 3);

        if (!NeedContent.HasValue)
            NeedContent = (Contents[Random.Range(0, (MaxIndexes > 4) ? 5 : 3)], -1);
        int NewIndex = Contents.IndexOf(NeedContent.Value.Item1);


        GameObject W = Instantiate(WaterMelonContentPrefabs[NewIndex], transform);
        W.GetComponent<WaterMelonContent>().Type = NeedContent.Value.Item2;
        W.GetComponent<WaterMelonContent>().Sort(Contents[NewIndex]);
        if(NeedContent.Value.Item2 == 0) NexText.text = Contents[NewIndex].EnglishSource;
        else NexText.text = Contents[NewIndex].RussianSource;
        OnControll = W;
        Created.Add(W.GetComponent<WaterMelonContent>());
    }





}
