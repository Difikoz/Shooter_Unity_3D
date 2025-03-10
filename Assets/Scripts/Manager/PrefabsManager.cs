using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PrefabsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _npcPrefab;
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private GameObject _itemPrefab;

        public NPCController GetNPC(Transform point)
        {
            return GetNPC(point.position, point.rotation);
        }

        public NPCController GetNPC(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_npcPrefab, position, rotation).GetComponent<NPCController>();
        }

        public PawnController GetPawn(Transform point)
        {
            return GetPawn(point.position, point.rotation);
        }

        public PawnController GetPawn(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_pawnPrefab, position, rotation).GetComponent<PawnController>();
        }

        public GameObject GetVisual(string name, Transform root)
        {
            return LeanPool.Spawn(GameManager.StaticInstance.ConfigsManager.GetVisual(name).Model, root);
        }

        public InteractableItem GetItem(Transform point)
        {
            return GetItem(point.position, point.rotation);
        }

        public InteractableItem GetItem(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_itemPrefab, position, rotation).GetComponent<InteractableItem>();
        }

        public void DespawnObject(GameObject go, float delay = 0f)
        {
            LeanPool.Despawn(go, delay);
        }
    }
}