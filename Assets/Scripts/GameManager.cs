using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Field> playerFields;
    [SerializeField] List<Field> enemyFields;
    [SerializeField] ShipCollection playerShips;
    [SerializeField] ShipCollection enemyShips;

    [SerializeField] GameObject endGameCanvas;
    [SerializeField] Text endGameText;
    [SerializeField] Text winCountText;
    [SerializeField] Text loseCountText;



    public static bool playerMove = true;

    private void Start()
    {
        endGameCanvas.SetActive(false);
        InitializeViewPlayer(playerFields, playerShips);
        InitializeViewEnemy(enemyFields, enemyShips);
    }

    private void Update()
    {
        NewGame();
    }

    public void InitializeViewPlayer(List<Field> fields, ShipCollection collection)
    {
        foreach (var ship in collection.ships)
        {
            foreach (var id in ship.fieldsIDs)
            {
                fields[id].SetFieldStatus(FieldStatus.Ship);
                fields[id].SetFieldLabel(ship.size.ToString());
            }
        }
    }

    public void InitializeViewEnemy(List<Field> fields, ShipCollection collection)
    {
        foreach (var ship in collection.ships)
        {
            foreach (var id in ship.fieldsIDs)
            {
                fields[id].SetFieldStatus(FieldStatus.Ship);
            }
        }
    }

    public void MakeEnemyMove()
    {
        if(playerMove == true)
        {
            return;
        }
        while (true)
        {
            int fieldId = Random.Range(0, 100);
            Field field = playerFields[fieldId];
            if (field.fieldStatus == FieldStatus.Empty || field.fieldStatus == FieldStatus.Ship)
            {
                field.AttackField();
                playerMove = true;
                break;
            }
        }
    }

    public void EndGame()
    {
      
        if(playerMove ==true)
        {
            foreach (var field in playerFields)
            {
                if (field.fieldStatus == FieldStatus.Ship)
                    return;
            }
            LoseDataUpdate();
            ShowData();
        }
        else
        {
            foreach (var field in enemyFields)
            {
                if (field.fieldStatus == FieldStatus.Ship)
                    return;
            }
            WinDataUpdate();
            ShowData();
        }
        endGameCanvas.SetActive(true);
    }

    void NewGame()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void WinDataUpdate()
    {
        InitializeData();
        endGameText.text = "Win";
        var temp = PlayerPrefs.GetInt("WinCount") + 1;
        PlayerPrefs.SetInt("WinCount", temp);
    }

    void LoseDataUpdate()
    {
        InitializeData();
        endGameText.text = "Lose";
        var temp = PlayerPrefs.GetInt("LoseCount") + 1;
        PlayerPrefs.SetInt("LoseCount", temp);

    }

    void ShowData()
    {
        winCountText.text = "Win Count: " + PlayerPrefs.GetInt("WinCount");
        loseCountText.text = "Lose Count: " + PlayerPrefs.GetInt("LoseCount");
        PlayerPrefs.Save();
    }

    void InitializeData()
    {
        if (!PlayerPrefs.HasKey("WinCount"))
        {
            PlayerPrefs.SetInt("WinCount", 0);
        }

        if (!PlayerPrefs.HasKey("LoseCount"))
        {
            PlayerPrefs.SetInt("LoseCount", 0);
        }
    }
}
