using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace HyperCasualMatchGame.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textScore;

        internal void UpdateUI(int score)
        {
            _textScore.text = $"Score: {score.ToString()}";
        }
    }
}