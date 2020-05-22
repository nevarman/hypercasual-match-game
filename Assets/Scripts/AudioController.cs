using UnityEngine;
namespace HyperCasualMatchGame.Audio
{
    public class AudioController : MonoBehaviour
    {
        private static AudioController _instance;
        public static AudioController Instance
        {
            get { return _instance; }
        }
        [SerializeField]
        private AudioSource _success, _fail, _newHex, _special;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _instance = this;
        }

        public void PlaySuccesSound()
        {
            _success.Play();
        }
        public void PlayFailSound()
        {
            _fail.Play();
        }
        public void PlayHexSound()
        {
            _newHex.Play();
        }
        public void PlaySpecialSound()
        {
            _special.Play();
        }
    }
}