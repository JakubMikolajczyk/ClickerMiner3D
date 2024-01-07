using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    public void loadSceneNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
