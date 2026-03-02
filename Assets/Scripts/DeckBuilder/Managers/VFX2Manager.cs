using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VFX2Manager : Singleton<VFX2Manager>
{
    public List<DBContainer> _Containers;
    public Material _DefaultMat;
    public Light2D _2DLight;
    public float _FadeRate;
    //public List<Material> _EffectMats;

    public Material _FireMat;

    private void Start()
    {

        _DefaultMat = _Containers[0]._spriteRender.GetComponent<Renderer>().material;
        //StartCoroutine(changeEvery1sec(_Containers[0]._Coin._spriteRender.GetComponent<Renderer>(), _FireMat));
        //_Containers[0]._Coin.GetComponent<Renderer>().material = _FireMat;
        
    }

    public void OpenShopVFX()
    {
        StartCoroutine(OpenShop());
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

    IEnumerator OpenShop()
    {
        float t = _2DLight.pointLightOuterRadius;

        Game2Manager.Instance.DisableUI();

        while (_2DLight.pointLightOuterRadius > 0)
        {
            _2DLight.pointLightOuterRadius -= 1;
            yield return new WaitForSeconds(_FadeRate);
        }

        Game2Manager.Instance._ShopScene.SetActive(true);
        Game2Manager.Instance._ContainerManager.ClearActiveContainers();//clears the current active containers

        while (_2DLight.pointLightOuterRadius < t)
        {
            _2DLight.pointLightOuterRadius += 1;
            yield return new WaitForSeconds(_FadeRate);

        }



        yield return null;
    }
}
