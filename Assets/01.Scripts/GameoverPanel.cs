using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{

    public Button robby;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        robby.onClick.AddListener(GameManager.instance.GameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
