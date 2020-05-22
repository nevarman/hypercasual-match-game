using UnityEngine;
namespace HyperCasualMatchGame
{
    /// <summary>
    /// Board settings to be used from Bootstrap
    /// </summary>
    [CreateAssetMenu]
    public class HexBoardSettings : ScriptableObject
    {
        public float outerRadius = 1.3f; // for now hardcoded, we can determine from sprite as well
        public float innerRadius = 1.2f;
        public int width = 7; // board width
        public int height = 6; // board height
        public Transform hexCellPrefab;
        public HexBlock[] hexBlockPrefabs;
        public float dropSpeed = 2f;
        public float waitForRefill = 0.5f;
        public int scoreAddition = 10;
        public int scoreAdditionSpecial = 50;
        public int endScore = 500;
        public GameObject specialHexEffect;
    }
}