using UnityEngine;
//Camera.cs was created from memory of the from the camera script taught in roll a ball, changes have been made to make it fit this game and the 2D environment
//Link to roll a ball: https://learn.unity.com/course/roll-a-ball/tutorial/moving-the-camera-1?version=6.3
public class Camera : MonoBehaviour
{
    private Vector3 comparison;
    public GameObject playerChar;
    private Vector3 tempVector;
    void Start()
    {
        //At the start, the temporary vector is set to the position of the cameras transform component.
        tempVector = transform.position;
        /*The comparison vector's x value is set to the result of the current camera's position minus the player chars x position*/
        comparison.x = transform.position.x - playerChar.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //The temp vector's x is then set to the result of the current player's x position plus the comparison that was just made, allowing the x positon of the camera to follow the player.
        tempVector.x = playerChar.transform.position.x + comparison.x;
        //The transform position finalises this, the camera now follows the player.
        transform.position = tempVector;
    }
}
