using UnityEngine;
using UnityEditor;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotWithoutConditions))]
    public class PGPlotWithoutConditionsEditor : PGPlotConditionEditor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Without Conditions", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            base.OnInspectorGUI();
        }
    }
}