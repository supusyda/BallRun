using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] int level;\
    [SerializeField] Button[] buttons;
    [SerializeField] int levelButtonCount;
    [SerializeField] GameObject Audio;
    private void Awake()
    {
        ButtonToArray();
        int unlockLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < unlockLevel)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;

            }
        }

    }
    public void SelectedLevel(int level)
    {
        Debug.Log(level);

        // level = this.level;
        if (level != 0)
        {
            Audio.SetActive(true);
            SceneManager.LoadScene("Level " + level);


        }
    }
    void ButtonToArray()
    {

        levelButtonCount = transform.childCount;
        buttons = new Button[levelButtonCount];
        for (int i = 0; i < levelButtonCount; i++)
        {
            buttons[i] = transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
