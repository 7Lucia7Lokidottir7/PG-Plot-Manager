using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

namespace PG.PlotManagement
{
    [Serializable]
    public class PGPlotState
    {
        public List<PGLanguageState> languageStates = new List<PGLanguageState>(1);

        public string targetSceneName;

        public bool startTimelineEnable = true;
        public PlayableAsset startTimelineAsset;
        public List<PGPlotObjectActive> plotObjectActives = new List<PGPlotObjectActive>();
        public bool endTimelineEnable = true;
        public PlayableAsset endTimelineAsset;
        public List<PGPlotCondition> plotConditions = new List<PGPlotCondition>(1);
        public List<PGPlotBehaviour> plotBehaviours = new List<PGPlotBehaviour>();


        public PGPlotState nextState;
        public bool isStarted = false;

        public bool startEventEnable = true;
        public int startEventObjectIndex;

        public bool endEventEnable = true;
        public int endEventObjectIndex;

        public void OnStartState(PGPlotController plotController)
        {
            if(SceneManager.GetActiveScene().name == targetSceneName)
            {
                if (startTimelineEnable)
                {
                    plotController.playableDirector.Play(startTimelineAsset);
                }
                OnChangeObjectsActive(plotController);
                OnStartObjectEventInvoke(plotController);
                foreach (var item in plotConditions)
                {
                    item.OnStartCondition(plotController);
                }
                if (plotBehaviours.Count > 0)
                {
                    foreach (var item in plotBehaviours)
                    {
                        item.OnStartBehaviourState(plotController);
                    }
                }
                isStarted = true;
            }
        }
        void OnChangeObjectsActive(PGPlotController plotController)
        {
            for (int i = 0; i < plotObjectActives.Count; i++)
            {
                GameObject plotObject = plotController.GetElement(plotObjectActives[i].index);
                if (plotObject == null)
                {
                    continue;
                }
                plotObject.SetActive(plotObjectActives[i].active);
            }
        }
        void OnStartObjectEventInvoke(PGPlotController plotController)
        {
            if (startEventEnable)
            {
                plotController.GetEventElement(startEventObjectIndex)?.Invoke();
            }
        }
        void OnEndObjectEventInvoke(PGPlotController plotController)
        {
            if (endTimelineEnable)
            {
                plotController.playableDirector.Play(endTimelineAsset);
            }
            if (endEventEnable)
            {
                plotController.GetEventElement(endEventObjectIndex)?.Invoke();
            }
        }
        public void OnUpdateState(PGPlotController plotController)
        {
            if (SceneManager.GetActiveScene().name == targetSceneName)
            {
                foreach (var item in plotConditions)
                {
                    item.OnUpdateCondition(plotController);
                }
                if (plotBehaviours.Count > 0)
                {
                    foreach (var item in plotBehaviours)
                    {
                        item.OnUpdateBehaviourState(plotController);
                    }
                }
            }
        }
        public void OnEndState(PGPlotController plotController)
        {
            if (SceneManager.GetActiveScene().name == targetSceneName)
            {
                OnEndObjectEventInvoke(plotController);
                if (endTimelineEnable)
                {
                    plotController.playableDirector.Play(endTimelineAsset);
                }

                foreach (var item in plotConditions)
                {
                    item.OnEndCondition(plotController);
                }
                if (plotBehaviours.Count > 0)
                {
                    foreach (var item in plotBehaviours)
                    {
                        item.OnEndBehaviourState(plotController);
                    }
                }
            }
        }
    }
}