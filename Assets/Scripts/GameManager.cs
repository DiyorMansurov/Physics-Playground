using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private InputPlayerActions _input;
    public static GameManager Instance;

    private void Awake() {
        Instance = this;

        _input = new InputPlayerActions();
        _input.Player.Enable();
    }
    private void Start() {
        Time.timeScale = 0f;
        UIManager.Instance.TogglePauseMenu(true);
        AudioListener.pause = true;
    }

    void OnEnable()
    {
        _input.Player.Restart.performed += Restart_performed;
        _input.Player.Tutorial.performed += GamePause;
    }

    void OnDisable()
    {
        _input.Player.Restart.performed -= Restart_performed;
        _input.Player.Tutorial.performed -= GamePause;
    }
    
    private void Restart_performed(InputAction.CallbackContext context) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
    private void GamePause(InputAction.CallbackContext context) {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            UIManager.Instance.TogglePauseMenu(true);
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.TogglePauseMenu(false);
            AudioListener.pause = false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
