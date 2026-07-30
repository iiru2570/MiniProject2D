using System.Collections;
using UnityEngine;

public class PlayerMpHeal : MonoBehaviour
{
    private PlayerStat stat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stat = GetComponent<PlayerStat>();
        StartCoroutine(MpHealing());
    }

    private IEnumerator MpHealing()
    {
        while (true)
        {
            int amountMp = Mathf.RoundToInt(stat.MaxMp * 0.1f);
            if (stat.NowMp + amountMp > stat.MaxMp)
            {
                stat.NowMp = stat.MaxMp;
            }
            else
            {
                stat.NowMp += amountMp;
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
