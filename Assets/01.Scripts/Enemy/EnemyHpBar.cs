using System.Collections;
using UnityEngine;
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
        hpFill.fillAmount = (float)stat.NowHp / (float)stat.MaxHp;
    }
}
