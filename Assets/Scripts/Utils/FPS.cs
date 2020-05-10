using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] Text fpsText;

    string fpsTextTemplate = "FPS: ";
    float deltaTime = 0f;
    // Update is called once per frame
    private void Awake()
    {
        Application.targetFrameRate = 300;
        fpsText.color = Color.blue;
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        if (fps < 100f)
        {
            fpsText.color = Color.red;
        }
        else if (fps > 200f)
        {
            fpsText.color = Color.green;
        }
        else
        {
            fpsText.color = Color.yellow;
        }

        fpsText.text = fpsTextTemplate + Mathf.Ceil(fps).ToString();
    }
}
