using UnityEditor;
using UnityEngine;
namespace PG.PlotManagement
{
    public partial class PGPlotControllerEditorWindow : EditorWindow
    {
        private Vector2 _menuObjectsScrollView;
        private Vector2 _menuObjectEventsScrollView;
        private Vector2 _leftMenuScrollView;

        private bool _objectEventsMenu;
        private bool _objectsMenu;

        private string _newObjectName;
        private string _newEventObjectName;
        public static void ObjectsPopup(ref int targetValue)
        {
            targetValue = EditorGUILayout.Popup("Object", targetValue, asset.Objects.ToArray());
        }
        public static void ObjectsPopup(ref int targetValue, string nameObject)
        {
            targetValue = EditorGUILayout.Popup(nameObject, targetValue, asset.Objects.ToArray());
        }
        public static void EventObjectsPopup(ref int targetValue)
        {
            targetValue = EditorGUILayout.Popup("Event Object", targetValue, asset.EventObjects.ToArray());
        }
        public static void EventObjectsPopup(ref int targetValue, string nameObject)
        {
            targetValue = EditorGUILayout.Popup(nameObject, targetValue, asset.EventObjects.ToArray());
        }
        void OnObjectsMenu()
        {
            GUILayout.BeginVertical("box");
            GUILayout.Label("Objects");
            _newObjectName = GUILayout.TextField(_newObjectName);
            if (GUILayout.Button("Create"))
            {
                CreateObject(ref _newObjectName);
                EditorUtility.SetDirty(asset);
            }

            _menuObjectsScrollView = GUILayout.BeginScrollView(_menuObjectsScrollView);
            for (int i = 0; i < asset.Objects.Count; i++)
            {
                GUILayout.BeginHorizontal("box");
                asset.Objects[i] = EditorGUILayout.TextArea(asset.Objects[i], new GUIStyle(EditorStyles.textArea) { wordWrap = true });
                if (GUILayout.Button("x", GUILayout.Width(30)))
                {
                    asset.Objects.RemoveAt(i);
                    EditorUtility.SetDirty(asset);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();

            GUILayout.EndVertical();
        }
        void CreateObject(ref string value)
        {
            if (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
            {
                asset.Objects.Add(value);
                value = "";
                GUI.FocusControl(null);
                EditorUtility.SetDirty(asset);
            }
        }
        void OnEventObjectsMenu()
        {
            GUILayout.BeginVertical("box");
            GUILayout.Label("Event Objects");
            _newEventObjectName = GUILayout.TextField(_newEventObjectName);
            if (GUILayout.Button("Create"))
            {
                CreateEventObject(ref _newEventObjectName);
                EditorUtility.SetDirty(asset);
            }

            _menuObjectEventsScrollView = GUILayout.BeginScrollView(_menuObjectEventsScrollView);
            for (int i = 0; i < asset.EventObjects.Count; i++)
            {
                GUILayout.BeginHorizontal("box");
                asset.EventObjects[i] = EditorGUILayout.TextArea(asset.EventObjects[i], new GUIStyle(EditorStyles.textArea) { wordWrap = true});
                if (GUILayout.Button("x", GUILayout.Width(30)))
                {
                    asset.EventObjects.RemoveAt(i);
                    EditorUtility.SetDirty(asset);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();

            GUILayout.EndVertical();
        }
        void CreateEventObject(ref string value)
        {
            if (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
            {
                asset.EventObjects.Add(value);
                value = "";
                GUI.FocusControl(null);
                EditorUtility.SetDirty(asset);
            }
        }
    }
}