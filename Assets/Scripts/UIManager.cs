using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private GameObject[] targets;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        
        targets = GameObject.FindGameObjectsWithTag("TargetUI");
    }

    public void CompleteTarget(string name)
    {
        foreach (var _target in targets)
        {
            if (_target.name == name)
            {
                _target.transform.GetChild(0).gameObject.SetActive(true);
            }  
        }
    }

}

