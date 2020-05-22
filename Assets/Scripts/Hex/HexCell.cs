using UnityEngine;
namespace HyperCasualMatchGame
{
    /// <summary>
    /// A single hex cell representing on a board
    /// </summary>
    [System.Serializable]
    public class HexCell
    {
        public int x { get; set; }
        public int y { get; set; }
        public Transform transform { get; set; }
        public bool IsEmpty() => transform.childCount == 0;
        public HexBlock GetHexBlock() => transform.GetChild(0).GetComponent<HexBlock>();
    }
}