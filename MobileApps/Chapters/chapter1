using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class Chapter1Script : MonoBehaviour
{
    public RawImage imgDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D inputTexture = Resources.Load("test") as Texture2D;
        // Create an empty Mat
        Mat img = new Mat(inputTexture.height, inputTexture.width, CvType.CV_8UC4);
        // Convert Texture to Mat
        Utils.texture2DToMat(inputTexture, img);

        /////////////////////////
        /////////////////////////
        //>>>>>>>CODE>>>>>>>>>>//
        /////////////////////////
        /////////////////////////

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
