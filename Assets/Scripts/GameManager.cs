using NGS.ExtendableSaveSystem;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    public void StartGame(GameObject StartedPanel)
    {
        UIManager.instance.TurnOff(StartedPanel);
        GameMaster.instance.LoadGame();
        Table.instance.InitTable();
    }

    public void ResumeGame(GameObject StartedPanel)
    {
        UIManager.instance.TurnOff(StartedPanel);
        GameMaster.instance.LoadGame();
    }
    public void GameOver()
    {
        AudioManager.instance.PlayGameOverSound();
        UIManager.instance.TurnOn(gameOverPanel);

    }
    public void Restart(GameObject panel)
    {
        UIManager.instance.TurnOff(panel);
        Table.instance.InitTable();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void OnApplicationPause(bool pauseStatus)  // Ứng dụng bị tạm dừng hoặc chuyển sang nền
    {
        if (pauseStatus)
        {
           
            GameMaster.instance.SaveGame();
        }
    }

    private void OnApplicationFocus(bool hasFocus)     // Ứng dụng mất tiêu điểm 
    {
        if (!hasFocus)
        {
        
            GameMaster.instance.SaveGame();
        }
    }

    private void OnDestroy() // Ứng dụng bị tiêu diệt hoặc đối tượng bị hủy
    { 
        GameMaster.instance.SaveGame();
    }
    private void OnApplicationQuit() // thoát ứng dụng 
    {
        GameMaster.instance.SaveGame();
    }
}