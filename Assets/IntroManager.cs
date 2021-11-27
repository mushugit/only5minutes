using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [TextArea(5,20)]
    public string IntroString = "Il me reste encore au moins 4 heures pour faire mon devoir ... Pfff 800 mots et je n'ai toujours pas d'insipration. Heureusement que Julien a une astuce, le fameux logiciel d'aide a la redaction ! Allez du coup je m'y mets bientot, je vais faire un tour en attendant ...";

    public string EndIntroString = "MAIS il ne me reste plus que 5 minutes !!!";
    public float TextDelay = 0.1f;
    public TextMeshProUGUI IntroText;
    private int maxIntroIndex;
    private bool scrollEnded = false;

    public GameObject MainDecoCanvas;
    public GameObject ImageCanvas;

    public float ImageDelayFirst = 2f;

    public TextMeshProUGUI InstructionDateText;
    public TextMeshProUGUI InstructionWordCountText;
    public string InstructionStart = "Essai pour le ";
    public string WordCountEnd = "mots";
    public int WinWordCount = 800;

    public float EndDelay = 2f;

    public Color EndTextColor;

    // Start is called before the first frame update
    void Start()
    {
        maxIntroIndex = IntroString.Length;

        StartCoroutine("IntroTextDisplay");
        InitializeInstructions();
        StartCoroutine("IntroImageDisplay");
    }

    private void InitializeInstructions(){
        InstructionDateText.text = $"{InstructionStart}\r\n{DateTime.Now.ToShortDateString()}";
        InstructionWordCountText.text = $"{WinWordCount}\r\n{WordCountEnd}";    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(scrollEnded){
                NextLevel();
            }
            else{
                StopCoroutine("IntroTextDisplay");
                StopCoroutine("IntroImageDisplay");
                IntroText.text = IntroString;
                MainDecoCanvas.SetActive(true);
                ImageCanvas.SetActive(true);
                scrollEnded = true;
                StartCoroutine("EndDisplay");
            }
        }
    }

    private IEnumerator IntroImageDisplay(){
        yield return new WaitForSeconds(ImageDelayFirst);
        MainDecoCanvas.SetActive(true);
        yield return null;
    }
    private IEnumerator IntroTextDisplay(){
        yield return new WaitForSeconds(TextDelay);
        for(var i = 0;i<= maxIntroIndex;i++){
            IntroText.text = IntroString.Substring(0,i);
            yield return new WaitForSeconds(TextDelay);
        }
        StartCoroutine("EndDisplay");
        yield return null;
    }

    private IEnumerator EndDisplay(){
        ImageCanvas.SetActive(true);
        yield return new WaitForSeconds(EndDelay);
        IntroText.text = EndIntroString;
        IntroText.color = EndTextColor;
        yield return new WaitForSeconds(EndDelay);
        NextLevel();
        yield return null;
    }

    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
