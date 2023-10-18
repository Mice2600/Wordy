using Base.Word;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;

public class BuildTheBox : MonoBehaviour
{

    public List<GameObject> Objects;
    [ShowInInspector]
    public List<(GameObject, Vector2Int Size)> ObjectsAndSize;
    void Start()
    {
        Generator.Generate();
        CreatG();
    }

    public GameObject BackBoxPrefab;
    public List<GameObject> BoxPrefab;
    public GameObject Parrent;
    public void CreatG() 
    {
        Parrent.ClearChilds();
        Objects = new List<GameObject>();
        Generator.Gropes.ForEach((Grope, GropeIndex) => {
            GameObject GropeObject = new GameObject("Grope " + GropeIndex);
            GropeObject.transform.SetParent(Parrent.transform);
            GropeObject.transform.localScale = Vector3.one;
            GropeObject.transform.localPosition = new Vector3(0, 0, 0);
            GameObject BoxPrefab = this.BoxPrefab.RandomItem();
            Grope.ForEach(item => {
                GameObject dd = Instantiate(BoxPrefab, GropeObject.transform);
                dd.name = item.Item2.ToString();
                dd.transform.localPosition = new Vector3(item.Item1.x, item.Item1.y, 0);
                dd.GetComponentInChildren<TMP_Text>().text = item.Item2.ToString();


                GameObject Bass = Instantiate(BackBoxPrefab, Parrent.transform);
                Bass.name = item.Item2.ToString();
                Bass.transform.localPosition = new Vector3(item.Item1.x, item.Item1.y, 1);

            });
            Objects.Add(GropeObject);
        });
        
        
        Objects.ForEach(a => { 

            
            float Max_X = 0;
            float Min_X = 100;
            float Max_Y = 0;
            float Min_Y = 100;
            a.transform.Childs().ForEach(D =>
            {
                if (Max_X < D.localPosition.x) Max_X = D.localPosition.x;
                if (Max_Y < D.localPosition.y) Max_Y = D.localPosition.y;
                if (Min_X > D.localPosition.x) Min_X = D.localPosition.x;
                if (Min_Y > D.localPosition.y) Min_Y = D.localPosition.y;
            });
            a.transform.Childs().ForEach(ss =>ss.transform.transform.localPosition = new Vector3(ss.transform.transform.localPosition.x - Min_X, ss.transform.transform.localPosition.y - Min_Y, ss.transform.transform.localPosition.z));
        });



        ObjectsAndSize = new List<(GameObject, Vector2Int Size)>();
        Objects.ForEach(a => { ObjectsAndSize.Add((a, FindSize(a))); });

        Vector2Int FindSize(GameObject a) 
        {
            float Max_X = 0;
            float Min_X = 100;
            float Max_Y = 0;
            float Min_Y = 100;
            a.transform.Childs().ForEach(D =>
            {
                if (Max_X < D.localPosition.x) Max_X = D.localPosition.x;
                if (Max_Y < D.localPosition.y) Max_Y = D.localPosition.y;
                if (Min_X > D.localPosition.x) Min_X = D.localPosition.x;
                if (Min_Y > D.localPosition.y) Min_Y = D.localPosition.y;
            });
            return new Vector2Int(SystemBox.Simpls.TMath.Distance(Max_X, Min_X).ToInt(), SystemBox.Simpls.TMath.Distance(Max_Y, Min_Y).ToInt()) + Vector2Int.one;
        }

        AccomadatePositions(new(4, 5));
    }
    public void AccomadatePositions(Vector2Int Size) 
    {
        
        TList<Vector2Int> Arrea = new List<Vector2Int>();
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                Arrea.AddIfDirty(new Vector2Int(3, -9) + new Vector2Int(x, y));
                Arrea.AddIfDirty(new Vector2Int(3, -9) - new Vector2Int(x, y));
            }
        }


        List<(GameObject Object, Vector2Int Pos)> FinelResult = new List<(GameObject Object, Vector2Int Pos)>();

        if (Accomadate(Arrea, ObjectsAndSize.Mix(), new List<(GameObject Object, Vector2Int Pos)>()))
            FinelResult.ForEach(s => s.Object.transform.localPosition = new Vector3(s.Pos.x, s.Pos.y, s.Object.transform.localPosition.z));
        //else AccomadatePositions(Size + Vector2Int.one);
        bool Accomadate(TList<Vector2Int>  Arrea, List<(GameObject Object, Vector2Int Size)> OtherBoxes, List<(GameObject Object, Vector2Int Pos)> Result) 
        {
            foreach (var Pos in Arrea)
            {
                foreach (var item in OtherBoxes)
                {
                    if (isSuitble(Pos, item.Size)) 
                    {
                        List<Vector2Int>  newVErsion = DevorseVertion(Pos, item.Size);
                        List<(GameObject Object, Vector2Int Pos)> NewResult = new List<(GameObject Object, Vector2Int Pos)>(Result) { (item.Object, Pos) };
                        List<(GameObject Object, Vector2Int Pos)> NewOtherBoxes = new List<(GameObject Object, Vector2Int Pos)>(OtherBoxes);
                        Debug.Log(NewResult);
                        NewOtherBoxes.Remove(item);
                        if (NewOtherBoxes.Count == 0) 
                        {
                            Debug.Log("ddd");
                            FinelResult = NewResult;
                            return true; 
                        }
                        Accomadate(newVErsion, NewOtherBoxes, NewResult);
                    }
                }
            }
            return false;




            bool isSuitble(Vector2Int NeedPos, Vector2Int NeedSize) 
            {
                for (int y = 0; y < NeedSize.y; y++)
                    for (int x = 0; x < NeedSize.x; x++)
                        if (!Arrea.Contains(NeedPos + new Vector2Int(NeedSize.x, NeedSize.y))) return false;
                return true;

            }

            List<Vector2Int> DevorseVertion(Vector2Int NeedPos, Vector2Int NeedSize)
            {
                List<Vector2Int> NewList = new List<Vector2Int>(Arrea);
                for (int y = 0; y < NeedSize.y; y++)
                    for (int x = 0; x < NeedSize.x; x++)
                        NewList.Remove(NeedPos + new Vector2Int(NeedSize.x, NeedSize.y));
                return NewList;
            }

        }

    }
    /*
    private void OnDrawGizmos()
    {
        Giz.ForEach((g, i) => 
        {
            Gizmos.color = new TList<Color>() 
            {Color.red, Color.white, Color.blue, Color.green, Color.gray, Color.black, Color.cyan, Color.magenta}[i, ListGetType.Loop]; 
            g.ForEach(d => Gizmos.DrawCube(Parrent.transform.position + new Vector3(d.x, d.y, 0) * .6f, new Vector3(.5f, .5f, .5f))); });
    }*/

}
