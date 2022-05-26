using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public CellController[] row1;
    public CellController[] row2;
    public CellController[] row3;
    public CellController[] row4;
    public CellController[] col1;
    public CellController[] col2;
    public CellController[] col3;
    public CellController[] col4;
    public CellController[] house1;
    public CellController[] house2;
    public CellController[] house3;
    public CellController[] house4;
    public CellController[] allCells;
    public GameObject nextButton;
    public GameObject playAgainButton;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI warningText;
    public List<GameObject> numberCrunchers;
    public GameObject numberCruncherPrefab;
    public Animator boardAnimator;
    public Canvas canvas;
    public GameObject menuPanel;

    public int level = 1;

    bool timerEnabled = false;
    float timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard(); 
        timerEnabled = true;
        Invoke("StartEvent", Random.Range(10, 30));
    }

    // Update is called once per frame
    void Update()
    {
       if (timerEnabled) {
            timeElapsed += Time.deltaTime;
            int minutes = (int)(timeElapsed / 60);
            int seconds = (int)(timeElapsed % 60);
            int fraction = (int)((timeElapsed * 100) % 100);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        } 
    }

    int recursionCount = 0;
    public void GenerateBoard()
    {
        recursionCount++;
        foreach (var cell in allCells)
        {
            cell.Reset();
            cell.gameController = this;
        }
        // add 1s
        SetRandomCell(1);
        SetRandomCell(1);
        SetRandomCell(1);
        SetRandomCell(1);
        SetRandomCell(2);
        SetRandomCell(2);
        SetRandomCell(2);
        SetRandomCell(2);
        SetRandomCell(3);
        SetRandomCell(3);
        SetRandomCell(3);
        SetRandomCell(3);
        SetRandomCell(4);
        SetRandomCell(4);
        SetRandomCell(4);
        SetRandomCell(4);
        if (!IsBoardValid() && recursionCount < 50000)
        {
            GenerateBoard();
        } else 
        {
            Debug.Log($"recursions: {recursionCount})");
            recursionCount = 0;
            // remove the value on some random cells
            for (int i = 0; i < level + 7; i++)
            {
                int rand = Random.Range(0, allCells.Length);
                allCells[rand].SetValue(0);
            }
            // lock the leftover numbers in place
            foreach (var cell in allCells)
            {
                if (cell.value != 0)
                {
                    cell.Lock();
                } 
            }
        }
    }

    bool IsBoardValid()
    {
        foreach (var cell in allCells)
        {
            if (cell.value == 0)
            {
                return false;
            }
        }
        if (row1[0].value == row1[1].value || row1[0].value == row1[2].value || row1[0].value == row1[3].value)
        {
            return false;
        }
        if (row1[1].value == row1[2].value || row1[1].value == row1[3].value)
        {
            return false;
        }
        if (row1[2].value == row1[3].value)
        {
            return false;
        }
        if (row2[0].value == row2[1].value || row2[0].value == row2[2].value || row2[0].value == row2[3].value)
        {
            return false;
        }
        if (row2[1].value == row2[2].value || row2[1].value == row2[3].value)
        {
            return false;
        }
        if (row2[2].value == row2[3].value)
        {
            return false;
        }
        if (row3[0].value == row3[1].value || row3[0].value == row3[2].value || row3[0].value == row3[3].value)
        {
            return false;
        }
        if (row3[1].value == row3[2].value || row3[1].value == row3[3].value)
        {
            return false;
        }
        if (row3[2].value == row3[3].value)
        {
            return false;
        }
        if (row4[0].value == row4[1].value || row4[0].value == row4[2].value || row4[0].value == row4[3].value)
        {
            return false;
        }
        if (row4[1].value == row4[2].value || row4[1].value == row4[3].value)
        {
            return false;
        }
        if (row4[2].value == row4[3].value)
        {
            return false;
        }

        if (col1[0].value == col1[1].value || col1[0].value == col1[2].value || col1[0].value == col1[3].value)
        {
            return false;
        }
        if (col1[1].value == col1[2].value || col1[1].value == col1[3].value)
        {
            return false;
        }
        if (col1[2].value == col1[3].value)
        {
            return false;
        }
        if (col2[0].value == col2[1].value || col2[0].value == col2[2].value || col2[0].value == col2[3].value)
        {
            return false;
        }
        if (col2[1].value == col2[2].value || col2[1].value == col2[3].value)
        {
            return false;
        }
        if (col2[2].value == col2[3].value)
        {
            return false;
        }
        if (col3[0].value == col3[1].value || col3[0].value == col3[2].value || col3[0].value == col3[3].value)
        {
            return false;
        }
        if (col3[1].value == col3[2].value || col3[1].value == col3[3].value)
        {
            return false;
        }
        if (col3[2].value == col3[3].value)
        {
            return false;
        }
        if (col4[0].value == col4[1].value || col4[0].value == col4[2].value || col4[0].value == col4[3].value)
        {
            return false;
        }
        if (col4[1].value == col4[2].value || col4[1].value == col4[3].value)
        {
            return false;
        }
        if (col4[2].value == col4[3].value)
        {
            return false;
        }
        if (house1[0].value == house1[1].value ||
            house1[0].value == house1[2].value ||
            house1[0].value == house1[3].value ||
            house1[1].value == house1[2].value ||
            house1[1].value == house1[3].value ||
            house1[2].value == house1[3].value)
        {
            return false;
        }
        if (house2[0].value == house2[1].value ||
            house2[0].value == house2[2].value ||
            house2[0].value == house2[3].value ||
            house2[1].value == house2[2].value ||
            house2[1].value == house2[3].value ||
            house2[2].value == house2[3].value)
        {
            return false;
        }
        if (house3[0].value == house3[1].value ||
            house3[0].value == house3[2].value ||
            house3[0].value == house3[3].value ||
            house3[1].value == house3[2].value ||
            house3[1].value == house3[3].value ||
            house3[2].value == house3[3].value)
        {
            return false;
        }
        if (house4[0].value == house4[1].value ||
            house4[0].value == house4[2].value ||
            house4[0].value == house4[3].value ||
            house4[1].value == house4[2].value ||
            house4[1].value == house4[3].value ||
            house4[2].value == house4[3].value)
        {
            return false;
        }
        Debug.Log("it's valid!");
        return true;
    }

    void SetRandomCell(int value)
    {
        int count = 0;
        while (true)
        {
            count++;
            int rand = Random.Range(0, allCells.Length);
            CellController currentCell = allCells[rand];
            bool invalidPlacement = false;
            foreach (var cell in allCells)
            {
                if (cell.row == currentCell.row && cell.value == value)
                {
                    // don't place
                    invalidPlacement = true;
                } 
                if (cell.col == currentCell.col && cell.value == value)
                {
                    // don't place
                    invalidPlacement = true;
                } 
                if (cell.house == currentCell.house && cell.value == value)
                {
                    // don't place
                    invalidPlacement = true;
                } 
            }
            if (currentCell.value == 0 && invalidPlacement == false)
            {
                currentCell.SetValue(value);
                return;
            }
            if (count > 16)
            {
                return;
            }
        }
    }

    public void CellSelected(CellController cell)
    {
        foreach (var c in allCells)
        {
            if (c != cell)
            {
                c.Deselect();
            } 
        }
    }

    public void CellUpdated(CellController cell)
    {
        if (IsBoardValid())
        {
            // clear out any events
            ClearWarningText();
            boardAnimator.Play("Empty");
            foreach (var cruncher in numberCrunchers)
            {
                Destroy(cruncher); 
            }
            numberCrunchers.Clear();

            // complete and deselect all cells
            foreach (var c in allCells)
            {
                c.Complete();
                c.Deselect();
            }
            // show next button or play again button
            if (level < 4)
            {
                nextButton.SetActive(true);
            }
            else 
            {
                timerEnabled = false;
                playAgainButton.SetActive(true);
            }
            string[] victoryTextOptions = new string[] {
                "VICTORY!",
                "SUCCESS!",
                "BOOYEAH!",
                "HOORAY!",
                "SUDOKU'D!",
                "FANTASTIC!",
                };
            victoryText.text = victoryTextOptions[Random.Range(0, victoryTextOptions.Length)];
            victoryText.gameObject.SetActive(true);
        }
    }

    void SpawnNumberCrunchers()
    {
        // return if game is over
        if (!timerEnabled) return;

        if (!nextButton.activeSelf)
        {
            warningText.text = "NUMBER CRUNCHERS INCOMING! Click them before they ruin your progress.";
            // send out a random number or crunchers
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                NumberCruncher cruncher = Instantiate(numberCruncherPrefab).GetComponent<NumberCruncher>();
                cruncher.gameController = this;
                cruncher.transform.SetParent(canvas.transform);
                cruncher.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                float randomAngle = Random.Range(0f, 360f);
                float randomRadius = Random.Range(500f, 700f);
                float x = randomRadius * Mathf.Cos(randomAngle);
                float y = randomRadius * Mathf.Sin(randomAngle);
                cruncher.transform.localPosition = new Vector3(x, y, 0);
                cruncher.startPos = cruncher.transform.position;
                cruncher.target = allCells[Random.Range(0, allCells.Length)];
                cruncher.Go();
                numberCrunchers.Add(cruncher.gameObject);
            }
            Invoke("ClearWarningText", 6);
        }
    }

    void RotateBoard()
    {
        if (!timerEnabled) return;

        if (!nextButton.activeSelf)
        {
            warningText.text = "Things are spinning out of control...hold on tight!";
            Invoke("ClearWarningText", 6);
            boardAnimator.Play("RotateBoard");
            Invoke("StopRotatingBoard", 20);
        }
    }

    void BlackoutCells()
    {
        if (!timerEnabled) return;

        if (!nextButton.activeSelf)
        {
            warningText.text = "Power Outage! Get those lights back on!";
            Invoke("ClearWarningText", 6);
            foreach (var cell in allCells)
            {
                cell.Blackout();
            }
        }
    }

    void StopRotatingBoard()
    {
        boardAnimator.Play("Empty");
    }

    void StartEvent()
    {
        if (!timerEnabled) return;

        if (!nextButton.activeSelf)
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                RotateBoard();
            }
            if (rand == 1)
            {
                SpawnNumberCrunchers();
            }
            if (rand == 2)
            {
                BlackoutCells();
            }
        }
        Invoke("StartEvent", Random.Range(20, 40));
    }

    void ClearWarningText()
    {
        warningText.text = "";
    }

    public void Next() 
    {
        if (level < 4)
        {
            level++;
            GenerateBoard();
            nextButton.SetActive(false);
            victoryText.gameObject.SetActive(false);
            levelText.text = $"{level}/4";
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
