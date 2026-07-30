using System.Collections;
using UnityEngine;

public class Telegraph : MonoBehaviour
{
    private SpriteRenderer sp;
    
    private float duration;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        //기본값
        duration = 2f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public IEnumerator Fade()
    {
        Color color = sp.color;
        color.a = 0f;
        sp.color = color;

        float timer = 0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / duration);
            sp.color = color;

            yield return null;
        }

        color.a = 1f;
        sp.color = color;
    }

    public void Trigger(Vector3 pos, int damage)
    {
        StartCoroutine(Detect(pos, damage));
    }

    private IEnumerator Detect(Vector3 pos, int damage)
    {
        yield return StartCoroutine(Fade());

        Collider2D hit = Physics2D.OverlapPoint(pos);

        if (hit != null)
        {
            PlayerController player = hit.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        else
        {
            Debug.Log("없음");
        }
        SoundManager.instance.PlaySFX(SFXType.Bomb);
        Destroy(gameObject);
    }

}
