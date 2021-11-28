using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypingMiniGame : MonoBehaviour
{
    public TextMeshProUGUI TargetText;
    public TMP_InputField InputText;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI LorenText;

    public TextMeshProUGUI TimerText;

    public TextMeshProUGUI InstructionDateText;
    public TextMeshProUGUI InstructionHeureText;
    public TextMeshProUGUI InstructionWordCountText;
    public TextMeshProUGUI InstructionComputerText;
    public string InstructionStart = "Essai pour le ";
    public string WordCountEnd = "mots";

    public string InstructionComputer = "Utiliser les mots de l'ordi";

    public TextAsset WordListTextFile;

    public string ScorePrefix = "Nb de mots : ";

    const char NewLine = '\n';
    const char CarriageReturn = '\r';
    const char BackSpace = '\b';
    private string[] WordList;

    private int currentWordIndex = -1;
    private string currentWord;
    public readonly bool PrintInput = true;
    public readonly int MaximumCharacters = 20;

    public int WinWordCount = 800;

    public int score = 0;

    public float timer = 5 * 60;

    public int minWordSize = 3;

    private int charactersAdded = 0;

    // Start is called before the first frame update
    void Start()
    {
        LoadWordList();

        InputText.text = "";
        NextWord();

        InitializeInstructions();
    }

    private void InitializeInstructions(){
        InstructionDateText.text = $"{InstructionStart}\r\n{DateTime.Now.ToShortDateString()}";
        InstructionHeureText.text = $"{DateTime.Now.AddSeconds(timer).ToShortTimeString()}";
        InstructionWordCountText.text = $"{WinWordCount}\r\n{WordCountEnd}";
        InstructionComputerText.text = $"{InstructionComputer}";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0){
            timer = 0;
            Loose();
        }
        UpdateTimerText();
    }

    private void UpdateTimerText(){
        var exactTime = Mathf.RoundToInt(timer);
        var secondes = exactTime % 60;
        var minutes = (exactTime - secondes)/ 60;
        TimerText.text = $"{minutes.ToString().PadLeft(2,'0')}:{secondes.ToString().PadLeft(2,'0')}";
    }

    public void OnInputTextChange(string value){
        CheckWord(value);
        UpdateLoren();
    }

    private void UpdateLoren(){
        var firstSpace = LorenText.text.IndexOf(' ');
        var start = LorenText.text.Substring(0,firstSpace);
        
        var rest = LorenText.text.Substring(firstSpace+1);
        LorenText.text = $"{rest} {start}";
    }
    private void CheckWord(string inputValue){
        if(inputValue.Equals(TargetText.text,StringComparison.OrdinalIgnoreCase)){
            AddScore();
            NextWord();
        }
    }

    private void LoadWordList(){
        WordList  = WordListTextFile.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
    }

    private void AddScore(){
        score += 10;
        ScoreText.text = $"{ScorePrefix}{score}";

        if(IsWinning()){
            LoadNextLevel();
        }
    }

    private bool IsWinning(){
        return score >= WinWordCount;
    }

    private void NextWord(){
        int newWordIndex = -1;
        do{
            newWordIndex = UnityEngine.Random.Range(0,WordList.Length);
        }while(newWordIndex == currentWordIndex || WordList[newWordIndex].Length < minWordSize);
        currentWordIndex = newWordIndex;
        currentWord = WordList[currentWordIndex];
        TargetText.text = currentWord;
        InputText.text = "";
    }

    private void LoadNextLevel(){
        SceneManager.LoadScene("WIN");
    }

    private void Loose(){
        SceneManager.LoadScene("LOST");
    }

    public void Refocus(string value){
        InputText.ActivateInputField();
    }
}
