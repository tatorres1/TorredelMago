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
        // Asegurarse de que solo haya una instancia de ScoreManager
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
        StartCoroutine(UpdateScoreCoroutine(playerId, score));
    }

    private IEnumerator UpdateScoreCoroutine(string playerId, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", playerId);
        form.AddField("score", score.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(updateScoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score updated successfully");
            }
            else
            {
                Debug.LogError("Error updating score: " + www.error);
            }
        }
    }
}
