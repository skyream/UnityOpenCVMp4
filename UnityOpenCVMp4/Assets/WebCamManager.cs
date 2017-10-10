using UnityEngine;

public class WebCamManager : MonoBehaviour
{
    private  int index;
    private WebCamTexture webcamTexture;
    private Texture2D texture2D;
    private bool isOk;
    void Start()
    {
         webcamTexture = new WebCamTexture();
        if (WebCamTexture.devices.Length > 0)
        {
            webcamTexture.deviceName = WebCamTexture.devices[0].name;
            webcamTexture.Play();
            int width = webcamTexture.width;
            int height = webcamTexture.height;
            isOk = Record.MPEGInitDataToC("Test.mp4", 24, webcamTexture.width, webcamTexture.height);
            Debug.Log("初始化:" + isOk);
            texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);
        }
    }
    private byte[] ByteByColors(Color[] colors)
    {
        byte[] bytes = new byte[colors.Length * 3];
        for (int i = 0; i < colors.Length; i++)
        {
            bytes[i * 3 + 0] = (byte)(colors[i].r * 255);
            bytes[i * 3 + 1] = (byte)(colors[i].g * 255);
            bytes[i * 3 + 2] = (byte)(colors[i].b * 255);
        }
        return bytes;
    }
    private void Update()
    {
        if (isOk)
        {
            int width = webcamTexture.width;
            int height = webcamTexture.height;
            texture2D.SetPixels(webcamTexture.GetPixels());
            texture2D.Apply();
            index++;
            if (index < 300)
            {
                byte[] arr = texture2D.GetRawTextureData();// ByteByColors(webcamTexture.GetPixels());
                Record.MPEGDataToC(arr);
            }

            else if (index == 300)
            {
                Record.MPEGReleaseToC();
            }

        }
    }
}
