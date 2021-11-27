using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    private bool scrollEnded = false;
    public TextMeshProUGUI WinText;
    public string WinStringTop;
    
    [TextArea(5,20)]
    public string WinStringMiddle;

    public float TextDelay = 0.1f;
    public float EndDelay = 3f;

    private int maxWinIndex;
    // Start is called before the first frame update
    void Start()
    {
        maxWinIndex = WinStringMiddle.Length;

        StartCoroutine("LostTextDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(scrollEnded){
                MainMenu();
            }
            else{
                StopCoroutine("LostTextDisplay");
                WinText.text = $"{WinStringTop}\r\n{WinStringMiddle}";
                scrollEnded = true;
                StartCoroutine("EndDisplay");
            }
        }
    }

    private IEnumerator WinTextDisplay(){
        WinText.text = WinStringTop;
        yield return new WaitForSeconds(TextDelay);
        for(var i = 0;i<= maxWinIndex;i++){
            WinText.text = $"{WinStringTop}\r\n{WinStringMiddle.Substring(0,i)}";
            yield return new WaitForSeconds(TextDelay);
        }
        StartCoroutine("EndDisplay");
        yield return null;
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    private IEnumerator EndDisplay(){
        yield return new WaitForSeconds(EndDelay);
        MainMenu();
        yield return null;
    }
}
