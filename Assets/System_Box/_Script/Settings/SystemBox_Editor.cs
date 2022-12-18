

using System.Diagnostics;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace SystemBox
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class SystemBox_Editor
    {
        #if UNITY_EDITOR

        static Texture2D texture;
        static Texture2D texture_EventBox;
        static SystemBox_Editor()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
            texture = AssetDatabase.LoadAssetAtPath("Assets/System_Box/_Images/Ctrs_Image.png", typeof(Texture2D)) as Texture2D;
            texture_EventBox = AssetDatabase.LoadAssetAtPath("Assets/System_Box/_Images/Megaphone.png", typeof(Texture2D)) as Texture2D;
        }
        #region He
        const string IgnoreIcons = "GameObject Icon, Prefab Icon";
        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            Color _backgroundColor = new Color(.76f, .76f, .76f);

            var obj = EditorUtility.InstanceIDToObject(instanceID); // GAmeobjekt  herachiri id


            if (obj != null)
            {
                GameObject gameObj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                if (MySystemTool != null) 
                {
                    Rect offsetRect = new Rect(selectionRect);
                    if (MySystemTool.UesIcons) 
                    {
                        var content = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);
                        if (content.image != null && !IgnoreIcons.Contains(content.image.name))
                            GUI.DrawTexture(new Rect(selectionRect.xMax - 16, selectionRect.yMin, 16, 16), content.image);
                        if (gameObj.GetComponent<SystemTools>() != null)
                        {
                            offsetRect = new Rect(selectionRect);
                            offsetRect.x = offsetRect.width - 0;
                            offsetRect.width = 20;
                            //EditorGUI.DrawRect(selectionRect, _backgroundColor);

                            GUI.Label(offsetRect, texture);
                        }
                        if (gameObj.GetComponent<EventBox>() != null)
                        {
                            offsetRect = new Rect(selectionRect);
                            offsetRect.x = offsetRect.width + 18;
                            offsetRect.width = 20;
                            

                            GUI.Label(offsetRect, texture_EventBox);
                        }
                    }
                    

                    HierarchyItemsValues nv = MySystemTool.GetAllHI_Items(gameObj);
                    if (Selection.activeObject != gameObj) 
                    {
                        if (MySystemTool.system.HasFlag(MySystemTools.CustomHierarchy) && MySystemTool._Hierarchy)
                        {

                            EditorGUI.DrawRect(selectionRect, nv.Backgraund);
                        }
                    }
                    
                    if (MySystemTool.system.HasFlag(MySystemTools.HotKey) && MySystemTool._Hotkey) 
                    {
                        
                        offsetRect = new Rect(selectionRect);
                        offsetRect.x = offsetRect.width - 40;
                        GUI.enabled = false;
                        EditorGUI.TextField(offsetRect, label: nv.HotkeyText, "",GUIStyle.none);
                        GUI.enabled = true;
                    }
                }

               

            }

        }
        #endregion



        #region HotKay

        public static SystemTools MySystemTool 
        {
            set {
                _hotKeySystem = value;
            }
            get {
                if (_hotKeySystem == null) 
                {
                    SystemTools[] ll = GameObject.FindObjectsOfType<SystemTools>();
                    if (ll.Length > 0) _hotKeySystem = ll[0];
                }
                return _hotKeySystem;
            }
        }
        public static SystemTools _hotKeySystem;

       


        [MenuItem("Tools/SystemBox/Hotkeys/#p #p")]
        public static void Shift_P()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_P); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#g #g")]
        public static void Shift_G()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_G); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#l #l")]
        public static void Shift_L()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_L); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#q #q")]
        public static void Shift_Q()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_Q); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#w #w")]
        public static void Shift_W()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_W); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#t #t")]
        public static void Shift_T()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_T); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#y #y")]
        public static void Shift_Y()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_Y); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#u #u")]
        public static void Shift_U()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_U); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#i #i")]
        public static void Shift_I()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_I); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#o #o")]
        public static void Shift_O()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_O); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#a #a")]
        public static void Shift_A()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_A); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#s #s")]
        public static void Shift_S()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_S); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#h #h")]
        public static void Shift_H()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_H); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#j #j")]
        public static void Shift_J()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_J); }
        }

        [MenuItem("Tools/SystemBox/Hotkeys/#z #z")]
        public static void Shift_Z()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_Z); }
        }

        [MenuItem("Tools/SystemBox/Hotkeys/#x #x")]
        public static void Shift_X()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_X); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#v #v")]
        public static void Shift_V()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_V); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#b #b")]
        public static void Shift_B()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_B); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#n #n")]
        public static void Shift_N()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_N); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#m #m")]
        public static void Shift_M()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Shift_M); }
        }
        //--------------------------------------------------
        [MenuItem("Tools/SystemBox/Hotkeys/%q %q")]
        public static void Cintrl_Q()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Q); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/%w %w")]
        public static void Cintrl_W()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_W); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/%t %t")]
        public static void Cintrl_T()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_T); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/%u %u")]
        public static void Cintrl_U()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_U); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/%l %l")]
        public static void Cintrl_L()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_L); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/%m %m")]
        public static void Cintrl_M()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_M); }
        }
        //--------------------------------------------------------------------------

        [MenuItem("Tools/SystemBox/Hotkeys/#%q #%q")]
        public static void Shift_Cintrl_Q()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_Q); }
        }

        [MenuItem("Tools/SystemBox/Hotkeys/#%w #%w")]
        public static void Shift_Cintrl_W()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_W); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%e #%e")]
        public static void Shift_Cintrl_E()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_E); }
        }

        [MenuItem("Tools/SystemBox/Hotkeys/#%t #%t")]
        public static void Shift_Cintrl_T()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_T); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%y #%y")]
        public static void Shift_Cintrl_Y()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_Y); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%u #%u")]
        public static void Shift_Cintrl_U()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_U); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%o #%o")]
        public static void Shift_Cintrl_O()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_O); }
        }

        [MenuItem("Tools/SystemBox/Hotkeys/#%d #%d")]
        public static void Shift_Cintrl_D()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_D); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%g #%g")]
        public static void Shift_Cintrl_G()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_G); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%h #%h")]
        public static void Shift_Cintrl_H()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_H); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%j #%j")]
        public static void Shift_Cintrl_J()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_J); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%l #%l")]
        public static void Shift_Cintrl_L()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_L); }
        }
        [MenuItem("Tools/SystemBox/Hotkeys/#%m #%m")]
        public static void Shift_Cintrl_M()
        {
            if (MySystemTool != null) { MySystemTool.OnKey(ComandsHotKey.Ctrl_Shift_M); }
        }


        [MenuItem("Tools/SystemBox/Hotkeys/%&h %&h")]
        public static void Alt_Cintrl_H()
        {
            if (MySystemTool != null) { Selection.activeGameObject = MySystemTool.gameObject; }
        }

        //-------------------------------------------------------------------
        #endregion


        #endif
    }
}




