using System.Collections;
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

        private Coroutine _spawnCoroutine;

        public void Initialize()
        {
            if (_repeatSpawn)
            {
                StartSpawn();
            }
            else
            {
                OnSpawn();
            }
        }

        public void StartSpawn()
        {
            StopSpawn();
            _spawnCoroutine = StartCoroutine(SpawnTimer());
        }

        public void StopSpawn()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        private IEnumerator SpawnTimer()
        {
            WaitForSeconds delay = new(_spawnCooldown);
            while (true)
            {
                OnSpawn();
                yield return delay;
            }
        }

        protected abstract void OnSpawn();
    }
}