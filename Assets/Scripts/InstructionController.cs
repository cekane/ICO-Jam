using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class InstructionController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI buttonText;
    public Button startGamebutton;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTitleText(string t)
    {
        title.text = t;
    }

    public void SetDescriptionText(string t)
    {
        description.text = t;
    }

    public void SetButtonText(string t)
    {
        buttonText.text = t;
    }

    public void HideDescriptionUI()
    {
        gameObject.SetActive(false);
    }

    public void SetButtonClickListener(InstructionParent parent)
    {
        Debug.Log("Adding listener");
        startGamebutton.onClick.AddListener(parent.StartButtonClick);
    }
}
