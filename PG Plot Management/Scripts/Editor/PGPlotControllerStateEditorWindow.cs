using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace PG.PlotManagement
{
    public partial class PGPlotControllerEditorWindow : EditorWindow
    {
        private Vector2 _menuStateScrollView;
        private Vector2 _menuObjectStateScrollView;

        private int _languageIndex;
        private bool _objectsStateMenu;
        void OnStateMenu(int chapter, int state)
        {
            GUILayout.BeginVertical("box");
            InitializeLanguages();

            PGPlotChapter plotChapter = asset.chapters[chapter];
            PGPlotState plotState = plotChapter.states[state];
            _menuStateScrollView = GUILayout.BeginScrollView(_menuStateScrollView);

            GUILayout.BeginVertical("box");
            _languageIndex = EditorGUILayout.Popup("Language", _languageIndex, asset.Languages.ToArray());
            plotState.languageStates[_languageIndex].name = EditorGUILayout.TextField("Name", plotState.languageStates[_languageIndex].name);
            plotState.languageStates[_languageIndex].description = EditorGUILayout.TextField("Description", plotState.languageStates[_languageIndex].description, GUILayout.MinHeight(60));
            EditorUtility.SetDirty(asset);
            GUILayout.EndVertical();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal("box");
            plotState.targetSceneName = EditorGUILayout.TextField("Target Scene Name", plotState.targetSceneName);
            if (GUILayout.Button("Get current scene"))
            {
                plotState.targetSceneName = SceneManager.GetActiveScene().name;
            }
            GUILayout.EndHorizontal();

            plotState.startTimelineEnable = EditorGUILayout.Toggle("Start Timeline Enable", plotState.startTimelineEnable);
            if (plotState.startTimelineEnable)
            {
                plotState.startTimelineAsset = EditorGUILayout.ObjectField("Start Timeline Asset", plotState.startTimelineAsset, typeof(PlayableAsset), true) as PlayableAsset;
            }
            plotState.startEventEnable = EditorGUILayout.Toggle("Start Event Enable", plotState.startEventEnable);
            if (plotState.startEventEnable)
            {
                plotState.startEventObjectIndex = EditorGUILayout.Popup("Start Event object", plotState.startEventObjectIndex, asset.EventObjects.ToArray());
            }


            GUILayout.Space(10);
            _objectsStateMenu = EditorGUILayout.BeginFoldoutHeaderGroup(_objectsStateMenu, "Objects");
            EditorGUILayout.EndFoldoutHeaderGroup();
            if (_objectsStateMenu)
            {
                OnObjectsMenu(chapter, state);
            }
            GUILayout.EndScrollView();
            GUILayout.Space(10);

            plotState.endTimelineEnable = EditorGUILayout.Toggle("End Timeline Enable", plotState.endTimelineEnable);
            if (plotState.endTimelineEnable)
            {
                plotState.endTimelineAsset = EditorGUILayout.ObjectField("End Timeline Asset", plotState.endTimelineAsset, typeof(PlayableAsset), true) as PlayableAsset;
            }

            plotState.endEventEnable = EditorGUILayout.Toggle("End Event Enable", plotState.endEventEnable);
            if (plotState.endEventEnable)
            {
                plotState.endEventObjectIndex = EditorGUILayout.Popup("End Event object", plotState.endEventObjectIndex, asset.EventObjects.ToArray());
            }
            GUILayout.Space(20);
            GUILayout.EndVertical();
        }
        void OnObjectsMenu(int chapter, int state)
        {
            PGPlotChapter plotChapter = asset.chapters[chapter];
            PGPlotState plotState = plotChapter.states[state];

            GUIStyle panelStyle = new GUIStyle(GUI.skin.box);
            panelStyle.margin = new RectOffset(10, 10, 10, 10);
            panelStyle.padding = new RectOffset(10, 10, 10, 10);
            panelStyle.border = new RectOffset(2, 2, 2, 2);
            panelStyle.normal.background = Texture2D.grayTexture; // Яркий цвет фона


            _menuObjectStateScrollView = GUILayout.BeginScrollView(_menuObjectStateScrollView, panelStyle, GUILayout.Height(550));
            if (GUILayout.Button("Add Object"))
            {
                plotState.plotObjectActives.Add(new PGPlotObjectActive());
                GUI.FocusControl(null);
                EditorUtility.SetDirty(asset);
            }
            for (int i = 0; i < plotState.plotObjectActives.Count; i++)
            {
                PGPlotObjectActive plotObjectActive = plotState.plotObjectActives[i];
                GUILayout.BeginHorizontal(panelStyle);
                plotObjectActive.index = EditorGUILayout.Popup(plotObjectActive.index, asset.Objects.ToArray());
                GUILayout.Label("Active");
                plotObjectActive.active = EditorGUILayout.Toggle(plotObjectActive.active);
                if (GUILayout.Button("x"))
                {
                    plotState.plotObjectActives.Remove(plotObjectActive);
                    EditorUtility.SetDirty(asset);
                    GUI.FocusControl(null);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
    }
}