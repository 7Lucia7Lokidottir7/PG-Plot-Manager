using UnityEditor;
using UnityEngine;
namespace PG.PlotManagement
{
    [CustomEditor(typeof(PGTransformTween))]
    public class PGTransformTweenEditor : PGPlotBehaviourEditor
    {
        private PGTransformTween _transformTween;
        private SerializedProperty _withPosition;
        private SerializedProperty _position;
        private SerializedProperty _withRotation;
        private SerializedProperty _rotation;
        private SerializedProperty _withLerp;
        private SerializedProperty _duration;
        private SerializedProperty _curve;
        private void OnEnable()
        {
            _withPosition = serializedObject.FindProperty("withPosition");
            _position = serializedObject.FindProperty("position");
            _withRotation = serializedObject.FindProperty("withRotation");
            _rotation = serializedObject.FindProperty("rotation");
            _withLerp = serializedObject.FindProperty("withLerp");
            _duration = serializedObject.FindProperty("duration");
            _curve = serializedObject.FindProperty("curve");
        }

        public void PGPropertyField(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(property.displayName));
        }
        public override void OnInspectorGUI()
        {
            _transformTween = (PGTransformTween)target;
            GUILayout.Label("Transform Tween After Cutscene", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            PGPlotControllerEditorWindow.ObjectsPopup(ref _transformTween.objectIndex, "Transform Index");
            serializedObject.Update();
            PGPropertyField(_withPosition);
            if (_withPosition.boolValue)
            {
                PGPropertyField(_position);
            }
            PGPropertyField(_withRotation);
            if (_withRotation.boolValue)
            {
                PGPropertyField(_rotation);
            }
            PGPropertyField(_withLerp);
            if (_withLerp.boolValue)
            {
                PGPropertyField(_duration);
                PGPropertyField(_curve);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
