using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunTimeScreenShot : MonoBehaviour
{
    public InputField inputWidth;
    public InputField inputHeight;

    int resWidth = Screen.width * 4;
    int resHeight = Screen.height * 4;

    public Camera myCamera;
    int scale = 1;

    public GameObject[] nonRenderables;

    string path = "";
    RenderTexture renderTexture;

    bool isTransparent = true;

    void Start()
    {
        myCamera = Camera.main;
        path = Path.GetFullPath(".");
    }

    public string ScreenShotName(int width, int height)
    {
        string strPath = "";
        strPath = string.Format("{0}/screen_{1}x{2}_{3}.png",
                             path,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        return strPath;
    }

    public void TakeHiResShot()
    {
        if (inputWidth.text != "")
        {
            resWidth = int.Parse(inputWidth.text);
        }

        if (inputHeight.text != "")
        {
            resHeight = int.Parse(inputHeight.text);
        }

        int resWidthN = resWidth * scale;
        int resHeightN = resHeight * scale;
        RenderTexture rt = new RenderTexture(resWidthN, resHeightN, 24);
        myCamera.targetTexture = rt;

        TextureFormat tFormat;
        if (isTransparent)
            tFormat = TextureFormat.ARGB32;
        else
            tFormat = TextureFormat.RGB24;

        Texture2D screenShot = new Texture2D(resWidthN, resHeightN, tFormat, false);
        myCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidthN, resHeightN), 0, 0);
        myCamera.targetTexture = null;
        RenderTexture.active = null;
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidthN, resHeightN);
        System.IO.File.WriteAllBytes(filename, bytes);
        Application.OpenURL(filename);
    }
}