using UnityEngine;
using UnityEngine.Playables;

namespace PG.PlotManagement
{
    public class PGPlotAnimationAfterCutsceneBehaviour : PGPlotBehaviour
    {
        public int animatorObjectIndex;
        private Animator _animator;
        public PGPlotController controller { get; private set; }
        public enum AnimatorType { Trigger, Bool, Int, Float }
        public AnimatorType animatorType;
        public string parameterName;
        public bool parameterBool;
        public int parameterInt;
        public float parameterFloat;
        private PlayableDirector _playableDirector;

        private bool _isInvoked;

        public override void OnStartBehaviourState(PGPlotController plotController)
        {
            controller = plotController;
            _animator = plotController.GetElement(animatorObjectIndex).GetComponent<Animator>();
            _playableDirector = plotController.playableDirector;
        }
        public override void OnUpdateBehaviourState(PGPlotController plotController)
        {
            if (!_isInvoked)
            {
                if (_playableDirector.time / _playableDirector.duration > _playableDirector.duration - 0.05f)
                {
                    _isInvoked = true;
                    switch (animatorType)
                    {
                        case AnimatorType.Trigger:
                            _animator.SetTrigger(parameterName);
                            break;
                        case AnimatorType.Bool:
                            _animator.SetBool(parameterName, parameterBool);
                            break;
                        case AnimatorType.Int:
                            _animator.SetInteger(parameterName, parameterInt);
                            break;
                        case AnimatorType.Float:
                            _animator.SetFloat(parameterName, parameterFloat);
                            break;
                    }
                }
            }
        }
    }
}