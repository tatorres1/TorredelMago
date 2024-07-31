using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviourPunCallbacks
{
    public float runSpeed = 4;
    public float rotationSpeed = 250;
    public Animator animator;
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
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime*rotationSpeed, 0);
        transform.Translate(0,0, y * Time.deltaTime*runSpeed);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY",y);
    }

    public void CollectItem()
    {
        score++;
        // Actualiza la puntuación en la base de datos
        Debug.Log("nuevo puntaje: " + score);
        Debug.Log("actualizando el nuevo puntaje epara el jugador ID: " + PhotonNetwork.LocalPlayer.ActorNumber.ToString());

    }

    private void WinGame()
    {
        // Lógica para ganar el juego, por ejemplo, cargar una escena de victoria
        Debug.Log("Ganaste!");
        //SceneManager.LoadScene("VictoryScene"); // Asegúrate de que esta escena exista
    }
}
