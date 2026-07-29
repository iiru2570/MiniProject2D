using UnityEngine;
using UnityEngine.UI;

public class PlayerMpBar : MonoBehaviour
{
    public PlayerStat stat;
    public Image mpFill;


    private void Update()
    {
        SetFill();
    }
    public void SetFill()
    {
        if (stat.MaxMp <= 0)
        {
            return;
        }
        mpFill.fillAmount = (float)stat.NowMp / (float)stat.MaxMp;
    }
}
