using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;
    public TextMesh text;
    
    //get the Score updated in Timer file and display it in the text in unity
    void Update()
    {
        scoreText.GetComponent<Text>().text = "Score: " + theScore;
        text.text = "Score: " + theScore;
    }

} 
