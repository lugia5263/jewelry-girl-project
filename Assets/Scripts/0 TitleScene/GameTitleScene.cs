using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 �� ��ũ��Ʈ�� ���� ���� - �ΰ� - �α��� ��� - ������ Ȯ�� - ��ġ - �α��� - �ε� - ������ �����Ϸ��� ��ư���� �����ϴ� ��ũ��Ʈ�Դϴ�
 �ش� ���´� enum������ �����մϴ�
 */


public class GameTitleScene : MonoBehaviour
{
	/*
		Ÿ��Ʋ ���� ���¸� �����ϴ� �����Դϴ�.
		FadeIn_Logo = 0		���̵� �εǸ� �ΰ� ������ (�ϴ� 1��)
		LogoWait,			�ΰ� �������� ��� �ð�
		FadeOut_Logo		���̵� �ƿ� �ΰ�
		WaitLogIn			�α��� ���
		AppVersion_Check	�� ���� üũ
		DataFile_Check		������ ���� üũ
		DownLoad_File		���ҽ� �ٿ�ε� üũ - �������� ��¼�� - �ٿ�ε� - ���ҽ� �ٿ� ���� üũ
		Button_Wait			������ �����Ϸ��� ��ư�� �����ּ���
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

	// FadeIn or Out�� �� ���� ����
	//[SerializeField] private UnityEngine.UI.Image hideImage;
	[SerializeField] private Image hideImage;

	// === FadeIn_Logo ������ ===
	private float waitFadeInLogoTime = 2.0f;
	// ====

	// === LogoWait ����
	private float waitLogoTime = 1.0f;
	// ===

	// === FadeOut_Logo 
	private float waitFadeOutLogoTime = 2.0f;

	// ���� ������Ʈ�� Ȱ��ȭ ���� UI�� �����ִ� ���
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
	/// ���̵� �� �ð��� ������ �ڷ�ƾ�� �����Ͽ� �ΰ� ���� �̵��մϴ�.
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
	/// �ΰ� ��Ⱑ ������ ���̵� �ƿ� �ڷ�ƾ���� �Ѿ�ϴ�.
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
	/// ���̵� �ƿ��� ������ update���� üũ�Ͽ� 
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

		// �ӽ� ����
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
	/// ���߿� ������ ���̺� ���� �ڵ尡 ������
	/// </summary>
	public void OnApplicationQuit()
	{
		
	}

}