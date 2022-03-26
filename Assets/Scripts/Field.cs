using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public FieldStatus fieldStatus = FieldStatus.Empty;
    public FieldType fieldType;
    public Text fieldLabel;
    public Image fieldBackGround;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        SetBackGroundColor(fieldStatus); 
    }

    public void SetFieldStatus(FieldStatus status)
    {
        fieldStatus = status;
        SetBackGroundColor(fieldStatus);
    }

    public void SetFieldLabel(string text)
    {
        fieldLabel.text = text;
    }

    public void SetBackGroundColor(FieldStatus status)
    {

        switch (status)
        {
            default:
            case FieldStatus.Empty:
            case FieldStatus.Ship:
                fieldBackGround.color = Color.white;
                break;
            case FieldStatus.Attacked:
                fieldBackGround.color = Color.red;
                break;
            case FieldStatus.Used:
                fieldBackGround.color = Color.clear;
                break;    
        }
    }

    public void AttackField()
    {
        if (fieldStatus == FieldStatus.Ship)
        {
            SetFieldStatus(FieldStatus.Attacked);
            SetFieldLabel("X");
            
        }
        else
        {
            SetFieldStatus(FieldStatus.Used);
        }
        gameManager.EndGame();
    }

    public void Attack()
    {
        if (GameManager.playerMove && transform.parent.tag== "EnemyGrid" && fieldStatus != FieldStatus.Attacked && fieldStatus != FieldStatus.Used)
        {
            AttackField();
            GameManager.playerMove = false;
            gameManager.MakeEnemyMove();
        }
    }
    

    public void AddShip(string shipID)
    {
        SetFieldStatus(FieldStatus.Ship);
        SetFieldLabel(shipID);
    }
}

public enum FieldStatus
{
    Empty,Ship, Attacked, Used
}

public enum FieldType
{
    Player, Enemy
}
