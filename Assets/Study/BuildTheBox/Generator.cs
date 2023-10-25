using Base.Word;
using Study.aSystem;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Study.BuildTheBox
{
    public static class Generator
    {
        public static TList<Vector2Int> Places;
        public static Dictionary<Vector2Int, char> PlacesAndLetters;
        public static TList<TList<(Vector2Int, char)>> Gropes;
        public static TList<Content> ChosenContents;
        public static void Generate()
        {


            TList<Content> Contents = new TList<Content>(GameObject.FindObjectOfType<Quest>().NeedWordList);

            PlacesAndLetters = new Dictionary<Vector2Int, char>();



            // mac cound = 7 item max count = 6
            ChosenContents = new TList<Content>();
            int YSize = Random.Range(4, 8);

            Contents.ForEach(c =>
            {
                if (c.EnglishSource.Length < 7 && ChosenContents.Count < YSize) ChosenContents.Add(c);
            });
            ChosenContents.ForEach((C, i) =>
            {
                List<(Vector2Int, char)> values = Acomadate(C.EnglishSource, i);
                values.ForEach(d => PlacesAndLetters.Add(d.Item1, d.Item2));
            });
            Places = new TList<Vector2Int>(PlacesAndLetters.Keys);
            Gropes = new TList<TList<(Vector2Int, char)>>();
            TList<TList<Vector2Int>> Giz = Divorce(new List<Vector2Int>(PlacesAndLetters.Keys));
            Giz.ForEach((Grupe) =>
            {
                Gropes.Add(new TList<(Vector2Int, char)>());
                Grupe.ForEach((Item) => Gropes.Last.Add((Item, PlacesAndLetters[Item])));
            });



            List<(Vector2Int, char)> Acomadate(string Word, int Y)
            {
                List<(Vector2Int, char)> Places = new List<(Vector2Int, char)>();
                int BackGaps = 0;
                for (int i = 0; i < 6 - Word.Length; i++) BackGaps += (Random.Range(0, 100) > 50) ? 1 : 0;
                int OldLenght = Word.Length;
                for (int i = 0; i < OldLenght; i++)
                {
                    Places.Add((new Vector2Int(BackGaps + i, Y), Word[0]));
                    Word = Word.Remove(0, 1);
                }
                return Places;
            }

        }

        private static TList<TList<Vector2Int>> Divorce(TList<Vector2Int> Positions)
        {

            Vector2Int SaverMax = new Vector2Int(-99, -99);
            Vector2Int SaverMin = new Vector2Int(99, 99);
            for (int i = 0; i < Positions.Count; i++)
            {
                if (SaverMax.x < Positions[i].x) SaverMax.x = Positions[i].x;
                if (SaverMax.y < Positions[i].y) SaverMax.y = Positions[i].y;
                if (Positions[i].x < SaverMin.x) SaverMin.x = Positions[i].x;
                if (Positions[i].y < SaverMin.y) SaverMin.y = Positions[i].y;
            }
            Vector2Int UR = SaverMax;
            Vector2Int DL = SaverMin;
            Vector2Int UL = new Vector2Int(SaverMin.x, SaverMax.y);
            Vector2Int DR = new Vector2Int(SaverMax.x, SaverMin.y);

            List<Vector2Int> ConerPatterns = new List<Vector2Int>() { new(2, 2), new(2, 3), new(3, 2) };

            //List<Vector2Int> OtherPatterns = new List<Vector2Int>() {new(2, 1), new(1, 2)};
            TList<TList<Vector2Int>> Divorced = new TList<TList<Vector2Int>>();
            DivorceConner(ConerPatterns.RandomItem(), UR, new(-1, -1));
            DivorceConner(ConerPatterns.RandomItem(), UL, new(1, -1));
            DivorceConner(ConerPatterns.RandomItem(), DR, new(-1, 1));
            DivorceConner(ConerPatterns.RandomItem(), DL, new(1, 1));

            void DivorceConner(Vector2Int Size, Vector2Int Coner, Vector2Int MoveSide)
            {
                List<Vector2Int> NeedPoses = new List<Vector2Int>();
                for (int y = 0; y < Size.y; y++)
                    for (int x = 0; x < Size.x; x++)
                        NeedPoses.Add(Coner + new Vector2Int((MoveSide.x > 0) ? x : -x, (MoveSide.y > 0) ? y : -y));
                TList<Vector2Int> ResoltPoses = new List<Vector2Int>();
                NeedPoses.ForEach(d => ResoltPoses.AddIf(d, Positions.Contains(d)));
                ResoltPoses.ForEach(a => Positions.Remove(a));
                Divorced.Add(ResoltPoses);
            }






            List<List<List<Vector2Int>>> AllDevorses = new List<List<List<Vector2Int>>>();

            List<Vector2Int> OtherBigPatterns = new() { new(2, 3), new(2, 2) };
            List<Vector2Int> OtherPatterns = new() { new(5, 2), new(4, 2), new(5, 1), new(4, 1), new(1, 3), new(2, 1), new(1, 1) };
            OtherBigPatterns.Mix().ForEach(s => OtherPatterns.AddTo(0, s));







            List<(List<List<Vector2Int>> AllDevorses, List<Vector2Int> LeftPoses)> Resolts = new();
            for (int i = 0; i < OtherPatterns.Count; i++)
            {
                Resolts = new();
                FindAcomadation(Positions, new List<List<Vector2Int>>(), OtherPatterns[i]);

                (List<List<Vector2Int>> AllDevorses, List<Vector2Int> LeftPoses)? Ch1 = null;
                Resolts.ForEach(a => { if (Ch1 == null || Ch1.Value.LeftPoses.Count > a.LeftPoses.Count) Ch1 = a; });
                if (Ch1 != null)
                {
                    Ch1.Value.AllDevorses.ForEach(a => Divorced.Add(a));
                    Ch1.Value.AllDevorses.ForEach(d => d.ForEach(qq => Positions.Remove(qq)));
                }
            }

            void FindAcomadation(List<Vector2Int> LeftPoses, List<List<Vector2Int>> Devorses, Vector2Int Patterns)
            {
                foreach (var Box in LeftPoses)
                {
                    List<Vector2Int> OtherPatterns = new List<Vector2Int>() { Patterns, new Vector2Int(Patterns.y, Patterns.x) }.Mix();
                    foreach (var Pattern in OtherPatterns)
                    {
                        if (CanAcomadate(Pattern, Box))
                        {
                            List<Vector2Int> NewLeftPoses = Divorce(Pattern, Box, out List<Vector2Int> EDDS);
                            List<List<Vector2Int>> NewDevorses = new List<List<Vector2Int>>(Devorses) { EDDS };
                            if (NewLeftPoses.Count == 0)
                            {
                                //Debug.Log("dd");
                                Resolts.Add((NewDevorses, NewLeftPoses));
                                return;
                            }
                            FindAcomadation(NewLeftPoses, NewDevorses, Pattern);
                        }
                        else if (LeftPoses.Count != Positions.Count)
                        {
                            if (Resolts.Count > 0)
                            {
                                if (Resolts.Last().AllDevorses.Last()[0] != Devorses.Last()[0])
                                {
                                    Resolts.Add((Devorses, LeftPoses));
                                }
                            }
                            else Resolts.Add((Devorses, LeftPoses));

                        }
                    }
                }

                bool CanAcomadate(Vector2Int Size, Vector2Int Coner)
                {
                    for (int y = 0; y < Size.y; y++)
                        for (int x = 0; x < Size.x; x++)
                            if (!LeftPoses.Contains(Coner + new Vector2Int(x, y)))
                                return false;
                    return true;
                }
                List<Vector2Int> Divorce(Vector2Int Size, Vector2Int Coner, out List<Vector2Int> Devorsed)
                {
                    Devorsed = new List<Vector2Int>();
                    for (int y = 0; y < Size.y; y++)
                        for (int x = 0; x < Size.x; x++)
                            Devorsed.Add(Coner + new Vector2Int(x, y));
                    TList<Vector2Int> ResoltPoses = new List<Vector2Int>(LeftPoses);
                    Devorsed.ForEach(a => ResoltPoses.Remove(a));
                    return ResoltPoses;
                }

            }



            return Divorced;
        }


        /*
        private void OnDrawGizmos()
        {
            Giz.ForEach((g, i) =>
            {
                Gizmos.color = new TList<Color>()
                {Color.red, Color.white, Color.blue, Color.green, Color.gray, Color.black, Color.cyan, Color.magenta}[i, ListGetType.Loop];
                g.ForEach(d => Gizmos.DrawCube(Parrent.transform.position + new Vector3(d.x, d.y, 0) * .6f, new Vector3(.5f, .5f, .5f)));
            });
        }*/
    }
}