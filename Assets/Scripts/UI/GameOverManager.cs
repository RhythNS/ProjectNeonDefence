using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] private UIView gameOverView;
    [SerializeField] private UIDrawer towerDrawer;

    private void Awake()
    {
        Instance = this;
        gameOverView.Hide(true);
    }

    public void Show()
    {
        gameOverView.Show();
        UpgradeManager.Instance.UIView.Hide();
        towerDrawer.Close();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}