using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public bool IsUsed = false;
    public BoxData boxData;
    [SerializeField]
    private GameObject LeftSide, RightSide, UpSide, DownSide;
    private static List<Letter> Letttereess;
    private static Dictionary<Vector2Int ,Letter> LetttereessDiicc;
    private void Start()
    {
        if (Letttereess == null || Letttereess.Count == 0 || Letttereess[0] == null) 
        {
            Letttereess = FindObjectsOfType<Letter>().ToList();
            LetttereessDiicc = new Dictionary<Vector2Int, Letter>();
            LetttereessDiicc = Letttereess.ToDictionary((d) => new Vector2Int(d.boxData.X, d.boxData.Y));
        }
        LeftSide.SetActive(!LetttereessDiicc.ContainsKey(new Vector2Int(boxData.X, boxData.Y) + Vector2Int.left));
        RightSide.SetActive(!LetttereessDiicc.ContainsKey(new Vector2Int(boxData.X, boxData.Y) + Vector2Int.right));
        UpSide.SetActive(!LetttereessDiicc.ContainsKey(new Vector2Int(boxData.X, boxData.Y) + Vector2Int.up));
        DownSide.SetActive(!LetttereessDiicc.ContainsKey(new Vector2Int(boxData.X, boxData.Y) + Vector2Int.down));
        

    }
}
