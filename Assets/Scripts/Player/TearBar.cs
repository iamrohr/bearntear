using UnityEngine;
using UnityEngine.UI;

public class TearBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxTearLevel(int tear)
    {
        slider.maxValue = tear;
        slider.value = tear;
    }

    public void SetTearLevel(int tear)
    {
        slider.value = tear;
    }

}