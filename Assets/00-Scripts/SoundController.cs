using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private TimerDirector timerDirector;
    private bool playAlarm = false;

    void Start()
    {
        timerDirector = GameObject.Find("Timer").GetComponent<TimerDirector>();
    }

    void Update()
    {
        TimeUpAlarm();

        if(Input.GetKeyDown(KeyCode.A) == true){
            Singleton<SoundPlayer>.instance.playBGM( "se001", 3.0f );
        }
    }

    public void TimeUpAlarm()
    {
        if(timerDirector.timeUp && !playAlarm)
        {
            playAlarm = true;
            Singleton<SoundPlayer>.instance.playSE("se001");
        }

        if(!timerDirector.timeUp)
            playAlarm = false;
        
    }
}

//シングルトン（T：型）
public class Singleton<T> where T : class, new() {
    static T obj = null;

    Singleton(){}

    public static T instance {
        get {
            if( obj == null )
                obj = new T();
            return obj;
        }
    }
}

//サウンド再生機能
public class SoundPlayer
{
    GameObject soundPlayerObj;
    AudioSource audioSource;

    BGMPlayer curBGMPlayer;
    BGMPlayer fadeOutBGMPlayer;

    //音源を登録していく配列
    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();

    //ファイル名と変数名の紐づけを行う
    class AudioClipInfo{
        public string resourceName;
        public string name;
        public AudioClip clip;

        public AudioClipInfo( string resourceName, string name){
            this.resourceName = resourceName;
            this.name = name;
        }
    }

    //音源を登録する
    public SoundPlayer(){
        audioClips.Add("se001", new AudioClipInfo("Alarm01", "se001"));
        audioClips.Add("bgm001", new AudioClipInfo("BGM_fire01", "bgm001"));
    }

    //SE再生
    public bool playSE( string seName ){
        if( audioClips.ContainsKey( seName ) == false )
            return false; //not register
        
        AudioClipInfo info = audioClips[seName];

        //Load
        if(info.clip == null)
            info.clip = (AudioClip)Resources.Load( info.resourceName );
        
        if(soundPlayerObj == null){
            soundPlayerObj = new GameObject("SoundPlayer"); //引数はゲームオブジェクトの命名
            audioSource = soundPlayerObj.AddComponent<AudioSource>();
        }

        //play SE
        audioSource.loop = true;
        audioSource.PlayOneShot( info.clip );

        return true;
    }

    //BGM再生
    public void playBGM( string bgmName, float fadeTime ) {
        // destory old BGM
        if ( fadeOutBGMPlayer != null )
            fadeOutBGMPlayer.destory();

        // change to fade out for current BGM
        if ( curBGMPlayer != null ) {
            curBGMPlayer.stopBGM( fadeTime );
            fadeOutBGMPlayer = curBGMPlayer;
        }

        // play new BGM
        if ( audioClips.ContainsKey( bgmName ) == false ) {
            // null BGM
            curBGMPlayer = new BGMPlayer();
        } else {
            curBGMPlayer = new BGMPlayer( audioClips[ bgmName ].resourceName );
            curBGMPlayer.playBGM( fadeTime );
        }
    }

    public void playBGM() {
        if ( curBGMPlayer != null )
            curBGMPlayer.playBGM();
        if ( fadeOutBGMPlayer != null )
            fadeOutBGMPlayer.playBGM();
    }

    public void pauseBGM() {
        if ( curBGMPlayer != null )
            curBGMPlayer.pauseBGM();
        if ( fadeOutBGMPlayer != null )
            fadeOutBGMPlayer.pauseBGM();
    }

    public void stopBGM( float fadeTime ) {
        if ( curBGMPlayer != null )
            curBGMPlayer.stopBGM( fadeTime );
        if ( fadeOutBGMPlayer != null )
            fadeOutBGMPlayer.stopBGM( fadeTime );
    }
}

//BGM周辺
class BGMPlayer {
    // State
    class State {
        protected BGMPlayer bgmPlayer;
        public State( BGMPlayer bgmPlayer ) {
            this.bgmPlayer = bgmPlayer;
        }
        public virtual void playBGM() {}
        public virtual void pauseBGM() {}
        public virtual void stopBGM() {}
        public virtual void update() {}
    }

    class Wait : State {

        public Wait( BGMPlayer bgmPlayer ) : base( bgmPlayer ) {}

        public override void playBGM() {
            if ( bgmPlayer.fadeInTime > 0.0f )
                bgmPlayer.state = new FadeIn( bgmPlayer );
            else
                bgmPlayer.state = new Playing( bgmPlayer );
        }
    }

    class FadeIn : State {

        float t = 0.0f;

        public FadeIn( BGMPlayer bgmPlayer ) : base( bgmPlayer ) {
            bgmPlayer.source.Play();
            bgmPlayer.source.volume = 0.0f;
        }

        public override void pauseBGM() {
            bgmPlayer.state = new Pause( bgmPlayer, this );
        }

        public override void stopBGM() {
            bgmPlayer.state = new FadeOut( bgmPlayer );
        }

        public override void update() {
            t += Time.deltaTime;
            bgmPlayer.source.volume = t / bgmPlayer.fadeInTime;
            if ( t >= bgmPlayer.fadeInTime ) {
            bgmPlayer.source.volume = 1.0f;
            bgmPlayer.state = new Playing( bgmPlayer );
            }
        }
    }

    class Playing : State {

        public Playing( BGMPlayer bgmPlayer ) : base( bgmPlayer ) {
            if ( bgmPlayer.source.isPlaying == false ) {
                bgmPlayer.source.volume = 1.0f;
                bgmPlayer.source.Play();
            }
        }

        public override void pauseBGM() {
            bgmPlayer.state = new Pause( bgmPlayer, this );
        }

        public override void stopBGM() {
            bgmPlayer.state = new FadeOut( bgmPlayer );
        }
    }

    class Pause : State {

        State preState;

        public Pause( BGMPlayer bgmPlayer, State preState ) : base( bgmPlayer ) {
            this.preState = preState;
            bgmPlayer.source.Pause();
        }

        public override void stopBGM() {
            bgmPlayer.source.Stop();
            bgmPlayer.state = new Wait( bgmPlayer );
        }

        public override void playBGM() {
            bgmPlayer.state = preState;
            bgmPlayer.source.Play();
        }
    }

    class FadeOut : State {
        float initVolume;
        float t = 0.0f;

        public FadeOut( BGMPlayer bgmPlayer ) : base( bgmPlayer ) {
            initVolume = bgmPlayer.source.volume;
        }

        public override void pauseBGM() {
            bgmPlayer.state = new Pause( bgmPlayer, this );
        }

        public override void update() {
            t += Time.deltaTime;
            bgmPlayer.source.volume = initVolume * ( 1.0f - t / bgmPlayer.fadeOutTime );
            if ( t >= bgmPlayer.fadeOutTime ) {
                bgmPlayer.source.volume = 0.0f;
                bgmPlayer.source.Stop();
                bgmPlayer.state = new Wait( bgmPlayer );
            }
        }
    }

    GameObject obj;
    AudioSource source;
    State state;
    float fadeInTime = 0.0f;
    float fadeOutTime = 0.0f;

    public BGMPlayer() {}

    public BGMPlayer( string bgmFileName ) {
        AudioClip clip = (AudioClip)Resources.Load( bgmFileName );
        if ( clip != null ) {
            obj = new GameObject( "BGMPlayer" );
            source = obj.AddComponent<AudioSource>();
            source.clip = clip;
            state = new Wait( this );
        } else
            Debug.LogWarning( "BGM " + bgmFileName + " is not found." );
    }

    public void destory() {
        if ( source != null )
            GameObject.Destroy( obj );
    }

    public void playBGM() {
        if ( source != null ) {
            state.playBGM();
        }
    }

    public void playBGM( float fadeTime ) {
        if ( source != null ) {
            this.fadeInTime = fadeTime;
            state.playBGM();
        }
    }

    public void pauseBGM() {
        if ( source != null )
            state.pauseBGM();
    }

    public void stopBGM( float fadeTime ) {
        if ( source != null ) {
            fadeOutTime = fadeTime;
            state.stopBGM();
        }
    }

    public void update() {
        if ( source != null )
            state.update();
    }
}