﻿using UnityEditor;
using UnityEngine;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotEnableObjectCondition))]
    public class PGPlotEnableObjectConditionEditor : PGPlotConditionEditor
    {
        private PGPlotEnableObjectCondition _enableObjectCondition;
        public override void OnInspectorGUI()
        {
            _enableObjectCondition = (PGPlotEnableObjectCondition)target;
            GUILayout.Label("Enable Object Condition", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            PGPlotControllerEditorWindow.ObjectsPopup(ref _enableObjectCondition.objectIndex);
            _enableObjectCondition.objectActive = EditorGUILayout.Toggle("Active", _enableObjectCondition.objectActive);
            base.OnInspectorGUI();
        }
    }
}