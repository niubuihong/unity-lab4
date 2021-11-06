using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    private int _health = 3;
    public int ObstacleHealth
    {
        get { return _health; }
        private set
        {
            _health = value;
            if (_health <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Obstacle destroyed.");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Obstacle being hitted");
        // Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Blast(Clone)")
        {
            ObstacleHealth -= 1;
            Debug.Log("Critical hit!");
        }
    }
}
