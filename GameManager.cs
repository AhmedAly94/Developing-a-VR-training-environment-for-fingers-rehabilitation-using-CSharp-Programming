
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
 
    public float restartdelay =10000f; //delay
    public static bool restartTime = false;
    public static float startTimeTwo;
    public GameObject completeLevelUI; //2d icon for showing that level is completed
    public GameObject failedLevelUI;   //2d icon for showing that level is failed

    //call in timer(main) to show icon in the console
    public void CompleteLevel()
    {
        Debug.Log("Level Won");//display a message in the console
        completeLevelUI.SetActive(true);//show icon for Visual feedback to inform the user of success 
    }

    //load next level Scene
    public void NextLevel()
    {
        restartTime = true;
        Invoke("LoadNextLevel", restartdelay);      
    }

    //helper method to load next scene
    void LoadNextLevel()
    {
        startTimeTwo = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    //restartLevel
    void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void failedLevel()
    {
        Debug.Log("Level failed");//display a message in the console
        failedLevelUI.SetActive(true);//show icon for Visual feedback to inform the user of success
    }

    //delay after completing the level
    void delay() 
    {
        Invoke(nameof(CompleteLevel), restartdelay); 
    }

    //when glove touches the box it collides
    private void OnTriggerEnter(Collider other) 
    {
        LoadNextLevel();
    }

   
}
