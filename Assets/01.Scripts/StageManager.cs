using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using DG.Tweening;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;

    public Tilemap tilemap;

    [SerializeField] private PlayerStat stat;

    private bool usePortal;

    [SerializeField] TextMeshProUGUI stageInfo;

    [SerializeField] private Tilemap wall;
    [SerializeField] private Tilemap portalObj;
    [SerializeField] private Tilemap portalNext;
    [SerializeField] private Tilemap portalPrevios;
    [SerializeField] private Tilemap portalLobby;
    [SerializeField] private List<Vector3Int> usedPos = new List<Vector3Int>();
    [SerializeField] private int summonNum;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private List<Rect> spawnArea;
    [SerializeField] private Color color = new Color(1, 0, 0, 0.5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //summonNum = 5;
        usedPos.Clear();
        for(int i=0; i<summonNum; i++)
        {
            SummonEnemy();
        }
        usePortal = false;
        StartCoroutine(StageText(SceneManager.GetActiveScene().name)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StageText(string str)
    {
        stageInfo.text = str;
        stageInfo.gameObject.SetActive(true);
        Color color = stageInfo.color;
        color.a = 1f;
        stageInfo.color = color;

        stageInfo.DOFade(0f, 6f);
        yield return new WaitForSeconds(6f);
        stageInfo.gameObject.SetActive(false);
    }
    private void SummonEnemy()
    {
        Rect spawnRect = spawnArea[Random.Range(0, spawnArea.Count)];

        Vector2 randPos = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));

        //나온 좌표를 타일맵 좌표로 변경.
        Vector3Int randPoscell = tilemap.WorldToCell(randPos);

        if (usedPos.Contains(randPoscell))
        {
            SummonEnemy();
            return;
        }
        //스폰된 자리 기억
        usedPos.Add(randPoscell);


        Vector3 spawnPos = tilemap.GetCellCenterWorld(randPoscell);
        spawnPos.y += 0.3f;

        //몬스터 리스트에서 랜덤
        List<GameObject> list = EnemyPoolManager.instance.GetEnemyList();
        int rand = Random.Range(0, list.Count);

        GameObject enemy = EnemyPoolManager.instance.GetEnemy(list[rand].name);

        if (enemy != null)
        {
            enemy.transform.position = spawnPos;
        }
    }

    public void CountEnemy()
    {
        summonNum--;
        if(summonNum == 0)
        {
            portalObj.gameObject.SetActive(false);
        }
    }


    public bool IsUsedPos(Vector3 pos)
    {
        Vector3Int temp = tilemap.WorldToCell(pos);

        IsPortal(temp);

        if (IsPortalObj(temp))
        {
            if (summonNum == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        if (IsWall(temp))
        {
            return true;
        }

        if (usedPos.Contains(temp))
        {
            return true;
        }
        else
        {
            usedPos.Add(temp);
            return false;
        }
    }
    public void UsePos(Vector3 pos)
    {
        Vector3Int temp = tilemap.WorldToCell(pos);
        if (!usedPos.Contains(temp))
        {
            usedPos.Add(temp);
        }
    }
    public void ReturnPos(Vector3 pos)
    {
        Vector3Int temp = tilemap.WorldToCell(pos);
        usedPos.Remove(temp);
    }


    private void OnDrawGizmosSelected()
    {
        if(spawnArea == null)
        {
            return;
        }
        Gizmos.color = color;
        foreach(var area in spawnArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }
    public bool IsWall(Vector3Int pos)
    {
        return wall.HasTile(pos);
    }
    public bool IsPortalObj(Vector3Int pos)
    {
        return portalObj.HasTile(pos);
    }
    public void IsPortal(Vector3Int pos)
    {
        if (portalPrevios.HasTile(pos))
        {
            if (SceneChanger.instance.nowStage == 2)
            {
                Debug.Log("뒤로 갈 스테이지가 없음");
                SoundManager.instance.PlaySFX(SFXType.Denided);
            }
            else
            {
                if(usePortal == false)
                {
                    usePortal = true;
                    stat.NowHp = stat.MaxHp;
                    stat.NowMp = stat.MaxMp;
                    GameManager.instance.SaveStat(stat);
                    SoundManager.instance.PlaySFX(SFXType.Portal);
                    SceneChanger.instance.LoadScene(SceneChanger.instance.nowStage);
                } 
            }   
        }

        if (portalNext.HasTile(pos))
        {
            if(usePortal == false)
            {
                usePortal = true;
                stat.NowHp = stat.MaxHp;
                stat.NowMp = stat.MaxMp;
                GameManager.instance.SaveStat(stat);
                SoundManager.instance.PlaySFX(SFXType.Portal);
                SceneChanger.instance.LoadScene(SceneChanger.instance.nowStage + 1);
                SceneChanger.instance.nowStage += 1;
            }     
        }
        if (portalLobby.HasTile(pos))
        {
            if(usePortal == false)
            {
                usePortal = true;
                stat.NowHp = stat.MaxHp;
                stat.NowMp = stat.MaxMp;
                GameManager.instance.SaveStat(stat);
                SoundManager.instance.PlaySFX(SFXType.Portal);
                SceneChanger.instance.LoadScene(2);
            }
        }
        else
        {
            return;
        }
    }
}
