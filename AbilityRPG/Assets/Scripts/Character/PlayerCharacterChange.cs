using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterChange : MonoBehaviour
{
    public List<GameObject> characterList;

    // 현재 위치
    public GameObject nowPlayer;

    public FadeManager fadeManager;

    public Joystick joystick;

    private void Awake()
    {
        fadeManager = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<FadeManager>();
        nowPlayer = GameObject.FindGameObjectWithTag("Player");

        joystick = FindObjectOfType<Joystick>();
    }

    // 카메라 연결 해줘야함.
    private void Start()
    {
        if (Camera.main.GetComponent<CameraMovement>().Player != null)
        {
            Debug.Log("카메라 접근 테스트");
            Debug.Log(Camera.main.transform.position);
        }
    }

    public void CharacterChange(int number)
    {
        if (characterList.Count == 0 || number < 0 || characterList.Count <= number)
            return;

        nowPlayer.SetActive(false);

        joystick.gameObject.SetActive(true);
        GameObject newPlayer = Instantiate(characterList[number], nowPlayer.transform.position, nowPlayer.transform.rotation);
        newPlayer.GetComponent<PlayerController>().PlayerSetting();
        Camera.main.GetComponent<CameraMovement>().Player = newPlayer;
        nowPlayer = newPlayer;
    }

    public void test()
    {
        Debug.Log("qwer");
    }
}
