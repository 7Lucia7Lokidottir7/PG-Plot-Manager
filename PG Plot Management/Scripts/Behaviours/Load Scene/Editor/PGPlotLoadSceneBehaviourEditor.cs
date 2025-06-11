using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotLoadSceneBehaviour))]
    public class PGPlotLoadSceneBehaviourEditor : PGPlotBehaviourEditor
    {
        private SerializedProperty _sceneNameProperty;
        private SerializedProperty _conditionIndexProperty;
        private SerializedProperty _sceneModeProperty;
        private SerializedProperty _unloadSceneProperty;
        private SerializedProperty _unloadSceneNameProperty;

        private void OnEnable()
        {
            _sceneNameProperty = serializedObject.FindProperty("sceneName");
            _conditionIndexProperty = serializedObject.FindProperty("conditionIndex");
            _sceneModeProperty = serializedObject.FindProperty("sceneMode");
            _unloadSceneProperty = serializedObject.FindProperty("unloadScene");
            _unloadSceneNameProperty = serializedObject.FindProperty("unloadSceneName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.Label("Load scene", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });

            DrawContextMenuField(_sceneNameProperty, "Scene Name");
            EditorGUILayout.PropertyField(_conditionIndexProperty, new GUIContent("Condition Index"));
            EditorGUILayout.PropertyField(_sceneModeProperty, new GUIContent("Type loading"));
            EditorGUILayout.PropertyField(_unloadSceneProperty, new GUIContent("Unload scene"));

            if (_unloadSceneProperty.boolValue)
            {
                DrawContextMenuField(_unloadSceneNameProperty, "Unload Scene Name");
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawContextMenuField(SerializedProperty property, string label)
        {
            Rect fieldRect = EditorGUILayout.GetControlRect();
            EditorGUI.PropertyField(fieldRect, property, new GUIContent(label));

            HandleContextMenu(property, fieldRect);
        }

        private void HandleContextMenu(SerializedProperty property, Rect rect)
        {
            if (Event.current.type == EventType.ContextClick && rect.Contains(Event.current.mousePosition))
            {
                GenericMenu menu = new GenericMenu();
                if (property.propertyType == SerializedPropertyType.String)
                {
                    string value = property.stringValue;
                    menu.AddItem(new GUIContent("Copy"), false, () => EditorGUIUtility.systemCopyBuffer = value);
                    menu.AddItem(new GUIContent("Paste"), false, () =>
                    {
                        property.stringValue = EditorGUIUtility.systemCopyBuffer;
                        serializedObject.ApplyModifiedProperties();
                    });
                }
                else if (property.propertyType == SerializedPropertyType.Integer)
                {
                    int value = property.intValue;
                    menu.AddItem(new GUIContent("Copy"), false, () => EditorGUIUtility.systemCopyBuffer = value.ToString());
                    menu.AddItem(new GUIContent("Paste"), false, () =>
                    {
                        if (int.TryParse(EditorGUIUtility.systemCopyBuffer, out int parsedValue))
                        {
                            property.intValue = parsedValue;
                            serializedObject.ApplyModifiedProperties();
                        }
                    });
                }

                menu.ShowAsContext();
                Event.current.Use();
            }
        }
    }
}
