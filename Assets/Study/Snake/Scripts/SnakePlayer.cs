using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Study.Crossword;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
public class SnakePlayer : MonoBehaviour
{
    public Dictionary<Vector2Int, Letter> Map;
    [System.NonSerialized]
    public TList<Letter> UsedLetters;
    [System.NonSerialized]
    public TList<Letter> ToShowLetters;
    [System.NonSerialized]
    public Letter LastLetter;
    [System.NonSerialized]
    public Letter MyLetter;
    [SerializeField, Required]
    private GameObject TrackObject;
    Dictionary<Vector2Int, Track> Tracks;
    public Func<bool> CanMove;
    List<Camera> cameras;
    void Start()
    {
        ToShowLetters = new TList<Letter>();
        Tracks = new Dictionary<Vector2Int, Track>();
        Map = new Dictionary<Vector2Int, Letter>();
        FindObjectsOfType<Letter>().ForEach( (L) => {
            Map.Add(new Vector2Int(L.boxData.X, L.boxData.Y) , L);
        });
        UsedLetters = new List<Letter>() { MyLetter };
        LastLetter = MyLetter;
        SwipeDetector d = GetComponent<SwipeDetector>();
        d.OnSwipeUpAction += OnSwipeUp;
        d.OnSwipeDownAction += OnSwipeDown;
        d.OnSwipeRightAction += OnSwipeRight;
        d.OnSwipeLeftAction += OnSwipeLeft;
        transform.SetAsLastSibling();
        cameras = FindObjectsOfType<Camera>().ToList();
    }





    float SpeedDefault = 30;
    float Speed = 30;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, LastLetter.transform.position, Time.deltaTime * Speed);
        if (transform.position == LastLetter.transform.position) 
        {


            if (
                UsedLetters.Count < Map.Count &&
                
                (!Map.ContainsKey(new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)) ||
                UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)])) &&
                
                (!Map.ContainsKey(new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)) ||
                UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)])) &&

                (!Map.ContainsKey(new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y )) ||
                UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y)])) &&

                (!Map.ContainsKey(new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)) ||
                UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)]))

                ) 
            {
                CanMove = null;
                StartCoroutine(enumerator());
                IEnumerator enumerator() 
                {
                    yield return new WaitForSeconds(1);
                    FindObjectOfType<QuestSnake>().OnFineshed?.Invoke();
                }
            }


            if (CanMove != null) 
            {
                bool Waiter = CanMove.Invoke();
                if (!Waiter) return;
            }
            
            if (!Tracks.ContainsKey(new Vector2Int(LastLetter.boxData.X, LastLetter.boxData.Y))) 
            {
                Track dd = Instantiate(TrackObject, transform.parent).GetComponent<Track>();
                dd.transform.position = transform.position;
                dd.gameObject.SetActive(true);
                dd.MyLetter = LastLetter;
                Tracks.Add(new Vector2Int(LastLetter.boxData.X, LastLetter.boxData.Y), dd);
                
            }

            if (UsedLetters.Last != LastLetter) 
            {
                LastLetter = UsedLetters.NextOf(LastLetter);
                ToShowLetters.Add(LastLetter);
            }
            else Speed = SpeedDefault;
        }
    }


    const float CameraSheakDegger = .1f;


    

    void OnSwipeUp()
    {



        cameras.ForEach(d => {
            d.transform.DOMove(d.transform.position - Vector3.up * CameraSheakDegger, .05f).onComplete += () =>
            d.transform.DOMove(d.transform.position + Vector3.up * CameraSheakDegger, .05f);
        });
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)]))
            if (transform.position != MyLetter.transform.position) { Speed *= 1.6f; return; }

        while (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)) &&
            !UsedLetters.Contains( Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)])) 
        {   
            MyLetter = Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y + 1)];
            UsedLetters.Add(MyLetter);
        }
    }

    

    void OnSwipeDown()
    {
        cameras.ForEach(d => {
            d.transform.DOMove(d.transform.position - Vector3.down * CameraSheakDegger, .05f).onComplete += () =>
            d.transform.DOMove(d.transform.position + Vector3.down * CameraSheakDegger, .05f);
        });
        
        //transform.rotation = Quaternion.Euler(0, 0, -180);
        if (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)]))
            if (transform.position != MyLetter.transform.position) { Speed *= 1.6f; return; }

        while (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)]))
        {
            MyLetter = Map[new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y - 1)];
            UsedLetters.Add(MyLetter);
        }
    }
    void OnSwipeLeft()
    {
        cameras.ForEach(d => {
            d.transform.DOMove(d.transform.position - Vector3.left * CameraSheakDegger, .05f).onComplete += () =>
            d.transform.DOMove(d.transform.position + Vector3.left * CameraSheakDegger, .05f);
        });
        // transform.rotation = Quaternion.Euler(0,0,90);
        if (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y)]))
            if (transform.position != MyLetter.transform.position) { Speed *= 1.6f; return; }

        while (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y)]))
        {
            MyLetter = Map[new Vector2Int(MyLetter.boxData.X - 1, MyLetter.boxData.Y)];
            UsedLetters.Add(MyLetter);
        }
    }
    void OnSwipeRight()
    {
        cameras.ForEach(d => {
            d.transform.DOMove(d.transform.position - Vector3.right * CameraSheakDegger, .05f).onComplete += () =>
            d.transform.DOMove(d.transform.position + Vector3.right * CameraSheakDegger, .05f);
        });
        //transform.rotation = Quaternion.Euler(0, 0, -90);
        if (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)]))
            if (transform.position != MyLetter.transform.position) { Speed *= 1.6f; return; }

        while (Map.ContainsKey(new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)) &&
            !UsedLetters.Contains(Map[new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)]))
        {
            MyLetter = Map[new Vector2Int(MyLetter.boxData.X + 1, MyLetter.boxData.Y)];
            UsedLetters.Add(MyLetter);
        }
    }



}
