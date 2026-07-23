using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class StageManager : MonoBehaviour
{

    public static StageManager instance;

    public Tilemap tilemap;

    [SerializeField] private Tilemap wall;
    [SerializeField] private Tilemap portal;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        GameObject enemy = EnemyPoolManager.instance.GetEnemy("NormalDevil");

        if(enemy != null)
        {
            enemy.transform.position = spawnPos;
        }
        else
        {
            Debug.Log("slime없음");
        }
    }

    public bool IsUsedPos(Vector3 pos)
    {
        Vector3Int temp = tilemap.WorldToCell(pos);

        IsPortal(temp);

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
    public void IsPortal(Vector3Int pos)
    {
        if (portal.HasTile(pos))
        {
            SceneChanger.instance.LoadScene(0);
        }
        else
        {
            return;
        }
    }

}
