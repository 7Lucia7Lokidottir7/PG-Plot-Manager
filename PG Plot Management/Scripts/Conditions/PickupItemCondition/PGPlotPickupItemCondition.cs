using UnityEngine;
namespace PG.PlotManagement
{
    [System.Serializable]
    public class PGPlotPickupItemCondition : PGPlotCondition
    {
        public int objectIndex;
        private ObjectInteractable _item;

        public override void OnStartCondition(PGPlotController plotController)
        {
            _item = plotController.GetElement(objectIndex).GetComponent<ObjectInteractable>();
        }
        public override void OnUpdateCondition(PGPlotController plotController)
        {
            if (_item.pickupped && plotController.plotAsset.isActive)
            {
                plotController.NextPlot(this);
            }
        }
    }
}
