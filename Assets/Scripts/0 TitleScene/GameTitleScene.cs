using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 이 스크립트는 게임 시작 - 로고 - 로그인 대기 - 데이터 확인 - 패치 - 로그인 - 로딩 - 게임을 시작하려면 버튼까지 구현하는 스크립트입니다
 해당 상태는 enum값으로 제어합니다
 */


public class GameTitleScene : MonoBehaviour
{
	/*
		타이틀 씬의 상태를 제어하는 변수입니다.
		FadeIn_Logo = 0		페이드 인되며 로고가 보여짐 (일단 1개)
		LogoWait,			로고 보여지는 대기 시간
		FadeOut_Logo		페이드 아웃 로고
		WaitLogIn			로그인 대기
		AppVersion_Check	앱 버전 체크
		DataFile_Check		데이터 파일 체크
		DownLoad_File		리소스 다운로드 체크 - 와이파이 어쩌구 - 다운로드 - 리소스 다운 검증 체크
		Button_Wait			게임을 시작하려면 버튼을 눌러주세요
	 */
	private enum TITLESCENE_STATE : byte
	{
		FadeIn_Logo = 0,
		WaitLogo,
		FadeOut_Logo,
		WaitLogIn,
		AppVersion_Check,
		DataFile_Check,
		DownLoad_File,
		Button_Wait
	}
	private TITLESCENE_STATE titleSceneState = TITLESCENE_STATE.FadeIn_Logo;

	private enum TITLESCENE_UI_TAG : int
	{
		LogIn_Button = 0,
		Application_Page,
		DataFile_Page,
		Resources_Page,
		ButtonWait,
		ApplicationQuit
	}

	// FadeIn or Out에 쓸 공용 변수
	//[SerializeField] private UnityEngine.UI.Image hideImage;
	[SerializeField] private Image hideImage;

	// === FadeIn_Logo 변수들 ===
	private float waitFadeInLogoTime = 2.0f;
	// ====

	// === LogoWait 변수
	private float waitLogoTime = 1.0f;
	// ===

	// === FadeOut_Logo 
	private float waitFadeOutLogoTime = 2.0f;

	// 게임 오브젝트를 활성화 시켜 UI를 보여주는 방식
	[SerializeField] private GameObject[] uiList;

	private void Start()
	{
		foreach (var item in uiList)
		{
			item.SetActive(false);
		}
		hideImage.color = Color.black;
		hideImage.gameObject.SetActive(true);

		StartCoroutine(FadeIn());
	}


	/// <summary>
	/// 페이드 인 시간이 끝나면 코루틴을 실행하여 로고 대기로 이동합니다.
	/// </summary>
	private IEnumerator FadeIn()
	{
		float _timerPercentage = 1.0f;
		float _nowTime = 0.0f;
		float _dTime = 0.0f;

		while(_nowTime < waitFadeInLogoTime)
		{
			_dTime = Time.deltaTime;
			_timerPercentage = _nowTime / waitFadeInLogoTime;

			hideImage.color = new UnityEngine.Color(0.0f,0.0f,0.0f, 1- _timerPercentage);
			_nowTime += _dTime;
			//Debug.Log("nowTime : " + _nowTime + "\n _timer Percentage = " + _timerPercentage );
			yield return null;
		}

		hideImage.color = new UnityEngine.Color(0, 0, 0, 0.0f);
		titleSceneState = TITLESCENE_STATE.WaitLogo;
		Debug.Log("FadeIn To LogoWait");
		StartCoroutine(WaitLogo());
	}


	/// <summary>
	/// 로고 대기가 끝나면 페이드 아웃 코루틴으로 넘어갑니다.
	/// </summary>
	/// <returns></returns>
	private IEnumerator WaitLogo()
	{
		float _nowTime = 0.0f;
		float _dTime = 0.0f;

		while(_nowTime < waitLogoTime)
		{
			_dTime = Time.deltaTime;

			_nowTime += _dTime;
			//Debug.Log("nowTime : " + _nowTime);
			yield return null;
		}

		titleSceneState = TITLESCENE_STATE.FadeOut_Logo;
		Debug.Log("LogoWait To FadeOut");
		StartCoroutine(FadeOut());
	}


	/// <summary>
	/// 페이드 아웃이 끝나면 update에서 체크하여 
	/// </summary>
	/// <returns></returns>
	private IEnumerator FadeOut()
	{
		float _timerPercentage = 1.0f;
		float _nowTime = 0.0f;
		float _dTime = 0.0f;

		while(_nowTime < waitFadeOutLogoTime)
		{
			_dTime = Time.deltaTime;
			_timerPercentage = _nowTime / waitFadeOutLogoTime;

			hideImage.color = new UnityEngine.Color(0.0f, 0.0f, 0.0f, _timerPercentage);
			_nowTime += _dTime;
			//Debug.Log("nowTime : " + _nowTime + "\n _timer Percentage = " + _timerPercentage );
			yield return null;
		}
		titleSceneState = TITLESCENE_STATE.WaitLogIn;

		// 임시 연출
		uiList[(int)TITLESCENE_UI_TAG.LogIn_Button].SetActive(true);
		uiList[(int)TITLESCENE_UI_TAG.ApplicationQuit].SetActive(true);
		Destroy(hideImage);
		StopAllCoroutines();
	}	

	public void BtnLogIn()
	{
		uiList[(int)TITLESCENE_UI_TAG.Application_Page].SetActive(true);
		titleSceneState = TITLESCENE_STATE.DataFile_Check;
		Debug.Log("logIn Complete \n State =  TITLESCENE_STATE.AppVersion_Check");
	}


	public void BtnApplicationQuit()
	{
		Application.Quit();
	}

	/// <summary>
	/// 나중에 데이터 세이브 같은 코드가 들어가야함
	/// </summary>
	public void OnApplicationQuit()
	{
		
	}

}