
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Sirenix.OdinInspector;
using SystemBox.Simpls;
namespace SystemBox
{
    public struct HierarchyItemsValues 
    {
        public Color Backgraund;
        public string HotkeyText;
    }
    [HideMonoScript]
    public class SystemTools : MonoBehaviour
    {
#if UNITY_EDITOR
        [TitleGroup("pres CTRL + Alt + H  to selecl this object"), HorizontalGroup("pres CTRL + Alt + H  to selecl this object/KK")]
        public bool UesIcons = true;
        [TitleGroup("pres CTRL + Alt + H  to selecl this object"), HideLabel ,HorizontalGroup("pres CTRL + Alt + H  to selecl this object/KK")]
        public MySystemTools system;
        

        #region Hotkey
        public bool TestSystemHotkey() => system.HasFlag(MySystemTools.HotKey);
        
        [ToggleGroup("_Hotkey")]
        [ShowIf("TestSystemHotkey")]
        public bool _Hotkey;

        [ShowIf("TestSystemHotkey")]
        [ToggleGroup("_Hotkey")]
        public List<ItemHotkey> itemHotkeys;
        public void OnKey(ComandsHotKey ll)
        {

            itemHotkeys.ForEach(delegate (ItemHotkey d) { if (d.Tuyp == ll) { d.callme(); } });
        }
        [OnInspectorGUI]
        public void TestAllKays()
        {
            if (SystemBox_Editor.MySystemTool == null) SystemBox_Editor.MySystemTool = this;
            if (SystemBox_Editor.MySystemTool != this)
            {
                DestroyImmediate(this);
                Selection.activeGameObject = SystemBox_Editor.MySystemTool.gameObject;
                return;
            }

            if (itemHotkeys == null) itemHotkeys = new List<ItemHotkey>();
            for (int i = 0; i < itemHotkeys.Count; i++)
            {
                if (itemHotkeys[i].ID == "") itemHotkeys[i].ID = "ID_" + Random.Range(-1000, 1000);

                itemHotkeys.ForEach(delegate (ItemHotkey item)
                {
                    if (item.ID != itemHotkeys[i].ID)
                    {
                        bool hesErorror = false;
                        if (itemHotkeys[i].Tuyp == item.Tuyp)
                        {

                            hesErorror = true;

                        }
                        itemHotkeys[i].Erorr = hesErorror;
                    }
                });
            }
        }
        [System.Serializable]
        public class ItemHotkey
        {
            [DisplayAsString, HideLabel, HorizontalGroup("Info")]
            public string ID = "";
            [DisplayAsString, HorizontalGroup("Info")]
            public bool Erorr;

            public bool showTextOnHierarchy;

            [InfoBox("This HotKey alredi using", VisibleIf = "Erorr", InfoMessageType = InfoMessageType.Error)]
            [HideLabel]
            public ComandsHotKey Tuyp;

            [EnumToggleButtons, HideLabel]
            public JopTuyp jop;


            [ShowIf("CanShowSelectObject")]
            public bool UesGrope;

            [ShowIf("CanShowObjectToSellect")]
            public Transform ObjectToSellect;

            [ShowIf("CanShowObjects_To_Sellect")]
            public List<GameObject> Objects_To_Sellect;


            public void callme()
            {
                if (jop == JopTuyp.SelectObject)
                {
                    if (UesGrope)
                    {
                        Selection.objects = Objects_To_Sellect.ToArray();
                    }
                    else if (ObjectToSellect != null)
                    {
                        Selection.activeGameObject = ObjectToSellect.gameObject;
                    }
                }
            }
            public bool CanShowObjectToSellect() => jop == JopTuyp.SelectObject && !UesGrope;
            public bool CanShowObjects_To_Sellect() => jop == JopTuyp.SelectObject && UesGrope;
            public bool CanShowSelectObject() => jop == JopTuyp.SelectObject;
            public enum JopTuyp { SelectObject }
        }
        
        #endregion


        #region CustomHierarchy
        public bool TestSystemCustomHierarchy() => system.HasFlag(MySystemTools.CustomHierarchy);

        [ShowIf("TestSystemCustomHierarchy")]
        [ToggleGroup("_Hierarchy")]
        public bool _Hierarchy;
        [ShowIf("TestSystemCustomHierarchy")]
        [ToggleGroup("_Hierarchy"),]
        public List<HierarchyItems> hierarchyItems;
        
        public HierarchyItemsValues GetAllHI_Items(GameObject go) 
        {
            if (hierarchyItems == null) hierarchyItems = new List<HierarchyItems>();
            HierarchyItemsValues nv = new HierarchyItemsValues();
            if (system.HasFlag(MySystemTools.CustomHierarchy)) 
            {
                hierarchyItems.ForEach(delegate (HierarchyItems items) { if (items.Item_Object == go) { nv.Backgraund = items.BackGraundColor; } });
            }
            if (system.HasFlag(MySystemTools.HotKey)) 
            {
                itemHotkeys.ForEach(delegate (ItemHotkey d)
                {
                if (d.showTextOnHierarchy)
                {
                        if (!d.UesGrope && d.ObjectToSellect != null &&  d.ObjectToSellect.gameObject == go)
                        {
                            nv.HotkeyText = d.Tuyp.ToString();
                        }
                        else if (d.UesGrope && d.Objects_To_Sellect.IndexOf(go) != -1) 
                        {
                            nv.HotkeyText = d.Tuyp.ToString();
                        }
                    }
                });
            }




                return nv;

        }
        [System.Serializable]
        public class HierarchyItems 
        {
            
            public Color BackGraundColor { get => (EditorGUIUtility.isProSkin)? BackGraundColor_Dark : BackGraundColor_Light;  }
            [HorizontalGroup("das")]
            [HideLabel]
            [Title("Objekt")]
            public GameObject Item_Object;
            [Title("Light")]
            [HorizontalGroup("das")]
            [HideLabel]
            public Color BackGraundColor_Light = new Color(0.7843138f, 0.7843138f, 0.7843138f, .30f);
            [Title("Dark")]
            [HorizontalGroup("das")]
            [HideLabel]
            public Color BackGraundColor_Dark = new Color(0.2196079f, 0.2196079f, 0.2196079f, .30f);
        }


        #endregion



        #region Gizmos


        public bool TestSystemCustom_Gizmos() => system.HasFlag(MySystemTools.Gizmos);

        [ShowIf("TestSystemCustom_Gizmos")]
        [ToggleGroup("_Gizmos")]
        public bool _Gizmos;
        [ShowIf("TestSystemCustom_Gizmos")]
        [ToggleGroup("_Gizmos"),]
        public List<sGizmos> GizmosItems;
        [System.Serializable]
        public class sGizmos 
        {
            [Header("_____________________________________________________")]
            public sGizmosTuyp type;

            public bool IsLine() => type == sGizmosTuyp.Line;



            [HideLabel]
            [ShowIf("IsLine")]
            public List<PosBlock> blocks;

            
            [System.Serializable]
            public class PosBlock
            {


                [EnumToggleButtons]
                public TorgetTuyp TuypTorget;

                public bool isTorget() => TuypTorget == TorgetTuyp.Torget;

                [Required]
                [HorizontalGroup("FF"),HideLabel, ShowIf("isTorget")]
                public Transform Linetransform;

                [HorizontalGroup("FF"), HideLabel, ShowIf("isTorget")]
                public Vector3 LinetransformOffset;
                
                [HorizontalGroup("FF"), HideLabel, HideIf("isTorget")]
                public Vector3 T_Vector3;
                [HideLabel]
                public Color G_color;
            }


            public void OnGizmos()
            {
                
                if (type == sGizmosTuyp.Line) Gizmosline();


            }
            static public void drawString(string text, Vector3 worldPos, Color? colour = null)
            {
                UnityEditor.Handles.BeginGUI();

                var restoreColor = GUI.color;

                if (colour.HasValue) GUI.color = colour.Value;
                var view = UnityEditor.SceneView.currentDrawingSceneView;
                Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

                if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
                {
                    GUI.color = restoreColor;
                    UnityEditor.Handles.EndGUI();
                    return;
                }

                Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
                GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
                GUI.color = restoreColor;
                UnityEditor.Handles.EndGUI();
            }


            public void Gizmosline() 
            {
                for (int i = 0; i < blocks.Count; i++)
                {

                    Vector3 ST = Vector3.zero;
                    Vector3 SE = Vector3.zero;
                    bool CanDraw = false;
                    if (blocks[i].TuypTorget == TorgetTuyp.Torget)
                    {
                        if (blocks[i].Linetransform != null)
                        {
                            ST = blocks[i].Linetransform.position + blocks[i].LinetransformOffset;
                            if (i + 1 != blocks.Count)
                            {
                                if (blocks[i + i].TuypTorget == TorgetTuyp.Torget)
                                {
                                    if (blocks[i + i].Linetransform != null)
                                    {
                                        SE = blocks[i + 1].Linetransform.position + blocks[i + 1].LinetransformOffset;
                                        CanDraw = true;
                                    }
                                }
                                else if (blocks[i + i].TuypTorget == TorgetTuyp.Vector3)
                                {
                                    SE = blocks[i + 1].T_Vector3;
                                    CanDraw = true;
                                }


                            }
                        }
                    }
                    else if (blocks[i].TuypTorget == TorgetTuyp.Vector3)
                    {
                        ST = blocks[i].T_Vector3;
                        if (i + 1 != blocks.Count)
                        {
                            if (blocks[i + 1].TuypTorget == TorgetTuyp.Torget)
                            {
                                if (blocks[i + 1].Linetransform != null)
                                {
                                    SE = blocks[i + 1].Linetransform.position + blocks[i + 1].LinetransformOffset;
                                    CanDraw = true;
                                }
                            }
                            else if (blocks[i + 1].TuypTorget == TorgetTuyp.Vector3)
                            {
                                SE = blocks[i + 1].T_Vector3;
                                CanDraw = true;
                            }
                        }
                    }
                    if (CanDraw)
                    {
                        Gizmos.color = blocks[i].G_color;
                        Gizmos.DrawLine(ST, SE);
                    }
                }
            }



            public enum TorgetTuyp { Vector3, Torget }

        }

        #endregion


        private void OnDrawGizmos()
        {
            if (_Gizmos) 
            {
                for (int i = 0; i < GizmosItems.Count; i++)
                {
                    GizmosItems[i].OnGizmos();
                }
            }
        }






#endif
    }


    public enum ComandsHotKey
    {
        Shift_P,
        Shift_G,
        Shift_L,
        Shift_Q,
        Shift_W,
        Shift_T,
        Shift_Y,
        Shift_U,
        Shift_I,
        Shift_O,
        Shift_A,
        Shift_S,
        Shift_H,
        Shift_J,
        Shift_Z,
        Shift_X,
        Shift_V,
        Shift_B,
        Shift_N,
        Shift_M,
        Ctrl_Q,
        Ctrl_W,
        Ctrl_T,
        Ctrl_U,
        Ctrl_L,
        Ctrl_M,
        Ctrl_Shift_Q,
        Ctrl_Shift_W,
        Ctrl_Shift_E,
        Ctrl_Shift_T,
        Ctrl_Shift_Y,
        Ctrl_Shift_U,
        Ctrl_Shift_O,
        Ctrl_Shift_D,
        Ctrl_Shift_G,
        Ctrl_Shift_H,
        Ctrl_Shift_J,
        Ctrl_Shift_L,
        Ctrl_Shift_M
    }


    public enum sGizmosTuyp
    {
        Line
    }


    [System.Flags]
    public enum MySystemTools
    {
        HotKey = 1 << 1,//----------------
        CustomHierarchy = 1 << 3,
        Gizmos = 1 << 4
    }
}


