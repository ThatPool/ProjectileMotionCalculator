using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartPresentation() {
        Invoke("StartPresentation_Delayed", 3f);
    }
    private void StartPresentation_Delayed() {
        SceneManager.LoadScene("Scene 1");
    }
}
