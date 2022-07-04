using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider livesSlider;
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject riotImage;
    [SerializeField] GameObject shieldImage;
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
        StartCoroutine(CloseDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        livesSlider.value = health.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

    IEnumerator CloseDialogue()
    {
        yield return new WaitForSeconds(2f);
        dialogue.GetComponent<Animator>().SetBool("Fade_out", true);// Get Animator in dialogue game object
        yield return new WaitForSeconds(1f);
        dialogue.SetActive(false);
    }

    public void ShowRiot()
    {
        riotImage.SetActive(true);
    }

    public void RemoveRiot()
    {
        riotImage.SetActive(false);
    }
}
