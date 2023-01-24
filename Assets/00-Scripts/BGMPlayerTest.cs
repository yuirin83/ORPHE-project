using UnityEngine;
using System.Collections;

public class BGMPlayerTest : MonoBehaviour {

    public string audioClipName = "";
    public float fadeInTime = 0.0f;
    public float fadeOutTime = 0.0f;

    BGMPlayer player;

    void OnGUI() {
        // Play
        if ( GUI.Button( new Rect( 20, 20, 60, 45 ), "Play" ) == true ) {
            if ( player == null ) {
                player = new BGMPlayer( audioClipName );
                player.playBGM( fadeInTime );
            } else
                player.playBGM();
        }

        // Pause
        if ( GUI.Button( new Rect( 80, 20, 60, 45 ), "Pause" ) == true ) {
            if ( player != null )
                player.pauseBGM();
        }

        // Stop
        if ( GUI.Button( new Rect( 140, 20, 60, 45 ), "Stop" ) == true ) {
            if ( player != null )
                player.stopBGM( fadeOutTime );
        }

        // Delete
        if ( GUI.Button( new Rect( 200, 20, 60, 45 ), "Delete" ) == true ) {
            if ( player != null ) {
                player.destory();
                player = null;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if ( player != null )
            player.update();
    }
}