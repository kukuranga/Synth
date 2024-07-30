using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynthOptions : MonoBehaviour
{

    public TextMeshProUGUI _Text;

    public Slider _slider;
    [Range(1, 11)]int _PreviousSVal = 1;
    [Range(1,11)]int _SVal = 1;

    [Range(0,4)]public int _Index;

    private void Start()
    {
        if (_slider == null)
        {
            Debug.LogError("Slider component not found.");
            return;
        }

        switch (_Index)
        {
            case 0:
                _SVal = SynthManager.Instance._Ci1P;
                break;
            case 1:
                _SVal = SynthManager.Instance._Ci2P;
                break;
            case 2:
                _SVal = SynthManager.Instance._Cu1P;
                break;
            case 3:
                _SVal = SynthManager.Instance._Cu2P;
                break;
            case 4:
                _SVal = SynthManager.Instance._Cu3P;
                break;
            default:
                Debug.LogError("Invalid _Index: " + _Index);
                return;
        }

        _PreviousSVal = _SVal;
        _slider.value = _SVal;
        _Text.text = (_SVal - 1).ToString();

    }

    public void OnClickSliderPicker()
    {
        _SVal = (int)_slider.value;

        //Limits the value of the slider if the player does not have enough points
        if (SynthManager.Instance._AvailableUpgradePoints < (_SVal - _PreviousSVal) && SynthManager.Instance._AvailableUpgradePoints != 0)
        {
            _SVal = SynthManager.Instance._AvailableUpgradePoints + _PreviousSVal;
            _slider.value = _SVal;
        }

        if (SynthManager.Instance._AvailableUpgradePoints > 0 || _SVal < _PreviousSVal)
        {

            switch (_Index)
            {
                case 0:
                    SynthManager.Instance._Ci1P = _SVal;
                    break;
                case 1:
                    SynthManager.Instance._Ci2P = _SVal;
                    break;
                case 2:
                    SynthManager.Instance._Cu1P = _SVal;
                    break;
                case 3:
                    SynthManager.Instance._Cu2P = _SVal;
                    break;
                case 4:
                    SynthManager.Instance._Cu3P = _SVal;
                    break;
            }

            SynthManager.Instance._UsedUpgradePoints += (_SVal - _PreviousSVal);
            _PreviousSVal = _SVal;
            _Text.text = (_SVal - 1).ToString();
            SynthManager.Instance.UpdateSynthValues();
        }
        else
        {
            _slider.value = _PreviousSVal;
        }
    }
}
