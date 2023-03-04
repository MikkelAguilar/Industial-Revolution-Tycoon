using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpPopUp : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    [SerializeField] TMP_Text _exitText;
    [SerializeField] TMP_Text _mainText1;
    [SerializeField] TMP_Text _mainText2;
    [SerializeField] TMP_Text _mainText3;
    [SerializeField] TMP_Text _mainText4;
    [SerializeField] TMP_Text _mainText5;
    [SerializeField] TMP_Text _mainText6;
    [SerializeField] TMP_Text _mainText7;
    Transform _canvas;

    public void Init(Transform canvas, string exitText) {
        _exitText.text = exitText;
        _mainText1.text = "Welcome to our Industrial Revolution Tycoon, here are the mechanics:";
        _mainText2.text = "- Assign employees to machines to earn money!";
        _mainText3.text = "- Upgrade your machines";
        _mainText4.text = "- Add modifications to your machines";
        _mainText5.text = "- Hire new employees";
        _mainText6.text = "Make sure to to keep your employees' happiness level up by assigning them to the fun slide!";
        _mainText7.text = "Keep making money, then after that, make even more money!";
        
        _canvas = canvas;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        _exitButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });
    }
}
