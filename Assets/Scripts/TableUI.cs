using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TableUI : MonoBehaviour
{
    public TextMeshProUGUI FirstPlace;
    public TextMeshProUGUI SecondPlace;
    public TextMeshProUGUI ThirdPlace;
    
    void Start()
    {
        
    }

    public void ResetScore()
    {
        Debug.Log("Reset button pressed");
    }

    public void BackToMenu()
    {
        Debug.Log("Back button pressed");
    }


    void Update()
    {
        
    }
}
