using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class Chapter3Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayGray;
    public RawImage imgDisplayBlur;

    // Start is called before the first frame update
    void Start()
    {
        // load image
        Texture2D inputTexture = Resources.Load("test") as Texture2D;
        // Create an empty Mat
        Mat img = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC4);
        // Convert Texture to Mat
        Utils.texture2DToMat(inputTexture, img);

        /////////////////////////

        Mat imgResize = new Mat();
        Mat imgCrop = new Mat();

        // resize
        Size newSize = new Size(85, 85);
        Imgproc.resize(img, imgResize, newSize, 0, 0);

        // Crop
        OpenCVForUnity.CoreModule.Rect rectCrop = new OpenCVForUnity.CoreModule.Rect(250, 250, 300, 300);
        imgCrop = new Mat(img, rectCrop);



        /////////////////////////

        // create new 2D Texture
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D resizeTexture = new Texture2D((int) newSize.width, (int) newSize.height, TextureFormat.RGBA32, false);
        Texture2D cropTexture = new Texture2D(300, 300, TextureFormat.RGBA32, false);

        // convert mat to texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgResize, resizeTexture);
        Utils.matToTexture2D(imgCrop, cropTexture, true, 1);

        // display the texture on the raw image
        imgDisplay.texture = outputTexture;
        imgDisplayGray.texture = resizeTexture;
        imgDisplayBlur.texture = cropTexture;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
