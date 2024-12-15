using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public static Action<string> OnGivingFeeback; // evento per il popup di feedback

    [Header("References")]

    [SerializeField] RectTransform leftButton;

    [SerializeField] RectTransform rightButton;

    [SerializeField] RectTransform upButton;

    [SerializeField] RectTransform stopButton;

    [SerializeField] RectTransform buttonsHolder;

    [SerializeField] TMP_Text scoreText;

    [SerializeField] GameObject notificationHolder;

    [SerializeField] TMP_Text notificationText;

    [Header("Settings")]
    [SerializeField] float buttonsHolderDefaultOutY;
    [SerializeField] float buttonsHolderDefaultInY;

    Action<Direction> OnChoice; // azione per rimandare la scelta del pulsante al CrossRoads.cs interessato


    private void Awake()
    {
        instance = this;
        DisableButtons();
        notificationHolder.SetActive(false);
        buttonsHolder.anchoredPosition = new Vector2(buttonsHolder.anchoredPosition.x, -600);
        scoreText.text = "0";
    }

    private void OnEnable()
    {

        OnGivingFeeback += Notify;
        BusStop.OnStopPossibility += ShowStopButton;
        GameManager.OnUpdateScore += UpdateScore;
    }
    private void OnDisable()
    {

        OnGivingFeeback -= Notify;
        BusStop.OnStopPossibility -= ShowStopButton;
        GameManager.OnUpdateScore -= UpdateScore;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="leftDirectionTransform"></param>
    /// <param name="straightDirectionTransform"></param>
    /// <param name="rightDirectionTransform"></param>
    /// <param name="onChoiceMade"></param>
    public void ShowButtonsAvailable(Transform left, Transform straight, Transform right, Action<Direction> onChoiceMade)
    {
        this.OnChoice = onChoiceMade;
        if (left != null) leftButton.gameObject.SetActive(true);
        if (straight != null) upButton.gameObject.SetActive(true);
        if (right != null) rightButton.gameObject.SetActive(true);

        buttonsHolder.gameObject.SetActive(true);

        if (buttonsHolder.anchoredPosition.y != buttonsHolderDefaultInY) buttonsHolder.transform.DOLocalMoveY(buttonsHolderDefaultInY, 0.5f);
    }

    public void DirectionButtonPressed(string direction)
    {
        Enum.TryParse(direction, out Direction choice);
        Debug.Log(choice.ToString());
        OnChoice?.Invoke(choice);
        buttonsHolder.DOLocalMoveY(buttonsHolderDefaultOutY, 0.5f);
        DisableButtons();
    }

    void ShowStopButton(bool value)
    {
        buttonsHolder.gameObject.SetActive(value);
        stopButton.gameObject.SetActive(value);
        if (buttonsHolder.anchoredPosition.y != buttonsHolderDefaultInY && value) buttonsHolder.transform.DOLocalMoveY(buttonsHolderDefaultInY, 0.5f);
        else buttonsHolder.transform.DOLocalMoveY(buttonsHolderDefaultOutY, 0.5f);
    }

    public void StopButtonPressed()
    {
        BusStop.OnStopPressed?.Invoke();
        buttonsHolder.DOLocalMoveY(buttonsHolderDefaultOutY, 1);
        DisableButtons();
    }

    void DisableButtons()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(false);

        buttonsHolder.gameObject.SetActive(false);
    }

    void UpdateScore()
    {
        scoreText.text = GameManager.Score.ToString();
        Debug.Log(scoreText.text);
    }

    void Notify(string message)
    {

        notificationText.text = message;


        notificationHolder.transform.localScale = Vector3.zero;
        notificationHolder.SetActive(true);


        notificationHolder.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {

            DOVirtual.DelayedCall(2f, () =>
            {

                notificationHolder.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    notificationHolder.SetActive(false);
                });
            });
        });
    }







}
