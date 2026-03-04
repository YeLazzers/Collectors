using UnityEngine;

namespace YeLazzers.Buildings
{
    public class WorkerTrainer : MonoBehaviour
    {
        [SerializeField] private WorkerHub _hub;
        [SerializeField] private ResourceStorage _storage;
        [SerializeField] private int _cost;

        private void OnResourcesUpdated(int amount)
        {
            if (amount >= _cost)
            {
                _hub.TrainWorker(1);
                _storage.Spend(amount);
            }
        }

        public void TryTrainWorker()
        {
            if (_storage.Amount >= _cost)
            {
                _hub.TrainWorker(1);
                _storage.Spend(_cost);
            }
        }
    }
}
