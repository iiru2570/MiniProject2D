using UnityEngine;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour
{

    [SerializeField] private int sceneNum;
    private Button btn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(Move);
        }
    }

    public void Move()
    {
        SceneChanger.instance.LoadScene(sceneNum);
    }

}
