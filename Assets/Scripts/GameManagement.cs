using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;

public class GameManagement : MonoBehaviour {

	//private BannerView banner;

	private static bool isPaused = false;
	public static bool secondChance = true;
	public static bool IsPaused { 
		get {return isPaused; } 
		set {isPaused = value; }
	}
	
	static AudioSource audioSource;
	static AudioClip crash;
	static GameObject objGameOver;
	static GameObject objGameOverAds;

	static bool isPortuguese; 
	
	void Start(){
		isPortuguese = Application.systemLanguage == SystemLanguage.Portuguese;

		audioSource = GetComponent<AudioSource>();
		crash = Resources.Load("Crash") as AudioClip;

		if(isPortuguese) TranslateToPortuguese();

		if(CurrentScene() == "Game"){
			objGameOver = GameObject.Find("GameOver");
			objGameOverAds = GameObject.Find("GameOverAds");
			objGameOver.SetActive(false);
			objGameOverAds.SetActive(false);
		}
		if(CurrentScene() == "Menu"){
			GooglePlayGame.Init();
			GooglePlayGame.Login(success => {});
			GameObject txtBestScore = GameObject.Find("BestScore");
        	txtBestScore.GetComponent<TextMesh>().text += PlayerPrefs.GetInt("highscore"); 
			//RequestBanner();
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GoToMenu();
		}
	}

	public static void GameOver(){
		isPaused = true;
		if(secondChance) {
			objGameOverAds.SetActive(true);	
		} else { 
			objGameOver.SetActive(true);
		}
		SubmitHighScore();
	}

	static void SubmitHighScore(){
		if(Player.Score > PlayerPrefs.GetInt("highscore")){
			PlayerPrefs.SetInt("highscore", Player.Score);
			GooglePlayGame.ReportScore("CgkIqvODj6IWEAIQAA", Player.Score, success => {});
		}
	}

	void TranslateToPortuguese(){
		if(CurrentScene() == "Menu"){
			GameObject title = GameObject.Find("Title");
			title.GetComponent<TextMesh>().text = "Pior Jogo do Mundo";
			GameObject bestScore = GameObject.Find("BestScore");
			bestScore.GetComponent<TextMesh>().text = "Maior pontuacao: ";
		}
		if(CurrentScene() == "Game"){
			GameObject restart1 = GameObject.Find("Restart1");
			restart1.GetComponent<TextMesh>().text = "Recomecar";
			GameObject restart2 = GameObject.Find("Restart2");
			restart2.GetComponent<TextMesh>().text = "Recomecar";
			GameObject text = GameObject.Find("Text");
			text.GetComponent<TextMesh>().text = "Assista um video para\nmanter pontuacao";
			GameObject watch = GameObject.Find("Watch");
			watch.GetComponent<TextMesh>().text = "Assistir";
		}
	}

	public static void SwitchAudio(){
		audioSource.Stop ();
		audioSource.clip = crash;
		audioSource.Play ();
	}

	void GoToMenu(){
		Player.Score = 0;
		SceneManager.LoadScene (0);
	}

	string CurrentScene(){
		return SceneManager.GetActiveScene().name;
	}
	
	/* 
	void RequestBanner(){
		banner = new BannerView("ca-app-pub-1541045839364233/3344086476", AdSize.Banner, AdPosition.Bottom);
		banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		AdRequest request = new AdRequest.Builder().Build();
		banner.LoadAd(request);
		banner.Show();
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		//GameObject bestScore = GameObject.Find("BestScore");
		//bestScore.GetComponent<TextMesh>().text += "f";
		RequestBanner();
	} 
	 */

}
