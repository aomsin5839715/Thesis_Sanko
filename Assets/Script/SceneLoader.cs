using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        //LoadGameScene();
    }

    public void LoadGameScene()
    {
            SceneManager.LoadScene("PlayScene");
            //Debug.Log("Loading PlayScene with slot index: " + slotIndex);
    }
}
