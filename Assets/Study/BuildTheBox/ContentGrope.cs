using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class ContentGrope : MonoBehaviour
{
    static ContentGrope() 
    {
        TekkenPlaces = new List<Vector2Int>();
    }
    public TList<(Vector2Int, char)> MyGrope;
    private TList<Vector2Int> OnlyPlaces;
    public bool OnPlace = false;
    public List<Vector2Int> MyPattern;
    GameObject ShadowGrope;
    Vector3 StartPosition;
    Vector3 StartSize;

    float Max_X = 0;
    float Min_X = 100;
    float Max_Y = 0;
    float Min_Y = 100;

    private static BuildTheBox _BuildTheBox;

    private void Start()
    {
        if (_BuildTheBox == null) _BuildTheBox = GameObject.FindObjectOfType<BuildTheBox>();
        TekkenPlaces = new List<Vector2Int>();
        OnlyPlaces = new TList<Vector2Int>();
        MyGrope.ForEach(a => OnlyPlaces.Add(a.Item1));
        ShadowGrope = new GameObject("Shadow");
        ShadowGrope.transform.SetParent(_BuildTheBox.Parrent.transform);
        ShadowGrope.transform.localScale = Vector3.one;
        MyGrope.ForEach(a =>
        {
            GameObject D = Instantiate(_BuildTheBox.ShadowPrefab, ShadowGrope.transform);
            D.transform.localScale = Vector3.one;
            D.transform.localPosition = new Vector3(a.Item1.x, a.Item1.y, .9f);
            
        });
        
        
        ShadowGrope.transform.Childs().ForEach(D =>
        {
            if (Max_X < D.localPosition.x) Max_X = D.localPosition.x;
            if (Max_Y < D.localPosition.y) Max_Y = D.localPosition.y;
            if (Min_X > D.localPosition.x) Min_X = D.localPosition.x;
            if (Min_Y > D.localPosition.y) Min_Y = D.localPosition.y;
        });
        ShadowGrope.transform.Childs().ForEach(ss =>
        {
            ss.transform.transform.localPosition = new Vector3(ss.transform.transform.localPosition.x - Min_X, ss.transform.transform.localPosition.y - Min_Y, ss.transform.transform.localPosition.z);
            gameObject.AddComponent<BoxCollider2D>().offset = new Vector2(ss.transform.transform.localPosition.x, ss.transform.transform.localPosition.y);
        });
        gameObject.GetComponents<BoxCollider2D>().ForEach(a => a.isTrigger = true);


        StartPosition = transform.localPosition;
        StartSize = transform.localScale;
        MyPattern = new List<Vector2Int>();
        MyGrope.ForEach(a => {MyPattern.Add(a.Item1 - new Vector2Int(Min_X.ToInt(), Min_Y.ToInt()));});
        

    }
    static ContentGrope IsControlling = null;
    private void Update()
    {
        ShadowGrope.transform.position = transform.position;
        ShadowGrope.transform.localPosition = ShadowGrope.transform.localPosition.ToInt();
        if (Vector3.Distance(transform.localPosition, StartPosition) > 1) transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime);
        else transform.localScale = Vector3.MoveTowards(transform.localScale, StartSize, Time.deltaTime);

        if (IsControlling == this) 
        {
            transform.position = 
                Vector3.MoveTowards(transform.position,new Vector3(TInput.mouseWorldPoint(0).x - ( (Max_X - Min_X) / 2), TInput.mouseWorldPoint(0).y + .5f, -1), Time.deltaTime * 20f);
        }

    }
    private void OnMouseUp() 
    {
        if (IsControlling == this) TryToStey();
        IsControlling = null;
    }
    private void OnMouseDown()
    {
        IsControlling = this;
        StartFollowing();


    }


    static List<Vector2Int> TekkenPlaces;

    [Button]
    public void TryToStey() 
    {
        transform.parent.Childs().ForEach(a => a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y, 0));
        Vector3 WorldPos = _BuildTheBox.Parrent.transform.InverseTransformPoint(transform.position).ToInt();
        Vector2Int MyPos = new Vector2Int(WorldPos.x.ToInt(), WorldPos.y.ToInt());
        if (IsSuitblePos())
        {
            transform.DOMove(_BuildTheBox.Parrent.transform.TransformPoint(WorldPos), .1f);
            for (int i = 0; i < MyPattern.Count; i++) TekkenPlaces.Add(MyPos + MyPattern[i]);

            OnPlace = true;
            for (int i = 0; i < MyPattern.Count; i++)
            {
                if (!OnlyPlaces.Contains(MyPos + MyPattern[i]))
                {

                    OnPlace = false;
                    break;
                }
            }


        }
        else transform.DOLocalMove(StartPosition, .4f);

        bool IsSuitblePos() 
        {
            for (int i = 0; i < MyPattern.Count; i++)
                if (!Generator.Places.Contains(MyPos + MyPattern[i]) || 
                    TekkenPlaces.Contains(MyPos + MyPattern[i])) return false;
            return true;
        }

    }

    public void StartFollowing() 
    {
        transform.parent.Childs().ForEach(a => a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y, 0));
        Vector3 WorldPos = _BuildTheBox.Parrent.transform.InverseTransformPoint(transform.position).ToInt();
        Vector2Int MyPos = new Vector2Int(WorldPos.x.ToInt(), WorldPos.y.ToInt());
        for (int i = 0; i < MyPattern.Count; i++) TekkenPlaces.Remove(MyPos + MyPattern[i]);


        OnPlace = false;
        



    }

    
}
