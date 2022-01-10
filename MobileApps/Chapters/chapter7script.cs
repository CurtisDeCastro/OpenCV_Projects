using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class Chapter7Script : MonoBehaviour
{
    public RawImage imgDisplay;
    public RawImage imgDisplayEdges;
    public RawImage imgDisplayContours;

    // Start is called before the first frame update
    void Start()
    {
        // load image
        Texture2D inputTexture = Resources.Load("shapes") as Texture2D;
        // Create an empty Mat
        Mat img = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC4);
        // Convert Texture to Mat
        Utils.texture2DToMat(inputTexture, img);

        /////////////////////////


        ///// >> Pre-Processing
        Mat imgContours = img.clone();
        Mat imgGray = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgBlur = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgCanny = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);
        Mat imgEdges = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC1);

        // convert image to gray scale
        Imgproc.cvtColor(img, imgGray, Imgproc.COLOR_RGBA2GRAY);
        // add blur
        Imgproc.GaussianBlur(imgGray, imgBlur, new Size(5, 5), 5, 0);
        // find the edges
        Imgproc.Canny(imgBlur, imgCanny, 20, 50);
        // Increase / Decrease edge thickness
        Mat erodeElement = Imgproc.getStructuringElement(Imgproc.MORPH_CROSS, new Size(3, 3));
        Imgproc.dilate(imgCanny, imgEdges, erodeElement);

        ///// >> FIND CONTOURS
        List<MatOfPoint> contours = new List<MatOfPoint>();
        Mat hierarchy = new Mat();
        Imgproc.findContours(imgEdges, contours, hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_SIMPLE);

        for (int i = 0; i < contours.Count; i++)
        {
            double area = Imgproc.contourArea(contours[i]);

            if (area > 1000)
            {
                MatOfPoint2f cntF = new MatOfPoint2f(contours[i].toArray());
                MatOfPoint2f approx = new MatOfPoint2f();

                double peri = Imgproc.arcLength(cntF, true);
                Imgproc.approxPolyDP(cntF, approx, 0.02 * peri, true);
                print(approx.toArray().Length);

                Imgproc.drawContours(imgContours, contours, i, new Scalar(255, 0, 255, 255), 5);
                OpenCVForUnity.CoreModule.Rect bbox = Imgproc.boundingRect(approx);
                Imgproc.rectangle(imgContours, bbox.tl(), bbox.br(), new Scalar(0, 255, 0, 255), 2);

                //if (approx.toArray().Length == 3)
                //{

                //}
            }
        }

        /////////////////////////

        // create new 2D Texture
        Texture2D outputTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D edgesTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
        Texture2D contoursTexture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);

        // convert mat to texture
        Utils.matToTexture2D(img, outputTexture);
        Utils.matToTexture2D(imgEdges, edgesTexture);
        Utils.matToTexture2D(imgContours, contoursTexture);

        // display the texture on the raw image
        imgDisplay.texture = outputTexture;
        imgDisplayEdges.texture = edgesTexture;
        imgDisplayContours.texture = contoursTexture;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
