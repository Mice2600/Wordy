using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [System.NonSerialized]
    public Letter MyLetter;
    public static SnakePlayer SnakePlayer;
    [SerializeField, Required]
    private GameObject LeftTrack, RightTrack, DownTrack, UpTrack;
    private void Start()
    {
        if(SnakePlayer == null) SnakePlayer = FindObjectOfType<SnakePlayer>();

        
    }
    private void Update()
    {
        LeftTrack.SetActive(true);
        RightTrack.SetActive(true);
        DownTrack.SetActive(true);
        UpTrack.SetActive(true);
        if (SnakePlayer.UsedLetters.First != MyLetter) 
        {
            Letter Previous = SnakePlayer.UsedLetters.PreviousOf(MyLetter);
            if (new Vector2Int(Previous.boxData.X, Previous.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.down) 
                DownTrack.SetActive(false);
            if (new Vector2Int(Previous.boxData.X, Previous.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.up)
                UpTrack.SetActive(false);
            if (new Vector2Int(Previous.boxData.X, Previous.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.left)
                LeftTrack.SetActive(false);
            if (new Vector2Int(Previous.boxData.X, Previous.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.right)
                RightTrack.SetActive(false);
        }

        if (SnakePlayer.UsedLetters.Last != MyLetter) 
        {
            Letter Next = SnakePlayer.UsedLetters.NextOf(MyLetter);
            if (new Vector2Int(Next.boxData.X, Next.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.down)
                DownTrack.SetActive(false);
            if (new Vector2Int(Next.boxData.X, Next.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.up)
                UpTrack.SetActive(false);
            if (new Vector2Int(Next.boxData.X, Next.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.left)
                LeftTrack.SetActive(false);
            if (new Vector2Int(Next.boxData.X, Next.boxData.Y) == new Vector2Int(MyLetter.boxData.X, MyLetter.boxData.Y) + Vector2Int.right)
                RightTrack.SetActive(false);
        }
    }
}
