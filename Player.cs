using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed=5f;
    [SerializeField] GameObject gameover;
    void Start()
    {
        gameover = GameObject.Find("Gameover");
        gameover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "DeathLine")
            {
                Debug.Log("遊戲結束");
                gameover.SetActive(true);
                Time.timeScale = 0f;
            }
            if (other.gameObject.tag == "Ceiling")
            {
                Debug.Log("遊戲結束");
                gameover.SetActive(true);
                Time.timeScale = 0f;
            }
        }
}
