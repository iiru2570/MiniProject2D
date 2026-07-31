using TMPro;
using UnityEngine;

public class PlayerUIPanel : MonoBehaviour
{

    public PlayerStat stat;
    public TextMeshProUGUI hpNum;
    public TextMeshProUGUI mpNum;
    public TextMeshProUGUI expNum;
    public TextMeshProUGUI attack;

    void Update()
    {
        attack.text = $"Att [ {stat.Damage} ]";
        hpNum.text = $"{stat.NowHp} / {stat.MaxHp}";
        mpNum.text = $"{stat.NowMp} / {stat.MaxMp}";
        expNum.text = $"EXP [ {stat.Exp} ]";
    }
}
