

/*

ComponentController v 0.1
    transform bor yurg'izsabo'ladi yahshilab test qilish kere
    transform joyiga borbo'ganda evit ishlidi
ComponentController v 0.2
    SystemEnum qo'shildi
    Animation controller qo'shildi
 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector; 
namespace SystemBox
{
    public enum TransformTuyp
    {
        Local,
        World
    }
    [HideMonoScript]
    public class ComponentController : MonoBehaviour
    {

        public UesSystem UsingSystem;

        #region Enums
        public enum UpdateTuyp
        {
            Update,
            FixedUpdate,
            LateUpdate,
            EndOfFrame,
            OnTime,
            ByEvent
        }

        public enum Need_Positon_Tuyp // only mowe to wards
        {
            Need_Transform,
            Vector3,
            Vector2XY,
            Vector2XZ,
            Vector2YZ,
            Me_Trnsform_And_V3,
            Need_Transform_And_V3,
            MeTransform_Start_Add_V3

        }
        #endregion


        #region MoweTowars

        [ToggleGroup("UesMoveTowards"), ShowIf("TestUesMoveTowards")]
        public bool UesMoveTowards;
        [ToggleGroup("UesMoveTowards"), ShowIf("UesMoveTowards")]

        [OnValueChanged("Test_MeTransform")]
        public List<_MoveTowards> System_MoveTowards;
        [Serializable]
        public class _MoveTowards
        {
            [EnumPaging]
            [BoxGroup("GeneralSetings")]
            public UpdateTuyp Update_using = UpdateTuyp.Update;
            [BoxGroup("GeneralSetings")]
            public TransformTuyp MoweTuyp = TransformTuyp.World;
            [BoxGroup("GeneralSetings")]
            [LabelText("ID")]
            public string MoweToWardsID = "null";
            [Min(0.02f), ShowIf("$Test_Update_using_Time"), BoxGroup("GeneralSetings")]
            public float WaitTime = 0.2f;
            [Required, BoxGroup("GeneralSetings")]
            public Transform MeTransform;
            [BoxGroup("GeneralSetings")]
            public float Speed;
            [BoxGroup("GeneralSetings")]
            public bool letsMowtowar;
            [EnumPaging]
            public Need_Positon_Tuyp Need_Mowing_Tuyp = Need_Positon_Tuyp.Need_Transform;

            [ShowIf("$NeedTransform_Test"), Required]
            public Transform NeedTransform;
            [ShowIf("$NeedVector3_Test")]
            public Vector3 NeedVector3;


            private Vector3 Vector2XY { get => new Vector3(Vector2XY_X, Vector2XY_Y, 0f); }
            [HorizontalGroup("Vector2XY"), ShowIf("$Vector2XY_Test"), LabelText("X"), LabelWidth(10), SerializeField]
            public float Vector2XY_X;
            [HorizontalGroup("Vector2XY"), ShowIf("$Vector2XY_Test"), LabelText("Y"), LabelWidth(10), SerializeField]
            public float Vector2XY_Y;

            private Vector3 Vector2XZ { get => new Vector3(Vector2XZ_X, 0f, Vector2XZ_Z); }
            [HorizontalGroup("Vector2XZ"), ShowIf("$Vector2XZ_Test"), LabelText("X"), LabelWidth(10), SerializeField]
            public float Vector2XZ_X;
            [HorizontalGroup("Vector2XZ"), ShowIf("$Vector2XZ_Test"), LabelText("Z"), LabelWidth(10), SerializeField]
            public float Vector2XZ_Z;

            [HideInInspector]
            public Coroutine MyTimeCuratin;

            private Vector3 Vector2YZ { get => new Vector3(0f, Vector2YZ_Y, Vector2YZ_Z); }
            [HorizontalGroup("Vector2YZ"), ShowIf("$Vector2YZ_Test"), LabelText("Y"), LabelWidth(10), SerializeField]
            public float Vector2YZ_Y;
            [HorizontalGroup("Vector2YZ"), ShowIf("$Vector2YZ_Test"), LabelText("Z"), LabelWidth(10), SerializeField]
            public float Vector2YZ_Z;
            [HideInInspector]
            public bool Starded;


            [ShowIf("NeedVector3_TestForEvent")]
            public HowMuch How_Much_Colls = HowMuch.evry;
            [ShowIf("NeedVector3_TestForEvent"), ShowIf("$TestCount"),]
            public int Time_Event;
            private bool TestCount() => How_Much_Colls == HowMuch.Count;

            [ShowIf("NeedVector3_TestForEvent")]
            public UnityEvent OnFinsh;



            private Vector3 StartMeTransform;
            public void OnUpdate()
            {
                if (!Starded) { StartMeTransform = (MoweTuyp == TransformTuyp.Local) ? MeTransform.localPosition : MeTransform.position; Starded = true; }
                Vector3 MeTransformPos = (MoweTuyp == TransformTuyp.Local) ? MeTransform.localPosition : MeTransform.position;
                Vector3 NeedTransformPos = (NeedTransform == null) ? Vector3.zero : (MoweTuyp == TransformTuyp.Local) ? NeedTransform.localPosition : NeedTransform.position;

                Vector3 NextPosition = Vector3.zero;

                bool needcoll = false;

                if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, NeedTransformPos, Speed * Time.deltaTime);
                    if (NextPosition == NeedTransformPos) needcoll = true;
                }


                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform_And_V3)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, NeedTransformPos + NeedVector3, Speed * Time.deltaTime);
                    if (NextPosition == NeedTransformPos + NeedVector3) needcoll = true;
                }
                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector3)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, NeedVector3, Speed * Time.deltaTime);
                    if (NextPosition == NeedVector3) needcoll = true;
                }
                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Me_Trnsform_And_V3)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, MeTransformPos + NeedVector3, Speed * Time.deltaTime);
                }

                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector2XY)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, new Vector3(Vector2XY.x, Vector2XY.y, MeTransformPos.z), Speed * Time.deltaTime);
                    if (NextPosition == new Vector3(Vector2XY.x, Vector2XY.y, MeTransformPos.z)) needcoll = true;
                }
                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector2XZ)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, new Vector3(Vector2XZ.x, MeTransformPos.y, Vector2XZ.z), Speed * Time.deltaTime);
                    if (NextPosition == new Vector3(Vector2XZ.x, MeTransformPos.y, Vector2XZ.z)) needcoll = true;
                }
                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector2YZ)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, new Vector3(MeTransformPos.x, Vector2YZ.y, Vector2YZ.z), Speed * Time.deltaTime);
                    if (NextPosition == new Vector3(MeTransformPos.x, Vector2YZ.y, Vector2YZ.z)) needcoll = true;
                }
                else if (Need_Mowing_Tuyp == Need_Positon_Tuyp.MeTransform_Start_Add_V3)
                {
                    NextPosition = Vector3.MoveTowards(MeTransformPos, StartMeTransform + NeedVector3, Speed * Time.deltaTime);
                    if (NextPosition == StartMeTransform + NeedVector3) needcoll = true;
                }
                //--------------------------------------------------------------
                if (MoweTuyp == TransformTuyp.Local)
                    MeTransform.localPosition = NextPosition;
                else
                    MeTransform.position = NextPosition;

                if (needcoll) callEvent();

            }
            private bool ListEventsStoped;
            private int ListEventsTime;
            public void callEvent()
            {
                if (How_Much_Colls == HowMuch.one && !ListEventsStoped) { OnFinsh?.Invoke(); ListEventsStoped = true; }
                if (How_Much_Colls == HowMuch.Count && ListEventsTime < Time_Event) { OnFinsh?.Invoke(); ListEventsTime++; }
                if (How_Much_Colls == HowMuch.evry) { OnFinsh?.Invoke(); }
            }

            private bool NeedTransform_Test() => Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform ||
                Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform_And_V3;

            private bool NeedVector3_TestForEvent() => (Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector3 || Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform_And_V3)
                || (Need_Mowing_Tuyp == Need_Positon_Tuyp.MeTransform_Start_Add_V3 || (Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform ||
                Vector2XY_Test() || (Vector2XZ_Test() || Vector2YZ_Test())
                ));

            private bool NeedVector3_Test() => (Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector3 || Need_Mowing_Tuyp == Need_Positon_Tuyp.Need_Transform_And_V3)
                || (Need_Mowing_Tuyp == Need_Positon_Tuyp.MeTransform_Start_Add_V3 || Need_Mowing_Tuyp == Need_Positon_Tuyp.Me_Trnsform_And_V3);
            private bool Vector2XY_Test() => Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector2XY;
            private bool Vector2XZ_Test() => Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector2XZ;
            private bool Vector2YZ_Test() => Need_Mowing_Tuyp == Need_Positon_Tuyp.Vector2YZ;
            private bool Test_Update_using_Time() => Update_using == UpdateTuyp.OnTime;
            [HideInInspector] public bool AddedMetransform = false;
        }



        public void _CallUpdate_MoveTowards(string NeedID)
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.ByEvent && NeedID == item.MoweToWardsID) item.OnUpdate();
            }
        }
        public void _Start_MoveTowards(string NeedID)
        {

            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (!item.letsMowtowar && NeedID == item.MoweToWardsID)
                {

                    item.letsMowtowar = true;
                    if (item.MeTransform != null && item.Update_using == UpdateTuyp.OnTime && item.MyTimeCuratin == null)
                        item.MyTimeCuratin = StartCoroutine(ManuralUpdate(item));
                }
            }
        }
        public void _Stop_MoveTowards(string NeedID)
        {
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.letsMowtowar && NeedID == item.MoweToWardsID)
                {
                    item.letsMowtowar = false;
                    if (item.MeTransform != null && item.Update_using == UpdateTuyp.OnTime && item.MyTimeCuratin == null)
                        StopCoroutine(item.MyTimeCuratin);
                }
            }
        }
        public void _RestartStartPosition(string NeedID)
        {
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.MeTransform != null && NeedID == item.MoweToWardsID) item.Starded = false;
            }
        }

        #region Private Functions
        public IEnumerator ManuralUpdate(_MoveTowards NL)
        {
            while (true)
            {
                yield return new WaitForSeconds(NL.WaitTime);
                NL.OnUpdate();
            }
        }
        private void Test_MeTransform()
        {
            foreach (_MoveTowards item in System_MoveTowards) if (item.MeTransform == null && !item.AddedMetransform)
                {
                    {
                        item.MeTransform = transform;
                        item.AddedMetransform = true;
                    }
                }
        }
        private void StartMoveTowards()
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.OnTime && item.MyTimeCuratin == null)
                    item.MyTimeCuratin = StartCoroutine(ManuralUpdate(item));
        }
        private void OnDisableMoveTowards()
        {
            foreach (_MoveTowards item in System_MoveTowards) item.MyTimeCuratin = null;

        }
        private void OnEnableMoveTowards()
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.OnTime && item.MyTimeCuratin == null)
                    item.MyTimeCuratin = StartCoroutine(ManuralUpdate(item));
        }

        private void UpdateMoveTowards()
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.Update) item.OnUpdate();
            }
        }
        private void FixedUpdateMoveTowards()
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.FixedUpdate) item.OnUpdate();
            }
        }
        private void LateUpdateMoveTowards()
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.LateUpdate) item.OnUpdate();
            }
        }
        private void ForEndOfFrameUpdateMoveTowards()
        {
            if (!UesMoveTowards) return;
            foreach (_MoveTowards item in System_MoveTowards)
            {
                if (item.letsMowtowar && item.MeTransform != null && item.Update_using == UpdateTuyp.EndOfFrame) item.OnUpdate();
            }
        }
        #endregion
        #endregion

        #region MouseControler


        [ToggleGroup("UesMouseControler"), ShowIf("TestMouseControler")]
        public bool UesMouseControler;

        [ToggleGroup("UesMouseControler"), ShowIf("TestMouseControler"), ShowIf("UesMouseControler")]
        public bool MC_UesEvent;
        [ToggleGroup("UesMouseControler"), ShowIf("TestCanShowMC_Elments"),]
        public UnityEvent EvntOnMouseDown;
        [ToggleGroup("UesMouseControler"), ShowIf("TestCanShowMC_Elments")]
        public UnityEvent EvntOnMouseDrag;
        [ToggleGroup("UesMouseControler"), ShowIf("TestCanShowMC_Elments")]
        public UnityEvent EvntOnMouseUp;

        private bool TestCanShowMC_Elments() => (UesMouseControler && MC_UesEvent);

        private Transform meTransform;
        private Transform myZ;
        private bool OnCantrol;
        private Camera MyCam;
        private float dd;
        bool onExit;
        [HorizontalGroup("UesMouseControler/kk"), ShowIf("UesMouseControler")]
        public bool X, Y;
        Vector3 OfsetPos;

        private void MC_Start()
        {
            if (MyCam == null) MyCam = Camera.main;
            myZ = new GameObject("kere").transform;
            myZ.hideFlags = HideFlags.HideInHierarchy;
            myZ.parent = MyCam.transform;
            meTransform = transform;
        }

        private void MC_Update()
        {

            myZ.position = meTransform.position;
            myZ.localPosition = new Vector3(myZ.localPosition.x, myZ.localPosition.y, 0f);
            myZ.LookAt(meTransform.position);
            if (Input.GetMouseButton(0) && OnCantrol)
            {
                Vector3 ff = GetWorldPositionOnPlane();
                ff = new Vector3((X) ? meTransform.position.x : ff.x, (Y) ? meTransform.position.y : ff.y, ff.z);
                meTransform.position = ff;
                onExit = true;

                if (MC_UesEvent) EvntOnMouseDrag?.Invoke();

            }
            else if (Input.GetMouseButtonUp(0) && OnCantrol)
            {
                OnCantrol = false;
                onExit = false;
                if (MC_UesEvent) EvntOnMouseUp?.Invoke();


            }
            else if (onExit)
            {
                OnCantrol = false;
                onExit = false;
                if (MC_UesEvent) EvntOnMouseUp?.Invoke();
            }
        }


        public Vector3 GetWorldPositionOnPlane()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = dd;
            var point = MyCam.ScreenToWorldPoint(mousePos);
            return point + OfsetPos;
        }
        public void _Reset()
        {
            OnCantrol = false;
            onExit = false;
        }
        private void MC_OnMouseDown()
        {
            OnCantrol = true;
            dd = Vector3.Distance(myZ.position, meTransform.transform.position);

            OfsetPos = Vector3.zero;

            OfsetPos = meTransform.transform.position - GetWorldPositionOnPlane();

            if (MC_UesEvent) EvntOnMouseDown?.Invoke();
        }



        #endregion



        #region ANimationController


        [ToggleGroup("UesAnimationController"), ShowIf("TestAnimationController")]
        public bool UesAnimationController;
        [ToggleGroup("UesAnimationController"), ShowIf("UesAnimationController")]

        [OnValueChanged("Test_animationC")]
        [InfoBox("Problem With ID", InfoMessageType.Error, "$TestIdes")]
        public List<AnimationController> animationC;
        [Serializable]
        public class AnimationController
        {
            [InfoBox("Problem With ID", InfoMessageType.Error, "$TestIdes")]
            public string ID_true;
            [InfoBox("Problem With ID", InfoMessageType.Error, "$TestIdes")]
            public string ID_false;

            private bool TestIdes() => ID_true == ID_false;

            [Required]
            [OnValueChanged("GetFirstName")]
            public Animation aim;


            public void GetFirstName()
            {
                if (AnimName == "Null" && aim != null && aim.clip != null) AnimName = aim.clip.name;
            }

            public string AnimName = "Null";

            public bool loop;
            public float ForwrdSpeed = 1;
            public float BackSpeed = -1;



            public void Start()
            {
                aim[AnimName].time = 0;
                aim.Play(AnimName);
                aim[AnimName].speed = (loop) ? 0 : BackSpeed;
            }
            public void Update()
            {
                //if (StopTime && aim[AnimName].time != NeedTime){aim[AnimName].time = Mathf.MoveTowards(aim[AnimName].time, NeedTime, NeedSpeed);}
            }
            public void SetBool(bool Nb)
            {
                if (loop)
                {

                    aim[AnimName].speed = (Nb) ? 1 : 0;
                    aim.Play(AnimName);
                }
                else
                {
                    if (Nb && aim[AnimName].speed == ForwrdSpeed && aim[AnimName].time == 0) return;
                    if (!Nb && aim[AnimName].speed == BackSpeed && aim[AnimName].time == 0) return;
                    if (!Nb && aim[AnimName].speed == ForwrdSpeed && aim[AnimName].time == 0) aim[AnimName].time = aim[AnimName].length;
                    aim[AnimName].speed = (Nb) ? ForwrdSpeed : BackSpeed;
                    aim.Play(AnimName);
                }
            }

            public bool CanUes() => aim != null && AnimName != "Null";

        }

        private bool TestIdes()
        {
            if (animationC == null) return false;

            for (int i = 0; i < animationC.Count; i++)
            {
                if (animationC[i].ID_false == animationC[i].ID_true) return true;
                for (int ii = 0; ii < animationC.Count; ii++)
                {
                    if (i != ii)
                    {

                        if (animationC[i].ID_false == animationC[ii].ID_false) return true;
                        if (animationC[i].ID_true == animationC[ii].ID_true) return true;
                    }
                }
            }
            return false;
        }

        private void Test_animationC()
        {
            for (int i = 0; i < animationC.Count; i++)
            {
                if (GetComponent<Animation>() != null && animationC[i].aim == null) animationC[i].aim = GetComponent<Animation>();
                if (animationC[i].aim != null && animationC[i].AnimName == "Null") animationC[i].AnimName = animationC[i].aim.clip.name;
            }
        }

        public void _SetAnimation(string Bool_ID)
        {
            if (UesAnimationController) animationC.ForEach(delegate (AnimationController d)
            {
                if (d.CanUes() && d.ID_false == Bool_ID) d.SetBool(false);
                if (d.CanUes() && d.ID_true == Bool_ID) d.SetBool(true);
            });
        }

        #endregion



        #region StandartFunctions
        public Coroutine EndofFrameCorati = null;
        private void Start()
        {
            if (EndofFrameCorati == null) EndofFrameCorati = StartCoroutine(EndOffFremUpdate());


            if (UesMouseControler) MC_Start();


            if (UesAnimationController) animationC.ForEach(delegate (AnimationController d) { if (d.CanUes()) d.Start(); });

            StartMoveTowards();
        }
        private void Update()
        {
            UpdateMoveTowards();
            if (UesMouseControler) MC_Update();
            if (UesAnimationController) animationC.ForEach(delegate (AnimationController d) { if (d.CanUes()) d.Update(); });
        }
        private void FixedUpdate()
        {
            FixedUpdateMoveTowards();
        }
        private void LateUpdate()
        {
            LateUpdateMoveTowards();
        }

        public IEnumerator EndOffFremUpdate()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                ForEndOfFrameUpdateMoveTowards();
            }
        }




        private void OnMouseDown()
        {
            if (UesMouseControler) MC_OnMouseDown();


        }



        private void OnDisable()
        {
            EndofFrameCorati = null;

            OnDisableMoveTowards();

        }
        private void OnEnable()
        {
            if (EndofFrameCorati == null) EndofFrameCorati = StartCoroutine(EndOffFremUpdate());
            OnEnableMoveTowards();
        }



        #endregion


        #region Test

        private bool TestUesMoveTowards() => UsingSystem.HasFlag(UesSystem.MoveTowards);
        private bool TestMouseControler() => UsingSystem.HasFlag(UesSystem.MouseControler);
        private bool TestAnimationController() => UsingSystem.HasFlag(UesSystem.AnimationController);

        #endregion




    }

    [System.Flags]
    public enum UesSystem
    {
        MoveTowards = 1 << 1,
        MouseControler = 1 << 2,
        AnimationController = 1 << 3
    }


}
