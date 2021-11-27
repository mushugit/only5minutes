using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostManager : MonoBehaviour
{
    private bool scrollEnded = false;
    public TextMeshProUGUI LostText;
    public string LostStringTop;
    
    [TextArea(5,20)]
    public string LostStringMiddle;

    public float TextDelay = 0.1f;
    public float EndDelay = 3f;

    private int maxLostIndex;
    // Start is called before the first frame update
    void Start()
    {
        maxLostIndex = LostStringMiddle.Length;

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
                LostText.text = $"{LostStringTop}\r\n{LostStringMiddle}";
                scrollEnded = true;
                StartCoroutine("EndDisplay");
            }
        }
    }

    private IEnumerator LostTextDisplay(){
        LostText.text = LostStringTop;
        yield return new WaitForSeconds(TextDelay);
        for(var i = 0;i<= maxLostIndex;i++){
            LostText.text = $"{LostStringTop}\r\n{LostStringMiddle.Substring(0,i)}";
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
