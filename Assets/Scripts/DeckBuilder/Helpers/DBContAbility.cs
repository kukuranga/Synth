using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBContAbility : MonoBehaviour
{
    public SpriteRenderer _SpriteRenderer;
    private Material _Mat;

    void Start()
    {
        //CreateNewAllInOneMaterial();
        _Mat = _SpriteRenderer.material;
        _Mat.EnableKeyword("FADE_ON");
        _Mat.SetFloat("FadeAmount", 0);
        _Mat.SetFloat("FadeBurnTransition", 0.3f);
    }

    public void FadeBurnOut()
    {
        StartCoroutine(FadeBurnOutCo());
    }

    IEnumerator FadeBurnOutCo()
    {

        float i = 0;

        while(i < 1.1f)
        {            
            _Mat.SetFloat("_FadeAmount", i);
            i += 0.01f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return null;
    }
}
