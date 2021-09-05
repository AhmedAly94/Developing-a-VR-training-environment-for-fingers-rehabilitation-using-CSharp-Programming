using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    public Scene activescene;

    //when glove touches the cube it restarts the level
    private void OnTriggerEnter(Collider other)
    {
        restartLevel();
    }
    void restartLevel()
    {
        SceneManager.LoadScene("level1");
    }
    
}
