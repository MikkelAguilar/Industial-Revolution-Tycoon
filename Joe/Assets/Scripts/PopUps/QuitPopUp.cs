using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuitPopUp : MonoBehaviour
{
    [SerializeField] Button _yesButton;
    [SerializeField] Button _noButton;
    [SerializeField] TMP_Text _yesText;
    [SerializeField] TMP_Text _noText;
    [SerializeField] TMP_Text _mainText;
    Transform _canvas;

    public void Init(Transform canvas, string yesText, string noText, string mainText) {
        _yesText.text = yesText;
        _noText.text = noText;
        _mainText.text = mainText;
        _canvas = canvas;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        _yesButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
            Application.Quit();
        });

        _noButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });
    }
}
