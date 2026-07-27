using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Image hpFill;
    private EnemyStat stat;

    private void Awake()
    {
        stat = GetComponentInParent<EnemyStat>();
    }

    void Update()
    {
        if(stat.MaxHp <= 0)
        {
            return;
        }
        hpFill.fillAmount = (float)stat.NowHp / (float)stat.MaxHp;
    }
}
