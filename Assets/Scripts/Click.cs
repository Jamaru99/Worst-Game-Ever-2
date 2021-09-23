using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    void OnMouseDown(){
		transform.localScale *= 0.9f;
	}

	void OnMouseUp(){
        switch (gameObject.name)
        {
            case "Restart1":
                GameManagement.IsPaused = false;
                GameManagement.secondChance = true;
			    SceneManager.LoadScene ("Game");
                Player.Score = 0;
                break;
            case "Restart2":
                GameManagement.IsPaused = false;
                GameManagement.secondChance = true;
			    SceneManager.LoadScene ("Game");
                Player.Score = 0;
                break;
            case "Play":
                GameManagement.IsPaused = false;
                GameManagement.secondChance = true;
                SceneManager.LoadScene ("Game");
                break;
            case "Ranking":
                GooglePlayGame.ShowLeadboards();
                break;
            
        }       
	}
}
