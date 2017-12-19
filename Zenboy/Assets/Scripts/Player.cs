using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [Range(0, 1f)]
    [HideInInspector]
    public float concentration = 1f;
    [HideInInspector]
    public float density = 0f;
    public float recoverConcentration;
    public Sprite[] cheers;
    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public bool pause;

    private void Start()
    {
        StartCoroutine("Score");
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().sprite = GetCheerByConcentration();
        SetConcentration();
        UpdateBar();
        UpdateScore();
    }

    public void RestartGame()
    {
        concentration = 1;
        pause = false;
        density = 0f;
    }

    private void SetConcentration()
    {
        if(concentration <= 0)
        {
            GameObject.Find("GameOver").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("GameOver").GetComponent<CanvasGroup>().blocksRaycasts = true;
            pause = true;
            score = 0;
            FindObjectOfType<Caller>().DisableObjects();
        }
        if(density != 0 && concentration > 0)
        {
            concentration -= density * Time.deltaTime;
        } else if(density == 0 && concentration < 1)
        {
            concentration += recoverConcentration * Time.deltaTime;
        } else if (concentration > 1)
        {
            concentration = 1;
        } else if (concentration < 0)
        {
            concentration = 0;
        }
    }

    private void UpdateBar()
    {
        Image bar = GameObject.Find("Bar").GetComponent<Image>();
        bar.fillAmount = concentration;
    }

    private void UpdateScore()
    {
        Text text = GameObject.Find("Score").GetComponent<Text>();
        text.text = score.ToString();
    }

    private Sprite GetCheerByConcentration()
    {
        for(int i = 0; i < cheers.Length; i++)
        {
            float portion = 1f / cheers.Length;
            float fraction = portion * i;
            if (concentration <= (fraction + portion) && concentration >= fraction)
            {
                return cheers[i];
            }
        }
        return GetComponent<SpriteRenderer>().sprite;
    }

    IEnumerator Score()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!pause)
            {
                score += 1;
            }
        }
    }
}