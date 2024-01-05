using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using SystemBox;
using TMPro;
using UnityEngine;
namespace Study.WaterMelon
{
    public class Creator : MonoBehaviour
    {


        [SerializeField, Required]
        private TList<TList<GameObject>> WaterMelonContentPrefabs;

        public struct WayRuntime
        {
            public List<GameObject> Prefab;
            public Content Content;
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj is not WayRuntime) return false;
                if ((obj as WayRuntime?).Value.Content != Content) return false;
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }




        private TList<Content> Contents;

        private TList<WaterMelonContent> Created;
        [SerializeField]
        private GameObject LeftObstecl, FightObstecl;
        [SerializeField]
        private GameObject WinViwe;
        private void Start()
        {
            FightObstecl.transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.right).x, FightObstecl.transform.position.y, FightObstecl.transform.position.z);
            LeftObstecl.transform.position = new Vector3(-FightObstecl.transform.position.x, FightObstecl.transform.position.y, FightObstecl.transform.position.z);

            Created = new TList<WaterMelonContent>();
            TList<Content> contents = new TList<Content>(WordBase.Wordgs);
            Contents = new TList<Content>();
            WaterMelonContentPrefabs.ForEach(G =>
            {

                Contents.Add(Find(G.RandomItem.GetComponent<WaterMelonContent>(), contents));
                contents.Remove(Contents.Last);
            });
            Content Find(WaterMelonContent waterMelonContent, TList<Content> contents)
            {
                TList<Content> NeedConttents = contents.FindAll(C =>
                {
                    if (C.EnglishSource.Length <= waterMelonContent.MaxCount)
                    {
                        bool Isthere = false;
                        (C as IMultiTranslation).Translations.ForEach(x => { if (C.RussianSource.Length <= waterMelonContent.MaxCount) Isthere = true; });
                        return Isthere;
                    }
                    return false;
                });
                if (NeedConttents.Count == 0) return null;
                return NeedConttents.Mix().RandomItem;
            }
        }

        private List<WaterMelonContent> DontUse;
        float Offcet;
        public void Creat(WaterMelonContent Left, WaterMelonContent Right)
        {
            if (Left == null) return;
            if (Right == null) return;
            if (DontUse == null) DontUse = new List<WaterMelonContent>();
            if (DontUse.Contains(Left) || DontUse.Contains(Right)) return;
            GameObject W = Instantiate(WaterMelonContentPrefabs[(Contents.IndexOf(Right.Content) + 1), ListGetType.Loop].RandomItem, transform);
            W.transform.position = (Right.transform.position + Left.transform.position) / 2;
            W.GetComponent<WaterMelonContent>().Sort(Contents[Contents.IndexOf(Right.Content) + 1, ListGetType.Loop]);
            Created.Add(W.GetComponent<WaterMelonContent>());
            DontUse.Add(Right);
            DontUse.Add(Left);


            Destroy(Left.gameObject);
            Destroy(Right.gameObject);
            //Right.Content
        }

        public GameObject OnControll;
        public TextMeshPro NexText;
        float LosseTime;
        bool StopDone;

        void Update()
        {

            if (StopDone) return;
            float OldVV = LosseTime;
            Created.ForEach(a =>
            {
                if (a == null) return;
                if (a.gameObject == OnControll) return;
                if (a.transform.position.y > 4) LosseTime += Time.deltaTime;
            });
            if (OldVV == LosseTime) LosseTime = 0f;
            if (LosseTime > 3)
            {
                StopDone = true;
                Study.aSystem.WinViwe.Creat(Contents);


            }
            Offcet += Time.deltaTime;
            if (Offcet < 1) return;
            if (OnControll == null) Creat();
            OnControll.transform.position = new Vector3(0, 7.5f, 0f);
            if (TInput.GetMouseButton(0))
            {
                OnControll.transform.position = new Vector3(TInput.mouseWorldPoint(0).x, 7.5f, 0f);
            }
            if (TInput.GetMouseButtonUp(0))
            {



                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                    EasyTTSUtil.SpeechAdd((OnControll.GetComponent<ContentObject>().Content as ISpeeker).SpeekText, 1, .5f, 1);
                else Debug.Log(OnControll.GetComponent<ContentObject>().Content.EnglishSource + " Speeking");


                OnControll.transform.position = new Vector3(TInput.mouseWorldPoint(0).x, 7.5f, 0f);
                OnControll = null;
                Offcet = 0;

            }
        }
        private void OnDrawGizmos()
        {

        }
        public void Creat()
        {


            List<Vector3> Rays = new List<Vector3>() { new(-3f, 4.5f, 0f), new(-2.5f, 4.5f, 0f), new(-2f, 4.5f, 0f), new(-1.5f, 4.5f, 0f), new(-1f, 4.5f, 0f), new(-1f, 4.5f, 0f), new(-.5f, 4.5f, 0f), new(0f, 4.5f, 0f),
        new(3f, 4.5f, 0f), new(2.5f, 4.5f, 0f), new(2f, 4.5f, 0f), new(1.5f, 4.5f, 0f), new(1f, 4.5f, 0f), new(1f, 4.5f, 0f), new(.5f, 4.5f, 0f), new(0f, 4.5f, 0f)};


            TList<(Content, int)> NeedContents = new List<(Content, int)>();

            Rays.ForEach(Pos =>
            {

                Rigidbody2D FirsRB = null;
                Physics2D.RaycastAll(Pos, Vector3.down, 15).ToList().ForEach(A =>
                {
                    if (FirsRB == null)
                        if (A.rigidbody != null)
                            FirsRB = A.rigidbody;
                });
                if (FirsRB != null)
                {
                    if (FirsRB.TryGetComponent<WaterMelonContent>(out WaterMelonContent CC))
                    {
                        NeedContents.AddIfDirty((CC.Content, 1));
                    }
                }
            });

            Created.RemoveNulls();
            int MaxIndexes = 3;
            Created.ForEach(i =>
            {
                if (i != null)
                {
                    int N = Contents.IndexOf(i.GetComponent<WaterMelonContent>().Content);
                    if (N > MaxIndexes) MaxIndexes = N;
                }
            });
            //int NewIndex = Random.Range(0, (MaxIndexes > 4) ? 5 : 3);

            (Content, int)? NeedContent = null;

            if (NeedContents.Count > 0 && Random.Range(0, 100) > 70) NeedContent = NeedContents.RandomItem;





            //int NewIndex = Random.Range(0, (MaxIndexes > 4) ? 5 : 3);

            if (!NeedContent.HasValue)
                NeedContent = (Contents[Random.Range(0, MaxIndexes)], -1);
            int NewIndex = Contents.IndexOf(NeedContent.Value.Item1);


            GameObject W = Instantiate(WaterMelonContentPrefabs[NewIndex].RandomItem, transform);
            W.GetComponent<WaterMelonContent>().Sort(Contents[NewIndex]);
            NexText.text = Contents[NewIndex].RussianSource;
            OnControll = W;
            Created.Add(W.GetComponent<WaterMelonContent>());
        }





    }
}