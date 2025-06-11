using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace PG.PlotManagement
{
    public class PGTransformTween : PGPlotBehaviour
    {

        [HideInInspector]public int objectIndex;
        private Transform _transform;

        public bool withPosition;
        public Vector3 position;
        public bool withRotation;
        public Quaternion rotation;

        public bool withLerp;
        public float duration = 1f;
        public AnimationCurve curve = AnimationCurve.Linear(0,1,0,1);

        public override void OnStartBehaviourState(PGPlotController plotController)
        {
            _transform = plotController.GetElement(objectIndex).transform;


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
