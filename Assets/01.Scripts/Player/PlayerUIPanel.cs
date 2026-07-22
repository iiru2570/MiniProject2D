using TMPro;
using UnityEngine;

public class PlayerUIPanel : MonoBehaviour
{

    public PlayerStat stat;
    public TextMeshProUGUI hpNum;
    public TextMeshProUGUI expNum;

    void Update()
    {
        hpNum.text = $"{stat.NowHp}";
        expNum.text = $"{stat.Exp}";
    }
}
