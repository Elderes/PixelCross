using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrackSpriteManager : MonoBehaviour
{
    public TrackSpriteDatabase trackDB;

    public TextMeshProUGUI nameText;
    public Image artworkSprite;

    private int selectedTrack = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedTrack"))
        {
            selectedTrack = 0;
        }
        else 
        {
            Load();
        }

        UpdateTrack(selectedTrack);
    }

    public void NextOption()
    {
        selectedTrack++;

        if (selectedTrack >= trackDB.TrackCount)
        {
            selectedTrack = 0;
        }

        UpdateTrack(selectedTrack);
        Save();
    }

    public void PreviousOption()
    {
        selectedTrack--;

        if (selectedTrack < 0)
        {
            selectedTrack = trackDB.TrackCount - 1;
        }

        UpdateTrack(selectedTrack);
        Save();
    }

    private void UpdateTrack(int selectedOption)
    {
        Track track = trackDB.GetBike(selectedOption);
        artworkSprite.sprite = track.trackSprite;
        nameText.text = track.trackName;
    }

    public void Load()
    {
        selectedTrack = PlayerPrefs.GetInt("selectedTrack");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("selectedTrack", selectedTrack);
    }

    public void Play()
    {
        SceneManager.LoadScene(nameText.text);
    }
}
