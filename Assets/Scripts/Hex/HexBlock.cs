using UnityEngine;
namespace HyperCasualMatchGame
{
    /// <summary>
    /// Hex block prefab which represents an image on a hex cell
    /// </summary>
    public class HexBlock : MonoBehaviour
    {
        public int x ;
        public int y ;
        public int type;
        public bool isSpecial;

        private void OnEnable()
        {
            this.name = type.ToString();
        }

        public static bool operator ==(HexBlock b1, HexBlock b2)
        {
            if(b1.isSpecial || b2.isSpecial)
                return true;
            return b1.type == b2.type;
        }
        public static bool operator !=(HexBlock b1, HexBlock b2)
        {
            if (b1.isSpecial || b2.isSpecial)
                return false; 
            return b1.type != b2.type;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var b = (HexBlock)obj ;
            bool v = b.type == this.type && b.x == this.x && b.y == this.y;
            return v;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}