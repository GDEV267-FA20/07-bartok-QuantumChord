using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ColorChangeMovement : MonoBehaviour
{
    public float color;

    Renderer m_Renderer;

    SpriteRenderer s_SpriteRenderer;

    public String colorSphereObject;

    public GameObject[] spriteColor;

    public Vector3 target;

    public float randomValue;

    public Vector3 topLeftSphere, topRightSphere, bottomLeftSphere, bottomRightSphere;

    public float timeStartMove;

    public bool shouldMove;

    public Vector3 startPosition, endPosition;

    public float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        colorSetRandom();
        targetSetRandom();
        startMovement();
    }

    public void colorSetRandom()
    {
        color = Random.Range(0, 4);

        m_Renderer = GetComponent<Renderer>();

        if(color < 1)
        {
            m_Renderer.material.color = Color.red;

            colorSphereObject = "Red";
        }

        else if(color < 2)
        {
            m_Renderer.material.color = Color.green;

            colorSphereObject = "Green";
        }

        else if (color < 3)
        {
            m_Renderer.material.color = Color.blue;

            colorSphereObject = "Blue";
        }

        else
        {
            m_Renderer.material.color = Color.yellow;

            colorSphereObject = "Yellow";
        }
    }

    public void spriteColorChange()
    {
        if(endPosition == bottomRightSphere)
        {
            s_SpriteRenderer = spriteColor[3].GetComponent<SpriteRenderer>();
        }

        else if (endPosition == bottomLeftSphere)
        {
            s_SpriteRenderer = spriteColor[2].GetComponent<SpriteRenderer>();
        }

        else if (endPosition == topRightSphere)
        {
            s_SpriteRenderer = spriteColor[1].GetComponent<SpriteRenderer>();
        }

        else
        {
            s_SpriteRenderer = spriteColor[0].GetComponent<SpriteRenderer>();
        }

        switch (colorSphereObject)
        {
            case "Red":
            s_SpriteRenderer.color = Color.red;
                break;

            case "Green":
                s_SpriteRenderer.color = Color.green;
                break;

            case "Blue":
                s_SpriteRenderer.color = Color.blue;
                break;

            case "Yellow":
                s_SpriteRenderer.color = Color.yellow;
                break;
        }
    }

    public void targetSetRandom()
    {
        randomValue = Random.Range(0, 4);

        if(randomValue < 1)
        {
            target = topLeftSphere;
        }

        else if (randomValue < 2)
        {
            target = topRightSphere;
        }

        else if (randomValue < 3)
        {
            target = bottomLeftSphere;
        }

        else
        {
            target = bottomRightSphere;
        }

        endPosition = target;

        if(endPosition == startPosition)
        {
            targetSetRandom();
        }
    }

    public void startMovement()
    {
        timeStartMove = Time.time;

        shouldMove = true;
    }

    public Vector3 Move(Vector3 start, Vector3 stop, float timeStartMove, float moveTime = 1)
    {
        float timeSinceStart = Time.time - timeStartMove;

        float partsCompleted = timeSinceStart / moveTime;

        var result = Vector3.Lerp(start, stop, partsCompleted);

        if (partsCompleted >= 1)
        {
            shouldMove = false;
            spriteColorChange();
        }

        return result;
    }

    void Update()
    {
        if (shouldMove)
        {
            transform.position = Move(startPosition, endPosition, timeStartMove, moveTime);
        }

        else
        {
            startPosition = endPosition;

            colorSetRandom();
            targetSetRandom();
            startMovement();
        }
    }
}
