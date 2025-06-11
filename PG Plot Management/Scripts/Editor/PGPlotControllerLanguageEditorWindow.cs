using UnityEditor;
using UnityEngine;

namespace PG.PlotManagement
{
    public partial class PGPlotControllerEditorWindow : EditorWindow
    {
        private bool _languageLeftMenuActive = true;
        private Vector2 _menuLanguagesScrollView;
        private Vector2 _menuLanguageScrollView;

        private string _newLanguageName;

        void OnLanguagesMenu()
        {
            if (_languageLeftMenuActive)
            {
                GUILayout.BeginVertical("box", GUILayout.ExpandWidth(false), GUILayout.MaxWidth(_maxWidthLeftMenu));
                GUILayout.Label("Languages");
                _newLanguageName = GUILayout.TextField(_newLanguageName);
                if (GUILayout.Button("Create"))
                {
                    CreateLanguage(ref _newLanguageName);
                    InitializeLanguages();
                }

                _menuLanguagesScrollView = GUILayout.BeginScrollView(_menuLanguagesScrollView, GUILayout.ExpandWidth(false));
                for (int i = 0; i < asset.Languages.Count; i++)
                {
                    GUILayout.BeginHorizontal("box");
                    asset.Languages[i] = EditorGUILayout.TextField(asset.Languages[i]);
                    if (GUILayout.Button("x", GUILayout.Width(30)))
                    {
                        asset.Languages.RemoveAt(i);
                        InitializeLanguages();
                        EditorUtility.SetDirty(asset);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();

                GUILayout.EndVertical();
            }
        }
        void CreateLanguage(ref string value)
        {
            if (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
            {
                asset.Languages.Add(value);
                value = "";
                GUI.FocusControl(null);
                EditorUtility.SetDirty(asset);
            }
        }

    }
}