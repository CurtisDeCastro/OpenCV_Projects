using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class Chapter4Script : MonoBehaviour
{
    public RawImage imgDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Mat img = new Mat(512, 512, CvType.CV_8UC3, new Scalar(255, 255, 255));

        // Draw Circle
        Imgproc.circle(img, new Point(256, 256), 150, new Scalar(255, 69, 0), Imgproc.FILLED);
        // Draw Rectangle
        Imgproc.rectangle(img, new Point(130, 226), new Point(382, 286),new Scalar(255, 255, 255), Imgproc.FILLED);
        // Draw Line
        Imgproc.line(img, new Point(130, 296), new Point(382, 296), new Scalar(255, 255, 255), 2);
        // Text
        Imgproc.putText(img, "Curtis' App", new Point(137, 262), Imgproc.FONT_HERSHEY_DUPLEX, 0.75, new Scalar(255, 69, 0), 2);

        // create new 2D Texture
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        // convert mat to texture
        Utils.matToTexture2D(img, outputTexture);
        // display the texture on the raw image
        imgDisplay.texture = outputTexture;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
