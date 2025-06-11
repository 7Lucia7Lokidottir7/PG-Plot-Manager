using UnityEngine;
using UnityEditor;

namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGPlotAnimationAfterCutsceneBehaviour))]
    public class PGPlotAnimationAfterCutsceneBehaviourEditor : PGPlotBehaviourEditor
    {
        private PGPlotAnimationAfterCutsceneBehaviour _animationBehaviour;
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Animation After Cutscene Behaviour", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            _animationBehaviour = (PGPlotAnimationAfterCutsceneBehaviour)target;
            PGPlotControllerEditorWindow.ObjectsPopup(ref _animationBehaviour.animatorObjectIndex, "Animator");
            _animationBehaviour.animatorType = (PGPlotAnimationAfterCutsceneBehaviour.AnimatorType)EditorGUILayout.EnumPopup("Type", _animationBehaviour.animatorType);
            GUILayout.BeginHorizontal();
            _animationBehaviour.parameterName = EditorGUILayout.TextField("Parameter", _animationBehaviour.parameterName);
            switch (_animationBehaviour.animatorType)
            {
                case PGPlotAnimationAfterCutsceneBehaviour.AnimatorType.Trigger:
                    break;
                case PGPlotAnimationAfterCutsceneBehaviour.AnimatorType.Bool:
                    _animationBehaviour.parameterBool = EditorGUILayout.Toggle(_animationBehaviour.parameterBool);
                    break;
                case PGPlotAnimationAfterCutsceneBehaviour.AnimatorType.Int:
                    _animationBehaviour.parameterInt = EditorGUILayout.IntField(_animationBehaviour.parameterInt);
                    break;
                case PGPlotAnimationAfterCutsceneBehaviour.AnimatorType.Float:
                    _animationBehaviour.parameterFloat = EditorGUILayout.FloatField(_animationBehaviour.parameterFloat);
                    break;
            }
            GUILayout.EndHorizontal();
        }

    }
}