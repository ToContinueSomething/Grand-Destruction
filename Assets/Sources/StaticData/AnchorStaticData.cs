using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu(fileName = "AnchorData",menuName = "Static Data/Anchor")]
    public class AnchorStaticData : ScriptableObject
    {
        [SerializeField] private float _startRadius;
        [SerializeField] private GameObject _prefab;

        public GameObject Prefab => _prefab;

        public float StartRadius => _startRadius;
    }
}