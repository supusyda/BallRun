using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance { get; private set; }
    [SerializeField] Animator changeSceneAnimator;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void NextLevel()
    {
        StartCoroutine(nameof(this.LoadScene));
    }
    IEnumerator LoadScene()
    {
        this.changeSceneAnimator.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        this.changeSceneAnimator.SetTrigger("start");
    }
}
