using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TableUI : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI PlaceInListTemplate;
    public GameObject Buttons;

    
    void Start()
    {
        //DataHandler.Instance.ReadFromSave();
        int len = DataHandler.Instance.ScoreList.Count;
        int i;
        for (i = 0; i < len; i++)
        {
            TextMeshProUGUI PlaceInListClone = Instantiate(PlaceInListTemplate);

            PlaceInListClone.transform.SetParent(canvas.transform, false);
            //PlaceInListClone.transform.parent = canvas.transform;
            Vector3 TemplatePosition = PlaceInListTemplate.GetComponent<RectTransform>().position;
            TemplatePosition.y = 80 - (80 * i);
            PlaceInListClone.GetComponent<RectTransform>().position = TemplatePosition;
            PlaceInListClone.gameObject.SetActive(true);
        }
    }

    public void ResetScore()
    {
        DataHandler.Instance.ResetScore();
        SceneManager.LoadScene(2);
        Debug.Log("Reset button pressed");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Back button pressed");
    }
}
