using System;

using System.Collections.Generic;

using UnityEngine;

public class CrossRoadPoint : MonoBehaviour, ISelectable
{
    
    public Action<Direction> OnChoiceMade;

    [SerializeField] Transform wayWest;
    [SerializeField] Transform wayEast;
    [SerializeField] Transform wayNorth;
    [SerializeField] Transform waySouth;
    [SerializeField] Direction correctDirectionOnCardinals; // to set wich is the correct direction assuming we are looking north
    Direction correctRelativeDirection;


    Transform relativeRight;
    Transform relativeLeft;
    Transform relativeStraight;


    Entrance orientation;
    IDrivable player;

    static readonly Dictionary<(Entrance, Direction), Direction> directionMapping = new Dictionary<(Entrance, Direction), Direction> // pairs the orientation+cardinal to the relative correct direction
    {
        { (Entrance.EAST, Direction.LEFT), Direction.STRAIGHT },
        { (Entrance.EAST, Direction.RIGHT), Direction.LEFT },
        { (Entrance.EAST, Direction.STRAIGHT), Direction.RIGHT },

        { (Entrance.NORTH, Direction.LEFT), Direction.RIGHT },
        { (Entrance.NORTH, Direction.RIGHT), Direction.LEFT },
        { (Entrance.NORTH, Direction.STRAIGHT), Direction.STRAIGHT },

        { (Entrance.SOUTH, Direction.LEFT), Direction.LEFT },
        { (Entrance.SOUTH, Direction.RIGHT), Direction.RIGHT },
        { (Entrance.SOUTH, Direction.STRAIGHT), Direction.STRAIGHT },

        { (Entrance.WEST, Direction.LEFT), Direction.RIGHT },
        { (Entrance.WEST, Direction.RIGHT), Direction.STRAIGHT },
        { (Entrance.WEST, Direction.STRAIGHT), Direction.LEFT }
    };



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
            direction = transform.InverseTransformDirection(direction); // converts the transform from world to local


            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                orientation = direction.x > 0 ? Entrance.EAST : Entrance.WEST;
            else
                orientation = direction.z > 0 ? Entrance.NORTH : Entrance.SOUTH;

            player = playerInTrigger;

            ConvertDirections();

            UI_Manager.instance.ShowButtonsAvailable(relativeLeft, relativeStraight, relativeRight, SetChosenDirection);

            Debug.Log($"Il player è entrato da: {orientation}");

        }
    }

    void ConvertDirections()
    {
        // reset every variables
        relativeLeft = null;
        relativeRight = null;
        relativeStraight = null;

        switch (orientation) // convert the cardianal transform to relative-to-player
        {
            case Entrance.EAST:
                relativeLeft = waySouth != null ? waySouth : null;
                relativeRight = wayNorth != null ? wayNorth : null;
                relativeStraight = wayWest != null ? wayWest : null;
                break;

            case Entrance.NORTH:
                relativeLeft = wayEast != null ? wayEast : null;
                relativeRight = wayWest != null ? wayWest : null;
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
        if (directionMapping.TryGetValue((orientation, correctDirectionOnCardinals), out Direction mappedDirection))
        {
            correctRelativeDirection = mappedDirection;
        }
        

        

    }



    public void SetChosenDirection(Direction chosenDirection) // get the input back and set the new agent relative destination
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

        if (chosenDirection == correctRelativeDirection)
        {
            UI_Manager.OnGivingFeeback?.Invoke("Correct!");
        }
        else
        {
            UI_Manager.OnGivingFeeback?.Invoke("Wrong Direction!");
        }

        player = null;
    }
        

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        List<Transform> drawable = new List<Transform> { wayEast, wayWest, wayNorth, waySouth };
        foreach (Transform t in drawable)
        {
            if (t != null) Gizmos.DrawLine(transform.position, t.position);
        }
    }
#endif

}
