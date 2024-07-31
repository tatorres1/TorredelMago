using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Instancia única para el acceso global
    public string updateScoreUrl = "http://localhost/mago3d_service/update_score.php"; // URL de tu script PHP

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para actualizar el puntaje en la base de datos
    public void UpdateScore(string playerId, int score)
    {
        Debug.Log("Starting UpdateScore");
        Debug.Log("Player ID: " + playerId);
        Debug.Log("Score: " + score);

        StartCoroutine(UpdateScoreCoroutine(playerId, score));
    }

    private IEnumerator UpdateScoreCoroutine(string playerId, int score)
    {
        Debug.Log("dentro del score");

        WWWForm form = new WWWForm();
        form.AddField("player_id", playerId);
        form.AddField("score", score.ToString());

        Debug.Log("Datos: player_id=" + playerId + ", score=" + score);

        using (UnityWebRequest www = UnityWebRequest.Post(updateScoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score actualizado exitoso: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error updating score: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
        }
    }
}
