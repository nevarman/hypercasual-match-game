using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndUI : MonoBehaviour
{
    [SerializeField]
    private Button _buttonRestart,_buttonQuit;

    void Awake(){
        _buttonQuit.onClick.AddListener(Quit);
        _buttonRestart.onClick.AddListener(Restart);
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void Quit()
    {
        Application.Quit();
    }
}
