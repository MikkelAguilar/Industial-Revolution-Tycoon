using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavePopUp : MonoBehaviour
{

    Transform _canvas;
    [SerializeField] Button _exitButton;
    public void Init(Transform canvas) {
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
