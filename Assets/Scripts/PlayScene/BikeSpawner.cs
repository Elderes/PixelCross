using UnityEngine;

public class BikeSpawner : MonoBehaviour
{
    public BikePrefabsDatabase bikeDatabase;

    void Start()
    {
        // Recupera o índice salvo no PlayerPrefs
        int selectedIndex = PlayerPrefs.GetInt("selectedOption", 0); // 0 é o padrão se não existir

        // Gera a bike na posição deste GameObject
        bikeDatabase.GenerateBike(selectedIndex, this.transform);
    }
}
