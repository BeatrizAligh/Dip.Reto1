using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public int puntaje = 0;
    public TMP_Text text;

    public float speed = 5f;
    public float rotationSpeed = 20f;


    void Update()
    {

        text.text = puntaje.ToString();
        if (puntaje == 3760)
        {
            puntaje = 3760;
            SceneManager.LoadScene("Win");
            Debug.Log("Win");

        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();
        transform.position = transform.position + movement * speed * Time.deltaTime;

        if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
    }
    public void coins()
    {
        puntaje += 20;
    }
    public void cherrycoins()
    {
        puntaje += 100;
    }



}
