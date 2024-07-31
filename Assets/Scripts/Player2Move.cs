using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class Player2Move : MonoBehaviour
{
    public float runSpeed = 4;
    public float rotationSpeed = 250;
    public Animator animator2;
    private float x, y;
    private ScoreManager scoreManager;
    public int score = 0;
    private int winScore = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (score >= winScore)
        {
            WinGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //x = Input.GetAxis("Horizontal");
        //y = Input.GetAxis("Vertical");
        x = (Input.GetKey(KeyCode.A) ? -1 : (Input.GetKey(KeyCode.D) ? 1 : 0));
        y = (Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0));

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        animator2.SetFloat("VelX", x);
        animator2.SetFloat("VelY", y);
    }
    public void CollectItem()
    {
        score++;
        // Actualiza la puntuación en la base de datos
        ScoreManager.Instance.UpdateScore(PhotonNetwork.LocalPlayer.ActorNumber.ToString(), score);
    }

    private void WinGame()
    {
        // Lógica para ganar el juego, por ejemplo, cargar una escena de victoria
        Debug.Log("ganste!");
        SceneManager.LoadScene("VictoryScene"); // Asegúrate de que esta escena exista
    }
}
