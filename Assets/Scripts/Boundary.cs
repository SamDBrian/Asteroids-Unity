using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    public Camera Cam;
    
    void Start(){
        var aspect = (float)Screen.width / Screen.height;
        var orthoSize = Cam.orthographicSize;
 
        var width = 2.0f * orthoSize * aspect;
        var height = 2.0f * Cam.orthographicSize;

        this.transform.localScale = new Vector2(width, height);
    }

    private void OnTriggerExit2D(Collider2D collision){
        GameObject ShipOrAsteroid = collision.gameObject;
        float EdgeOfScreenX = this.transform.localScale.x / 2;
        float EdgeOfScreenY = this.transform.localScale.y / 2;

        if (ShipOrAsteroid.transform.position.x >= EdgeOfScreenX || ShipOrAsteroid.transform.position.x <= -EdgeOfScreenX){
            ShipOrAsteroid.transform.position = new Vector3(ShipOrAsteroid.transform.position.x * -0.95f, ShipOrAsteroid.transform.position.y, ShipOrAsteroid.transform.position.z);
        } else if (ShipOrAsteroid.transform.position.y >= EdgeOfScreenY || ShipOrAsteroid.transform.position.y <= -EdgeOfScreenY){ 
            ShipOrAsteroid.transform.position = new Vector3(ShipOrAsteroid.transform.position.x, ShipOrAsteroid.transform.position.y * -0.95f, ShipOrAsteroid.transform.position.z);
        }
    }
}
