using HyperCasualMatchGame.UI;
using UnityEngine;
namespace HyperCasualMatchGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]
        private HexBoardSettings _settings;
        [SerializeField]
        private ScoreUI _scoreView;
        [SerializeField]
        private EndUI _endUI;
        private ScoreUIController _scoreUIController;

        void Awake()
        {
            // Create board and player input
            var boardController = new GameObject("BoardController").AddComponent<BoardController>();//Instantiate(_boardController, Vector3.zero, Quaternion.identity);
            boardController.Init(_settings);
            var playerInput = new GameObject("PlayerInput").AddComponent<PlayerInput>();
            playerInput.TilesSelected += boardController.CheckSelectedTiles;

            // Score UI
            ScoreUIModel model = new ScoreUIModel();
            boardController.ScoreChanged += (int score)=>{
                model.Value += score;
                // End score
                if(model.Value >= _settings.endScore)
                {
                    Destroy(playerInput);
                    Instantiate(_endUI);
                }
            };
            var view = Instantiate<ScoreUI>(_scoreView);
            _scoreUIController = new ScoreUIController(model, view);
        }
    }
}