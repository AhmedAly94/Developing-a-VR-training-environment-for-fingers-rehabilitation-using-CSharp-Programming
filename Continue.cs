using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
   //go to the next level
    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //when glove touches the door it load next level
    private void OnTriggerEnter(Collider other)
    {
        LoadNextLevel();
    }
}
