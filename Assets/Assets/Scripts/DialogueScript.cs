using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public bool playerIsClose;

    private int index;

    public BoxCollider2D bc;

    private bool isDeactivated;



    // Start is called before the first frame update
    void Start()
    {
        playerIsClose = false;
        textComponent.text = string.Empty;
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerIsClose)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }


    // breakes the string text into a character array
    IEnumerator TypeLine()
    {
        foreach (char line in lines[index].ToCharArray())
        {
            textComponent.text += line;
            yield return new WaitForSeconds(textSpeed);

        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            isDeactivated = false;
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

            // temporarily deactivates the dialogue box after each sentence
            gameObject.SetActive(false);
            isDeactivated = true;

        }
        else
        {
            Debug.Log("sets obj to false");
            gameObject.SetActive(false);
        }
    }


    /** starts typing out the characters in a sentece when dialogue box is reactivated*/
    private void whenDialogueIsReactivated()
    {
        if (isDeactivated == true)
        {
            StartCoroutine(TypeLine());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerIsClose = true;
        Debug.Log("trigger");
        // works until here
        if (other.CompareTag("NPC"))
        {
            Debug.Log("NPC dialogue should display");
            playerIsClose = true;
            bc.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        playerIsClose = false;
        Debug.Log("Closing Dialogue Box");
        if (other.CompareTag("NPC"))
        {
            Debug.Log("NPC dialogue should close");
            playerIsClose = false;
            bc.isTrigger = false;
        }
    }
}
