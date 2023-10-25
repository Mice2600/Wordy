using Base.Word;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
namespace Study.BuildTheBox
{
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

        public GameObject ShadowPrefab;
        public GameObject BackBoxPrefab;
        public List<GameObject> BoxPrefab;
        public GameObject Parrent;
        public GameObject BlocksParrent;
        public GameObject WinViwe;
        private TList<ContentGrope> contentGropes;

        public void CreatG()
        {
            Parrent.ClearChilds();
            BlocksParrent.ClearChilds();
            Objects = new List<GameObject>();
            contentGropes = new TList<ContentGrope>();
            Generator.Gropes.ForEach((Grope, GropeIndex) =>
            {
                GameObject GropeObject = new GameObject("Grope " + GropeIndex);
                contentGropes.Add(GropeObject.AddComponent<ContentGrope>());
                contentGropes.Last.MyGrope = Grope;

                GropeObject.transform.SetParent(BlocksParrent.transform);
                GropeObject.transform.localScale = Vector3.one;
                GropeObject.transform.localPosition = new Vector3(0, 0, 0);
                GameObject BoxPrefab = this.BoxPrefab.RandomItem();
                Grope.ForEach(item =>
                {
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


            Objects.ForEach(a =>
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
                a.transform.Childs().ForEach(ss => ss.transform.transform.localPosition = new Vector3(ss.transform.transform.localPosition.x - Min_X, ss.transform.transform.localPosition.y - Min_Y, ss.transform.transform.localPosition.z));
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
            AccomadatePositions(new(4, 7));
        }
        public TList<Vector2Int> Arrea = new List<Vector2Int>();
        [ShowInInspector]
        List<(GameObject Object, Vector2Int Size)> Biggest;
        public void AccomadatePositions(Vector2Int Size)
        {
            TList<Vector2Int> Arrea = new List<Vector2Int>();
            for (int y = 0; y < Size.y; y++)
                for (int x = 0; x < Size.x; x++)
                {
                    Arrea.AddIfDirty(new Vector2Int(x, y));
                    Arrea.AddIfDirty(new Vector2Int(-x, y));
                }


            this.Arrea = new TList<Vector2Int>(Arrea);

            //List<(GameObject Object, Vector2Int Size)> Biggest = new List<(GameObject Object, Vector2Int Size)>(ObjectsAndSize);
            Biggest = new List<(GameObject Object, Vector2Int Size)>(ObjectsAndSize);
            Biggest.Sort(Compare);
            int Compare((GameObject Object, Vector2Int Size) x, (GameObject Object, Vector2Int Size) y)
            {
                if (x.Size.y + x.Size.x > y.Size.y + y.Size.x) return -1;
                else return 1;
            }
            Biggest.ForEach(a =>
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Accomadate(a, out Vector2Int ResoltPos))
                    {
                        a.Object.transform.localPosition =
                        new Vector3(ResoltPos.x, ResoltPos.y, a.Object.transform.localPosition.z);
                        a.Object.transform.localScale = a.Object.transform.localScale * .9f;
                        break;
                    }
                }

            });
            bool Accomadate((GameObject Object, Vector2Int Size) OtherBoxes, out Vector2Int ResoltPos)
            {
                ResoltPos = Vector2Int.zero;
                for (int i = 0; i < Arrea.Count; i++)
                {
                    if (isSuitble(Arrea[i], OtherBoxes.Size))
                    {

                        ResoltPos = Arrea[i];
                        for (int y = 0; y < OtherBoxes.Size.y + 0; y++)
                            for (int x = 0; x < OtherBoxes.Size.x + 0; x++)
                                Arrea.Remove(ResoltPos + new Vector2Int(x, y));
                        return true;
                    }
                }
                return false;
                bool isSuitble(Vector2Int NeedPos, Vector2Int NeedSize)
                {
                    for (int y = 0; y < NeedSize.y; y++)
                        for (int x = 0; x < NeedSize.x; x++)
                            if (!Arrea.Contains(NeedPos + new Vector2Int(x, y))) return false;
                    return true;

                }
            }

        }




        bool IsDone = false;
        private void Update()
        {
            if (IsDone) return;

            bool isTrue = true;
            contentGropes.ForEach(a => { if (!a.OnPlace) isTrue = false; });
            if (isTrue)
            {
                IsDone = true;

                contentGropes.ForEach(a => a.enabled = false);

                StartCoroutine(OnWin());
                IEnumerator OnWin()
                {
                    yield return new WaitForSeconds(1f);
                    Instantiate(WinViwe).GetComponent<WinViwe>().contents = Generator.ChosenContents;
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
}