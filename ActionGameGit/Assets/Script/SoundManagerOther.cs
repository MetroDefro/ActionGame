using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoundManagerOther : MonoBehaviour
{
    public AudioClip jumpattack;
    public AudioClip newWave;

    AudioSource myAudio;
    public static SoundManagerOther instance;

    void Awake()  // Start함수보다 먼저 호출됨
    {
        if (SoundManagerOther.instance == null)  //게임시작했을때 이 instance가 없을때
            SoundManagerOther.instance = this;  // instance를 생성
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();  //myAudio에 컴퍼넌트에있는 AudioSource넣기
        //PlaySoundNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundJumpattack()
    {
        myAudio.PlayOneShot(jumpattack);
    }
    public void PlaySoundNewWave()
    {
        myAudio.PlayOneShot(newWave);
        StartCoroutine("FadeVolume");
    }

    IEnumerator FadeVolume()
    {
        UnityEngine.Debug.Log(myAudio.volume);
        yield return new WaitForSeconds(3f);
        while (myAudio.volume > 0f)
        {
            UnityEngine.Debug.Log(myAudio.volume);
            myAudio.volume -= 0.019f;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(5f);
        myAudio.volume = 0.5f;
        //StartCoroutine("FadeVolume");

    }
}
