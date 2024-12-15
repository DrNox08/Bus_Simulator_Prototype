using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{



    [Header("Settings")]
    [SerializeField] bool isAPlayerStop;
    [SerializeField] float stoppingTime;
    [SerializeField][TextArea(2, 2)] string notificationRightStop;
    [SerializeField][TextArea(2, 2)] string notificationWrongStop;

    IDrivable player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDrivable bus))
        {
            player = bus;
            UI_Manager.instance.ShowStopButton(true, StopAtBusStop);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        UI_Manager.instance.ShowStopButton(false, null);

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
        player = null;
        if (isAPlayerStop) { this.gameObject.SetActive(false); }
    }

}
