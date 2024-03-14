using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string menu_scene, game_scene;

    public void OnPlay()
    {
        SceneManager.LoadScene(game_scene);
        Cursor.visible = false;
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(menu_scene);
        Cursor.visible = true;
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
