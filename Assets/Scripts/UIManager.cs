using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private GameObject[] targets;
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private GameObject winBoard;
     private HashSet<string> completedTargetNames = new HashSet<string>();

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        
        targets = GameObject.FindGameObjectsWithTag("TargetUI");
    }

    public void CompleteTarget(string name)
    {   
        if (completedTargetNames.Contains(name))
            return;

        completedTargetNames.Add(name);

        foreach (var _target in targets)
        {
            if (_target.name == name)
            {
                _target.transform.GetChild(0).gameObject.SetActive(true);
            }  
        }

        ActivateWinBoard();
    }

    private void ActivateWinBoard()
    {
        if (completedTargetNames.Count >= targets.Length)
        {
            winBoard.SetActive(true);
        }
    }

    public void TogglePauseMenu(bool isPaused)
    {
        tutorialMenu.SetActive(isPaused);
    }

}

