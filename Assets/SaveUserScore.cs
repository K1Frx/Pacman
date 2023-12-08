using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class SaveUserScore : MonoBehaviour
{
	public InputField ScoreInputField;

	private void Start()
	{
		Button SaveButton = GameObject.Find("SaveButton").GetComponent<Button>();
		if (SaveButton != null)
		{
			SaveButton.onClick.AddListener(SaveScore);
		}
		else
		{
			Debug.LogError("Button component not found on this object.");
		}

		ScoreInputField = GameObject.Find("ScoreInputField").GetComponent<InputField>();
		if (ScoreInputField == null)
		{
			Debug.LogError("InputField component not found on this object.");
		}
	}

	void Update() { }

	IEnumerator Upload()
	{
		using (UnityWebRequest www = UnityWebRequest.Post("http://frxx.pythonanywhere.com/post", "{ \"field1\": 1, \"field2\": 2 }", "application/json"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log("Form upload complete!");
			}
		}
	}

	void SaveScore()
	{
		if (ScoreInputField != null)
		{
			string scoreValue = ScoreInputField.text;
			Debug.Log("Saved Score: " + scoreValue);
			StartCoroutine(Upload());
		}
		else
		{
			Debug.LogError("ScoreInputField is not assigned.");
		}
	}
}
