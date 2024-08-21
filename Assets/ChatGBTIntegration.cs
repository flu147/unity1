using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq; // Make sure to add the Newtonsoft.Json package via Unity Package Manager

public class ChatGPTIntegration : MonoBehaviour
{
    // Your OpenAI API key
    private const string apiKey = "sk-8brR4ZmnEaLQBmA25BLdUqL6vRCosn1VMCKiy0V94YT3BlbkFJ0xEjzsCbBvOfyto66nXU8bxQoOUPuUVVGXSBUnhucA";
    private const string apiEndpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";

    [SerializeField] private string prompt = "Hello, ChatGPT!";
    [SerializeField] private float maxTokens = 50f;
    [SerializeField] private float temperature = 0.7f;

    void Start()
    {
        StartCoroutine(SendChatGPTRequest(prompt));
    }

    private IEnumerator SendChatGPTRequest(string prompt)
    {
        var request = new UnityWebRequest(apiEndpoint, "POST");
        var requestData = new
        {
            prompt = prompt,
            max_tokens = (int)maxTokens,
            temperature = temperature
        };
        var jsonRequestData = JsonUtility.ToJson(requestData);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            string responseText = request.downloadHandler.text;
            JObject responseJson = JObject.Parse(responseText);
            string responseTextContent = responseJson["choices"][0]["text"].ToString();

            Debug.Log($"ChatGPT Response: {responseTextContent}");
        }
    }
}
