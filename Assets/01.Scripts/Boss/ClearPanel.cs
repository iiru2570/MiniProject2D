using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{

    public Button lobbyBtn;
    public TextMeshProUGUI clearTimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lobbyBtn.onClick.AddListener(toLobby);
    }
    private void OnEnable()
    {
        clearTimeText.text = GameManager.instance.GetTime();
    }
    public void toLobby()
    {
        SceneChanger.instance.LoadScene(0);
        GameManager.instance.gametime.text = "";
        gameObject.SetActive(false);
    }
}
