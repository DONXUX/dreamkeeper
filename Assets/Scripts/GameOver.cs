using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text messageText;
    public Button retryButton;

    private float fades = 0.0f;
    private float timer = 0;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (fades < 1.0f && timer >= 0.1f)
        {
            fades += 0.005f;
            print(fades);
            messageText.color = new Color(0.7f, 0, 0, fades);
        }
        else if (fades >= 1.0f)
        {
            retryButton.image.color = new Color(1.0f, 1.0f, 1.0f, fades);
            Text t = retryButton.GetComponentInChildren<Text>();
            t.color = new Color(1.0f, 1.0f, 1.0f, fades);
        }
    }
}
