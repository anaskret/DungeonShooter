using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject controlsButton;
    [SerializeField] GameObject optionsButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject backButtonOptions;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject options;

    private int index = -1;
    private int selected = 0;

    private bool dpadInput = false;

    private Color32 highlightedColor = new Color32(197, 193, 193, 45);
    private Color32 normalColor = new Color32(255, 255, 255, 0);

    private void Update()
    {
        CameraZoom();
        ToggleButton();
    }
    private void ToggleButton()
    {
        switch (index)
        {
            case 0:
                if (controls.activeSelf == true)
                {
                    backButton.GetComponent<Button>().Select();
                }
                else if (options.activeSelf == true)
                {
                    backButtonOptions.GetComponent<Button>().Select();
                }
                else
                {
                    startButton.GetComponent<Button>().Select();
                }
                break;
            case 1:
                controlsButton.GetComponent<Button>().Select();
                break;
            case 2:
                optionsButton.GetComponent<Button>().Select();
                break;
            case 3:
                exitButton.GetComponent<Button>().Select();
                break;
        }
        selected = index;
    }

    private void PressButton()
    {
        if (!Input.GetKeyDown("joystick button 0"))
            return;

        switch (selected)
        {
            case 0:
                startButton.GetComponent<Button>().onClick.Invoke();
                break;
            case 1:
                controlsButton.GetComponent<Button>().onClick.Invoke();
                index = 0;
                break;
            case 2:
                optionsButton.GetComponent<Button>().onClick.Invoke();
                index = 1;
                break;
            case 3:
                exitButton.GetComponent<Button>().onClick.Invoke();
                break;
        }
        selected = index;
    }

    private void CameraZoom()
    {
        if (Input.GetAxis("DPAD Y") == 0.0)
        {
            dpadInput = true;
        }

        float dpadY = Input.GetAxisRaw("DPAD Y");
        if (dpadY == -1f && dpadInput)
        {
            StartCoroutine(DpadControl(true));
        }
        else if (dpadY == 1f && dpadInput)
        {
            StartCoroutine(DpadControl(false));
        }
    }

    IEnumerator DpadControl(bool input)
    {
        dpadInput = false;

        yield return new WaitForSeconds(0.5f);
        if (input == false)
        {
            ChangeIndex(-1);
        }
        if (input == true)
        {
            ChangeIndex(1);
        }

        StopCoroutine(nameof(DpadControl));
    }

    private void ChangeIndex(int i)
    {
        index += i;

        if (controls.activeSelf == true)
        {
            index = 0;
        }
        else if (options.activeSelf == true)
        {
            if (index > 1)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = 1;
            }
        }
        if (index > 3)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = 3;
        }
        Debug.Log(index);
    } 
}
