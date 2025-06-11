using System.Collections;
using UnityEngine.Playables;
namespace PG.PlotManagement
{
    public class PGEventOnEndCutsceneBehaviour : PGPlotBehaviour
    {
        private PlayableDirector _playableDirector;
        public bool onStart;
        public int eventIndex = 0;
        public bool onEnd;
        public int endEventIndex = 0;
        private bool _isInvoked;
        public override void OnEndBehaviourState(PGPlotController plotController)
        {
            _isInvoked = false;
            if (onEnd)
            {
                plotController.StartCoroutine(OnEndEventInvoke(plotController));
            }
        }
        IEnumerator OnEndEventInvoke(PGPlotController plotController)
        {
            while (true)
            {
                if (_playableDirector.time > _playableDirector.duration - 0.05f)
                {
                    plotController.GetEventElement(endEventIndex).Invoke();
                    yield break;
                }
                yield return null;
            }
        }

        public override void OnStartBehaviourState(PGPlotController plotController)
        {
            _isInvoked = false;
            _playableDirector = plotController.playableDirector;
        }

        public override void OnUpdateBehaviourState(PGPlotController plotController)
        {
            if (!_isInvoked && onStart)
            {
                if (_playableDirector.time > _playableDirector.duration - 0.05f)
                {
                    _isInvoked = true;
                    plotController.GetEventElement(eventIndex).Invoke();
                }
            }
        }
    }
}
