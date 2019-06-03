using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public float score;

    public void UpdateUI()
    {
        scoreText.text = "Score " + score;
    }
}
