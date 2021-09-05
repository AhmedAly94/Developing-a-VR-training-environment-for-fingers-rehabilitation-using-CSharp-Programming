using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartLevelThree: MonoBehaviour
{
    public Scene activescene;

    //when glove touches the object it restart the level
    private void OnTriggerEnter(Collider other)
    {
        restartLevel();
    }
    void restartLevel()
    {
        SceneManager.LoadScene("level3");
    }

}
