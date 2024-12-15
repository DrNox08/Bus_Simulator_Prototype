using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoadPoint : MonoBehaviour, ISelectable
{
    public static Action<Transform, Transform, Transform> OnGivingChoice;
    public static Action<Direction> OnChoiceMade;

    [SerializeField] Transform wayWest;
    [SerializeField] Transform wayEast;
    [SerializeField] Transform wayNorth;
    [SerializeField] Transform waySouth;
    Direction correctDirection;

    Transform relativeRight;
    Transform relativeLeft;
    Transform relativeStraight;


    Entrance orientation;
    IDrivable player;

    private void OnEnable()
    {
        OnChoiceMade += SetChosenDirection;
    }
    private void OnDisable()
    {
        OnChoiceMade -= SetChosenDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDrivable playerInTrigger))
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = transform.InverseTransformDirection(direction); // converte la direzione da world a local


            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                orientation = direction.x > 0 ? Entrance.EAST : Entrance.WEST;
            else
                orientation = direction.z > 0 ? Entrance.NORTH : Entrance.SOUTH;

            player = playerInTrigger;

            ConvertDirections(player);

            OnGivingChoice(relativeLeft, relativeStraight, relativeRight);

            Debug.Log($"Il player è entrato da: {orientation}");

        }
    }

    void ConvertDirections(IDrivable player)
    {
        // resetta ogni riferimento ai transform relativi al player
        relativeLeft = null;
        relativeRight = null;
        relativeStraight = null;

        switch (orientation) // a seconda del lato da cui entra il player, converte le direzioni in base i punti caridali
        {
            case Entrance.EAST:
                relativeLeft = waySouth != null ? waySouth : null;
                relativeRight = wayNorth != null ? wayNorth : null;
                relativeStraight = wayWest != null ? wayWest : null;
                break;

            case Entrance.NORTH:
                relativeLeft = wayWest != null ? wayWest : null;
                relativeRight = wayEast != null ? wayEast : null;
                relativeStraight = waySouth != null ? waySouth : null;
                break;

            case Entrance.SOUTH:
                relativeLeft = wayWest != null ? wayWest : null;
                relativeRight = wayEast != null ? wayEast : null;
                relativeStraight = wayNorth != null ? wayNorth : null;
                break;

            case Entrance.WEST:
                relativeLeft = wayNorth != null ? wayNorth : null;
                relativeRight = waySouth != null ? waySouth : null;
                relativeStraight = wayEast != null ? wayEast : null;
                break;
        }

    }

    public void SetChosenDirection(Direction chosenDirection)
    {
        switch (chosenDirection)
        {
            case Direction.LEFT:
                player.SetNextWaypoint(relativeLeft);
                break;
            case Direction.RIGHT:
                player.SetNextWaypoint(relativeRight);
                break;
            case Direction.STRAIGHT:
                player.SetNextWaypoint(relativeStraight);
                break;
        }

        if (chosenDirection == correctDirection) { UI_Manager.OnGivingFeeback?.Invoke("Correct!"); }
        else UI_Manager.OnGivingFeeback?.Invoke("Wrong Direction!");

        player = null;
        Debug.Log("direzione scelta");
    }

}
