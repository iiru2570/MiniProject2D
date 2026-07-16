using UnityEngine;
using System.Collections.Generic;

public class EnemyPoolManager : MonoBehaviour
{

    public static EnemyPoolManager instance;

    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    Dictionary<string, Queue<GameObject>>pools = new Dictionary<string, Queue<GameObject>>();

    private int poolSize;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        poolSize = 5;
        foreach (GameObject enemy in enemyList)
        {
            pools[enemy.name] = new Queue<GameObject>();

            GameObject parentPool = new GameObject($"{enemy.name}_Pool");
            parentPool.transform.SetParent(this.transform);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(enemy, parentPool.transform);
                go.name = enemy.name;
                go.SetActive(false);
                pools[enemy.name].Enqueue(go);
            }
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public GameObject GetEnemy(string name)
    {
        if (!pools.ContainsKey(name))
        {
            return null;
        }
        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = Instantiate(enemyList.Find(enemy => enemy.name == name));
            go.name = name;
            return go;
        }
    }

    public void ReturnEnemy(string name, GameObject go)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            return;
        }
        go.SetActive(false);
        pools[name].Enqueue(go);
    }

}
