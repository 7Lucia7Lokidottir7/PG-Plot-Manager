using UnityEngine;
using UnityEditor;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotNextStateCondition))]
    public class PGPlotNextStateConditionEditor : PGPlotConditionEditor
    {
        private PGPlotNextStateCondition _nextStateCondition;
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Next State", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            _nextStateCondition = (PGPlotNextStateCondition)target;
            base.OnInspectorGUI();
        }
    }
}