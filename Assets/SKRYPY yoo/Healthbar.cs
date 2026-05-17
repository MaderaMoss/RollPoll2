using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int kurz)
    {
        slider.value = kurz;
        slider.maxValue = kurz;
    }
    public void SetHealth(int kurz)
    {
        slider.value = kurz;
    }

}
