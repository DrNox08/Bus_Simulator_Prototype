using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    public static Action<bool> OnStopPossibility;
    public static Action OnStopPressed;

    [Header("Settings")]
    [SerializeField] bool isAPlayerStop;
    [SerializeField] float stoppingTime;
    [SerializeField][TextArea(2, 2)] string notificationRightStop;
    [SerializeField][TextArea(2, 2)] string notificationWrongStop;

    IDrivable player;

    private void OnEnable()
    {
        OnStopPressed += StopAtBusStop;
    }
    private void OnDisable()
    {
        OnStopPressed -= StopAtBusStop;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDrivable bus))
        {
            player = bus;
            OnStopPossibility?.Invoke(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        OnStopPossibility?.Invoke(false);
        player = null;
    }

    void StopAtBusStop()
    {
        
        if (isAPlayerStop)
        {
            GameManager.OnUpdateScore?.Invoke();
            UI_Manager.OnGivingFeeback?.Invoke(notificationRightStop);
        }
        else UI_Manager.OnGivingFeeback?.Invoke(notificationWrongStop);

        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        player.StopAgent(true);
        float timeElapsed = 0;
        while (timeElapsed < stoppingTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        player.StopAgent(false);
    }

}
