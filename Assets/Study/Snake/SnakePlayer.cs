using Sirenix.Utilities;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
public class SnakePlayer : MonoBehaviour
{
    public Dictionary<Vector2Int, Letter> Map;
    [System.NonSerialized]
    public TList<Letter> UsedLetters;
    [System.NonSerialized]
    public Letter LastLetter;
    [System.NonSerialized]
    public Letter MyLetter;
    void Start()
    {
        Map = new Dictionary<Vector2Int, Letter>();
        FindObjectsOfType<Letter>().ForEach( (L) => {
            Map.Add(new Vector2Int(L.boxData.X, L.boxData.Y) , L);
            if (transform.position.ToInt() == L.transform.position.ToInt()) MyLetter = L;
        });
        UsedLetters = new List<Letter>() { MyLetter };
        LastLetter = MyLetter;
        SwipeDetector d = GetComponent<SwipeDetector>();
        d.OnSwipeUpAction += OnSwipeUp;
        d.OnSwipeDownAction += OnSwipeDown;
        d.OnSwipeRightAction += OnSwipeRight;
        d.OnSwipeLeftAction += OnSwipeLeft;
        transform.SetAsLastSibling();
    }

    float SpeedDefault = 30;
    float Speed = 30;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, LastLetter.transform.position, Time.deltaTime * Speed);
        if (transform.position == LastLetter.transform.position) 
        {
            if (UsedLetters.Last != LastLetter) LastLetter = UsedLetters.NextOf(LastLetter);
            else Speed = SpeedDefault;
        }
    }


    void OnSwipeUp()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
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
        transform.rotation = Quaternion.Euler(0, 0, -180);
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
        transform.rotation = Quaternion.Euler(0,0,90);
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
        transform.rotation = Quaternion.Euler(0, 0, -90);
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
