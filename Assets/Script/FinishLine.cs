using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UnlockNextLevel();
            GameManager.instance.NextLevel();
        }

    }
    void UnlockNextLevel()
    {
        Debug.Log("SceneManager.GetActiveScene().buildIndex" + SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("PlayerLevel"))
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
            Debug.Log(PlayerPrefs.GetInt("PlayerLevel"));
            PlayerPrefs.Save();
        }
    }
}
