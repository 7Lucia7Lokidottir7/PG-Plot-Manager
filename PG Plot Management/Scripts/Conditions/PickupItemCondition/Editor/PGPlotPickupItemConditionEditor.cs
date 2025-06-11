using UnityEditor;
using UnityEngine;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotPickupItemCondition))]
    public class PGPlotPickupItemConditionEditor : PGPlotConditionEditor
    {
        private PGPlotPickupItemCondition _pickupItemCondition;
        public override void OnInspectorGUI()
        {
            _pickupItemCondition = (PGPlotPickupItemCondition)target;
            GUILayout.Label("Pickup Item Condition", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            PGPlotControllerEditorWindow.ObjectsPopup(ref _pickupItemCondition.objectIndex);
            base.OnInspectorGUI();
        }
    }
}