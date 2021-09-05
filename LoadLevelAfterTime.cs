    
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime : MonoBehaviour
{

    [SerializeField]
    private float delayBeforeLoading = 10f;
    [SerializeField]
    private string sceneNameToLoad;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (ScoringSystem.theScore == 15)
        {
            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene(sceneNameToLoad);
            }
        }
    }
}
