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
    public GameObject _yellowPrefab;
    public GameObject _greenPrefab;
    public Transform _yelowWaypoint;
    public Transform _GreenWaypoint;
    public DBRotateAroundCentre _InnerCircleRotate;
    public DBRotateAroundCentre _OuterCircleRotate;

    //public List<Material> _EffectMats;

    public Material _FireMat;
    private Camera _CameraMain;
    private float InnerOrbitalSpeed;
    private float OuterOrbitalSpeed;

    private void Start()
    {

        _DefaultMat = _Containers[0]._spriteRender.GetComponent<Renderer>().material;
        _CameraMain = Game2Manager.Instance._CameraMain;
        //StartCoroutine(changeEvery1sec(_Containers[0]._Coin._spriteRender.GetComponent<Renderer>(), _FireMat));
        //_Containers[0]._Coin.GetComponent<Renderer>().material = _FireMat;
        
    }

    public void StopOrbitals()
    {
        InnerOrbitalSpeed = _InnerCircleRotate.orbitSpeed;
        OuterOrbitalSpeed = _OuterCircleRotate.orbitSpeed;

        _InnerCircleRotate.orbitSpeed = 0;
        _OuterCircleRotate.orbitSpeed = 0;
    }

    public void StartOrbitals()
    {
        _InnerCircleRotate.orbitSpeed = InnerOrbitalSpeed;
        _OuterCircleRotate.orbitSpeed = OuterOrbitalSpeed;
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

    #region Camera

    public void CameraFollow(GameObject _target, float _Time)
    {
        StartCoroutine(CameraFollowCo(_target, _Time));
    }

    IEnumerator CameraFollowCo(GameObject _target, float _Time)
    {
        Game2Manager.Instance.UpdateGameState(GameState.Animation);

        float originalZoom = _CameraMain.orthographicSize;

        yield return StartCoroutine(CameraZoomIn(10f, 0.1f));

        Vector3 startPos = _CameraMain.transform.position;

        yield return StartCoroutine(CameraLerpTo(startPos, _target, _Time/2));

        StartCoroutine(CameraLerpTo(_CameraMain.transform.position, startPos, _Time/4));

        yield return StartCoroutine(CameraZoomOut(originalZoom, 0.5f));

        yield return new WaitForSeconds(0.5f);

        Game2Manager.Instance.UpdateGameState(GameState.GamePlay);
    }

    IEnumerator CameraLerpTo(Vector3 startPos, GameObject _target, float _Time)
    {
        float elapsed = 0;

        while (elapsed < _Time)
        {
            Vector3 targetPos = new Vector3(_target.transform.position.x, _target.transform.position.y, startPos.z);
            _CameraMain.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / _Time*2);

            elapsed += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }IEnumerator CameraLerpTo(Vector3 startPos, Vector3 _target, float _Time)
    {
        float elapsed = 0;

        while (elapsed < _Time)
        {
            Vector3 targetPos = new Vector3(_target.x, _target.y, startPos.z);
            _CameraMain.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / _Time*2);

            elapsed += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

    IEnumerator CameraZoomIn(float targetZoom, float duration)
    {
        if (duration <= 0f)
        {
            _CameraMain.orthographicSize = targetZoom;
            yield break;
        }

        float startZoom = _CameraMain.orthographicSize;

        // Zoom in → target should be smaller than current
        if (targetZoom >= startZoom)
        {
            _CameraMain.orthographicSize = targetZoom; // safety fallback
            yield break;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Optional: ease in/out (uncomment one if you want nicer feel)
            // t = Mathf.SmoothStep(0f, 1f, t);                // smooth start & end
            // t = t * t * (3f - 2f * t);                      // smootherstep
             t = Mathf.Pow(t, 2f);                           // ease-in (accelerates)

            _CameraMain.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);

            yield return null;
        }

        // Ensure we exactly hit the target (avoids float precision issues)
        _CameraMain.orthographicSize = targetZoom;
    }

    IEnumerator CameraZoomOut(float targetZoom, float duration)
    {
        if (duration <= 0f)
        {
            _CameraMain.orthographicSize = targetZoom;
            yield break;
        }

        float startZoom = _CameraMain.orthographicSize;

        // Zoom out → target should be larger than current
        if (targetZoom <= startZoom)
        {
            _CameraMain.orthographicSize = targetZoom; // safety fallback
            yield break;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Optional: add easing (uncomment one if desired)
            // t = Mathf.SmoothStep(0f, 1f, t);
            // t = t * t * (3f - 2f * t);
             t = 1f - Mathf.Pow(1f - t, 2f);                 // ease-out (decelerates at end)

            _CameraMain.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);

            yield return null;
        }

        _CameraMain.orthographicSize = targetZoom;
    }

    #endregion

    #region Shop
    public void OpenShopVFX()
    {
        StartCoroutine(OpenShop());
    }

    public void CloseShopVFX()
    {
        StartCoroutine(CloseShop());
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

    IEnumerator CloseShop()
    {
        float t = _2DLight.pointLightOuterRadius;

        Game2Manager.Instance.DisableUI();

        while (_2DLight.pointLightOuterRadius > 0)
        {
            _2DLight.pointLightOuterRadius -= 1;
            yield return new WaitForSeconds(_FadeRate);
        }

        //Game2Manager.Instance.DisableUI();
        //Game2Manager.Instance._ShopScene.SetActive(false);
        Game2Manager.Instance._ContainerManager.ClearActiveContainers();//clears the current active containers

        while (_2DLight.pointLightOuterRadius < t)
        {
            _2DLight.pointLightOuterRadius += 1;
            yield return new WaitForSeconds(_FadeRate);

        }
        
        Game2Manager.Instance.UpdateGameState(GameState.RoundStart);

        yield return null;
    }
    #endregion

    #region CheckConditions

    public void CheckConditions(DBUnit _unit, Transform _SpawnPoint)
    {
        //spawns the appropriate prefab of an object = to the number of needed yellow or green coins


        //the objects will move towards the appropriate container and then be destroyed on contact with the container

        //after it is destroyed we add +1 to the appropriate resource
        StopOrbitals();

        if(_unit._GreenResourceGain > 0)
        {
            StartCoroutine(SpawnResource(true, _unit._GreenResourceGain, _SpawnPoint));
        }
        if(_unit._YellowResourceGain > 0)
        {
            StartCoroutine(SpawnResource(false, _unit._YellowResourceGain, _SpawnPoint));
        }
    }

    IEnumerator SpawnResource(bool _isGreen , int _numbertoSpawn, Transform _SpawnPoint)
    {
        for (int i = 0; i < _numbertoSpawn; i++)
        {
            

            if(_isGreen)
            {
                //spawn prefab, send it to the correct resource destination
                Instantiate(_greenPrefab, this.transform);
            }
            else
            {
                //spawn prefab, send it to the correct resource destination
                Instantiate(_yellowPrefab, this.transform);

            }

            yield return new WaitForSeconds(0.1f);

        }

        yield return null;
    }

    #endregion
}
