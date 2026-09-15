using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class choose : MonoBehaviour
{
    public GameObject setting;
    public void ClickToOpenSetting(){
        setting.SetActive(true);
    }
    public void ClickToCloseSetting(){
        setting.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
public void OpenScene(){
    SceneManager.LoadScene("Stroge 1");
}
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
