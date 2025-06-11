using UnityEngine;
using UnityEngine.Playables;
namespace PG.PlotManagement
{
    public class PGTransformTweenAfterCutscene : PGPlotBehaviour
    {
        private PlayableDirector _playableDirector;

        [HideInInspector]public int objectIndex;
        private Transform _transform;

        public bool withPosition;
        public Vector3 position;
        public bool withRotation;
        public Quaternion rotation;

        public bool withLerp;
        public float duration = 1f;
        public AnimationCurve curve = AnimationCurve.Linear(0,1,0,1);

        private bool _isInvoked;
        public override void OnEndBehaviourState(PGPlotController plotController)
        {
            _isInvoked = false;
        }

        public override void OnStartBehaviourState(PGPlotController plotController)
        {
            _isInvoked = false;
            _playableDirector = plotController.playableDirector;
            _transform = plotController.GetElement(objectIndex).transform;
        }

        public override void OnUpdateBehaviourState(PGPlotController plotController)
        {
            if (!_isInvoked)
            {
                if (_playableDirector.time / _playableDirector.duration > _playableDirector.duration - 0.05f)
                {
                    _isInvoked = true;
                    if (withLerp)
                    {
                        for (float i = 0; i < duration; i += Time.deltaTime)
                        {
                            if (withPosition)
                            {
                                _transform.position = Vector3.LerpUnclamped(_transform.position, position, curve.Evaluate(i / duration));
                            }
                            if (withRotation)
                            {
                                _transform.rotation = Quaternion.LerpUnclamped(_transform.rotation, rotation, curve.Evaluate(i / duration));
                            }
                        }
                    }
                    else
                    {
                        _transform.SetPositionAndRotation(position, rotation);
                    }
                }
            }
        }
    }
}
