using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwpieLevel : MonoBehaviour, IEndDragHandler
{
    // Start is called before the first frame update
    int currentPage;
    [SerializeField] int maxPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] float tweenTime;

    [SerializeField] RectTransform levelPageRect;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] List<Button> barButton;
    [SerializeField] Sprite barClose, barOpen;
    [SerializeField] Button preButton, nextButton;
    [SerializeField] GameObject navButton;
    [SerializeField] GameObject navButtonHolder;



    float dragTheshould;
    private void Awake()
    {
        maxPage = transform.Find("Level Page").childCount;
        CreateNavBar();

        currentPage = 1;
        this.targetPos = levelPageRect.localPosition;
        Transform buttons = transform.parent.Find("Nav Bar");
        foreach (Transform button in buttons)
        {
            barButton.Add(button.GetComponent<Button>());
        }
        this.UpdateBar();
        this.UpdateChangeButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MoveToNextPage();
        }
    }
    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MoveToNextPage();
        }
    }
    void MoveToNextPage()
    {
        Debug.Log("MOVE");
        levelPageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateChangeButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > this.dragTheshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) this.Previous();
            else this.Next();
        }
        else
        { this.MoveToNextPage(); }
    }
    void UpdateBar()
    {
        foreach (var item in barButton)
        {
            item.image.sprite = this.barClose;
        }
        // barButton[this.currentPage - 1].image.sprite = this.barOpen;
        barButton[this.currentPage].image.sprite = this.barOpen;

    }
    void CreateNavBar()
    {
        for (int i = 0; i <= this.maxPage - 1; i++)
        {

            GameObject navBar = Instantiate(this.navButton);
            navBar.SetActive(true);
            navBar.transform.SetParent(navButtonHolder.transform);


        }
    }
    void UpdateChangeButton()
    {
        if (this.maxPage == 1)
        {
            this.preButton.interactable = false;
            this.nextButton.interactable = false;
        }
        else if (this.currentPage == 1)
        {
            this.preButton.interactable = false;
        }
        else if (this.currentPage == this.maxPage)
        {
            this.nextButton.interactable = false;
        }

        else
        {
            this.preButton.interactable = true;
            this.nextButton.interactable = true;


        }
    }
}
