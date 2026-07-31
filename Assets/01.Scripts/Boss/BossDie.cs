using UnityEngine;

public class BossDie : MonoBehaviour
{
    [SerializeField]private EnemyController enemy;
    [SerializeField] private GameObject clearPanel;

    void Start()
    {

    }

    void Update()
    {
        if (Die())
        {
            GameManager.instance.StopTimer();
            clearPanel.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public bool Die()
    {
        if (enemy.stat.NowHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
