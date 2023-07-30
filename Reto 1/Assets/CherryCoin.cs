using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCoin : MonoBehaviour
{
    public AudioSource cherry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Move ctr = other.gameObject.GetComponent<Move>();
            if (ctr != null)
            {
                ctr.cherrycoins();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            cherry.Play();
            Destroy(gameObject);
        }

    }
}
