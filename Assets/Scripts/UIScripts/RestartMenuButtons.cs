using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para usar SceneManager

public class RestartMenuButtons : MonoBehaviour
{
    // Função chamada ao pressionar o botão
    public void ReiniciarCena()
    {
        // Obtém o nome da cena atual
        string nomeCenaAtual = SceneManager.GetActiveScene().name;

        // Recarrega a cena atual
        SceneManager.LoadScene(nomeCenaAtual);
    }

    public void ChangeScene(string sceneName)
    {
        // Recarrega a cena atual
        SceneManager.LoadScene(sceneName);
    }
}
