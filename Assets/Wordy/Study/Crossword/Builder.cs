using System.Collections;
using System.Collections.Generic;
using SystemBox.Simpls;
using SystemBox;
using TMPro;
using UnityEngine;
using System.Linq;
namespace Study.Crossword
{
    public class Builder : MonoBehaviour
    {
        public GameObject OneBox;

        public static LevlData ToBuild;
        void Start()
        {
            TList<string> vs = new TList<string>();



            GameObject.FindFirstObjectByType<QuestCrossword>().NeedWordList.ForEach(a => vs.Add(a.EnglishSource));
            ToBuild = Generaton.tryGnereat(vs, (Vector2Int.one * 10, -(Vector2Int.one * 10)));


            Dictionary<string, GameObject> Gropps = new Dictionary<string, GameObject>();
            ToBuild.GropesID.ForEach(Grope =>
            {
                GameObject NewGrope = new GameObject(Grope);
                NewGrope.transform.SetParent(transform);
                NewGrope.AddComponent<RectTransform>();
                NewGrope.AddComponent<InputTest>();
                NewGrope.transform.localPosition = Vector3.zero;
                Gropps.Add(Grope, NewGrope);
            });
            ToBuild.AllPoseIDWord.ForEach(One =>
            {
                GameObject OneeObjecrt = Instantiate(OneBox, transform);
                OneeObjecrt.GetComponentsInChildren<TextMeshProUGUI>().ToList().ForEach(a => a.text = One.Word.ToString().ToUpper());
                OneeObjecrt.transform.localPosition = new Vector3(One.pos.x, One.pos.y);
                OneeObjecrt.transform.SetParent(Gropps[One.GropeID].transform);
                OneeObjecrt.GetComponent<OneBox>().MinInfo = One;
            });
            //return;
            Gropps.Values.ToList().ForEach(x => fixPos(x.transform));
            Gropps.Values.ToList().ForEach(x => x.transform.localPosition = Vector3.zero);
            AllItems = new TList<Transform>();
            Gropps.Values.ToList().ForEach(x =>
            {
                x.transform.position = FindEnptyPos(x, true);
                AllItems.AddRange(x.transform.Childs());
            });
            void fixPos(Transform OneGropper)
            {

                List<Transform> Cilds = OneGropper.transform.Childs();
                Cilds.ForEach(c => { c.SetParent(OneGropper.parent); });
                OneGropper.transform.position = Cilds[0].transform.position;
                Cilds.ForEach(c => { c.SetParent(OneGropper); });
            }

        }

        static TList<Transform> AllItems;

        public static Vector3Int FindEnptyPos(GameObject Me, bool BibSize = false)
        {
            if (AllItems == null) AllItems = new TList<Transform>();
            TList<Transform> Chailds = Me.transform.Childs();
            TList<Vector3> Others = new TList<Vector3>();
            AllItems.ForEach(a => Others.Add((Vector3)a.transform.position.ToInt()));
            if (BibSize == false) for (int i = 0; i < Chailds.Count; i++) Others.Remove(Chailds[i].transform.position.ToInt());




            Vector3 OldPos = Me.transform.position;
            Me.transform.position = Me.transform.position.ToInt();
            Vector3Int LastTrue = Me.transform.position.ToInt();







            TList<Vector3Int> vectors = new TList<Vector3Int>();
            vectors += new Vector3Int(LastTrue.x, LastTrue.y);
            for (int i = 0; i < 25; i++)
            {
                testHorizontalRight();
                testVerticalDown();
                testHorizontalLeft();
                testVerticalUp();
                void testHorizontalRight()
                {
                    while (true)
                    {
                        vectors += vectors.Last + Vector3Int.right;
                        if (!vectors.Contains(vectors.Last + Vector3Int.down)) break;
                    }
                }
                void testVerticalDown()
                {
                    while (true)
                    {
                        vectors += vectors.Last + Vector3Int.down;
                        if (!vectors.Contains(vectors.Last + Vector3Int.left)) break;
                    }
                }
                void testHorizontalLeft()
                {
                    while (true)
                    {
                        vectors += vectors.Last + Vector3Int.left;
                        if (!vectors.Contains(vectors.Last + Vector3Int.up)) break;
                    }
                }
                void testVerticalUp()
                {
                    while (true)
                    {
                        vectors += vectors.Last + Vector3Int.up;
                        if (!vectors.Contains(vectors.Last + Vector3Int.right)) break;
                    }
                }

            }


            TList<Vector3Int> TodisPos = new TList<Vector3Int>();
            for (int i = 0; i < vectors.Count; i++)
            {
                Me.transform.position = vectors[i];
                if (IsOk()) TodisPos += vectors[i];
                if (TodisPos.Count > 6) break;
            }

            float Dis = 999f;
            int Need_I = 0;
            for (int i = 0; i < TodisPos.Count; i++)
            {
                if (TMath.Distance(TodisPos[i], LastTrue) < Dis)
                {
                    Dis = TMath.Distance(TodisPos[i], LastTrue);
                    Need_I = i;
                }
            }
            LastTrue = TodisPos[Need_I];
            Me.transform.position = OldPos;



            bool IsOk()
            {
                for (int c = 0; c < Chailds.Count; c++)
                {
                    if (BibSize)
                    {
                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.left)) return false;
                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.left + Vector3.up)) return false;
                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.left + Vector3.down)) return false;

                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.right)) return false;
                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.right + Vector3.up)) return false;
                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.right + Vector3.down)) return false;

                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.up)) return false;
                        if (Others.Contains(Chailds[c].transform.position.ToInt() + Vector3.down)) return false;
                        if (Others.Contains(Chailds[c].transform.position.ToInt())) return false;
                    }
                    else if (Others.Contains(Chailds[c].transform.position.ToInt())) return false;
                }


                return true;
            }
            return LastTrue;
        }

        public static void TrayJoin(Transform ToTest)
        {

            List<OneBox> AllOtheres = GameObject.FindObjectsByType<OneBox>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
            List<OneBox> ToCheakBoxes = new List<OneBox>();
            ToTest.transform.Childs().ForEach(a => ToCheakBoxes.Add(a.GetComponent<OneBox>()));
            ToCheakBoxes.ForEach(a => AllOtheres.Remove(a));


            for (int i = 0; i < ToCheakBoxes.Count; i++)
            {

                TList<OneBox> Nears = new List<OneBox>();
                AllOtheres.ForEach(a => Nears.AddIf(a, Vector3.Distance(a.transform.position, ToCheakBoxes[i].transform.position) == 1f));


                if (Cheak(ToCheakBoxes[i], Nears, out OneBox NeedToMarge))
                {
                    Marge(ToCheakBoxes[i].transform.parent, NeedToMarge.transform.parent);
                    TrayJoin(ToCheakBoxes[i].transform.parent);
                    return;
                }
            }




            GameObject.FindFirstObjectByType<CrosswordSystem>().StartCoroutine(DD());
            IEnumerator DD()
            {
                yield return new WaitForSeconds(1f);

                if (GameObject.FindObjectsByType<InputTest>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length == 1)
                {
                    
                    GameObject.FindFirstObjectByType<CrosswordSystem>().OnWin();
                }
            }





            bool Cheak(OneBox ToCheak, List<OneBox> Nears, out OneBox NeedToMarge)
            {
                NeedToMarge = null;
                for (int i = 0; i < Nears.Count; i++)
                    if (LevlData.CanBeJoined(ToCheak.MinInfo, Nears[i].MinInfo, ToCheak.transform.position.x == Nears[i].transform.position.x))
                    {
                        NeedToMarge = Nears[i];
                        return true;
                    }
                return false;
            }

            void Marge(Transform First, Transform Second)
            {
                Second.Childs().ForEach(a => a.SetParent(First));
                GameObject ss = Second.gameObject;
                Destroy(ss);
            }


        }
    }
}