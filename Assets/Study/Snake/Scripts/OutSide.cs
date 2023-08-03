using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OutSide : MonoBehaviour
{
    private static List<OutSide> OutSides;
    private static List<Letter> Letterss;
    public BoxData boxData;
    private static Dictionary<Vector2Int, OutSide> OutSidesDiicc;
    [SerializeField]
    private GameObject Shaddow;
    [SerializeField]
    private GameObject UpLine, UpRightConection, UpLeftConection;
    [SerializeField]
    private GameObject DownLine, DownRightConection, DownLeftConection;
    [SerializeField]
    private GameObject LeftLine, LeftUpConection, LeftDownConection;
    [SerializeField]
    private GameObject RightLine, RightUpConection, RightDownConection;


    void Start()
    {
        if (OutSides == null || OutSides.Count == 0 || OutSides[0] == null)
        {
            OutSides = FindObjectsOfType<OutSide>().ToList();
            Letterss = FindObjectsOfType<Letter>().ToList();
            OutSidesDiicc = new Dictionary<Vector2Int, OutSide>();
            OutSidesDiicc = OutSides.ToDictionary((d) => new Vector2Int(d.boxData.X, d.boxData.Y));
            Letterss.ForEach(d => OutSidesDiicc.Add(new Vector2Int(d.boxData.X, d.boxData.Y), null));
        }

        Vector2Int MyV = new Vector2Int(boxData.X, boxData.Y);

        if (OutSidesDiicc.ContainsKey(MyV + Vector2Int.down) && OutSidesDiicc[MyV + Vector2Int.down] == null ||
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.left) && OutSidesDiicc[MyV + Vector2Int.left] == null) Shaddow.SetActive(true);
        else Destroy(Shaddow);


        List<OutSide> Cheaked = new List<OutSide>();

        if (!FindWayOut(this)) 
        {
            Destroy(UpLine);
            Destroy(UpRightConection);
            Destroy(UpLeftConection);
            Destroy(DownLine);
            Destroy(DownRightConection);
            Destroy(DownLeftConection);
            Destroy(LeftLine);
            Destroy(LeftUpConection);
            Destroy(LeftDownConection);
            Destroy(RightLine);
            Destroy(RightUpConection);
            Destroy(RightDownConection);
            return;
        }

         
        bool FindWayOut(OutSide outSide, Side side =  Side.None) 
        {
            if (outSide == null) return true;
            if(Cheaked.Contains(outSide))return false;
            Cheaked.Add(outSide);
            Vector2Int MyV = new Vector2Int(outSide.boxData.X, outSide.boxData.Y);
            if (!OutSidesDiicc.ContainsKey(MyV + Vector2Int.up))return true;
            if (OutSidesDiicc[MyV + Vector2Int.up] != null && FindWayOut(OutSidesDiicc[MyV + Vector2Int.up])) return true;

            if (!OutSidesDiicc.ContainsKey(MyV + Vector2Int.down)) return true;
            if (OutSidesDiicc[MyV + Vector2Int.down] != null && FindWayOut(OutSidesDiicc[MyV + Vector2Int.down])) return true;

            if (!OutSidesDiicc.ContainsKey(MyV + Vector2Int.left)) return true;
            if (OutSidesDiicc[MyV + Vector2Int.left] != null && FindWayOut(OutSidesDiicc[MyV + Vector2Int.left])) return true;

            if (!OutSidesDiicc.ContainsKey(MyV + Vector2Int.right)) return true;
            if (OutSidesDiicc[MyV + Vector2Int.right] != null && FindWayOut(OutSidesDiicc[MyV + Vector2Int.right])) return true;
            return false;
        }



        
       


        
        if (OutSidesDiicc.ContainsKey(MyV + Vector2Int.up) && OutSidesDiicc[MyV + Vector2Int.up] == null) UpLine.SetActive(true);
        else { Destroy(UpLine); UpLine = null; }

        if (OutSidesDiicc.ContainsKey(MyV + Vector2Int.left) && OutSidesDiicc[MyV + Vector2Int.left] == null) LeftLine.SetActive(true);
        else { Destroy(LeftLine); LeftLine = null; }

        if (OutSidesDiicc.ContainsKey(MyV + Vector2Int.down) && OutSidesDiicc[MyV + Vector2Int.down] == null) DownLine.SetActive(true);
        else { Destroy(DownLine); DownLine = null; }

        if (OutSidesDiicc.ContainsKey(MyV + Vector2Int.right) && OutSidesDiicc[MyV + Vector2Int.right] == null) RightLine.SetActive(true);
        else { Destroy(RightLine); RightLine = null; }
        //------------------------------------------------------------------------------------------------------------------------------------------------

        if (UpLine == null && LeftLine != null ||
            (UpLine == null && LeftLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.up) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.left) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.up + Vector2Int.left) &&
             OutSidesDiicc[MyV + Vector2Int.up + Vector2Int.left] == null) )
            UpLeftConection.SetActive(true);
        else Destroy(UpLeftConection);

        if (UpLine == null && RightLine != null ||
            (UpLine == null && RightLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.up) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.right) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.up + Vector2Int.right) &&
             OutSidesDiicc[MyV + Vector2Int.up + Vector2Int.right] == null))
            UpRightConection.SetActive(true);
        else Destroy(UpRightConection);
        
        if (DownLine == null && RightLine != null ||
            (DownLine == null && RightLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.down) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.right) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.down + Vector2Int.right) &&
             OutSidesDiicc[MyV + Vector2Int.down + Vector2Int.right] == null)) DownRightConection.SetActive(true);
        else Destroy(DownRightConection);

        if (DownLine == null && LeftLine != null ||
            (DownLine == null && LeftLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.down) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.left) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.down + Vector2Int.left) &&
             OutSidesDiicc[MyV + Vector2Int.down + Vector2Int.left] == null)) DownLeftConection.SetActive(true);
        else Destroy(DownLeftConection);
        
        if (RightLine == null && UpLine != null ||
            (RightLine == null && UpLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.up) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.right) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.up + Vector2Int.right) &&
             OutSidesDiicc[MyV + Vector2Int.up + Vector2Int.right] == null)) RightUpConection.SetActive(true);
        else Destroy(RightUpConection);

        if (RightLine == null && DownLine != null ||
            (RightLine == null && DownLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.down) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.right) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.down + Vector2Int.right) &&
             OutSidesDiicc[MyV + Vector2Int.down + Vector2Int.right] == null)) RightDownConection.SetActive(true);
        else Destroy(RightDownConection);

        if (LeftLine == null && UpLine != null ||
            (LeftLine == null && UpLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.up) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.left) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.up + Vector2Int.left) &&
             OutSidesDiicc[MyV + Vector2Int.up + Vector2Int.left] == null)) LeftUpConection.SetActive(true);
        else Destroy(LeftUpConection);

        if (LeftLine == null && DownLine != null ||
            (LeftLine == null && DownLine == null && OutSidesDiicc.ContainsKey(MyV + Vector2Int.down) && OutSidesDiicc.ContainsKey(MyV + Vector2Int.left) &&
            OutSidesDiicc.ContainsKey(MyV + Vector2Int.down + Vector2Int.left) &&
             OutSidesDiicc[MyV + Vector2Int.down + Vector2Int.left] == null)) LeftDownConection.SetActive(true);
        else Destroy(LeftDownConection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
