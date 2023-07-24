using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConLevel3 : MonoBehaviour
{
    // Start is called before the first frame update

    private float passedTime = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;

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
        if (passedTime < 10f)
        {
            return false;
        }

        return true;
    }
}
