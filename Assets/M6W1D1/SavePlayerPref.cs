using UnityEngine;
using UnityEngine.UI;

public class SavePlayerPref : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject hide;

    private bool isVisible = true;

    public void ShowOrHide()
    {
        isVisible = !isVisible;
        hide.SetActive(isVisible);    
    }

    public void SaveValue()
    {
        PlayerPrefs.SetFloat("Slider", slider.value);

        PlayerPrefs.SetInt("Toogle",toggle.isOn ? 1 : 0);
    }

    public void LoadValue()
    {
        slider.value = PlayerPrefs.GetFloat("Slider");

        toggle.isOn = PlayerPrefs.GetInt("Toogle")!=0;
    }
}
