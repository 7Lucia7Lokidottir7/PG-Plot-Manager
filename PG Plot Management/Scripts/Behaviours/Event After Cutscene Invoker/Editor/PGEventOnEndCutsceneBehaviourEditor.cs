using UnityEditor;
using UnityEngine;
namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGEventOnEndCutsceneBehaviour))]
    public class PGEventOnEndCutsceneBehaviourEditor : PGPlotBehaviourEditor
    {
        private PGEventOnEndCutsceneBehaviour _eventOnEndCutsceneBehaviour;
        public override void OnInspectorGUI()
        {
            _eventOnEndCutsceneBehaviour = (PGEventOnEndCutsceneBehaviour)target;
            GUILayout.Label("Event On End Cutscene", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            _eventOnEndCutsceneBehaviour.onStart = EditorGUILayout.Toggle("On Start", _eventOnEndCutsceneBehaviour.onStart);
            if (_eventOnEndCutsceneBehaviour.onStart)
            {
                PGPlotControllerEditorWindow.EventObjectsPopup(ref _eventOnEndCutsceneBehaviour.eventIndex, "Event Index");
            }
            _eventOnEndCutsceneBehaviour.onEnd = EditorGUILayout.Toggle("On End", _eventOnEndCutsceneBehaviour.onEnd);
            if (_eventOnEndCutsceneBehaviour.onEnd)
            {
                PGPlotControllerEditorWindow.EventObjectsPopup(ref _eventOnEndCutsceneBehaviour.endEventIndex, "End Event Index");
            }
        }
    }
}
