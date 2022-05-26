using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CellController : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI text;
    public Image backgroundImage;
    public Image blackoutImage;
    public int row;
    public int col;
    public int house;
    public GameController gameController;
    public bool selected;
    public bool locked;
    public int value = 0;

    private Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        if ((row + col) % 2 == 0)
        {
            baseColor = Color.white;
        } else 
        {
            baseColor = new Color(0.9f, 0.9f, 0.9f);
        }
        backgroundImage.color = baseColor;
        text.color = new Color(0.08f, 0.41f, 0.16f);
        if (locked)
        {
            text.color = Color.black;
            // backgroundImage.color = new Color(0.8f, 0.8f, 0.8f);
        }
        Vector3 eulers = GetComponent<RectTransform>().eulerAngles;
        GetComponent<RectTransform>().eulerAngles = new Vector3(eulers.x, eulers.y, eulers.z + Random.Range(-2f, 2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (selected) {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
                text.text = "1";
                value = 1; 
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
                text.text = "2";
                value = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
                text.text = "3";
                value = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
                text.text = "4";
                value = 4;
            }
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete)) {
                text.text = "";
                value = 0;
            }
            gameController.CellUpdated(this);
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        blackoutImage.enabled = false;
        if (locked)
        {
            return;
        }
        selected = !selected;
        if (selected)
        {
            backgroundImage.color = new Color(0.51f, 0.79f, 0.58f);
            gameController.CellSelected(this);
        } 
        else 
        {
            backgroundImage.color = Color.white;
        }
    }

    public void SetValue(int val)
    {
        value = val;
        if (val == 0)
        {
            text.text = "";
        }
        if (val == 1)
        {
            text.text = "1";
        }
        if (val == 2)
        {
            text.text = "2";
        }
        if (val == 3)
        {
            text.text = "3";
        }
        if (val == 4)
        {
            text.text = "4";
        }
    }

    public void Deselect() 
    {
        backgroundImage.color = baseColor;
        selected = false;
    }

    public void Lock()
    {
        // backgroundImage.color = new Color(0.8f, 0.8f, 0.8f);
        locked = true;
        text.fontStyle = FontStyles.Bold;
        // text.color = new Color(0.08f, 0.41f, 0.16f);
        text.color = Color.black;
        text.fontSize = 36;
        text.GetComponent<Animator>().speed = Random.Range(0.8f, 1.2f);
        int randomEffect = Random.Range(0, 20 - gameController.level);
        if (randomEffect == 0)
        {
            text.GetComponent<Animator>().Play("TextRotate");
        } else if (randomEffect == 1) 
        {
            text.GetComponent<Animator>().Play("TextRotate2");
        } else if (randomEffect == 2)
        {
            text.GetComponent<Animator>().Play("TextScale");
        } else if (randomEffect == 3)
        {
            text.GetComponent<Animator>().Play("TextFade");
        } else if (randomEffect == 4)
        {
            if (value == 1)
            {
                text.text = "I";
            }
            if (value == 2)
            {
                text.fontSize = 30;
                text.text = "II";
            }
            if (value == 3)
            {
                text.fontSize = 25;
                text.text = "III";
            }
            if (value == 4)
            {
                text.text = "IV";
            }
        } else if (randomEffect == 5)
        {
            text.fontSize = 25;
            if (value == 1)
            {
                text.text = "9-8";
            }
            if (value == 2)
            {
                text.text = "6/3";
            }
            if (value == 3)
            {
                text.text = "2+1";
            }
            if (value == 4)
            {
                text.text = "2x2";
            }
        } else if (randomEffect == 6)
        {
            text.fontSize = 25;
            if (value == 1)
            {
                text.text = "1x1";
            }
            if (value == 2)
            {
                text.text = "6-4";
            }
            if (value == 3)
            {
                text.text = "9/3";
            }
            if (value == 4)
            {
                text.text = "3+1";
            }
        } else if (randomEffect == 7)
        {
            text.fontSize = 24;
            if (value == 1)
            {
                text.text = "one";
            }
            if (value == 2)
            {
                text.text = "two";
            }
            if (value == 3)
            {
                text.fontSize = 20;
                text.text = "three";
            }
            if (value == 4)
            {
                text.fontSize = 21;
                text.text = "four";
            }
        }
    }

    public void Reset() 
    {
        SetValue(0);
        locked = false;
        text.fontStyle = FontStyles.Normal;
        text.fontSize = 30;
        // text.color = new Color(0.3f, 0.3f, 0.3f);
        text.color = new Color(0.08f, 0.41f, 0.16f);
        text.GetComponent<Animator>().Play("EmptyState");
    }

    public void Complete()
    {
        locked = true;
        text.fontStyle = FontStyles.Bold;
        text.fontSize = 36;
        text.color = Color.black;
        SetValue(value);
        text.GetComponent<Animator>().Play("EmptyState");
    }

    public void Blackout()
    {
        blackoutImage.enabled = true;
    }
}
