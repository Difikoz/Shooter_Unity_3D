using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        [SerializeField] protected List<Transform> _spawnPoints = new();
        [SerializeField] protected int _minAmount = 1;
        [SerializeField] protected int _maxAmount = 2;
        [SerializeField] private bool _repeatSpawn = true;
        [SerializeField] private float _spawnCooldown = 60f;

        private float _currentTime;

        public void Initialize()
        {
            _currentTime = 0f;
            OnSpawn();
        }

        public void OnUpdate()
        {
            if (!_repeatSpawn)
            {
                return;
            }
            if (_currentTime >= _spawnCooldown)
            {
                _currentTime = 0f;
                OnSpawn();
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }

        protected abstract void OnSpawn();
    }
}