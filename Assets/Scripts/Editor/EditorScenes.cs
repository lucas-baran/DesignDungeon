using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

// Unload scenes except active scene before Play Mode
// Load them back when enter Editor Mode 

[InitializeOnLoad]
public static class EditorScenes
{
    // -- PROPERTIES

    private static Scene _activeScene => SceneManager.GetActiveScene();

    // -- CONSTRUCTORS

    static EditorScenes()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    // -- METHODS

    private static void OnPlayModeStateChanged( PlayModeStateChange state )
    {
        switch( state )
        {
            case PlayModeStateChange.EnteredEditMode:
                OpenScenes();
                break;

            case PlayModeStateChange.ExitingEditMode:
                CloseScenes();
                break;

            default:
                break;
        }
    }

    private static void OpenScenes()
    {
        for( int i = 0; i < SceneManager.sceneCount; i++ )
        {
            Scene scene = SceneManager.GetSceneAt( i );

            if( !IsActiveScene( scene ) )
            {
                OpenScene( scene );
            }
        }
    }

    private static void CloseScenes()
    {
        for( int i = 0; i < SceneManager.sceneCount; i++ )
        {
            Scene scene = SceneManager.GetSceneAt( i );

            if( !IsActiveScene( scene ) )
            {
                CloseScene( scene );
            }
        }
    }

    private static void OpenScene( Scene scene )
    {
        EditorSceneManager.OpenScene( scene.path, OpenSceneMode.Additive );
    }

    private static void CloseScene( Scene scene )
    {
        EditorSceneManager.CloseScene( scene, false );
    }

    private static bool IsActiveScene( Scene scene )
    {
        return scene == _activeScene;
    }
}
