using System.Runtime.InteropServices;

public class Record
{
    [DllImport("OpencvVideoWriter")]
    private static extern bool MPEGInitData(byte[] path, int fps,int width, int height);
    [DllImport("OpencvVideoWriter")]
    private static extern void MPEGData(byte[] imageData);
    [DllImport("OpencvVideoWriter")]
    private static extern void MPEGRelease();

    /// <summary>
    /// 录制初始化
    /// </summary>
    public static bool MPEGInitDataToC(string path, int fps, int width, int height)
    {
        byte[] nameArr = System.Text.Encoding.Default.GetBytes(path);
        bool isOk = MPEGInitData(nameArr,fps, width, height);
        return isOk;
    }
    /// <summary>
    /// 更新视频
    /// </summary>
    /// <param name="bytes"></param>
    public static void MPEGDataToC(byte[] bytes)
    {
        MPEGData(bytes);
    }
    /// <summary>
    /// 录制完成
    /// </summary>
    public static void MPEGReleaseToC()
    {
        MPEGRelease();
    }
}
