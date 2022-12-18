using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;


//  versia 1.2
//      Touchsystem qo'shilgan
//      hozir faqat 2d ishlidi  3d ni qo'shib chiqish kere
//             OnTouchCount,OnInputCount lani ishlatish kere
//             GyroscopeEvent Qo'shilga lekin ulutshenia qilish kere
namespace SystemBox
{

    public class MobilEventBox : MonoBehaviour
    {
        [HideInInspector]
        public Mesh SphereMesh2d;


        public UesMobilEvent UsingEvent;


        public void _StartGyroscopeEvent(string ID)
        {
            GyroscopeEvents.ForEach(delegate (GyroscopeEvent d) { if (d.ID == ID) d.LetsCheakTesting = true; });
        }





        #region TouchEvent

        [ToggleGroup("UesTouchEvent"), ShowIf("TestUesTouchEvent")]
        public bool UesTouchEvent;
        [ToggleGroup("UesTouchEvent"), ShowIf("UesTouchEvent"), ShowIf("UesTouchEvent")]
        public List<TouchEvent> touchEvents;

        [System.Serializable]
        public class TouchEvent
        {

            public TuchMode tuchMode;



            public TouchCheakMods ChekMode;


            [HideIf("TestIsOnTouchUpAsButton")]
            public HowMuch How_Much_Colls = HowMuch.evry;


            [ShowIf("$TestCount")]
            public int Time_Count_LateUpdate;
            private bool ListEventsStoped;
            private int ListEventsTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;

            [HideIf("TestIsOnTouchUpAsButton")]
            public UnityEvent OnEvent;

            [ShowIf("TestIsOnTouchUpAsButton"), GUIColor(0.61f, 1, 0.1464119f, 1)]
            public UnityEvent OnDown;
            [ShowIf("TestIsOnTouchUpAsButton"), GUIColor(0.915f, 1, 0.1464119f, 1)]
            public UnityEvent OnDrag;

            [ShowIf("TestIsOnTouchUpAsButton"), HorizontalGroup("dd"), GUIColor(1f, 0.8760508f, 0.4858491f, 1)]
            public UnityEvent OnUp;
            [ShowIf("TestIsOnTouchUpAsButton"), HorizontalGroup("dd"), GUIColor(1f, 0.57734f, 0.4858491f, 1)]
            public UnityEvent OnExit;
            public List<TouchFillers> Fillers;

            [System.Serializable]
            public class TouchFillers
            {
                public bool UesGizmos;
                [EnableIf("UesGizmos")]
                public Color GizmosColor;
                public Vector3 SentorPos;
                public float Radius;
            }
            public void CollMee()
            {
                if (How_Much_Colls == HowMuch.one && !ListEventsStoped) { OnEvent?.Invoke(); ; ListEventsStoped = true; }
                if (How_Much_Colls == HowMuch.Count && ListEventsTime < Time_Count_LateUpdate) { OnEvent?.Invoke(); ListEventsTime++; }
                if (How_Much_Colls == HowMuch.evry) { OnEvent?.Invoke(); }
            }
            public void CollMeeAssButton(string ff)
            {
                if (ff == "Down") { OnDown?.Invoke(); }
                if (ff == "Drag") { OnDrag?.Invoke(); }
                if (ff == "Up") { OnUp?.Invoke(); }
                if (ff == "Exit") { OnExit?.Invoke(); }
            }
            public List<int> MyTouchIndex = new List<int>();

            private bool TestCheakModis2D() => ChekMode == TouchCheakMods._2D;

            private bool TestIsOnTouchUpAsButton() => tuchMode == TuchMode.OnTouchUpAsButton;

        }
        public void InputTester()
        {

            if (Input.touchCount > 0) 
            {
                for (int i = 0; i < Input.touchCount; i++)
                {



                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        touchEvents.ForEach(delegate (TouchEvent d)
                        {

                            if (d.tuchMode == TuchMode.OnTouchUpAsButton || (d.tuchMode == TuchMode.OnTouchDown || d.tuchMode == TuchMode.OnInputDown))
                            {

                                Vector2 touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position); 
                            bool isHitTouch = false;

                                d.Fillers.ForEach(delegate (TouchEvent.TouchFillers f)
                                {
                                    if (!isHitTouch && Vector2.Distance(touchpos, transform.position + f.SentorPos) <= f.Radius) isHitTouch = true;
                                });
                                if (isHitTouch)
                                {
                                    if (d.tuchMode == TuchMode.OnTouchUpAsButton)
                                    {
                                        d.MyTouchIndex.Add(i);
                                        d.CollMeeAssButton("Down");
                                    }
                                    else if (d.tuchMode == TuchMode.OnTouchDown)
                                    {
                                        d.CollMee();
                                    }
                                }
                                else if (!isHitTouch && d.tuchMode == TuchMode.OnInputDown)
                                {
                                    d.CollMee();
                                }
                            }
                        });
                    }
                    else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                        touchEvents.ForEach(delegate (TouchEvent d)
                        {


                            if ((d.MyTouchIndex.IndexOf(i) != -1 && d.tuchMode == TuchMode.OnTouchUpAsButton) || (d.tuchMode == TuchMode.OnTouchUp || d.tuchMode == TuchMode.OnInputUp))
                            {

                                Vector2 touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                            bool isHitTouch = false;

                                d.Fillers.ForEach(delegate (TouchEvent.TouchFillers f)
                                {
                                    if (!isHitTouch && Vector2.Distance(touchpos, transform.position + f.SentorPos) <= f.Radius) isHitTouch = true;
                                });
                                if (isHitTouch)
                                {


                                    if (d.tuchMode == TuchMode.OnTouchUpAsButton)
                                    {
                                        touchEvents.ForEach(delegate (TouchEvent g)
                                        {
                                            if (g.MyTouchIndex.IndexOf(i) != -1)
                                            {

                                                g.MyTouchIndex.RemoveAt(g.MyTouchIndex.IndexOf(i));
                                            }

                                            for (int t = 0; t < Input.touchCount; t++)
                                            {
                                                if (t >= i)
                                                {
                                                    if (g.MyTouchIndex.IndexOf(t) != -1)
                                                    {
                                                        g.MyTouchIndex[g.MyTouchIndex.IndexOf(t)]--;
                                                    }
                                                }
                                            }
                                        });
                                        d.CollMeeAssButton("Up");
                                    }
                                    else if (d.tuchMode == TuchMode.OnTouchUp)
                                    {
                                        d.CollMee();
                                    }



                                }
                                else if (!isHitTouch && d.tuchMode == TuchMode.OnInputUp)
                                {
                                    d.CollMee();
                                }
                            }
                        });
                    }
                    else
                    {
                        touchEvents.ForEach(delegate (TouchEvent d)
                        {

                            if (d.MyTouchIndex.IndexOf(i) != -1 && d.tuchMode == TuchMode.OnTouchUpAsButton)
                            {
                                Vector2 touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                                bool isHitTouch = false;

                                d.Fillers.ForEach(delegate (TouchEvent.TouchFillers f)
                                {
                                    if (!isHitTouch && Vector2.Distance(touchpos, transform.position + f.SentorPos) <= f.Radius) isHitTouch = true;
                                });
                                if (!isHitTouch)
                                {

                                    if (d.MyTouchIndex.IndexOf(i) != -1)
                                    {

                                        d.MyTouchIndex.RemoveAt(d.MyTouchIndex.IndexOf(i));
                                    }

                                    for (int t = 0; t < Input.touchCount; t++)
                                    {
                                        if (t >= i)
                                        {
                                            if (d.MyTouchIndex.IndexOf(t) != -1)
                                            {
                                                d.MyTouchIndex[d.MyTouchIndex.IndexOf(t)]--;
                                            }
                                        }
                                    }
                                    d.CollMeeAssButton("Exit");

                                }
                                else
                                {
                                    d.CollMeeAssButton("Drag");
                                }

                            }
                            else if (d.tuchMode == TuchMode.OnTouchDrag || d.tuchMode == TuchMode.OnInputDrag)
                            {
                                Vector2 touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                                bool isHitTouch = false;

                                d.Fillers.ForEach(delegate (TouchEvent.TouchFillers f)
                                {
                                    if (!isHitTouch && Vector2.Distance(touchpos, transform.position + f.SentorPos) <= f.Radius) isHitTouch = true;
                                });
                                if (isHitTouch && d.tuchMode == TuchMode.OnTouchDrag)
                                {
                                    d.CollMee();
                                }
                                else if (d.tuchMode == TuchMode.OnInputDrag)
                                {
                                    d.CollMee();
                                }
                            }
                        });


                    }



                }

            }

        }
        #endregion

        #region Gyroscope
        [ToggleGroup("UesGyroscope"), ShowIf("TestUesGyroscope")]
        public bool UesGyroscope;
        [ToggleGroup("UesGyroscope"), ShowIf("UesGyroscope"), OnValueChanged("GyroscopeEventChange")]
        public List<GyroscopeEvent> GyroscopeEvents;

        [HideInInspector]
        public Texture2D FaceDownWiwer, FaceUpWiwer, LandscapeLeftWiwer, LandscapeRightWiwer, PortraitWiwer, PortraitUpsideDownWiwer;



        [System.Serializable]
        public class GyroscopeEvent
        {
            public DeviceFaces NeedQuaterion;

            public string ID;
            public bool LetsCheakTesting;

            [HideIf("TestShekWiwer")]
            public HowMuch How_Much_Colls = HowMuch.evry;

            [ShowIf("$TestCount")]

            public int Time_Count_LateUpdate;
            private bool ListEventsStoped;
            private int ListEventsTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count && NeedQuaterion != DeviceFaces.OnShake;


            [System.Serializable]
            public class ShekerValues
            {
                public HowMuch How_Much_Colls = HowMuch.evry;
                private bool TestCount() => How_Much_Colls == HowMuch.Count;
                [ShowIf("$TestCount")]
                public int Time_Count_LateUpdate;
                [Range(1.8f, 20)]
                public float OfsetFiller;

                [MinValue(0.15f), MaxValue(10f)]
                public float Time;
                public UnityEvent OnEvent;
                [HideInInspector]
                public Coroutine ShakeCoratine;

                [HideInInspector]
                public bool ListEventsStoped;
                private int ListEventsTime;

                public void CallMee()
                {

                    if (How_Much_Colls == HowMuch.one && !ListEventsStoped) { OnEvent?.Invoke(); ; ListEventsStoped = true; }
                    if (How_Much_Colls == HowMuch.Count && ListEventsTime < Time_Count_LateUpdate) { OnEvent?.Invoke(); ListEventsTime++; }
                    if (How_Much_Colls == HowMuch.evry) { OnEvent?.Invoke(); }
                }


            }
            [ShowIf("TestShekWiwer")]
            public List<ShekerValues> shekerValues;




            private bool TestShekWiwer() => NeedQuaterion == DeviceFaces.OnShake;



            [HideInInspector]
            public Texture2D FaceDownWiwer;
            [HideInInspector]
            public Texture2D FaceUpWiwer;
            [HideInInspector]
            public Texture2D LandscapeLeftWiwer;
            [HideInInspector]
            public Texture2D LandscapeRightWiwer;
            [HideInInspector]
            public Texture2D PortraitWiwer;
            [HideInInspector]
            public Texture2D PortraitUpsideDownWiwer;


            [HideIf("TestShekWiwer")]
            public UnityEvent OnEvent;
            public void Callmee()
            {
                bool NeedCall = false;
                if (Input.deviceOrientation == DeviceOrientation.FaceDown && NeedQuaterion == DeviceFaces.FaceDown) { NeedCall = true; }
                else
                if (Input.deviceOrientation == DeviceOrientation.FaceUp && NeedQuaterion == DeviceFaces.FaceUp) { NeedCall = true; }
                else
                if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft && NeedQuaterion == DeviceFaces.LandscapeLeft) { NeedCall = true; }
                else
                if (Input.deviceOrientation == DeviceOrientation.LandscapeRight && NeedQuaterion == DeviceFaces.LandscapeRight) { NeedCall = true; }
                else
                if (Input.deviceOrientation == DeviceOrientation.Portrait && NeedQuaterion == DeviceFaces.Portrait) { NeedCall = true; }
                else
                if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown && NeedQuaterion == DeviceFaces.PortraitUpsideDown) { NeedCall = true; }
                if (NeedCall)
                {

                    if (How_Much_Colls == HowMuch.one && !ListEventsStoped) { OnEvent?.Invoke(); ; ListEventsStoped = true; }
                    if (How_Much_Colls == HowMuch.Count && ListEventsTime < Time_Count_LateUpdate) { OnEvent?.Invoke(); ListEventsTime++; }
                    if (How_Much_Colls == HowMuch.evry) { OnEvent?.Invoke(); }

                }
            }



            [OnInspectorGUI("DrawWiwer", append: true)]
            private void DrawWiwer()
            {
                Texture2D texture = null;
                if (NeedQuaterion == DeviceFaces.FaceDown) texture = FaceDownWiwer;
                if (NeedQuaterion == DeviceFaces.FaceUp) texture = FaceUpWiwer;
                if (NeedQuaterion == DeviceFaces.LandscapeLeft) texture = LandscapeLeftWiwer;
                if (NeedQuaterion == DeviceFaces.LandscapeRight) texture = LandscapeRightWiwer;
                if (NeedQuaterion == DeviceFaces.Portrait) texture = PortraitWiwer;
                if (NeedQuaterion == DeviceFaces.PortraitUpsideDown) texture = PortraitUpsideDownWiwer;
                //Input.gyro.
                if (texture == null) return;

                GUILayout.BeginVertical(GUI.skin.box);
                GUILayout.Label(texture, new GUILayoutOption[] { GUILayout.Height(100f), GUILayout.Width(100f) });
                GUILayout.EndVertical();

            }
        }
        public void TestSheking()
        {
            if (!UesGyroscope) return;
            for (int i = 0; i < GyroscopeEvents.Count; i++)
            {
                if (GyroscopeEvents[i].LetsCheakTesting)
                {
                    for (int l = 0; l < GyroscopeEvents[i].shekerValues.Count; l++)
                    {
                        if (!GyroscopeEvents[i].shekerValues[l].ListEventsStoped
                            && GyroscopeEvents[i].shekerValues[l].ShakeCoratine == null) GyroscopeEvents[i].shekerValues[l].ShakeCoratine = StartCoroutine(ShakeTester(i, l));
                    }
                }

            }
        }


        public IEnumerator ShakeTester(int gyroscopeIndex, int SheakValueIndex)
        {

            bool NeeCall = true;
            int LastNulls = 0;
            for (float i = 0; i < GyroscopeEvents[gyroscopeIndex].shekerValues[SheakValueIndex].Time; i += Time.deltaTime)
            {
                float dd = Vector3.Distance(Input.gyro.rotationRate, Vector3.zero);
                if (dd < GyroscopeEvents[gyroscopeIndex].shekerValues[SheakValueIndex].OfsetFiller)
                {
                    LastNulls++;
                    float LastNullsTester = (8 * (GyroscopeEvents[gyroscopeIndex].shekerValues[SheakValueIndex].Time / 8));
                    if (LastNullsTester < 8) LastNullsTester = 4;
                    if (LastNulls > LastNullsTester) { NeeCall = false; break; }
                }
                yield return new WaitForEndOfFrame();
            }

            if (NeeCall) GyroscopeEvents[gyroscopeIndex].shekerValues[SheakValueIndex].CallMee();

            GyroscopeEvents[gyroscopeIndex].shekerValues[SheakValueIndex].ShakeCoratine = null;
        }
        private void GyroscopeEventChange()
        {
            GyroscopeEvents.ForEach(delegate (GyroscopeEvent d)
            {
                d.FaceDownWiwer = FaceDownWiwer;
                d.FaceUpWiwer = FaceUpWiwer;
                d.LandscapeLeftWiwer = LandscapeLeftWiwer;
                d.LandscapeRightWiwer = LandscapeRightWiwer;
                d.PortraitWiwer = PortraitWiwer;
                d.PortraitUpsideDownWiwer = PortraitUpsideDownWiwer;
            });
        }

        #endregion


        void Start()
        {
            if (UesGyroscope && GyroscopeEvents.Count > 0) Input.gyro.enabled = true;
        }

        void Update()
        {
            InputTester();

            GyroscopeEvents.ForEach(delegate (GyroscopeEvent d) { d.Callmee(); });
            TestSheking();
        }







        #region Test

        private bool TestUesTouchEvent() => UsingEvent.HasFlag(UesMobilEvent.TouchInputs);
        private bool TestUesGyroscope() => UsingEvent.HasFlag(UesMobilEvent.GyroscopeInputs);

        #endregion


        private void OnDrawGizmos()
        {
            _2DtouchfillerGizmos();
        }

        private void _2DtouchfillerGizmos()
        {
            if (!UesTouchEvent) return;
            for (int i = 0; i < touchEvents.Count; i++)
            {
                if (touchEvents[i].Fillers != null)
                    touchEvents[i].Fillers.ForEach(delegate (TouchEvent.TouchFillers d)
                    {
                        if (d.UesGizmos)
                        {
                            Gizmos.color = d.GizmosColor;
                            Gizmos.DrawWireMesh(SphereMesh2d, transform.position + new Vector3(d.SentorPos.x, d.SentorPos.y, d.SentorPos.z), Quaternion.Euler(0f, 0f, 0f), new Vector3(d.Radius, d.Radius, d.Radius));
                        }
                    });
            }
        }

    }
    public enum TouchCheakMods { _2D, _3D }
    public enum TuchMode
    {
        OnTouchDown,
        OnTouchUp, 
        OnTouchUpAsButton,
        OnTouchDrag,

        OnInputDown,
        OnInputUp,
        OnInputDrag,

        OnTouchCount,
        OnInputCount
    }

    public enum DeviceFaces
    {
        OnShake,
        FaceDown,
        FaceUp,
        LandscapeLeft,
        LandscapeRight,
        Portrait,
        PortraitUpsideDown
    }




    [System.Flags]
    public enum UesMobilEvent
    {
        TouchInputs = 1 << 1,
        GyroscopeInputs = 1 << 2
    }

}