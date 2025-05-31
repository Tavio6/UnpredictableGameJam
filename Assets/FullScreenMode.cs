using UnityEngine;

public class FullScreenMode : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            FScreenMode();
        }
    }

    public void FScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
