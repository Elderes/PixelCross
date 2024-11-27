using UnityEngine;

[CreateAssetMenu]
public class BikePrefabsDatabase : ScriptableObject
{
    public GameObject[] bikePrefabsDB;

    public int BikeCount
    {
        get
        {
            return bikePrefabsDB.Length;
        }
    }

    // Método que gera a moto a partir do índice fornecido
    public void GenerateBike(int index, Transform spawnTransform)
    {
        if (index >= 0 && index < bikePrefabsDB.Length)
        {
            GameObject bikePrefab = bikePrefabsDB[index];
            // Instancia o prefab
            GameObject instantiatedBike = Instantiate(bikePrefab, spawnTransform.position, spawnTransform.rotation);

            // Certifica-se de que o objeto esteja ativo
            instantiatedBike.SetActive(true);
        }
        else
        {
            Debug.LogError("Índice inválido para bike.");
        }
    }
}
