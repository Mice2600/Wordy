using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox.Simpls;
using SystemBox;
using UnityEngine;
using Base.Word;

public class GeneratonOld : MonoBehaviour
{

    public static Dictionary<Vector3Int, Box> AllCreated => _AllCreated ??= new Dictionary<Vector3Int, Box>();
    private static Dictionary<Vector3Int, Box> _AllCreated;

    [Button]
    public LevlData tryGnereat()
    {
        GameObject.FindObjectsOfType<TextMesh>().ToList().ForEach(t => { GameObject g = t.gameObject; DestroyImmediate(g); });

        TList<string> vs = new TList<string>();


        WordBase.Wordgs.GetContnetList(20).ForEach(a => vs.Add(a.EnglishSource));


        _AllCreated = new Dictionary<Vector3Int, Box>();
        vs.Mix();

        List<List<string>> TrueIdes = new List<List<string>>();

        while (vs.Count > 0)
        {
            int RandomIndex = Random.Range(0, vs.First.Length);
            char NeedID = vs.First[RandomIndex];
            TList<Box> boxesToTest = new TList<Box>(AllCreated.Values);
            TList<Vector3Int> PosToCheak = new TList<Vector3Int>();
            for (int i = 0; i < boxesToTest.Count; i++)
                if (boxesToTest[i].Word == NeedID)
                    PosToCheak += boxesToTest[i]._transform.position.ToInt();
            if (PosToCheak.Count == 0) PosToCheak += Vector3Int.zero;
            PosToCheak.Mix();

            for (int i = 0; i < PosToCheak.Count; i++)
                if (TryCreaGrupe(vs.First, PosToCheak[i]))
                    break;



            vs.RemoveFirst();

        }

        Dictionary<Box, List<Box>> Gropeer()
        {
            List<Box> allboxes = new List<Box>();

            List<Vector3Int> keyssss = new List<Vector3Int>(_AllCreated.Keys);
            for (int i = 0; i < _AllCreated.Count; i++) allboxes.Add(_AllCreated[keyssss[i]]);

            List<Box> AllEnds = new List<Box>();
            for (int i = 0; i < allboxes.Count; i++) if (allboxes[i].IsEnd || allboxes[i].IsDubleEnd) AllEnds.Add(allboxes[i]);




            List<Box> TTAllEnds = new List<Box>(AllEnds);


            Dictionary<Box, List<Box>> AllGropes = new Dictionary<Box, List<Box>>();
            List<Box> Usenboxes = new List<Box>();

            while (AllEnds.Count > 0)
            {
                for (int i = 0; i < AllEnds.Count; i++)
                {
                    if (!_Update(AllEnds[i]))
                    {
                        AllEnds.RemoveAt(i); i--;
                    }
                }
            }
            bool _Update(Box Lider) //return true if some newws
            {
                if (!AllGropes.ContainsKey(Lider))
                {
                    AllGropes.Add(Lider, new List<Box>() { Lider });
                    Usenboxes.Add(Lider);
                }
                Box lastt = AllGropes[Lider][AllGropes[Lider].Count - 1];

                List<Box> toSellect = new List<Box>();

                if (_AllCreated.ContainsKey(lastt._transform.position.ToInt() + Vector3Int.up))
                    if (!Usenboxes.Contains(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.up]))
                        toSellect.Add(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.up]);


                if (_AllCreated.ContainsKey(lastt._transform.position.ToInt() + Vector3Int.down))
                    if (!Usenboxes.Contains(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.down]))
                        toSellect.Add(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.down]);

                if (_AllCreated.ContainsKey(lastt._transform.position.ToInt() + Vector3Int.left))
                    if (!Usenboxes.Contains(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.left]))
                        toSellect.Add(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.left]);

                if (_AllCreated.ContainsKey(lastt._transform.position.ToInt() + Vector3Int.right))
                    if (!Usenboxes.Contains(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.right]))
                        toSellect.Add(_AllCreated[lastt._transform.position.ToInt() + Vector3Int.right]);

                if (toSellect.Count == 0) return false; //done remove 
                Box sellected = toSellect[Random.Range(0, toSellect.Count)];
                Usenboxes.Add(sellected);
                AllGropes[Lider].Add(sellected);
                return true;
            }

            new List<Box>(AllGropes.Keys).ForEach(k => {
                Color NNC = TColor.RandomColor();
                AllGropes[k].ForEach(i => i._transform.gameObject.GetComponent<TextMesh>().color = NNC);
            });



            return AllGropes;
        }
        Dictionary<Box, List<Box>> GropedDic = Gropeer();



        TList<(Vector3Int pos, string ID, char Word, string GropeID)> AllPoseIDWord = new TList<(Vector3Int pos, string ID, char Word, string GropeID)>();

        GropedDic.Values.ToList().ForEach((F, i) => {
            string GropeName = "Grope: " + i + " " + Random.Range(-99999f, 99999f);
            F.ForEach(b => AllPoseIDWord.Add((b.transform.position.ToInt(), b.ID, b.Word, GropeName)));
        });

        LevlData Resolt = new LevlData() { AllPoseIDWord = AllPoseIDWord };





        if (AllPoseIDWord.Count != _AllCreated.Count) return tryGnereat();

        return Resolt;
    }

    public bool TryCreaGrupe(string tt, Vector3Int Pos)
    {
        if (tt.Length < 3) return false;
        TList<int> indexsToTry = new TList<int>();
        if (AllCreated.ContainsKey(Pos))
            for (int i = 0; i < tt.Length; i++)
                if (tt[i] == AllCreated[Pos].Word)
                    indexsToTry += i;
        if (AllCreated.ContainsKey(Pos) && indexsToTry.Count == 0) return false;
        if (indexsToTry.Count == 0) indexsToTry += 0;

        for (int i = 0; i < indexsToTry.Count; i++)
        {
            int needStartCount = 0;
            int needEndCount = 0;
            for (int h = 0; h < tt.Length; h++)
            {
                if (h < indexsToTry[i]) needStartCount++;
                if (h > indexsToTry[i]) needEndCount++;
            }


            if (LeftEnptyCount(Pos) > needStartCount && RightEnptyCount(Pos) > needEndCount)
            {
                if (needStartCount == 0 && LeftEnptyCount(Pos) == 0
                    || needEndCount == 0 && RightEnptyCount(Pos) == 0)
                {
                    // not work becous  horizontal 1234 + 4562 is wrong
                }
                else
                {
                    CreaGrupe(tt, tt.Length - needEndCount - 1, true, Pos);
                    return true;
                }


            }
            if (UpEnptyCount(Pos) > needStartCount && DownEnptyCount(Pos) > needEndCount)
            {
                if (needStartCount == 0 && UpEnptyCount(Pos) == 0
                    || needEndCount == 0 && DownEnptyCount(Pos) == 0)
                {
                    // not work becous  vertical 1234 + 4562 is wrong
                }
                else
                {
                    CreaGrupe(tt, tt.Length - needEndCount - 1, false, Pos);
                    return true;
                }

            }
        }

        return false;
    }

    public void CreaGrupe(string tt, int index, bool horizontal, Vector3Int IndexPos) // tested
    {
        for (int i = 0; i < tt.Length; i++)
        {
            Vector3 pos = new Vector3();
            if (i == index) { pos = IndexPos; }
            if (i < index)
            {
                pos = new Vector3(
   (horizontal) ? IndexPos.x - (index - i) : IndexPos.x,
   (!horizontal) ? IndexPos.y + (index - i) : IndexPos.y, 0);
            }
            if (i > index)
            {
                pos = new Vector3(
                    (horizontal) ? (IndexPos.x - index) + i : IndexPos.x,
                    (!horizontal) ? IndexPos.y - (i - index) : IndexPos.y, 0);
            }
            if (AllCreated.ContainsKey(pos.ToInt())) continue;
            GameObject go = new GameObject("t" + Random.Range(0, 1111));
            go.transform.position = pos;
            go.AddComponent<TextMesh>().text = tt[i].ToString().ToUpper();
            Box dd = go.transform.gameObject.AddComponent<Box>();
            dd.Word = tt[i];
            dd.ID = $"Full_ID_<{tt}>_|_Count_<{i}>_|_Text_<{tt[i]}>_End";
            AllCreated.Add(dd._transform.position.ToInt(), dd);
        }
    }

    public static int UpEnptyCount(Vector3Int PosToTest)
    {
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.up * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.up))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.up))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.up))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;
    }

    public static int DownEnptyCount(Vector3Int PosToTest)
    {
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.down * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.down))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.down))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.down))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;

    }

    public static int RightEnptyCount(Vector3Int PosToTest)
    {
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.right * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.down))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.up))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.down))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.up))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;
    }

    public static int LeftEnptyCount(Vector3Int PosToTest)
    {
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.left * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.down))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.up))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.down))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.up))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;
    }

    public class Box : MonoBehaviour
    {
        public char Word;
        public string ID;
        public Transform _transform => transform;

        [ShowInInspector]
        public int SaidCount
        {
            get
            {
                int SC = 0;
                if (GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.up)) SC++;
                if (GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.down)) SC++;
                if (GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.left)) SC++;
                if (GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.right)) SC++;
                return SC;
            }
        }
        [ShowInInspector]
        public bool IsEnd => SaidCount <= 1;
        public bool IsDubleEnd
        {
            get
            {
                if (SaidCount != 2) return false;
                if (GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.up) && GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.down)) return false;
                if (GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.left) && GeneratonOld.AllCreated.ContainsKey(_transform.position.ToInt() + Vector3Int.right)) return false;
                return true;
            }
        }

    }
}
