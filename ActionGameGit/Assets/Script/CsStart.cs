using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Stage1");
        if (Input.GetMouseButton(0))
            SceneManager.LoadScene("Stage1");
    }

    public void GoToStage()
    {
        SceneManager.LoadScene("Stage1");
    }
}
