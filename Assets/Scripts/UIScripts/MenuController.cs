using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Função para ser chamada quando o botão Jogar for clicado
    public void ChangeScene(string sceneToLoad)
    {
        // Carrega a Scene do jogo (substitua "JogoScene" pelo nome da Scene do seu jogo)
        SceneManager.LoadScene(sceneToLoad);
    }

    // Função para sair do jogo
    public void ExitButton()
    {
        // Fecha o jogo (só funciona em builds, não no editor)
        Application.Quit();
    }
}
