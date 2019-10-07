using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    private float time = 2f;
    private SpriteRenderer sp;
    void Start()
    {
        StartCoroutine(Delay(time));
        sp = GetComponent<SpriteRenderer>();
    }
    private IEnumerator Delay(float _time) {
        float t = 0;
        while (t <= _time) {
            t += _time;
            yield return new WaitForSeconds(_time);
        }
        StartCoroutine(DimOpacity());
        yield return null;
    }
    private IEnumerator DimOpacity() {
        float t = 0;
        while (t <= 1) {
            t += 0.01f;
            sp.color = Color.Lerp(new Color(0,0,0,1), new Color(0,0,0,0), t);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
        yield return null;
    }
}
