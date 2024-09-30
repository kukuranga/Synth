using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SynthManager : Singleton<SynthManager>
    {
        
        //-----Upgrade Values---------------
        public int _TotalUpgradePoints;
        public int _UsedUpgradePoints;
        public int _AvailableUpgradePoints;

        public int _Ci1P = 1;//circle 1 points
        public int _Ci2P = 1;//circle 2 points
        public int _Cu1P = 1;//curved 1 points
        public int _Cu2P = 1;//curves 2 points
        public int _Cu3P = 1;//curves 3 points

    //----------------------------------
        public bool _Dance = true;
        public bool _Pulse = true;

        public float _ChaosMaxRange = 0.2f; //TODO: Remove this unused

        //Add ref for each element in the synth
        public DreamStarAnimator _Animator;
        public DreamStarGenerator_Mixer _Mixer;

        public Circle _Circle1;
        public Circle _Circle2;

        public Curved _Curved1;
        public Curved _Curved2;
        public Curved _Curved3;

        public float _PreviousChangeA = 100;

    //method to add a random amount to a field 



    //maybe add a ref to each material preset 

    public void RandomUpgrade()
    {
        int r = Random.Range(1, 5);
        Debug.Log("synth upgrade: " + r);
        switch (r)
        {
            case 1:
                WholeCircle(_Circle1);
                break;
            case 2:
                WholeCurve(_Curved1);
                break;
            case 3:
                WholeCurve(_Curved2);
                break;
            case 4:
                WholeCircle(_Circle2);
                break;
            case 5:
                WholeCurve(_Curved3);
                break;
        }
    }

    //Dance
    private float _originalYPosition;
    public float _amplitude = 20f; // Adjust this value to control the amplitude of the dance
    public float _frequency = 7f; // Adjust this value to control the frequency of the dance
    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _originalYPosition = this.transform.position.y;
    }

    private void Update()
    {
        Dance();
        CheckUpgradeValues();
    }

    private void CheckUpgradeValues()
    {
        _AvailableUpgradePoints = _TotalUpgradePoints - _UsedUpgradePoints;
    }

    public void AddTotalUpgradePoint()
    {
        _TotalUpgradePoints++;
    }

    public void UpdateSynthValues()
    {
        SetCircle(_Circle1, _Ci1P);
        SetCircle(_Circle2, _Ci2P);
        SetCurved(_Curved1, _Cu1P);
        SetCurved(_Curved2, _Cu2P);
        SetCurved(_Curved3, _Cu3P);

        AddBonusesToGame(); //TODO: move this to when the synth upgrade tab is closed
    }

    private void Dance()
    {
        if (_Dance)
        {
            float offsetY = _amplitude * Mathf.Sin(Time.time * _frequency);
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, _originalYPosition + offsetY);
        }
        if (_Pulse)
        {
            int _add = 2;
            float offsetP = Mathf.Sin(Time.time * (_frequency * 0.5f));
            _Circle1.Impact = (offsetP / 2) + _add;
            _Circle2.Impact = (offsetP / 2) + _add;
            _Curved1.Impact = (offsetP / 2) + _add;
            _Curved2.Impact = (offsetP / 2) + _add;    
            _Curved3.Impact = (offsetP / 2) + _add;
        }
    }

    public void GrowSynth()
    {
        //_Mixer.a += 10;
        //_PreviousChangeA = _Mixer.a;
        if (_Mixer.Radius < 10) //&& GameManager.Instance._Level > 5)
        {
            _Mixer.Radius += 0.15f;
        }
    }

    public void ResetSynth()
    {
        //_Mixer.a = 100;
        //_PreviousChangeA = 100;
        _Mixer.Radius = 0.5f;
    }

    public void AddBonusesToGame()
    {
        //Ci1 = Moves to start
        //ci2 = Moves on round won
        //cu1 = Golt itemChance
        //Cu2 = treasure item chance
        //Cu3 = Luck

        GameManager.Instance.SetSynthBonuses(_Ci2P,_Ci2P,_Cu1P,_Cu2P, _Cu3P);
    }

    //---------------------------------------------------------------------------Functions To Call---------------------------------------------------------------
    public void WholeCircle(Circle _c)
        {
            //bool Rand = GetRandomBoolean();
            //if(Rand)
             AddToCircle(_c, 0,  1);
            //else            
            //    AddToCircle(_Circle1, 0, -1);            
        } 

        public void WholeCurve(Curved _c)
        {
            //bool Rand = GetRandomBoolean();
            //if (Rand)
                AddToCurve(_c, 0, 1);
            //else
            //    AddToCurve(_Curved1, 0, -0.5f);
        }
        //-----------------------------------------------------------------------------------------------------------------------


        public void AddToCircle(Circle c, float impact, float angle)
        {
            c.Impact += impact;
            c.Angle_MP += angle;
        }

        public void AddToCurve(Curved c, float impact, float angle)
        {
            c.Impact += impact;
            c.Angle_MP += angle;
        }

        public void SetCircle(Circle c, float angle)
        {
            c.Angle_MP = angle;
        }

        public void SetCurved(Curved c, float angle)
        {
            c.Angle_MP = angle;
        }

        bool GetRandomBoolean()
        {
            int randomInt = Random.Range(0, 2);
            return randomInt == 1;
        }

        float GetRandomFloat()
        {
            return Random.Range(-_ChaosMaxRange, _ChaosMaxRange);
        }

    #region GetValues

    public float GetCircle1()
    {
        return _Circle1.Angle_MP;
    }

    public float GetCircle2()
    {
        return _Circle2.Angle_MP;
    }

    public float GetCurved1()
    {
        return _Curved1.Angle_MP;
    }

    public float GetCurved2()
    {
        return _Curved2.Angle_MP;
    }

    public float GetCurved3()
    {
        return _Curved3.Angle_MP;
    }

    public int GetUpgradePointsAvailable()
    {
        return _AvailableUpgradePoints;
    }

    public int GetTotalUpgradePoints()
    {
        return _TotalUpgradePoints;
    }

    public int GetUsedUpgradePoints()
    {
        return _UsedUpgradePoints;
    }

    #endregion
}
