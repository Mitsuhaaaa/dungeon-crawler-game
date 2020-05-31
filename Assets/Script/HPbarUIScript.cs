using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarUIScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text HPText;
    private const string hpMaxText = " / 10";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHpBarUI(int updateValue)
    {
        int value = (int) slider.value;

        value += updateValue;

        if (value > slider.maxValue)
        {
            value = (int)slider.maxValue;
        } else if (value < slider.minValue)
        {
            Debug.Log("DEAD");
        }

        slider.value = value;

        HPText.text = value.ToString() + hpMaxText;
    }
}
