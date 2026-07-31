using NUnit.Framework.Internal;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossTime : MonoBehaviour
{

    public static BossTime instance;

    [SerializeField] private PlayerController player;
    public TextMeshProUGUI bossTime;
    private float currentTime;
    private bool isTimer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isTimer = true;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            if(currentTime >= 1)
            {
                currentTime -= Time.deltaTime;
                bossTime.text = GetTime();
            }
            else
            {
                player.TakeDamage(player.stat.MaxHp);
                StopTimer();
            }

        }
    }

    public void StartTimer()
    {
        currentTime = 300f;
        isTimer = true;
    }
    public void StopTimer()
    {
        isTimer = false;
    }
    public string GetTime()
    {
        int minute = Mathf.FloorToInt(currentTime / 60f);
        int second = Mathf.FloorToInt(currentTime % 60f);

        return $"{minute:00} : {second:00}";
    }
}
