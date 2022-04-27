using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRecorder : MonoBehaviour
{
    private GameController gc;
    private SceneController sc;

    [SerializeField] private TMP_Text scoreValue;
    [SerializeField] private TMP_Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
        sc = GameObject.FindObjectOfType<SceneController>();

        nameText = GameObject.Find("NameText").GetComponent<TMP_Text>();
        scoreValue = GameObject.Find("ScoreValue").GetComponent<TMP_Text>();
    }

    void Update(){
        scoreValue.text = gc.PlayerScore+"";
    }

    public void RecordScore(){
        LeaderBoard.Record(nameText.text.ToUpper(), gc.PlayerScore);
    }
}
