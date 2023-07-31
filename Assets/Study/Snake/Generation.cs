using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Xml;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Generation : MonoBehaviour
{
    System.Action FoundedWay;
    public static Dictionary<Vector2Int, Box> Places;

    public Vector2Int Size = new Vector2Int(6, 6);

    static Box EndBox;
    static Box StartBox;

    public GameObject Prefab;
    public Transform Parrent;
    public List<Map> maps;

    private void Start()
    {
        Creat(new Vector2Int(6,6));
    }
    public void Creat(Vector2Int Size) 
    {
        this.Size = Size;
        Places = new Dictionary<Vector2Int, Box>();
        for (int i = 0; i < Size.y; i++) 
            for (int S = 0; S < Size.x; S++) 
                Places.Add(new Vector2Int(S, i), new Box(S, i));
        FindOneWay();
        //FindOneWay(Places, Distanetion, Index);
    }


    void FindOneWay() 
    {

        EndBox = Places.Values.ToList().RandomItem();
        StartBox = Places.Values.ToList().RandomItem();
        if (EndBox == StartBox) { FindOneWay(); return; }
        List<List<Box>> Ps = new List<List<Box>>();
        FoundedWay = () =>
        {
            if (!CheakForMultuWayes(EndBox, StartBox)) return;
            

            int COO = 0;
            List<Box> NewP = new List<Box>();
            Places.Values.ToList().ForEach(B => 
            {
                NewP.Add(new Box(B));
                if (B.Qoundition ==  Qoundition.Way) COO++;
            });
            if (COO > 19) Ps.Add(NewP);








        };
        GenereatAllStrateWays(EndBox, StartBox);
        if (Ps.Count == 0) { Start(); return; }
        Debug.Log(Ps.Count);
        //InstalateAll(Ps);

        Ps.ForEach((OneMap) => {
            Map NewPap = new Map();
            List<BoxData> boxDatas = OneMap.Select(DD => new BoxData() {  Qoundition = DD.Qoundition, Side = DD.Side, X = DD.X, Y = DD.Y}).ToList();
            NewPap.boxes = boxDatas;
            NewPap.Size = Size;
            NewPap.StartPosition = OneMap.IndexOf(StartBox);
            NewPap.EndPosition = OneMap.IndexOf(EndBox);
            maps.Add(NewPap);
        });

        GetComponent<Snake>().Creat(maps.RandomItem());


    }
    /*
    void FindAllWay(List<List<(int Place, string Side)>> Places, Vector2Int Distanetion, Vector2Int Index) 
    {
        TList<List<List<(int Place, string Side)>>> Ps = new TList<List<List<(int Place, string Side)>>>();

        FoundedWay = (P) =>
        {
            int COO = 0;
            List<List<(int Place, string Side)>> NewP = new List<List<(int Place, string Side)>>();
            for (int i = 0; i < P.Count; i++)
            {
                List<(int Place, string Side)> NewL = new List<(int Place, string Side)>();
                for (int L = 0; L < P[0].Count; L++)
                {
                    NewL.Add(P[i][L]);
                    if (P[i][L].Place == 1) COO++;
                }
                NewP.Add(NewL);
            }

            if (COO > 19) Ps.Add(NewP);
        };
        GenereatAllWays(Places, Distanetion, Index);
        Debug.Log(Ps.Count);
        InstalateAll(Ps);
    }

    */
    
    public void InstalateAll(List<List<Box>> Ps) 
    {

        StartCoroutine(Ways());
        IEnumerator Ways()
        {
            Ps.Mix();
            for (int i = 0; i < Ps.Count; i++)
            {
                Instalate(Ps[i]);
                yield return new WaitForSeconds(.1f);
                Parrent.ClearChilds();
            }
        }

    }

    public void Instalate(List<Box> Places) 
    {
        Dictionary<Side, string> SidesSimbol = new Dictionary<Side, string>();
        SidesSimbol.Add(Side.None, "O");
        SidesSimbol.Add(Side.Left, "←");
        SidesSimbol.Add(Side.Right, "→");
        SidesSimbol.Add(Side.Up, "↑");
        SidesSimbol.Add(Side.Down, "↓");

        GameObject dd = new GameObject("Way");
        dd.transform.SetParent(Parrent);
        dd.AddComponent<RectTransform>();
        Places.ForEach((YLine) => {
            if (YLine.Qoundition == Qoundition.Way)
            {
                GameObject FFA = Instantiate(Prefab, dd.transform);
                FFA.transform.transform.localPosition = new Vector3Int(YLine.X * 200, YLine.Y * 200, 0);
                FFA.GetComponentInChildren<TextMeshProUGUI>().text = SidesSimbol[YLine.Side];
                if (YLine.X == StartBox.X && YLine.Y == StartBox.Y) 
                {
                    FFA.GetComponent<Image>().color = TColor.Yellow;
                }
            }
        });
    }

    // 0 = enpty, 1 = FilledWithWay  2 = FilledWithWall  ↑ ↓ → ←
    void GenereatAllStrateWays(Box Distanetion, Box Index, Side LasDiraction = Side.None )
    {
        Index.Qoundition = Qoundition.Way;
        if (Distanetion == Index)
        {
            FoundedWay.Invoke();
            return;
        }
        List<(Box, Side)> NewPoss = Index.CorrectBoxes;
        NewPoss.Mix();


        
        for (int i = 0; i < NewPoss.Count; i++)
        {
            if (NewPoss[i].Item1.Qoundition ==  Qoundition.None)
            {
                System.Action ReDo = () => { };
                if (LasDiraction != Side.None && NewPoss[i].Item2 != LasDiraction) 
                {
                    if (LasDiraction == Side.Up && Index.Up != null && Index.Up.Qoundition == Qoundition.None) 
                    {
                        
                        Qoundition OldQQ = Index.Up.Qoundition;
                        Index.Up.Qoundition = Qoundition.Wall;
                        Box box = Index;
                        ReDo = () => box.Up.Qoundition = OldQQ;
                    }

                    else if(LasDiraction == Side.Down && Index.Down != null && Index.Down.Qoundition == Qoundition.None)
                    {
                        Qoundition OldQQ = Index.Down.Qoundition;
                        Index.Down.Qoundition = Qoundition.Wall;
                        Box box = Index;
                        ReDo = () => box.Down.Qoundition = OldQQ;
                    }

                    else if (LasDiraction == Side.Right && Index.Right != null && Index.Right.Qoundition ==  Qoundition.None)
                    {
                        Qoundition OldQQ = Index.Right.Qoundition;
                        Index.Right.Qoundition = Qoundition.Wall;
                        Box box = Index;
                        ReDo = () => box.Right.Qoundition = OldQQ;
                    }
                    else if (LasDiraction == Side.Left && Index.Left != null && Index.Left.Qoundition == Qoundition.None)
                    {
                        Qoundition OldQQ = Index.Left.Qoundition;
                        Index.Left.Qoundition = Qoundition.Wall;
                        Box box = Index;
                        ReDo = () => box.Left.Qoundition = OldQQ;
                    }
                }

                NewPoss[i].Item1.Qoundition =  Qoundition.Way;
                Index.Side = NewPoss[i].Item2;
                GenereatAllStrateWays(Distanetion, NewPoss[i].Item1, Index.Side);
                NewPoss[i].Item1.Qoundition = Qoundition.None;
                Index.Side = Side.None;
                ReDo.Invoke();
            }
        }
        Index.Side = Side.None;
        Index.Qoundition = Qoundition.None;
    }
    bool CheakForMultuWayes(Box Distanetion, Box Index) 
    {
        if (Index.CorrectBoxes.Where(e => e.Item1.Qoundition == Qoundition.Way).Count() == 1) return false;
        if (Distanetion.CorrectBoxes.Where(e => e.Item1.Qoundition == Qoundition.Way).Count() == 1) return false;
        int Ways = 0;
        int FreeChois = 0;
        
        Cheak(Index, new List<Box>());

        if (FreeChois < 20) return false;
        return Ways > 2;
        void Cheak(Box Index, List<Box> UsedList, Side Diraction = Side.None) 
        {
            if (Diraction == Side.Up && (Index.Up ==null || UsedList.Contains(Index.Up) || Index.Up.Qoundition != Qoundition.Way)) Diraction = Side.None;
            else if (Diraction == Side.Down && (Index.Down == null || UsedList.Contains(Index.Down) || Index.Down.Qoundition != Qoundition.Way)) Diraction = Side.None;
            else if(Diraction == Side.Left && (Index.Left == null || UsedList.Contains(Index.Left) || Index.Left.Qoundition != Qoundition.Way)) Diraction = Side.None;
            else if(Diraction == Side.Right && (Index.Right == null || UsedList.Contains(Index.Right) || Index.Right.Qoundition != Qoundition.Way)) Diraction = Side.None;

            UsedList.Add(Index);
            if (Diraction == Side.None)
            {
                List<(Box, Side)> NewPoss = Index.CorrectBoxes;
                int FreeChoisCount = NewPoss.Where((d => !UsedList.Contains(d.Item1) && d.Item1.Qoundition == Qoundition.Way)).Count();
                if(FreeChoisCount > 1) FreeChois += FreeChoisCount -1;
                bool isThereWays = false;
                for (int i = 0; i < NewPoss.Count; i++)
                {
                    if (UsedList.Contains(NewPoss[i].Item1)) continue;
                    
                    if (NewPoss[i].Item1.Qoundition == Qoundition.Way) 
                    {
                        isThereWays = true;
                        Cheak(NewPoss[i].Item1, new TList<Box>(UsedList), NewPoss[i].Item2);
                    }
                }
                if (!isThereWays) Ways++;
            }
            else 
            {
                if(Diraction == Side.Up) Cheak(Index.Up, new TList<Box>(UsedList), Diraction);
                if(Diraction == Side.Down) Cheak(Index.Down, new TList<Box>(UsedList), Diraction);
                if(Diraction == Side.Left) Cheak(Index.Left, new TList<Box>(UsedList), Diraction);
                if(Diraction == Side.Right) Cheak(Index.Right, new TList<Box>(UsedList), Diraction);
            }
        }

    }


    /*

    // 0 = enpty, 1 = Filled  ↑ ↓ → ←
    void GenereatAllWays(List<List<(int Place, string Side)>> Placeses, Vector2Int Distanetion, Vector2Int Index) 
    {
        Placeses[Index.x][Index.y] = (1, Placeses[Index.x][Index.y].Side);
        if (Distanetion == Index) {
            FoundedWay.Invoke(Placeses);
            return; 
        }
        List<(Vector2Int, string)> NewPoss = new List<(Vector2Int, string)>() {(Index + Vector2Int.up, "↑" ), (Index + Vector2Int.down, "↓"), (Index + Vector2Int.left, "←"), (Index + Vector2Int.right , "→")};
        NewPoss.Mix();
        for (int i = 0; i < NewPoss.Count; i++)
        {
            if (NewPoss[i].Item1.x >= Placeses.Count ||
                 NewPoss[i].Item1.y >= Placeses[0].Count ||
                NewPoss[i].Item1.y < 0 || NewPoss[i].Item1.x < 0) continue;

            if (Placeses[NewPoss[i].Item1.x][NewPoss[i].Item1.y].Place ==  0)
            {
                Placeses[NewPoss[i].Item1.x][NewPoss[i].Item1.y] = (1, NewPoss[i].Item2);
                GenereatAllWays(Placeses, Distanetion, NewPoss[i].Item1);        
                Placeses[NewPoss[i].Item1.x][NewPoss[i].Item1.y] = (0, "");
            }
        }
        Placeses[Index.x][Index.y] = (0, "");
    }

    bool GenereatFirstWay(List<List<(int Place, string Side)>> Placeses, Vector2Int Distanetion, Vector2Int Index, int BlocksCount, int CorrentCount = -1) 
    {
        CorrentCount++;
        if (CorrentCount > BlocksCount) { return false; }
        Placeses[Index.x][Index.y] = (1, Placeses[Index.x][Index.y].Side);
        if (CorrentCount == BlocksCount) 
        {
            if (Distanetion == Index)
            {
                FoundedWay.Invoke(new List<List<(int Place, string Side)>>(Placeses));
                return true;
            }
            else { return false;  }
        }
        List<(Vector2Int, string)> NewPoss = new List<(Vector2Int, string)>() { (Index + Vector2Int.up, "↑"), (Index + Vector2Int.down, "↓"), (Index + Vector2Int.left, "←"), (Index + Vector2Int.right, "→") };
        NewPoss.Mix();

        int CCSS = CorrentCount;

        for (int i = 0; i < NewPoss.Count; i++)
        {

            if (NewPoss[i].Item1.x >= Placeses.Count ||
                 NewPoss[i].Item1.y >= Placeses[0].Count ||
                NewPoss[i].Item1.y < 0 || NewPoss[i].Item1.x < 0) continue;
            if (Placeses[NewPoss[i].Item1.x][NewPoss[i].Item1.y].Place == 0)
            {

                Placeses[NewPoss[i].Item1.x][NewPoss[i].Item1.y] = (1, NewPoss[i].Item2);
                bool Ans = GenereatFirstWay(Placeses, Distanetion, NewPoss[i].Item1, BlocksCount, CCSS);
                if (Ans) return true;
                Placeses[NewPoss[i].Item1.x][NewPoss[i].Item1.y] = (0, "");
                
            }
        }
        Placeses[Index.x][Index.y] = (0, "");
        return false; 
    }
    */
}
[Serializable]
public class Box
{
    public Box(Box Clone)
    {

        this.Y = Clone.Y;
        this.X = Clone.X;
        Side = Clone.Side;
        Qoundition = Clone.Qoundition;
    }
    public Box(int X, int Y)
    {
        this.Y = Y;
        this.X = X;
    }
    public int Y;
    public int X;
    
    public List<(Box, Side)> CorrectBoxes
    {
        get
        {
            if (_CorrectBoxes == null) 
            {
                _CorrectBoxes = new List<(Box, Side)>();
                if (Up != null) _CorrectBoxes.Add((Up, Side.Up));
                if (Down != null) _CorrectBoxes.Add((Down, Side.Down));
                if (Left != null) _CorrectBoxes.Add((Left, Side.Left));
                if (Right != null) _CorrectBoxes.Add((Right, Side.Right));
            }
            return _CorrectBoxes;
        }
    }
    public List<(Box, Side)> _CorrectBoxes;


    public Box Up
    {
        get
        {
            if (!Generation.Places.ContainsKey(new Vector2Int(X, Y + 1))) return null;
            else return Generation.Places[new Vector2Int(X, Y + 1)];
        }
    }
    public Box Down
    {
        get
        {
            if (!Generation.Places.ContainsKey(new Vector2Int(X, Y - 1))) return null;
            else return Generation.Places[new Vector2Int(X, Y - 1)];
        }
    }
    public Box Left
    {
        get
        {
            if (!Generation.Places.ContainsKey(new Vector2Int(X - 1, Y))) return null;
            else return Generation.Places[new Vector2Int(X - 1, Y)];
        }
    }
    public Box Right
    {
        get
        {
            if (!Generation.Places.ContainsKey(new Vector2Int(X + 1, Y))) return null;
            else return Generation.Places[new Vector2Int(X + 1, Y)];
        }
    }

    public Side Side;
    public Qoundition Qoundition;
}
public enum Side {None, Up, Down, Left, Right }
public enum Qoundition
{
    None, Way, Wall
}




[Serializable]
public class BoxData 
{
    public int Y;
    public int X;
    public Side Side;
    public Qoundition Qoundition;
} 
[Serializable]
public class Map 
{
    public List<BoxData> boxes;
    public int UseCount => boxes.Count - 1;
    public Vector2Int Size;
    public int StartPosition;
    public int EndPosition;
    public List<int> WayToWin 
    {
        get 
        {
            Dictionary<Vector2Int, BoxData> Places = new Dictionary<Vector2Int, BoxData>();
            boxes.ForEach(s => Places.Add(new Vector2Int(s.X, s.Y), s));
            List<int> Way = new List<int>();
            Cheak(boxes[StartPosition]);
            void Cheak(BoxData Mowing) 
            {
                Way.Add(boxes.IndexOf(Mowing));
                if (Mowing.Side == Side.Right) Cheak(Places[new Vector2Int(Mowing.X + 1, Mowing.Y)]);
                else if (Mowing.Side == Side.Left) Cheak(Places[new Vector2Int(Mowing.X - 1, Mowing.Y)]);
                else if(Mowing.Side == Side.Up) Cheak(Places[new Vector2Int(Mowing.X, Mowing.Y + 1)]);
                else if(Mowing.Side == Side.Down) Cheak(Places[new Vector2Int(Mowing.X, Mowing.Y - 1)]);
            }
            return Way;
        }
    }
}

