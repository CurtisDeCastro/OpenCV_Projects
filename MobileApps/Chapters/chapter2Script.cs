using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class Chapter2Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayGray;
    public RawImage imgDisplayBlur;
    public RawImage imgDisplayCanny;
    public RawImage imgDisplayDil;
    public RawImage imgDisplayErode;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D inputTexture = Resources.Load("test") as Texture2D;
        // Create an empty Mat
        Mat img = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC4);
        // Convert Texture to Mat
        Utils.texture2DToMat(inputTexture, img);

        /////////////////////////

        Mat imgGray = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgBlur = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgCanny = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgDil = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgErode = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);

        // convert image to gray scale
        Imgproc.cvtColor(img, imgGray, Imgproc.COLOR_RGBA2GRAY);
        // add blur
        Imgproc.GaussianBlur(imgGray, imgBlur, new Size(9,9), 5, 0);
        // find the edges
        Imgproc.Canny(imgBlur, imgCanny, 20, 50);
        // Increase / Decrease edge thickness
        Mat erodeElement = Imgproc.getStructuringElement(Imgproc.MORPH_CROSS, new Size(9, 9));
        Imgproc.dilate(imgCanny, imgDil, erodeElement);
        Imgproc.erode(imgDil, imgErode, erodeElement);

        /////////////////////////

        // create new 2D Texture
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D grayTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D blurTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D cannyTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D dilTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D erodeTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        // convert mat to texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgGray, grayTexture);
        Utils.matToTexture2D(imgBlur, blurTexture);
        Utils.matToTexture2D(imgCanny, cannyTexture);
        Utils.matToTexture2D(imgDil, dilTexture);
        Utils.matToTexture2D(imgErode, erodeTexture);
        // display the texture on the raw image
        imgDisplay.texture = outputTexture;
        imgDisplayGray.texture = grayTexture;
        imgDisplayBlur.texture = blurTexture;
        imgDisplayCanny.texture = cannyTexture;
        imgDisplayDil.texture = dilTexture;
        imgDisplayErode.texture = erodeTexture;

    }

    // Update is called once per frame
    void Update()
    {

    }
}


