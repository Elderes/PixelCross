using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeSpriteManager : MonoBehaviour
{
    public BikeSpriteDatabase bikeDB;

    public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else 
        {
            Load();
        }

        UpdateBike(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= bikeDB.BikeCount)
        {
            selectedOption = 0;
        }

        UpdateBike(selectedOption);
        Save();
    }

    public void PreviousOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = bikeDB.BikeCount - 1;
        }

        UpdateBike(selectedOption);
        Save();
    }

    private void UpdateBike(int selectedOption)
    {
        Bike bike = bikeDB.GetBike(selectedOption);
        artworkSprite.sprite = bike.bikeSprite;
        nameText.text = bike.bikeName;
    }

    public void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
