using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonucPanelManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OyunaYenidenBasla()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
