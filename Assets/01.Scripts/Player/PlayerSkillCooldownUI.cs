using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCooldownUI : MonoBehaviour
{

    public PlayerController player;
    public Image skill1;
    public Image skill2;
    public Image skill3;

    // Update is called once per frame
    void Update()
    {
        skill1.fillAmount = player.GetSkill1CooldownRatio();
        skill2.fillAmount = player.GetSkill2CooldownRatio();
        skill3.fillAmount = player.GetSkill3CooldownRatio();
    }
}
