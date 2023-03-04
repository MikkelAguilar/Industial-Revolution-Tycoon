using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessBar : MonoBehaviour
{
    private HappinessSystem happinessSystem;
    public void setUp(HappinessSystem hs) {
        this.happinessSystem = hs;

        happinessSystem.OnHappinessChanged += happinessSystem_OnHealthChanged;
    }
    private void happinessSystem_OnHealthChanged(object sender, System.EventArgs e) {
        transform.Find("Bar").localScale = new Vector3(happinessSystem.getHappinessPercent(), 1);
    }
    /*void Update()
    {
        transform.Find("Bar").localScale = new Vector3(happinessSystem.getHappinessPercent(), 1);
    }*/
}
