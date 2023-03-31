using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField textObject;

    // Start is called before the first frame update
    void Start()
    {
        AppManager.Instance.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        AppManager.Instance.SaveData();
        SceneManager.LoadScene(1);
    }

    public void UpdateName()
    {
        AppManager.Instance.playerName = textObject.text;
        Debug.Log(AppManager.Instance.playerName);
    }

    public void ResetScores()
    {
        AppManager.Instance.highScore = 0;
        AppManager.Instance.highScoreName = "";
        AppManager.Instance.SaveData();
    }
}
