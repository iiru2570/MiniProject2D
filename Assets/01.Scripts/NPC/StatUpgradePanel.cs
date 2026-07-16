using UnityEngine;
using UnityEngine.UI;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField]private PlayerController player;
    public Button hpUpgradeBtn;
    public Button mpUpgradeBtn;
    public Button exitBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpUpgradeBtn.onClick.AddListener(player.UpgradeHp);
        exitBtn.onClick.AddListener(ExitBtnClick);
    }

    public void ExitBtnClick()
    {
        gameObject.SetActive(false);
    }

}
