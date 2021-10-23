using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip attack1D;
    public AudioClip attack1H;
    public AudioClip attack2D;
    public AudioClip attack2H;
    public AudioClip attack3D;
    public AudioClip attack3H;
    public AudioClip jumpattackD;
    public AudioClip Hit;
    public AudioClip air;

    AudioSource myAudio;
    public static SoundManager instance;

    void Awake()  // Start함수보다 먼저 호출됨
    {
        if (SoundManager.instance == null)  //게임시작했을때 이 instance가 없을때
            SoundManager.instance = this;  // instance를 생성
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();  //myAudio에 컴퍼넌트에있는 AudioSource넣기

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundAttack1()
    {
        myAudio.PlayOneShot(attack1D);
        myAudio.PlayOneShot(attack1H);
    }
    public void PlaySoundAttack2()
    {
        myAudio.PlayOneShot(attack2D);
        myAudio.PlayOneShot(attack2H);
    }
    public void PlaySoundAttack3()
    {
        myAudio.PlayOneShot(attack3D);
        myAudio.PlayOneShot(attack3H);
    }
    public void PlaySoundJumpattackDamaged()
    {
        myAudio.PlayOneShot(jumpattackD);
    }
    public void PlaySoundHit()
    {
        myAudio.PlayOneShot(Hit);
    }
    public void PlaySoundAir()
    {
        myAudio.PlayOneShot(air);
    }
}
