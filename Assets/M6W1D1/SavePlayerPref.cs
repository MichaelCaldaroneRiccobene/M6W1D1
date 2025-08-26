using UnityEngine;
using UnityEngine.UI;

public class SavePlayerPref : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject hide;

    private bool isVisible = true;
    private SaveData saveData = new SaveData();


    public void ShowOrHide()
    {
        isVisible = !isVisible;
        hide.SetActive(isVisible);    
    }

    public void SaveValue()
    {
        PlayerPrefs.SetFloat("Slider", slider.value);
        PlayerPrefs.SetInt("Toogle",toggle.isOn ? 1 : 0);

        saveData.posistion = new SavableVector3(player.position);
        saveData.rotation = new SavableQuaternion(player.rotation);

        SaveSystem.Save(saveData);
    }

    public void LoadValue()
    {
        slider.value = PlayerPrefs.GetFloat("Slider");
        toggle.isOn = PlayerPrefs.GetInt("Toogle")!=0;

        SaveSystem.Load();

        player.position = saveData.posistion.ToVector3();
        player.rotation = saveData.rotation.ToQuaternion();
    }
}
