using UnityEngine;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour
{
    private Button btn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(MoveNext);
        }
    }

    //tutorial 버튼
    public void MoveNext()
    {
        SoundManager.instance.PlaySFX(SFXType.Open);
        SceneChanger.instance.LoadScene(1);
        //기본 로비 씬이 2
        SceneChanger.instance.nowStage = 2;
        GameManager.instance.StartTimer();
    }
    //public void MovePrevios()
    //{
    //    SceneChanger.instance.LoadScene(SceneChanger.instance.nowStage-1);
    //    SceneChanger.instance.nowStage -= 1;
    //}

}
