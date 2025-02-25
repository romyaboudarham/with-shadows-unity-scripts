using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

/*
 *  Collision Between GameObjects
 *  -----------------------------
 *  Allows interaction when two game objects collide
 *  1. Both gameobjects need colliders
 *  2. At least one gameobject needs Rigidbody
 *    - collision (objects are solid)
 *    - trigger   (objects are pass thru)
*/
public class HandsCollisionStopAlarm : MonoBehaviour
{
    public GameObject hands;
    public GameObject alarmClockDir;
    public GameObject clickPhotoDir;
    public AudioSource alarmSound;
    public AudioSource switchOffSound;
    public GameObject photoCanvas;

    /*
    *  OnCollisionEnter()
    *  Detects collision and performs actions when collisions occur
    *  If collision
    *  - turn off alarm sound * text direction
    *  - turn on photo direction, set photo to active (clickable)
    */
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{this.name} CollisionEnter {collision.gameObject.name}");

        if (collision.gameObject == hands)
        {
            switchOffSound.Play();
            alarmSound.Stop();
            clickPhotoDir.SetActive(true);
            photoCanvas.SetActive(true);
            alarmClockDir.SetActive(false);
        }
    }
}
