using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{

    public PlayerStat stat;
    public Image hpFill;


    private void Update()
    {
        SetFill();
    }
    public void SetFill()
    {
        hpFill.fillAmount = (float)stat.NowHp / (float)stat.MaxHp;
    }
}
