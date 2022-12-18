using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;
/// <summary>
///  versia 1
///  versia 1.1
///     Added SystemFlag
///     addedSendManeger
///     Cunt bug tuzatilgan
///     Randomystem Qo'shilgan
///     InputClas Qo'shilgan
///     Curatine Random Time qo'shilgan
///     Call your self qo'shilgan
///     Send masage Grope qo'shilgan
///     Input Duble clic qo'shilgan
///     Grope System qo'shilgan
///     hamma monobehavr eventla bitta class qilindi
///     audio system qo'shildi
///     eventla ulitshenia bo'ldi event with value
///     HotKeyQo'shildi Lekin unda faqat selekt turibti ulutshenia qilish kere
/// </summary>
namespace SystemBox
{
    [HideMonoScript]
    public class EventBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region TestSelectionBase
        [OnValueChanged("TestSelectionBase")] public bool uesSelectionBase = false;
        private void TestSelectionBase()
        {
            if (uesSelectionBase && GetComponent<SelectionParent>() == null)
                gameObject.AddComponent<SelectionParent>().HideFLAG();
            else if (!uesSelectionBase && GetComponent<SelectionParent>() != null)
                DestroyImmediate(gameObject.GetComponent<SelectionParent>());
        }
        #endregion
        #region TestDestroyer
        [OnValueChanged("ChekeDestroyerForLoders")] public bool DontDestroyer = false;
        #endregion
        public UesEvent _UesEvent;
        #region Call_UnitiyEvent
        public void _JustCallListFunction(string ID)
        {
            if(JustCallList)
            foreach (ListEvents item in SystemCall) if (item.EventID == ID) item.Callme();
        }
        public void _PlayAnimation(string AnimId)
        {
            if(UesAnimation)
            foreach (PlayAnim item in SystemAnimation) if (item.AnimationID == AnimId && item.Callme())
                {
                    if (item.StartTtime > 0 && gameObject.activeInHierarchy) StartCoroutine(StarAnimationTime(item));
                    else if (item.StartTtime <= 0) { StartAnimation(item); }
                }

        }
        public void _PlayCoroutineEvent(string CoroutineId)
        {
            if(UesCoroutine) foreach (PlayCoratin item in SystemCoroutine) if (item.CoroutineID == CoroutineId && item.Callme()) StartCoroutine(StarUesCoroutineTime(item));
        }
        public void _StartDestroyEvent(string DestroyId)
        {
            if(UesDestroy) foreach (DestroyBlock item in SystemDestroy) if (item.DestroyId == DestroyId) StartCoroutine(StarDestroyTime(item));
        }
        public void _StartInstantiate(string NeedID)
        {
            if (!UesInstantiate) return;
            foreach (InstantiateBlock item in SystemInstantiate)
            {
                if (item.InstalId == NeedID && !item.stoped)
                {
                    if (item.InstlTime > 0) StartCoroutine(WaitTime(item));
                    else item.CanCall();
                }
            }
        }
        public void _LoadScaneFunction(string ID)
        {
            if(_LoadScane) SystemScanes.ForEach(delegate (ListScanes d) { if (d.EventID == ID) d.Callme(); }); 
        }
        public void _CollSendMessage(string NeedId)
        {
           if(Message) systemSends.ForEach(delegate (SystemSendMessage d) { if (d.ID == NeedId) d.CoolMe(); });
        }
        public void _CollRandomSystem(string NeedId)
        {
           if(_RandomSysem) systemRandom.ForEach(delegate (SystemRandom d) { if (d.ID == NeedId) d.CallMee(); });
        }
        public void _CollGroupSystem(string NeedId)
        {
            if(_GropeSystem) groupSystem.ForEach(delegate (GroupSystem d) { if (d.ID == NeedId) d.Callme(); });
        }
        public void _CollPlayerPrefsUtility(string NeedId) 
        {
            if(UesPlayerPrefsUtility) PlayerPrefsUtility_Call.ForEach(delegate (PlayerPrefsUtility d) { if (d.ID == NeedId) d.Callme(); });
        }

        #endregion
        #region JustCallList
        [ToggleGroup("JustCallList"), ShowIf("TestJustCallList")]
        public bool JustCallList;
        [ToggleGroup("JustCallList"), ShowIf("JustCallList"), ShowIf("TestJustCallList")]
        public List<ListEvents> SystemCall;
        [System.Serializable]
        public class ListEvents
        {
            [Header("____________________________________________________")]
            public string EventID = "null";
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Time_Count_LateUpdate;
            public UnityEvent ListEvent;
            private bool ListEventsStoped;
            private int ListEventsTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public void Callme()
            {
                if (How_Much_Colls == HowMuch.one && !ListEventsStoped) { ListEvent?.Invoke(); ListEventsStoped = true; }
                if (How_Much_Colls == HowMuch.Count && ListEventsTime < Time_Count_LateUpdate) { ListEvent?.Invoke(); ListEventsTime++; }
                if (How_Much_Colls == HowMuch.evry) { ListEvent?.Invoke(); }
            }
        }
        #endregion
        #region Update_Time
        public enum UpdateFirswaiterTuyp
        {
            ResetOnDisabset,
            WaitOnlyOnStart
        }
        [ToggleGroup("_UpdateTime"), ShowIf("TestUpdateTime")]
        public bool _UpdateTime;
        [System.Serializable]
        public class PlayUpdateTime
        {
            [Header("____________________________________________________")]
            public string UpdateID;
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;

            [HorizontalGroup("T"), MinValue(0f)]
            public float StartTime;



            [HorizontalGroup("T"), HideLabel]
            public UpdateFirswaiterTuyp FirstWaiter;

            [MinValue(0.04f)] // plis Dont Touch
            public float OffsideTime = 0.1f;

            public UnityEvent OnUpdate;
            [HideInInspector]
            public bool Stoped;
            [HideInInspector]
            public bool FistWaited;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            [HideInInspector]
            public Coroutine MyWaiter = null;
            private int LeftTime;
            public void Callme()
            {
                if (How_Much_Colls == HowMuch.one && !Stoped)
                {
                    Stoped = true; OnUpdate?.Invoke();
                }
                if (How_Much_Colls == HowMuch.Count && LeftTime < Coll_Cunt) { Stoped = true; LeftTime++; OnUpdate?.Invoke(); }
                if (How_Much_Colls == HowMuch.evry) { OnUpdate?.Invoke(); }
            }
        }
        [ToggleGroup("_UpdateTime"), ShowIf("_UpdateTime"), ShowIf("TestUpdateTime")]
        public List<PlayUpdateTime> SystemUpdate;
        IEnumerator CoratinTimeUpdate(PlayUpdateTime OnUpdateSystem)
        {
            bool First = false;
            while (!OnUpdateSystem.Stoped)
            {
                if (!OnUpdateSystem.FistWaited || (OnUpdateSystem.FirstWaiter == UpdateFirswaiterTuyp.ResetOnDisabset && !First))
                {
                    if (OnUpdateSystem.StartTime > 0) yield return new WaitForSeconds(OnUpdateSystem.StartTime);
                    OnUpdateSystem.FistWaited = true;
                    First = true;

                }
                yield return new WaitForSeconds(OnUpdateSystem.OffsideTime);
                OnUpdateSystem.Callme();
            }
        }
        private void TimeUpdateOnStart(bool ison)
        {
            if (!Application.isPlaying) return;
            if (_UpdateTime)
            {
                foreach (PlayUpdateTime item in SystemUpdate)
                {
                    if (ison)
                    {
                        if (item.MyWaiter == null && !item.Stoped)
                        {
                            item.MyWaiter = StartCoroutine(CoratinTimeUpdate(item));
                        }
                    }
                    else
                    {
                        StopCoroutine(item.MyWaiter);
                        item.MyWaiter = null;
                    }
                }
            }
        }
        #endregion
        #region PlayAnimation



        private List<string> ddds() => new List<string>() { " dsa", "dsad" };

        [System.Serializable]
        public class PlayAnim
        {
            [Header("____________________________________________________")]
            public string AnimationID = "null";

            public CollYourSelf CollWhen;

            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;

            [ValidateInput("isThereAnimation",
                "if you don’t give a value, then object's the animation will be play", InfoMessageType.Warning)]
            [OnValueChanged("GetFirstName")]
            public Animation _animation;
            public void GetFirstName()
            {
                if (AnimtionClipName == "Null" && _animation != null && _animation.clip != null) AnimtionClipName = _animation.clip.name;
            }

            public bool isThereAnimation(Animation Value) => Value != null;



            public string AnimtionClipName = "Null";



            public float StartTtime;

            private bool PlayAnimStoped;
            private int PlayAnimTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;

            public bool Callme()
            {
                if (How_Much_Colls == HowMuch.one && !PlayAnimStoped) { PlayAnimStoped = true; return true; }
                if (How_Much_Colls == HowMuch.Count && PlayAnimTime < Coll_Cunt) { PlayAnimTime++; return true; }
                if (How_Much_Colls == HowMuch.evry) { return true; }
                return false;
            }
        }
        [ToggleGroup("UesAnimation"), ShowIf("TestAnimation")]
        public bool UesAnimation;
        [ToggleGroup("UesAnimation"), ShowIf("UesAnimation"), ShowIf("TestAnimation")]
        public List<PlayAnim> SystemAnimation;


        private IEnumerator StarAnimationTime(PlayAnim NAnim)
        {
            Animation Anim = NAnim._animation;
            float needTime = NAnim.StartTtime;
            string ClipName = NAnim.AnimtionClipName;
            string ClassId = NAnim.AnimationID;
            yield return new WaitForSeconds(needTime);
            if (Anim == null && gameObject.GetComponent<Animation>() != null) Anim = gameObject.GetComponent<Animation>();
            if (Anim != null)
            {
                if (Anim.clip == null && ClipName == "Null")
                    Debug.LogError(gameObject.name + "'s EventBox : you trayed to start animation but animation with ID(" + ClassId + ")dosnt have any animationClip!");
                else if (ClipName == "Null" && Anim.clip != null)
                {
                    Anim.Play();
                }
                else if (ClipName != "Null" && Anim[ClipName] == null)
                    Debug.LogError(gameObject.name + "'s EventBox : you trayed to start animationClip(" + ClipName + ")but animation with ID(" + ClassId + ")dosnt have!");
                else if (ClipName != "Null" && Anim[ClipName] != null)
                {
                    Anim.Play(ClipName);
                }
            }
            else
            {
                Debug.LogError(gameObject.name + "'s EventBox : you tryed to start animation but animation with ID(" + ClassId + ") == null");
            }
        }
        private void StartAnimation(PlayAnim NAnim)
        {
            Animation Anim = NAnim._animation;
            string ClipName = NAnim.AnimtionClipName;
            string ClassId = NAnim.AnimationID;
            if (Anim == null && gameObject.GetComponent<Animation>() != null) Anim = gameObject.GetComponent<Animation>();
            if (Anim != null)
            {
                if (Anim.clip == null && ClipName == "Null")
                    Debug.LogError(gameObject.name + "'s EventBox : you trayed to start animation but animation with ID(" + ClassId + ")dosnt have any animationClip!");
                else if (ClipName == "Null" && Anim.clip != null)
                {
                    Anim.Play();
                }
                else if (ClipName != "Null" && Anim[ClipName] == null)
                    Debug.LogError(gameObject.name + "'s EventBox : you trayed to start animationClip(" + ClipName + ")but animation with ID(" + ClassId + ")dosnt have!");
                else if (ClipName != "Null" && Anim[ClipName] != null)
                {
                    Anim.Play(ClipName);
                }
            }
            else
            {
                Debug.LogError(gameObject.name + "'s EventBox : you tryed to start animation but animation with ID(" + ClassId + ") == null");
            }
        }

        #endregion
        #region PlayCuratine


        [System.Serializable]
        public class PlayCoratin
        {
            [Header("____________________________________________________")]
            public string CoroutineID = "null";

            public CollYourSelf CollWhen;



            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            [EnumPaging]
            public RandomFloat randomTime;

            [HideIf("testRandomFloa")]
            public float WAitTime;

            [ShowIf("testRandomFloa"), HorizontalGroup("dd"), HideLabel, Indent(8)]
            public float MinTime;
            [ShowIf("testRandomFloa"), HorizontalGroup("dd"), HideLabel,]
            public float MaxTime;

            private bool testRandomFloa() => randomTime == RandomFloat.Random;


            public UnityEvent OnTimeEnt;
            private bool PlayCoratinStoped;
            private int PlayCoratinTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;

            public float GiveTime()
            {
                if (randomTime == RandomFloat.Random) return Random.Range(MinTime, MaxTime);
                return WAitTime;
            }

            public bool Callme()
            {
                if (How_Much_Colls == HowMuch.one && !PlayCoratinStoped) { PlayCoratinStoped = true; return true; }
                if (How_Much_Colls == HowMuch.Count && PlayCoratinTime < Coll_Cunt) { PlayCoratinTime++; return true; }
                if (How_Much_Colls == HowMuch.evry) { return true; }
                return false;
            }
        }
        [ToggleGroup("UesCoroutine"), ShowIf("TestUesCoroutine")]
        public bool UesCoroutine;
        [ToggleGroup("UesCoroutine"), ShowIf("UesCoroutine"), ShowIf("TestUesCoroutine")]
        public List<PlayCoratin> SystemCoroutine;


        private IEnumerator StarUesCoroutineTime(PlayCoratin NCoroutine)
        {
            float needTime = NCoroutine.GiveTime();
            string ClassId = NCoroutine.CoroutineID;
            yield return new WaitForSeconds(needTime);
            if (NCoroutine.OnTimeEnt != null) NCoroutine.OnTimeEnt.Invoke();
        }
        #endregion
        #region OnKeyKode
        public enum UesKeyTuyp
        {
            OnDown, OnDrag, OnUp
        }
        [System.Serializable]
        public class KeysBlock
        {
            [Header("____________________________________________________")]
            public string KeyID;

            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            public UesKeyTuyp UesTuyp;

            public KeyCode NeedKay;
            public UnityEvent OnKeyEvent;

            private bool KeysBlockStoped;
            private int KeysBlockTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public void Callme()
            {
                if (How_Much_Colls == HowMuch.one && !KeysBlockStoped) { KeysBlockStoped = true; OnKeyEvent?.Invoke(); }
                if (How_Much_Colls == HowMuch.Count && KeysBlockTime < Coll_Cunt) { KeysBlockTime++; OnKeyEvent?.Invoke(); }
                if (How_Much_Colls == HowMuch.evry) { OnKeyEvent?.Invoke(); }
            }
        }
        [ToggleGroup("UesKay"), ShowIf("TestUesKay")]
        public bool UesKay;
        [ToggleGroup("UesKay"), ShowIf("UesKay"), ShowIf("$CheakEnabled"), ShowIf("TestUesKay")]
        public List<KeysBlock> SystemKays;
        private void TestKayEvent()
        {
            foreach (KeysBlock item in SystemKays)
            {
                if ((item.UesTuyp == UesKeyTuyp.OnDown && Input.GetKeyDown(item.NeedKay)) ||
                    ((item.UesTuyp == UesKeyTuyp.OnDrag && Input.GetKey(item.NeedKay)) ||
                    (item.UesTuyp == UesKeyTuyp.OnUp && Input.GetKeyUp(item.NeedKay)))) item.Callme();
            }
        }
        #endregion
        #region Destrot
        [System.Serializable]
        public class DestroyBlock
        {
            [Header("____________________________________________________")]

            public string DestroyId;

            public CollYourSelf CollWhen;

            public float DetryoTime;
            [Required]
            public UnityEngine.Object DestroyObject;
            public bool isthereGameobjekt(UnityEngine.Object onga) => onga != null;
            public UnityEvent OnDestroyed;
        }

        [ToggleGroup("UesDestroy"), ShowIf("TestUesDestroy")]
        public bool UesDestroy;
        [ToggleGroup("UesDestroy"), ShowIf("UesDestroy"), ShowIf("$CheakEnabled"), ShowIf("TestUesDestroy")]
        public List<DestroyBlock> SystemDestroy;

        private IEnumerator StarDestroyTime(DestroyBlock NDestroy)
        {
            yield return new WaitForSeconds(NDestroy.DetryoTime);
            if (NDestroy.DestroyObject != null)
            {
                Destroy(NDestroy.DestroyObject);
                NDestroy?.OnDestroyed.Invoke();
            }
            else Debug.LogError(gameObject.name + "'s EventBox : you trayed to start destroy but Object with ID(" + NDestroy.DestroyId + ")dosnt have!");
        }
        #endregion
        #region Instantiate
        public enum Need_Transform_Position
        {
            Parent,

            Parent_Random,

            Need_Transform,

            Need_Transform_Random,

            Vector3,

            Vector3_Random,

            Me_Trnsform_And_V3,

            Me_Trnsform_And_V3_Random,

            Need_Transform_And_V3,

            Need_Transform_And_V3_Random
        }
        public enum TransformTuyp
        {
            Local,
            World
        }
        [System.Serializable]
        public class InstantiateBlock
        {


            [Header("____________________________________________________")]
            public string InstalId;
            [EnumPaging]
            public HowMuch How_Much_Colls = HowMuch.evry;

            [MinValue(0)]
            public float InstlTime;
            [Required]
            public GameObject InstantiateObject;

            [EnumPaging, ShowIf("UesPosition"), ShowIf("$TestTransformTuypNeed_Transform"), ShowIf("$TestTransformTuypParent")]
            public TransformTuyp TransformTuyp;
            [EnumPaging]
            public Need_Transform_Position PositionTuyp;

            [ShowIf("$TestTransformTuypNeed_TransformIss")]
            public Transform NeeObject;
            //-------------------------------------------------------------------------------------------------------
            [HorizontalGroup("Pos"), ShowIf("$TestTransformTuypNeed_Transform"), LabelText("UesPos"), LabelWidth(60)]
            public bool UesPosition;
            [ShowIf("UesPosition"), HideLabel, HorizontalGroup("Pos"), ShowIf("$TestTransformTuypNeed_Transform"), ShowIf("$LessTestRandom")]
            public Vector3 NeedPosition;

            [HorizontalGroup("Rot"), ShowIf("$TestTransformTuypNeed_Transform_Rot"), LabelText("UesRot"), LabelWidth(60)]
            public bool UesRotetion;
            [ShowIf("UesRotetion"), HideLabel, HorizontalGroup("Rot"), ShowIf("$TestTransformTuypNeed_Transform"), ShowIf("$LessTestRandom")]
            public Quaternion NeedRotetion;

            [HorizontalGroup("Size"), ShowIf("$TestTransformTuypNeed_Transform"), LabelText("UesScale"), LabelWidth(60)]
            public bool UesLocalScale;
            [ShowIf("UesLocalScale"), HideLabel, HorizontalGroup("Size"), ShowIf("$TestTransformTuypNeed_Transform"), ShowIf("$LessTestRandom")]
            public Vector3 NeedLocalScale;
            //----------------------------------------------------------------------------------------------------------


            [ShowIf("UesPosition"), ShowIf("$TestRandom"), VerticalGroup("Pos/P"), LabelWidth(50)]
            public Vector3 MinPos;
            [ShowIf("UesPosition"), ShowIf("$TestRandom"), VerticalGroup("Pos/P"), LabelWidth(50)]
            public Vector3 MaxPos;


            [ShowIf("UesRotetion"), ShowIf("$TestRandom_Rot"), VerticalGroup("Rot/R"), LabelWidth(50)]
            public Vector3 MinRot;
            [ShowIf("UesRotetion"), ShowIf("$TestRandom_Rot"), VerticalGroup("Rot/R"), LabelWidth(50)]
            public Vector3 MaxRot;


            [ShowIf("UesLocalScale"), ShowIf("$TestRandom"), VerticalGroup("Size/S"), LabelWidth(50)]
            public Vector3 MinScale;
            [ShowIf("UesLocalScale"), ShowIf("$TestRandom"), VerticalGroup("Size/S"), LabelWidth(50)]
            public Vector3 MaxScale;
            //-----------------------------------------------------------------------------------------------------



            [ShowIf("$TestNeed_TransformIss"), LabelWidth(180)]
            public bool Ues_Transform_position;
            [ShowIf("$TestNeed_TransformIss"), LabelWidth(180)]
            public bool Ues_Transform_rotetion;
            [ShowIf("$TestNeed_TransformIss"), LabelWidth(180)]
            public bool Ues_Transform_localScale;

            public UnityEvent OnInstantiate;

            [ReadOnly]
            public Transform ThisTransform;

            [HideInInspector]
            public bool stoped;
            [HideInInspector]
            public int NeedCount, leftCount;
            public void CanCall()
            {
                if (How_Much_Colls == HowMuch.one && !stoped) { stoped = true; InstalMee(); }
                if (How_Much_Colls == HowMuch.Count && leftCount < NeedCount)
                {
                    leftCount++;
                    InstalMee();
                    if (leftCount == NeedCount) stoped = true;
                }
                if (How_Much_Colls == HowMuch.evry) InstalMee();
            }

            private void InstalMee()
            {
                GameObject OnObject = Instantiate(InstantiateObject);

                if (PositionTuyp == Need_Transform_Position.Parent)
                {
                    OnObject.transform.parent = NeeObject;
                    if (UesPosition) OnObject.transform.localPosition = NeedPosition;
                    if (UesRotetion) OnObject.transform.localRotation = NeedRotetion;
                    if (UesLocalScale) OnObject.transform.localScale = NeedLocalScale;
                }
                if (PositionTuyp == Need_Transform_Position.Parent_Random)
                {
                    OnObject.transform.parent = NeeObject;
                    if (TransformTuyp == TransformTuyp.Local)
                    {
                        if (UesPosition) OnObject.transform.localPosition = getRandomVector3(MinPos, MaxPos);
                        if (UesRotetion) OnObject.transform.localRotation = getRandomQuaterion(MinRot, MaxRot);
                    }
                    else
                    {
                        if (UesPosition) OnObject.transform.position = getRandomVector3(MinPos, MaxPos);
                        if (UesRotetion) OnObject.transform.rotation = getRandomQuaterion(MinRot, MaxRot);
                    }
                    if (UesLocalScale) OnObject.transform.localScale = getRandomVector3(MinScale, MaxScale);
                }
                if (PositionTuyp == Need_Transform_Position.Need_Transform)
                {
                    if (Ues_Transform_position) OnObject.transform.position = NeeObject.position;
                    if (Ues_Transform_rotetion) OnObject.transform.rotation = NeeObject.rotation;
                    if (Ues_Transform_localScale) OnObject.transform.localScale = NeeObject.localScale;
                }
                if (PositionTuyp == Need_Transform_Position.Need_Transform_Random)
                {
                    if (TransformTuyp == TransformTuyp.Local)
                    {
                        if (Ues_Transform_position) OnObject.transform.localPosition = NeeObject.position + getRandomVector3(MinPos, MaxPos);
                    }
                    else
                    {
                        if (Ues_Transform_position) OnObject.transform.position = NeeObject.position + getRandomVector3(MinPos, MaxPos);
                    }
                    if (Ues_Transform_localScale) OnObject.transform.localScale = NeeObject.localScale + getRandomVector3(MinScale, MaxScale);
                }
                if (PositionTuyp == Need_Transform_Position.Vector3)
                {
                    if (UesPosition)
                    {
                        if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localPosition = NeedPosition;
                        else OnObject.transform.position = NeedPosition;
                    }

                    if (UesRotetion)
                    {
                        if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localRotation = NeedRotetion;
                        else OnObject.transform.rotation = NeedRotetion;
                    }
                    if (UesLocalScale) OnObject.transform.localScale = NeedLocalScale;
                }


                if (PositionTuyp == Need_Transform_Position.Need_Transform_And_V3)
                {
                    if (UesPosition)
                    {
                        if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localPosition = getRandomVector3(MinPos, MaxPos);
                        else OnObject.transform.position = getRandomVector3(MinPos, MaxPos);
                    }

                    if (UesRotetion)
                    {
                        if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localRotation = getRandomQuaterion(MinRot, MaxRot);
                        else OnObject.transform.rotation = getRandomQuaterion(MinRot, MaxRot);
                    }
                    if (UesLocalScale) OnObject.transform.localScale = getRandomVector3(MinScale, MaxScale);
                }



                if (PositionTuyp == Need_Transform_Position.Me_Trnsform_And_V3)
                {
                    if (UesPosition)
                    {
                        if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localPosition += NeedPosition;
                        else OnObject.transform.position += NeedPosition;
                    }


                    if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localRotation = NeedRotetion;
                    else OnObject.transform.rotation = NeedRotetion;

                    OnObject.transform.localScale += NeedLocalScale;
                }


                if (PositionTuyp == Need_Transform_Position.Me_Trnsform_And_V3_Random)
                {
                    if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localPosition = EventBox.GetRandom(MinPos, MaxPos);
                    else OnObject.transform.position += NeedPosition;

                    if (TransformTuyp == TransformTuyp.Local) OnObject.transform.localRotation = NeedRotetion;
                    else OnObject.transform.rotation = NeedRotetion;
                }
                OnInstantiate?.Invoke();
            }

            private bool TestTransformTuypNeed_Transform() => PositionTuyp != Need_Transform_Position.Need_Transform;
            private bool TestTransformTuypNeed_Transform_Rot() =>
                PositionTuyp != Need_Transform_Position.Need_Transform &&
                PositionTuyp != Need_Transform_Position.Need_Transform_Random &&
                PositionTuyp != Need_Transform_Position.Me_Trnsform_And_V3 &&
                PositionTuyp != Need_Transform_Position.Me_Trnsform_And_V3_Random &&
                PositionTuyp != Need_Transform_Position.Need_Transform_And_V3_Random;
            private bool TestTransformTuypNeed_TransformIss() => PositionTuyp == Need_Transform_Position.Need_Transform || PositionTuyp == Need_Transform_Position.Parent;
            private bool TestNeed_TransformIss() => PositionTuyp == Need_Transform_Position.Need_Transform;
            private bool TestTransformTuypParent() => PositionTuyp != Need_Transform_Position.Parent;
            private bool TestRandom() => (PositionTuyp == Need_Transform_Position.Need_Transform_Random ||
                PositionTuyp == Need_Transform_Position.Parent_Random) ||
                ((PositionTuyp == Need_Transform_Position.Vector3_Random ||
                PositionTuyp == Need_Transform_Position.Me_Trnsform_And_V3) ||
                PositionTuyp == Need_Transform_Position.Me_Trnsform_And_V3_Random);
            private bool TestRandom_Rot() =>
                (PositionTuyp == Need_Transform_Position.Parent_Random ||
                PositionTuyp == Need_Transform_Position.Parent_Random) ||
                PositionTuyp == Need_Transform_Position.Vector3_Random;
            private bool LessTestRandom() =>
                PositionTuyp != Need_Transform_Position.Need_Transform_Random &&
                PositionTuyp != Need_Transform_Position.Parent_Random &&
                PositionTuyp != Need_Transform_Position.Vector3_Random &&
                PositionTuyp != Need_Transform_Position.Me_Trnsform_And_V3 &&
                PositionTuyp != Need_Transform_Position.Me_Trnsform_And_V3_Random;

            public Vector3 getRandomVector3(Vector3 V1, Vector3 V2) =>
                new Vector3(UnityEngine.Random.Range(V1.x, V2.x), UnityEngine.Random.Range(V1.y, V2.y), UnityEngine.Random.Range(V1.z, V2.z));
            public Quaternion getRandomQuaterion(Vector3 V1, Vector3 V2) =>
               Quaternion.Euler(UnityEngine.Random.Range(V1.x, V2.x), UnityEngine.Random.Range(V1.y, V2.y), UnityEngine.Random.Range(V1.z, V2.z));

        }


        [ToggleGroup("UesInstantiate"), ShowIf("TestInstantiate")]
        public bool UesInstantiate;
        [ToggleGroup("UesInstantiate"), ShowIf("UesInstantiate"), OnValueChanged("GetThisTransform"), ShowIf("TestInstantiate")]
        public List<InstantiateBlock> SystemInstantiate;

        private void GetThisTransform()
        {
            foreach (InstantiateBlock item in SystemInstantiate)
            {
                item.ThisTransform = transform;
            }
        }


        public IEnumerator WaitTime(InstantiateBlock obox)
        {
            yield return new WaitForSeconds(obox.InstlTime);
            obox.CanCall();
        }

        #endregion
        #region LodeScane
        [ToggleGroup("_LoadScane"), ShowIf("TestLoadScane")]
        public bool _LoadScane;
        [ToggleGroup("_LoadScane"), ShowIf("_LoadScane"), OnValueChanged("ChekeDestroyerForLoders"), ShowIf("TestLoadScane")]
        public List<ListScanes> SystemScanes;
        [System.Serializable]
        public class ListScanes
        {
            [Header("____________________________________________________")]
            public string EventID = "null";
            public bool UesActivScene = true;
            [HideIf("UesActivScene")]
            public string Scane_Name = "Scene_Name";

            public LoadSceneMode LoadMode;
            [ShowIf("DontDestroy")]
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount"), ShowIf("DontDestroy")]
            public int Time_Count;

            [ReadOnly]
            public bool DontDestroy;

            private bool ListEventsStoped;
            private int ListEventsTime;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public void Callme()
            {

                if (How_Much_Colls == HowMuch.one && !ListEventsStoped) { Loder(); ListEventsStoped = true; }
                if (How_Much_Colls == HowMuch.Count && ListEventsTime < Time_Count) { Loder(); ListEventsTime++; }
                if (How_Much_Colls == HowMuch.evry) { Loder(); }
            }
            private void Loder()
            {
                if (UesActivScene)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadMode);
                }
                else
                {
                    SceneManager.LoadScene(Scane_Name, LoadMode);
                }
            }
        }
        private void ChekeDestroyerForLoders()
        {
            if (SystemScanes != null) foreach (ListScanes item in SystemScanes) item.DontDestroy = DontDestroyer;
        }
        #endregion
        #region SendMessage
        [ToggleGroup("Message"), ShowIf("TestSendMessage")]
        public bool Message;
        [ToggleGroup("Message"), ShowIf("Message"), ShowIf("TestSendMessage")]
        public List<SystemSendMessage> systemSends;
        public enum MessageOptions
        {
            Object_Local, EventBox
        }


        [System.Serializable]
        public class SystemSendMessage
        {

            [Header("____________________________________________________")]
            public string ID = "null";

            public CollYourSelf CollWhen;

            public MessageOptions CollTuyp;

            public bool UesGrops = false;
            //----------------------------
            [ShowIf("testCollTuypLocal")]
            [Required]
            public GameObject Object_Local;
            [ShowIf("testCollTuypEventBox")]
            [Required]
            public EventBox box;
            //------------------------------
            [ShowIf("testCollTuypLocal_G")]
            [Required]
            public List<GameObject> Object_LocalGrops;
            [ShowIf("testCollTuypEventBox_G")]
            [Required]
            public List<EventBox> boxGrops;




            public List<string> Message;

            public void CoolMe()
            {

                if (CollTuyp == MessageOptions.Object_Local)
                {
                    if (!UesGrops) Message.ForEach(delegate (string g) { Object_Local.SendMessage(g); });
                    if (UesGrops) { Message.ForEach(delegate (string g) { Object_LocalGrops.ForEach(delegate (GameObject dd) { dd.SendMessage(g); }); }); }

                }
                if (CollTuyp == MessageOptions.EventBox)
                {
                    if (UesGrops) Message.ForEach(delegate (string g) { boxGrops.ForEach(delegate (EventBox dd) { dd.CollAllIdFunction(g); }); });
                    if (!UesGrops) Message.ForEach(delegate (string g) { box.CollAllIdFunction(g); });
                }

            }

            public bool testCollTuypLocal() => CollTuyp == MessageOptions.Object_Local && !UesGrops;
            public bool testCollTuypEventBox() => CollTuyp == MessageOptions.EventBox && !UesGrops;
            public bool testCollTuypLocal_G() => CollTuyp == MessageOptions.Object_Local && UesGrops;
            public bool testCollTuypEventBox_G() => CollTuyp == MessageOptions.EventBox && UesGrops;
        }

        #endregion
        #region RandomSystem
        [ToggleGroup("_RandomSysem"), ShowIf("TestRandomMeneger")]
        public bool _RandomSysem;
        [ToggleGroup("_RandomSysem"), ShowIf("_RandomSysem"), ShowIf("TestRandomMeneger")]
        public List<SystemRandom> systemRandom;
        [System.Serializable]
        public class SystemRandom
        {
            [Header("____________________________________________________")]
            public string ID;
            public CollYourSelf CollWhen;
            public RandomManeger RandomMode = RandomManeger.chosenOneOfList;
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public void CallMee()
            {
                if (How_Much_Colls == HowMuch.one && !stoped)
                {
                    if (RandomMode == RandomManeger.chosenOneOfList) CallListOfOne();
                    stoped = true;
                }
                if (How_Much_Colls == HowMuch.Count && leftCount < Coll_Cunt)
                {
                    leftCount++;
                    if (RandomMode == RandomManeger.chosenOneOfList) CallListOfOne();
                }
                if (How_Much_Colls == HowMuch.evry)
                {
                    if (RandomMode == RandomManeger.chosenOneOfList) CallListOfOne();
                }
            }
            #region ListOfOne
            [ShowIf("$TestListOfOne")]
            public List<EventBox> eventBoxes;
            [ShowIf("$TestListOfOne"), LabelText("Masage")]

            public string ListOfOneMasage;
            private void CallListOfOne()
            {
                if (eventBoxes.Count == 0) { Debug.LogError("Error Random WithId <" + ID + "> List.Count == 0"); return; }
                bool isntNullall = false;
                for (int i = 0; i < eventBoxes.Count; i++) if (eventBoxes[i] != null) { isntNullall = true; break; }
                if (!isntNullall) { Debug.LogError("Error Random WithId <" + ID + "> All List is null"); return; }
                EventBox NE = null;
                while (true)
                {
                    NE = eventBoxes[Random.Range(0, eventBoxes.Count)];
                    if (NE != null) break;
                }
                NE.CollAllIdFunction(ListOfOneMasage);
            }
            private bool TestListOfOne() => RandomMode == RandomManeger.chosenOneOfList;
            #endregion
            private bool stoped;
            private int leftCount;
        }
        #endregion
        #region PhisikInputs
        [ToggleGroup("_physicFiller"), ShowIf("TestphysicFiller")]
        public bool _physicFiller;
        [ToggleGroup("_physicFiller"), ShowIf("_physicFiller"), ShowIf("$CheakEnabled"), ShowIf("TestphysicFiller"), OnValueChanged("AddEventBox")]
        public List<SystemOnPhiscs> _physicBox;
        [System.Serializable]
        public class SystemOnPhiscs
        {
            [Header("____________________________________________________")]
            public string OnPhiscID = "null";

            public physicFiller physicFiller;

            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            [TagSelector]
            public List<string> _Tags;
            public UnityEvent _Event;

            public bool UesToolEvent;
            [ShowIf("testshowTrigerEvents")]
            public UnityEventWithCollider EventColider;
            [ShowIf("testshowTrigerEvents2D")]
            public UnityEventWithCollider2D EventColider2D;
            [ShowIf("testshowCollisionEvents2D")]
            public UnityEventWithCollision2D EventCollision2D;
            [ShowIf("testshowCollisionEvents")]
            public UnityEventWithCollision EventCollision;

            private bool stoped;
            private int leftCount;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public void Callme(GameObject NTag, bool UesTag = true)
            {
                if (CanCollMe(NTag,UesTag))
                {
                    _Event?.Invoke();
                    if (UesMassege) MyEventBox.CollAllIdFunction(Massage);
                }

            }
            public void Callme(GameObject NTag, Collider collider) 
            {
                if (CanCollMe(NTag))
                {
                    _Event?.Invoke();
                    if (UesMassege) MyEventBox.CollAllIdFunction(Massage);
                    if (UesToolEvent) EventColider.Invoke(collider);
                }
            }
            public void Callme(GameObject NTag, Collider2D collider)
            {
                if (CanCollMe(NTag))
                {
                    _Event?.Invoke();
                    if (UesMassege) MyEventBox.CollAllIdFunction(Massage);
                    if (UesToolEvent) EventColider2D.Invoke(collider);
                }
            }
            public void Callme(GameObject NTag, Collision collision)
            {
                if (CanCollMe(NTag))
                {
                    _Event?.Invoke();
                    if (UesMassege) MyEventBox.CollAllIdFunction(Massage);
                    if (UesToolEvent) EventCollision.Invoke(collision);
                }
            }
            public void Callme(GameObject NTag, Collision2D collision)
            {
                if (CanCollMe(NTag))
                {
                    _Event?.Invoke();
                    if (UesMassege) MyEventBox.CollAllIdFunction(Massage);
                    if (UesToolEvent) EventCollision2D.Invoke(collision);
                }
            }
            

            private bool CanCollMe(GameObject NTag, bool UesTag = true) 
            {
                string dd = NTag.transform.tag;
                
                if (!UesTag) dd = _Tags[0];

                if (How_Much_Colls == HowMuch.one && !stoped && _Tags.IndexOf(dd) != -1)
                {
                    stoped = true;
                    return true;
                }
                if (How_Much_Colls == HowMuch.Count && leftCount < Coll_Cunt && _Tags.IndexOf(dd) != -1)
                {
                    leftCount++;
                    return true;
                }
                if (How_Much_Colls == HowMuch.evry && _Tags.IndexOf(dd) != -1)
                {
                    return true;
                }
                return false;
            }

            public bool testshowCollisionEvents() => (physicFiller.HasFlag(physicFiller.OnCollisionEnter)   || 
                (physicFiller.HasFlag(physicFiller.OnCollisionExit) || physicFiller.HasFlag(physicFiller.OnCollisionStay))) && UesToolEvent;
            public bool testshowCollisionEvents2D() => (physicFiller.HasFlag( physicFiller.OnCollisionEnter2D) || 
                (physicFiller.HasFlag( physicFiller.OnCollisionExit2D )|| physicFiller.HasFlag(physicFiller.OnCollisionStay2D))) && UesToolEvent;
            public bool testshowTrigerEvents() => (physicFiller.HasFlag(physicFiller.OnTrigerEnter) || 
                (physicFiller.HasFlag(physicFiller.OnTrigerExit) || physicFiller.HasFlag( physicFiller.OnTrigerStay))) && UesToolEvent;
            public bool testshowTrigerEvents2D() => (physicFiller.HasFlag(physicFiller.OnTrigerEnter2D) ||
                (physicFiller.HasFlag(physicFiller.OnTrigerExit2D) || physicFiller.HasFlag(physicFiller.OnTrigerStay2D))) && UesToolEvent;




            [HorizontalGroup("Mass")]
            public bool UesMassege;
            [HorizontalGroup("Mass")]
            [ShowIf("UesMassege"), HideLabel]
            public string Massage;
            [HideInInspector]
            public EventBox MyEventBox;
        }
        private void AddEventBox()
        {
            _physicBox.ForEach(delegate (SystemOnPhiscs d) { d.MyEventBox = this; });
        }

        #endregion
        #region AudioPlayer


        [ToggleGroup("_AudioPlayer"), ShowIf("TestAudioPlayer")]
        public bool _AudioPlayer;
        [ToggleGroup("_AudioPlayer"), ShowIf("TestAudioPlayer")]
        public List<AudioEvent> audioPlayers;


        public void _PlayAudio(string ID)
        {
            for (int i = 0; i < audioPlayers.Count; i++)
                if (audioPlayers[i].ID == ID) audioPlayers[i].CallMee();
        }
        [System.Serializable]
        public class AudioEvent
        {
            [Header("_______________________________________________")]
            public string ID;
            [ValueDropdown("GveAllAudioID")]
            public string NeedIDAudio = "N_ID";
            public List<string> GveAllAudioID()
            {
                List<string> vs = new List<string>();
                for (int i = 0; i < SoundResurses.Item.Count; i++)
                    vs.Add(SoundResurses.Item[i].Name);
                return vs;
            }
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public CollYourSelf CollWhen = CollYourSelf.None;
            private bool stoped;
            private int leftCount;
            public void CallMee()
            {
                if (How_Much_Colls == HowMuch.one && !stoped)
                {
                    AudioPlayer.PlayAudio(NeedIDAudio);
                    stoped = true;
                }
                if (How_Much_Colls == HowMuch.Count && leftCount < Coll_Cunt)
                {
                    leftCount++;
                    AudioPlayer.PlayAudio(NeedIDAudio);
                }
                if (How_Much_Colls == HowMuch.evry)
                {
                    AudioPlayer.PlayAudio(NeedIDAudio);
                }
                
            }
        }

        #endregion
        #region GropeSystem
        [ToggleGroup("_GropeSystem"), ShowIf("TestGropeSystem")]
        public bool _GropeSystem;
        [ToggleGroup("_GropeSystem"), ShowIf("_GropeSystem"), ShowIf("TestGropeSystem")]
        public List<GroupSystem> groupSystem;
        [System.Serializable]
        public class GroupSystem
        {
            [Header("____________________________________________________")]
            public string ID = "null";

            public CollYourSelf CollWhen;
            [OnValueChanged("CheakChangin")]
            public GroupWorkerMode Mode;

            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;


            [HorizontalGroup("o", 0), OnValueChanged("CheakChangin"), HideLabel, ShowIf("TestObjectosShown")]
            public List<GameObject> Objectos;

            [HorizontalGroup("o", 0), OnValueChanged("CheakChangin"), HideLabel, HideIf("TestObjectosShown")]
            public List<Component> ComponentObjectos;

            private bool TestObjectosShown() => Mode == GroupWorkerMode.ActiverObject;


            [HorizontalGroup("o", 2), OnValueChanged("CheakChangin"), HideLabel]
            public List<bool> ObjectosActiver;




            private void CheakChangin()
            {
                if (ComponentObjectos == null) ComponentObjectos = new List<Component>();
                if (Objectos == null) Objectos = new List<GameObject>();
                while (true)
                {
                    if (Mode == GroupWorkerMode.ActiverObject)
                    {
                        if (Objectos.Count != ObjectosActiver.Count)
                        {
                            if (Objectos.Count > ObjectosActiver.Count) ObjectosActiver.Add(false);
                            else ObjectosActiver.RemoveAt(ObjectosActiver.Count - 1);
                        }
                        if (Objectos.Count == ObjectosActiver.Count) break;
                    }
                    else if (Mode == GroupWorkerMode.ActiverComponent)
                    {
                        if (ComponentObjectos.Count != ObjectosActiver.Count)
                        {
                            if (ComponentObjectos.Count > ObjectosActiver.Count) ObjectosActiver.Add(false);
                            else ObjectosActiver.RemoveAt(ObjectosActiver.Count - 1);
                        }
                        if (ComponentObjectos.Count == ObjectosActiver.Count) break;
                    }
                    else break;
                }
            }
            public void Callme()
            {
                if (How_Much_Colls == HowMuch.one && !Stoped) { Stoped = true; worker(); }
                if (How_Much_Colls == HowMuch.Count && BlockTime < Coll_Cunt) { BlockTime++; worker(); }
                if (How_Much_Colls == HowMuch.evry) { worker(); }
            }
            public void worker()
            {
                if (Mode == GroupWorkerMode.ActiverObject)
                {
                    for (int i = 0; i < Objectos.Count; i++)
                    {
                        if (Objectos[i] != null) Objectos[i].gameObject.SetActive(ObjectosActiver[i]);
                    }
                }
                if (Mode == GroupWorkerMode.ActiverComponent)
                {
                    for (int i = 0; i < ComponentObjectos.Count; i++)
                    {
                        if (ComponentObjectos[i] != null)
                        {
                            if (ComponentObjectos[i] is MonoBehaviour)
                            {
                                MonoBehaviour ll = ComponentObjectos[i] as MonoBehaviour;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is MeshRenderer)
                            {
                                MeshRenderer ll = ComponentObjectos[i] as MeshRenderer;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is BoxCollider)
                            {
                                BoxCollider ll = ComponentObjectos[i] as BoxCollider;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Camera)
                            {
                                Camera ll = ComponentObjectos[i] as Camera;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Collider)
                            {
                                Collider ll = ComponentObjectos[i] as Collider;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Collider2D)
                            {
                                Collider2D ll = ComponentObjectos[i] as Collider2D;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Animator)
                            {
                                Animator ll = ComponentObjectos[i] as Animator;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Animation)
                            {
                                Animation ll = ComponentObjectos[i] as Animation;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Renderer)
                            {
                                Renderer ll = ComponentObjectos[i] as Renderer;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Button)
                            {
                                Button ll = ComponentObjectos[i] as Button;
                                if (ll != null) ll.enabled = ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Rigidbody2D)
                            {
                                Rigidbody2D ll = ComponentObjectos[i] as Rigidbody2D;
                                if (ll != null) ll.isKinematic = !ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Rigidbody)
                            {
                                Rigidbody ll = ComponentObjectos[i] as Rigidbody;
                                if (ll != null) ll.isKinematic = !ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is SphereCollider)
                            {
                                SphereCollider ll = ComponentObjectos[i] as SphereCollider;
                                if (ll != null) ll.enabled = !ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is MeshCollider)
                            {
                                MeshCollider ll = ComponentObjectos[i] as MeshCollider;
                                if (ll != null) ll.enabled = !ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is Image)
                            {
                                Image ll = ComponentObjectos[i] as Image;
                                if (ll != null) ll.enabled = !ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is SpriteRenderer)
                            {
                                SpriteRenderer ll = ComponentObjectos[i] as SpriteRenderer;
                                if (ll != null) ll.enabled = !ObjectosActiver[i];
                            }
                            else if (ComponentObjectos[i] is BoxCollider2D)
                            {
                                BoxCollider2D ll = ComponentObjectos[i] as BoxCollider2D;
                                if (ll != null) ll.enabled = !ObjectosActiver[i];
                            }
                        }

                    }
                }
            }

            private bool Stoped;

            private int BlockTime;
        }
        #endregion
        #region MonoMehaverEvents
        [ToggleGroup("_MonoMehaverEvents"), ShowIf("TestMonoMehaverEvents")]
        public bool _MonoMehaverEvents;
        [ToggleGroup("_MonoMehaverEvents"), ShowIf("_MonoMehaverEvents"), ShowIf("TestMonoMehaverEvents")]
        public List<MonoMehaverEvents> MonoBehaverEvents;
        [System.Serializable]
        public class MonoMehaverEvents
        {
            public StandartEvents EventTuyp = StandartEvents.None;
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("$TestCount")]
            public int Coll_Cunt;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;
            public UnityEvent OnEvent;
            public void Callme()
            {
                if (How_Much_Colls == HowMuch.one && !Stoped) { Stoped = true; OnEvent.Invoke(); }
                if (How_Much_Colls == HowMuch.Count && BlockTime < Coll_Cunt) { BlockTime++; OnEvent.Invoke(); }
                if (How_Much_Colls == HowMuch.evry) { OnEvent.Invoke(); }
            }
            private bool Stoped;
            private int BlockTime;
        }
        public void ChekStandarEvents(StandartEvents standart)
        {
            if (!_MonoMehaverEvents) return;
            MonoBehaverEvents.ForEach(delegate (MonoMehaverEvents d) { if (d.EventTuyp == standart) d.Callme(); });
        }
        #endregion
        #region PlayerPrefsUtility


        [ToggleGroup("UesPlayerPrefsUtility"), ShowIf("TestUesPlayerPrefsUtility")]
        public bool UesPlayerPrefsUtility;
        [ToggleGroup("UesPlayerPrefsUtility"), ShowIf("UesPlayerPrefsUtility"), ShowIf("TestUesPlayerPrefsUtility")]
        public List<PlayerPrefsUtility> PlayerPrefsUtility_Call;

        [System.Serializable]
        public class PlayerPrefsUtility
        {
            [Header("____________________________________________________")]
            public string ID;
            public CollYourSelf CollWhen;
            [MinValue(0)]
            public int HowMuch_Colls = 1;
            public UnityEvent PlayerEvent;
            public void Callme()
            {
                if (PlayerPrefs.GetInt(ID) < HowMuch_Colls) 
                {
                    PlayerPrefs.SetInt(ID, PlayerPrefs.GetInt(ID)+1);
                    PlayerEvent?.Invoke();
                }
            }

            public string Getname() => "Reset_" + ID;
            [Button("$Getname")]
            public void Reset() => PlayerPrefs.SetInt(ID, 0);
        }

        #endregion
        #region InputMouse

        [ToggleGroup("_OnInputMouse"), ShowIf("TestOnMouseInput")]
        public bool _OnInputMouse;


        [ToggleGroup("_OnInputMouse"), ShowIf("_OnInputMouse"), ShowIf("TestOnMouseInput")]
        public List<SystemInput> systemInputs;




        [System.Serializable]
        public class SystemInput
        {

            public string ID;

            public InputMode inputMode = InputMode.OnMouseUpAsButton;
            public HowMuch How_Much = HowMuch.evry;
            [ShowIf("$TestCountMouseUpAsButton")]
            public int NeedCount;
            public bool IgnorUI = true;

            public UnityEvent NeedEvent;

            public void CallMe()
            {
                OnClick = 0;
                if (!IgnorUI && EventSystem.current.IsPointerOverGameObject()) return;
                if (How_Much == HowMuch.one && !Stoped) { NeedEvent?.Invoke(); Stoped = true; }
                if (How_Much == HowMuch.Count && MouseTime < NeedCount) { NeedEvent?.Invoke(); MouseTime++; }
                if (How_Much == HowMuch.evry) NeedEvent?.Invoke();
            }


            private bool TestCountMouseUpAsButton() => How_Much == HowMuch.Count;
            private bool Stoped;
            private int MouseTime;
            [HideInInspector]
            public int OnClick;
            
            [HideInInspector]
            public bool Downded = false;

        }

        public void TestInputDubleTab(int OnTestIndex)
        {
            if (systemInputs[OnTestIndex].OnClick == 0) StartCoroutine(WaitNextTab(OnTestIndex));
            else if (systemInputs[OnTestIndex].OnClick == 1)
            {
                systemInputs[OnTestIndex].CallMe();
            }
        }
        IEnumerator WaitNextTab(int OnClasIndex)
        {
            systemInputs[OnClasIndex].OnClick = 1;
            for (int i = 0; i < 10; i++) yield return new WaitForEndOfFrame();
            systemInputs[OnClasIndex].OnClick = 0;
        }
        #endregion
        #region Functions
        [HideInInspector]
        public void CollAllIdFunction(string NeedId)
        {
            if(JustCallList) SystemCall.ForEach(delegate (ListEvents d) { if (d.EventID == NeedId) d.Callme(); });
            if (_UpdateTime) SystemUpdate.ForEach(delegate (PlayUpdateTime d) { if (d.UpdateID == NeedId) d.Callme(); });
            if (UesAnimation) SystemAnimation.ForEach(delegate (PlayAnim d) { if (d.AnimationID == NeedId) d.Callme(); });
            if(UesCoroutine) SystemCoroutine.ForEach(delegate (PlayCoratin d) { if (d.CoroutineID == NeedId) d.Callme(); });
            if (UesKay) SystemKays.ForEach(delegate (KeysBlock d) { if (d.KeyID == NeedId) d.Callme(); });
            if (UesDestroy) SystemDestroy.ForEach(delegate (DestroyBlock d) { if (d.DestroyId == NeedId) _StartDestroyEvent(d.DestroyId); });
            if (UesInstantiate) SystemInstantiate.ForEach(delegate (InstantiateBlock d) { if (d.InstalId == NeedId) d.CanCall(); });
            if (_LoadScane) SystemScanes.ForEach(delegate (ListScanes d) { if (d.EventID == NeedId) d.Callme(); });
            if (_physicFiller) _physicBox.ForEach(delegate (SystemOnPhiscs d) { if (d.OnPhiscID == NeedId) d.Callme(null, false); });
            if (_RandomSysem) systemRandom.ForEach(delegate (SystemRandom d) { if (d.ID == NeedId) d.CallMee(); });
            if (Message) systemSends.ForEach(delegate (SystemSendMessage d) { if (d.ID == NeedId) d.CoolMe(); });
            if (_GropeSystem) groupSystem.ForEach(delegate (GroupSystem d) { if (d.ID == NeedId) d.Callme(); });
            if (_AudioPlayer) for (int i = 0; i < audioPlayers.Count; i++) if (audioPlayers[i].ID == NeedId) audioPlayers[i].CallMee();
            if(UesPlayerPrefsUtility) PlayerPrefsUtility_Call.ForEach(delegate (PlayerPrefsUtility d) { if (d.ID == NeedId) d.Callme(); });
        }
        public static Vector3 GetRandom(Vector3 min, Vector3 max) =>
            new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
        private bool CheakEnabled() { return this.isActiveAndEnabled; }
        private void cheakInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (_OnInputMouse)
                {
                    systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnInputMouseDown) d.CallMe(); });
                    for (int i = 0; i < systemInputs.Count; i++) if (systemInputs[i].inputMode == InputMode.OnInput_Double) TestInputDubleTab(i);
                }
                TestCollSelf(CollYourSelf.OnInputMouseDown);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnInputMouseUp) d.CallMe(); });
                TestCollSelf(CollYourSelf.OnInputMouseUp);
            }

            if (Input.GetMouseButton(0))
            {
                if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnInputMouseDrag) d.CallMe(); });
            }

            if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseDrag_UI && UI_pressing) d.CallMe(); });
        }
        public void TestCollSelf(CollYourSelf needenum)
        {
            if (_RandomSysem) systemRandom.ForEach(delegate (SystemRandom d) { if (d.CollWhen.HasFlag(needenum)) d.CallMee(); });
            if (Message) systemSends.ForEach(delegate (SystemSendMessage d) { if (d.CollWhen.HasFlag(needenum)) d.CoolMe(); });
            if (UesDestroy) SystemDestroy.ForEach(delegate (DestroyBlock d) { if (d.CollWhen.HasFlag(needenum)) StartCoroutine(StarDestroyTime(d)); });
            if (UesCoroutine) SystemCoroutine.ForEach(delegate (PlayCoratin d) { if (d.CollWhen.HasFlag(needenum) && d.Callme()) StartCoroutine(StarUesCoroutineTime(d)); });
            if (UesAnimation) SystemAnimation.ForEach(delegate (PlayAnim d) { if (d.CollWhen.HasFlag(needenum)) d.Callme(); });
            if (_GropeSystem) groupSystem.ForEach(delegate (GroupSystem d) { if (d.CollWhen.HasFlag(needenum)) d.Callme(); });
            if (_AudioPlayer) for (int i = 0; i < audioPlayers.Count; i++) if (audioPlayers[i].CollWhen.HasFlag(needenum)) audioPlayers[i].CallMee(); 
            if(UesPlayerPrefsUtility) PlayerPrefsUtility_Call.ForEach(delegate (PlayerPrefsUtility d) { if (d.CollWhen.HasFlag(needenum)) d.Callme(); });
        }
        #endregion
        #region UI_IPointer
        private bool UI_pressing = false;
        private bool UI_pressing_Started = false;
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (_OnInputMouse)
            {
                systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseDown_UI) d.CallMe(); });
                for (int i = 0; i < systemInputs.Count; i++) if (systemInputs[i].inputMode == InputMode.On_UI_Double) TestInputDubleTab(i);
                systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseUpAsButton_UI) { d.Downded = true; } });
            }
            TestCollSelf(CollYourSelf.OnMouseDown_UI);
            UI_pressing = true;
            UI_pressing_Started = true;
        }
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (UI_pressing)
            {
                if (_OnInputMouse)
                {
                    systemInputs.ForEach(delegate (SystemInput d) {
                        if (d.inputMode == InputMode.OnMouseUpAsButton_UI && d.Downded)
                        {
                            d.Downded = false;
                            d.CallMe();
                        }
                    });
                }
                TestCollSelf(CollYourSelf.OnMouseUp_UI);
            }

            if (_OnInputMouse)
            {
                systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseUp_UI) d.CallMe(); });
            }
            UI_pressing_Started = false;
            UI_pressing = false;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (UI_pressing_Started) UI_pressing = true;
            if (_OnInputMouse)
            {
                systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseEntor_UI) d.CallMe(); });
            }
            TestCollSelf(CollYourSelf.OnMouseEntor_UI);
        }
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            UI_pressing = false;

            if (_OnInputMouse)
            {
                systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseExit_UI) d.CallMe(); });
            }
            TestCollSelf(CollYourSelf.OnMouseExit_UI);
        }

        #endregion
        #region Standart Unity Events ______
        private void Awake()
        {

            TestCollSelf(CollYourSelf.OnAvake);

            _physicBox.ForEach(delegate (SystemOnPhiscs d) { d.MyEventBox = this; });
            if (DontDestroyer) DontDestroyOnLoad(gameObject);

            ChekStandarEvents(StandartEvents.OnAvake);

           
        }
        private void Start()
        {
            ChekStandarEvents(StandartEvents.OnStart);


            TimeUpdateOnStart(true);

            TestCollSelf(CollYourSelf.OnStart);
        }
        private void Update()
        {
            ChekStandarEvents(StandartEvents.OnUpdate);
            cheakInputs();
            if (UesKay) TestKayEvent();
        }
        private void LateUpdate() => ChekStandarEvents(StandartEvents.OnLateUpdate);
        private void OnTriggerEnter(Collider other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnTrigerEnter)) item.Callme(other.gameObject, other);
        }
        private void OnTriggerExit(Collider other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnTrigerExit)) item.Callme(other.gameObject, other);
        }
        private void OnTriggerStay(Collider other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnTrigerStay)) item.Callme(other.gameObject, other);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnTrigerEnter2D)) item.Callme(other.gameObject, other);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnTrigerExit2D)) item.Callme(other.gameObject, other);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnTrigerStay2D)) item.Callme(other.gameObject, other);
        }
        private void OnCollisionEnter(Collision other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnCollisionEnter)) item.Callme(other.gameObject, other);
        }
        private void OnCollisionExit(Collision other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnCollisionEnter)) item.Callme(other.gameObject, other);
        }
        private void OnCollisionStay(Collision other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnCollisionStay)) item.Callme(other.gameObject, other);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnCollisionEnter2D)) item.Callme(other.gameObject, other);
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnCollisionExit2D)) item.Callme(other.gameObject, other);
        }
        private void OnCollisionStay2D(Collision2D other)
        {
            if (_physicFiller) foreach (SystemOnPhiscs item in _physicBox) if (item.physicFiller.HasFlag(physicFiller.OnCollisionStay2D)) item.Callme(other.gameObject, other);
        }
        private void OnEnable()
        {
            ChekStandarEvents(StandartEvents.OnEnable);

            TestCollSelf(CollYourSelf.OnEnabled);

            TimeUpdateOnStart(true);
        }
        private void OnDisable()
        {
            ChekStandarEvents(StandartEvents.OnDisable);
            TimeUpdateOnStart(false);
        }
        private void OnDestroy() { ChekStandarEvents(StandartEvents.OnDestroy); }


        private void OnMouseUpAsButton()
        {
            if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseUpAsButton) d.CallMe(); });
            TestCollSelf(CollYourSelf.OnMouseUpAsButton);
        }
        private void OnMouseDown()
        {
            if (_OnInputMouse)
            {
                systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseDown) d.CallMe(); });
                for (int i = 0; i < systemInputs.Count; i++) if (systemInputs[i].inputMode == InputMode.On_Double) TestInputDubleTab(i);
            }
            TestCollSelf(CollYourSelf.OnMouseDown);
        }
        private void OnMouseUp()
        {
            if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseUp) d.CallMe(); });
            TestCollSelf(CollYourSelf.OnMouseUp);
        }
        private void OnMouseDrag()
        {
            if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseDrag) d.CallMe(); });
        }

        private void OnMouseEnter()
        {
            TestCollSelf(CollYourSelf.OnMouseEntor);
            if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseEntor) d.CallMe(); });
        }
        private void OnMouseExit()
        {
            if (_OnInputMouse) systemInputs.ForEach(delegate (SystemInput d) { if (d.inputMode == InputMode.OnMouseExit) d.CallMe(); });
            TestCollSelf(CollYourSelf.OnMouseExit);
        }

        #endregion
        #region Test
        public bool TestAudioPlayer() => _UesEvent.HasFlag(UesEvent.AudioPlayer);
        public bool TestJustCallList() => _UesEvent.HasFlag(UesEvent.JustCallList);
        public bool TestUpdateTime() => _UesEvent.HasFlag(UesEvent.UpdateTime);
        public bool TestAnimation() => _UesEvent.HasFlag(UesEvent.Animation);
        public bool TestUesCoroutine() => _UesEvent.HasFlag(UesEvent.UesCoroutine);
        public bool TestUesKay() => _UesEvent.HasFlag(UesEvent.UesKay);
        public bool TestUesDestroy() => _UesEvent.HasFlag(UesEvent.UesDestroy);
        public bool TestInstantiate() => _UesEvent.HasFlag(UesEvent.Instantiate);
        public bool TestLoadScane() => _UesEvent.HasFlag(UesEvent.LoadScane);
        public bool TestMonoMehaverEvents() => _UesEvent.HasFlag(UesEvent.MonoMehaverEvents);
        public bool TestphysicFiller() => _UesEvent.HasFlag(UesEvent.physicFiller);
        public bool TestGropeSystem() => _UesEvent.HasFlag(UesEvent.GropeSystem);
        public bool TestRandomMeneger() => _UesEvent.HasFlag(UesEvent.RandomMeneger);

        public bool TestOnMouseInput() => _UesEvent.HasFlag(UesEvent.OnMouseInput);

        public bool TestSendMessage() => _UesEvent.HasFlag(UesEvent.SendMessage);


        public bool TestUesPlayerPrefsUtility() => _UesEvent.HasFlag(UesEvent.PlayerPrefsUtility);

        #endregion

    }
    #region Enums
    public enum HowMuch { one, evry, Count }
    [System.Flags]
    public enum physicFiller
    {
        OnTrigerEnter = 1 << 0,
        OnTrigerExit = 1 << 2,
        OnTrigerStay = 1 << 3,
        OnTrigerEnter2D = 1 << 4,
        OnTrigerExit2D = 1 << 5,
        OnTrigerStay2D = 1 << 6,
        OnCollisionEnter = 1 << 7,
        OnCollisionExit = 1 << 8,
        OnCollisionStay = 1 << 9,
        OnCollisionEnter2D = 1 << 10,
        OnCollisionExit2D = 1 << 11,
        OnCollisionStay2D = 1 << 12
    }
    public enum RandomManeger
    {
        chosenOneOfList
    }
    public enum InputMode
    {
        OnMouseUpAsButton,
        OnMouseDown,
        OnMouseUp,
        OnMouseDrag,
        OnMouseEntor,
        OnMouseExit,
        On_Double,
        OnInputMouseUpAsButton,
        OnInputMouseDown,
        OnInputMouseUp,
        OnInputMouseDrag,
        OnInput_Double,
        OnMouseUpAsButton_UI,
        OnMouseDown_UI,
        OnMouseUp_UI,
        OnMouseDrag_UI,
        OnMouseEntor_UI,
        OnMouseExit_UI,
        On_UI_Double,
    }
    public enum GroupWorkerMode
    {
        ActiverObject,
        ActiverComponent
    }
    public enum RandomFloat
    {
        none,
        Random
    }
    [System.Flags]
    public enum CollYourSelf
    {
        None = 1 << 0,//---------------
        OnAvake = 1 << 1,//----------------
        OnStart = 1 << 2,//------------------
        OnEnabled = 1 << 3,//-------------------
        OnMouseUpAsButton = 1 << 4,//-------------------
        OnMouseDown = 1 << 5,//--------------------
        OnMouseUp = 1 << 6,//---------------------
        OnMouseEntor = 1 << 7,//-------------------
        OnMouseExit = 1 << 8,//-----------------------
        OnInputMouseDown = 1 << 9,//---------------------
        OnInputMouseUp = 1 << 10,//--------------------
        OnMouseDown_UI = 1 << 11,//----------------
        OnMouseUp_UI = 1 << 12,//---------------
        OnMouseEntor_UI = 1 << 13,
        OnMouseExit_UI = 1 << 14
    }
    public enum StandartEvents
    {
        None = 1 << 0,
        OnStart = 1 << 1,
        OnAvake = 1 << 2,
        OnUpdate = 1 << 3,
        OnLateUpdate = 1 << 4,
        //OnFixedUpdate = 1 << 5,
        OnEnable = 1 << 6,
        OnDisable = 1 << 7,
        OnDestroy = 1 << 8,
    }
    [System.Flags]
    public enum UesEvent
    {
        MonoMehaverEvents = 1 << 1,
        AudioPlayer = 1 << 3,
        JustCallList = 1 << 4,
        UpdateTime = 1 << 5,
        Animation = 1 << 6,
        UesCoroutine = 1 << 7,
        UesKay = 1 << 8,
        UesDestroy = 1 << 9,
        Instantiate = 1 << 0,
        LoadScane = 1 << 11,
        physicFiller = 1 << 12,
        OnMouseInput = 1 << 13,
        SendMessage = 1 << 14,
        GropeSystem = 1 << 15,
        RandomMeneger = 2 << 16,
        PlayerPrefsUtility = 2 << 17
    }
    #endregion

}