using System;

namespace PG.PlotManagement
{
    [Serializable]
    public class PGPlotNextStateCondition : PGPlotCondition
    {
        public override void OnStartCondition(PGPlotController plotController)
        {
            if (plotController.plotAsset.isActive)
            {
                plotController.NextPlot(this);
            }
        }
        public override void OnUpdateCondition(PGPlotController plotController)
        {
            return;
        }
        public override void OnEndCondition(PGPlotController plotController)
        {
            return;
        }
    }
}