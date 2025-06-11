using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotController))]
    public class PGPlotControllerEditor : Editor
    {
        private PGPlotController plotController;
        private bool _objectsPanel;
        private bool _eventsPanel;
        private GUIStyle _panelStyle;
        private List<bool> _eventsVisible = new List<bool>();
        private void OnEnable()
        {
            plotController = (PGPlotController)target;
        }
        public override void OnInspectorGUI()
        {
            _panelStyle = new GUIStyle(GUI.skin.box);
            _panelStyle.margin = new RectOffset(10, 10, 10, 10);
            _panelStyle.padding = new RectOffset(10, 10, 10, 10);
            _panelStyle.border = new RectOffset(2, 2, 2, 2);
            _panelStyle.normal.background = Texture2D.grayTexture; // Яркий цвет фона


            DrawDefaultInspector();
            GUILayout.Space(25);
            if (plotController.plotAsset != null)
            {
                if (GUILayout.Button("Open Asset"))
                {
                    PGPlotControllerEditorWindow.OpenWindow(plotController.plotAsset);
                }
            }
            GUILayout.Space(15);

            if (GUILayout.Button("Create Asset"))
            {
                // Создаем новый экземпляр вашего ассета (в данном случае, ScriptableObject)
                PGPlotAsset asset = ScriptableObject.CreateInstance<PGPlotAsset>();

                // Убедитесь, что путь куда вы сохраняете ассет существует
                string path = "Assets/New Plot Asset.asset";

                // Создаем ассет и сохраняем его в проекте
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                plotController.plotAsset = asset;
                PGPlotControllerEditorWindow.OpenWindow(plotController.plotAsset);
            }
            GUILayout.Space(15);
            if (plotController.plotAsset != null)
            {
                EventsPanel();
                GUILayout.Space(10);
                ObjectsPanel();
            }


            if (GUI.changed)
            {
                EditorUtility.SetDirty(plotController);
            }
        }
        void ObjectsPanel()
        {
            _objectsPanel = EditorGUILayout.BeginFoldoutHeaderGroup(_objectsPanel, "Objects");
            if (_objectsPanel)
            {
                GUILayout.BeginVertical(_panelStyle);
                if (GUILayout.Button("Add Object"))
                {
                    plotController.objectsElements.Add(new PGPlotController.ObjectElement());
                    EditorUtility.SetDirty(plotController);
                }


                for (int i = 0; i < plotController.objectsElements.Count; i++)
                {
                    GUILayout.BeginHorizontal(_panelStyle);


                    GUILayout.BeginVertical("box");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Name");
                    plotController.objectsElements[i].indexObject = EditorGUILayout.Popup(plotController.objectsElements[i].indexObject, plotController.plotAsset.Objects.ToArray());
                    GUILayout.EndVertical();


                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Object");
                    plotController.objectsElements[i].gameObject = (GameObject)EditorGUILayout.ObjectField(plotController.objectsElements[i].gameObject, typeof(GameObject), true);
                    GUILayout.EndHorizontal();

                    GUILayout.EndVertical();


                    if (GUILayout.Button("X", new GUIStyle(GUI.skin.button) { fontSize= 20}, GUILayout.Width(40), GUILayout.Height(40)))
                    {
                        plotController.objectsElements.RemoveAt(i);
                        if (plotController.objectsElements.Count > 0)
                        {
                            i--;
                        }
                        EditorUtility.SetDirty(plotController);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                }
                GUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        void EventsPanel()
        {
            _eventsPanel = EditorGUILayout.BeginFoldoutHeaderGroup(_eventsPanel, "Events");
            EditorGUILayout.EndFoldoutHeaderGroup();
            if (_eventsPanel)
            {
                if (_eventsVisible.Count < plotController.eventElements.Count)
                {
                    for (int i = 0; i < plotController.eventElements.Count; i++)
                    {
                        _eventsVisible.Add(false);
                    }
                }
                if (_eventsVisible.Count > plotController.eventElements.Count)
                {
                    for (int i = _eventsVisible.Count; i < plotController.eventElements.Count; i++)
                    {
                        _eventsVisible.RemoveAt(i);
                    }
                }
                GUILayout.BeginVertical(_panelStyle);
                if (GUILayout.Button("Add Event"))
                {
                    plotController.eventElements.Add(new PGPlotController.EventElement());
                    _eventsVisible.Add(false);
                    EditorUtility.SetDirty(plotController.plotAsset);
                }

                for (int i = 0; i < plotController.eventElements.Count; i++)
                {
                    GUILayout.BeginVertical(_panelStyle);


                    //// Меняем текст или иконку в зависимости от состояния
                    string headerText = _eventsVisible[i] ? "▼ " : "▶ ";
                    headerText += plotController.plotAsset.EventObjects[plotController.eventElements[i].indexEvent];

                    //// Убираем стандартную иконку стрелки через стиль
                    GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldoutHeader)
                    {
                        fontStyle = FontStyle.Bold, // Можно кастомизировать стиль
                        normal = { background = null }, // Убираем фон, если нужно
                        padding = new RectOffset(0, 0, 0, 0), // Дополнительно сдвигаем текст, чтобы компенсировать иконку
                    };


                    // Создаем заголовок кнопкой для полноценного клика
                    if (GUILayout.Button(headerText, foldoutStyle))
                    {
                        _eventsVisible[i] = !_eventsVisible[i]; // Переключаем видимость
                    }
                    //_eventsVisible[i] = EditorGUILayout.BeginFoldoutHeaderGroup(_eventsVisible[i], plotController.plotAsset.EventObjects[plotController.eventElements[i].indexEvent]);
                    //EditorGUILayout.EndFoldoutHeaderGroup();

                    if (_eventsVisible[i])
                    {
                        GUILayout.Space(5); // Добавляем отступы для эстетики
                        plotController.eventElements[i].indexEvent = EditorGUILayout.Popup("Name", plotController.eventElements[i].indexEvent, plotController.plotAsset.EventObjects.ToArray());

                        EditorGUILayout.Separator();
                        SerializedProperty eventElementProperty = serializedObject.FindProperty("eventElements").GetArrayElementAtIndex(i);
                        SerializedProperty eventProperty = eventElementProperty.FindPropertyRelative("plotEvent");

                        serializedObject.Update();
                        EditorGUILayout.PropertyField(eventProperty, true);
                        serializedObject.ApplyModifiedProperties();

                        if (GUILayout.Button("X"))
                        {
                            plotController.eventElements.RemoveAt(i);
                            _eventsVisible.RemoveAt(i);
                            if (plotController.eventElements.Count > 0)
                            {
                                i--;
                            }
                            EditorUtility.SetDirty(plotController);
                        }
                    }

                    GUILayout.EndVertical();
                    //GUILayout.Space(10);
                }
                GUILayout.EndVertical();
            }
        }

    }
}