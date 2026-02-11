using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX2Manager : MonoBehaviour
{
    public List<DBContainer> _Containers;
    public Material _DefaultMat;
    //public List<Material> _EffectMats;

    public Material _FireMat;

    private void Start()
    {

        _DefaultMat = _Containers[0]._spriteRender.GetComponent<Renderer>().material;
        //StartCoroutine(changeEvery1sec(_Containers[0]._spriteRender.GetComponent<Renderer>(), _FireMat));
        //_Containers[0]._Coin.GetComponent<Renderer>().material = _FireMat;
        
    }


    IEnumerator changeEvery1sec(Renderer matRenderer, Material changeMat)
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(1);
            matRenderer.material = _FireMat;
            yield return new WaitForSeconds(1);
            matRenderer.material = _DefaultMat;

        }
    }

}
