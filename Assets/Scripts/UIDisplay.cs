using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider livesSlider;
    Health health;
    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        health = FindObjectOfType<Player>().GetComponent<Health>();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        livesSlider.maxValue = health.GetHealth();
        livesSlider.value = health.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        livesSlider.value = health.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
