using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConLevel4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((CheckWinCondition()))
        {
            Debug.Log("Win condition met");
            Invoke("LoadNextScene", 2f);
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("Loading next scene");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool CheckWinCondition()
    {
        if (GameObject.FindGameObjectsWithTag("Coin").Length > 0)
        {
            return false;
        }

        return true;
    }
}
