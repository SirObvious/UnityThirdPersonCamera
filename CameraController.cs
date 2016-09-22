using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //Public References
    public Transform playerTransform;

    //Public Parameter
    public float maxDistanceToPlayer = 3f;
    public float smoothFactor = 2f; 
    public float rotationSpeed = 140f;

	void Start () {
	
	}
	
	void Update () {
        Vector2 axisInput = GetAnalogInput();
        RotationHorizontal(axisInput.x);
        RotationVertical(axisInput.y);
        TranslateToMaxDistance();
        LookAtPlayer();
	}


    private Vector2 GetAnalogInput()
    {
        float xInput = Input.GetAxisRaw("JoystickRightX");
        float yInput = Input.GetAxisRaw("JoystickRightY");
        return new Vector2(xInput, yInput);
    }

    private void RotationHorizontal(float horizontalInput)
    {
        this.transform.RotateAround(playerTransform.position, Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
    }

    private void RotationVertical(float verticalInput)
    {
        this.transform.RotateAround(playerTransform.position, this.transform.right, rotationSpeed * verticalInput * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * smoothFactor);
        //this.transform.LookAt(playerTransform.position);
    }

    private void TranslateToMaxDistance()
    {
         float distanceDelta = Vector3.Distance(this.transform.position, playerTransform.position) - maxDistanceToPlayer;
         Vector3 directionCamToPlayer = new Vector3(this.playerTransform.position.x - this.transform.position.x,
                                                    0f,
                                                    this.playerTransform.position.z - this.transform.position.z);
         if(distanceDelta > 0f)
         {
            this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + (directionCamToPlayer.normalized * distanceDelta), Time.deltaTime * smoothFactor);
         } 
    }
    


}
