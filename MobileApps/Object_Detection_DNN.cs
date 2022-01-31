#if !(PLATFORM_LUMIN && !UNITY_EDITOR)

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using OpenCVForUnity.DnnModule;
using System.Collections.Generic;
using System.Linq;

namespace OpenCVForUnityExample
{
    /// <summary>
    /// WebCamTextureToMatHelper Example
    /// </summary>
    [RequireComponent(typeof(WebCamTextureToMatHelper))]
    [RequireComponent(typeof(Image))]
    public class Object_Detection_DNN : MonoBehaviour
    {
        Image image;
        void Awake()
        {
            image = GetComponent<Image>();
        }

        public void ChangeColor(Color col)
        {
            image.color = col;
        }

        public ResolutionPreset requestedResolution = ResolutionPreset._480x640;
        public FPSPreset requestedFPS = FPSPreset._30;
        public Toggle rotate90DegreeToggle;
        public Toggle flipVerticalToggle;
        public Toggle flipHorizontalToggle;
        Texture2D texture;
        WebCamTextureToMatHelper webCamTextureToMatHelper;

        public GameObject LogoMenu;
        public GameObject Button_People;
        public GameObject Button_Animals;
        public GameObject Button_Transp;
        public GameObject Button_Food;
        public GameObject Button_Tech;
        public GameObject Button_Weapons;
        public GameObject Button_Infrastructure;

        public RawImage inputImage;
        public RawImage inputImageGray;

        public string model;
        public string config;
        public string classes;
        public float confThreshold = 0.5f;
        public float nmsThreshold = 0.4f;
        public float scale = 1.0f;
        public Scalar mean = new Scalar(0, 0, 0, 0);
        public bool swapRB = false;
        public int inpWidth = 320;
        public int inpHeight = 320;

        public bool showMenu = false;

        public bool detectPeople = false;
        public bool detectAnimals = false;
        public bool detectTransport = false;
        public bool detectFood = false;
        public bool detectTech = false;
        public bool detectWeapons = false;
        public bool detectInfrastructure = false;
        public bool detectFurniture = false;
        public bool detectPersonalItems = false;





        protected Mat bgrMat;
        protected Net net;

        protected List<string> classNames;
        protected List<string> outBlobNames;
        protected List<string> outBlobTypes;

        protected string classes_filepath;
        protected string config_filepath;
        protected string model_filepath;

        public void onClickLogo()
        {
            showMenu = !showMenu;
        }

        public void onClickPeople()
        {
            detectPeople = !detectPeople;
            GameObject buttonPeople = GameObject.FindGameObjectWithTag("button_people");
            if (detectPeople)
            {
                print("green");
                buttonPeople.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonPeople.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickAnimals()
        {
            detectAnimals = !detectAnimals;
            GameObject buttonAnimals = GameObject.FindGameObjectWithTag("button_animals");
            if (detectAnimals)
            {
                print("green");
                buttonAnimals.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonAnimals.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickTransport()
        {
            detectTransport = !detectTransport;
            GameObject buttonTransport = GameObject.FindGameObjectWithTag("button_transp");
            if (detectTransport)
            {
                print("green");
                buttonTransport.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonTransport.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };

        }
        public void onClickFood()
        {
            detectFood = !detectFood;
            GameObject buttonFood = GameObject.FindGameObjectWithTag("button_food");
            if (detectFood)
            {
                print("green");
                buttonFood.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonFood.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickTech()
        {
            detectTech = !detectTech;
            GameObject buttonTech = GameObject.FindGameObjectWithTag("button_tech");
            if (detectTech)
            {
                print("green");
                buttonTech.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonTech.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickWeapons()
        {
            detectWeapons = !detectWeapons;
            GameObject buttonWeapons = GameObject.FindGameObjectWithTag("button_weapons");
            if (detectWeapons)
            {
                print("green");
                buttonWeapons.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonWeapons.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickInfrastructure()
        {
            detectInfrastructure = !detectInfrastructure;
            GameObject buttonInfra = GameObject.FindGameObjectWithTag("button_infrastructure");
            if (detectInfrastructure)
            {
                print("green");
                buttonInfra.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonInfra.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickFurniture()
        {
            detectFurniture = !detectFurniture;
            GameObject buttonFurniture = GameObject.FindGameObjectWithTag("button_furniture");
            if (detectFurniture)
            {
                print("green");
                buttonFurniture.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonFurniture.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }
        public void onClickPersonalItems()
        {
            detectPersonalItems = !detectPersonalItems;
            GameObject buttonPersonal = GameObject.FindGameObjectWithTag("button_personal");
            if (detectPersonalItems)
            {
                print("green");
                buttonPersonal.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            else
            {
                print("white");
                buttonPersonal.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            };
        }

        public void HideGameObject()

        {
            if (LogoMenu.activeInHierarchy)
            {
                LogoMenu.SetActive(false);
            }
            else
            {
                LogoMenu.SetActive(true);
            }
        }

        // Use this for initialization
        void Start()
        {
            webCamTextureToMatHelper = gameObject.GetComponent<WebCamTextureToMatHelper>();
            int width, height;
            Dimensions(requestedResolution, out width, out height);
            webCamTextureToMatHelper.requestedWidth = width;
            webCamTextureToMatHelper.requestedHeight = height;
            webCamTextureToMatHelper.requestedFPS = (int)requestedFPS;
            webCamTextureToMatHelper.Initialize();


            LogoMenu = GameObject.FindGameObjectWithTag("LogoMenu");

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                RectTransform rt = inputImage.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(480, 640);
                rt.localScale = new Vector3(2.6f, 2.6f, 1.3f);

            }

            if (!string.IsNullOrEmpty(classes)) classes_filepath = Utils.getFilePath("dnn/" + classes);
            if (!string.IsNullOrEmpty(config)) config_filepath = Utils.getFilePath("dnn/" + config);
            if (!string.IsNullOrEmpty(model)) model_filepath = Utils.getFilePath("dnn/" + model);
            Run();

        }

        public void OnWebCamTextureToMatHelperInitialized()
        {
            Debug.Log("OnWebCamTextureToMatHelperInitialized");

            Mat webCamTextureMat = webCamTextureToMatHelper.GetMat();

            texture = new Texture2D(webCamTextureMat.cols(), webCamTextureMat.rows(), TextureFormat.RGBA32, false);
            Utils.fastMatToTexture2D(webCamTextureMat, texture);

            gameObject.GetComponent<Renderer>().material.mainTexture = texture;

            gameObject.transform.localScale = new Vector3(webCamTextureMat.cols(), webCamTextureMat.rows(), 1);
            Debug.Log("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);

            float width = webCamTextureMat.width();
            float height = webCamTextureMat.height();

            float widthScale = (float)Screen.width / width;
            float heightScale = (float)Screen.height / height;
            if (widthScale < heightScale)
            {
                Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
            }
            else
            {
                Camera.main.orthographicSize = height / 2;
            }


            ///////
            ///////
            bgrMat = new Mat(webCamTextureMat.rows(), webCamTextureMat.cols(), CvType.CV_8UC3);
        }
        public void OnWebCamTextureToMatHelperDisposed()
        {
            Debug.Log("OnWebCamTextureToMatHelperDisposed");

            if (texture != null)
            {
                Texture2D.Destroy(texture);
                texture = null;
            }
        }
        public void OnWebCamTextureToMatHelperErrorOccurred(WebCamTextureToMatHelper.ErrorCode errorCode)
        {
            Debug.Log("OnWebCamTextureToMatHelperErrorOccurred " + errorCode);

        }
        void OnDestroy()
        {
            webCamTextureToMatHelper.Dispose();

            //////
            //////
            if (net != null)
                net.Dispose();
            Utils.setDebugMode(false);
        }
        public void OnPlayButtonClick()
        {
            webCamTextureToMatHelper.Play();
        }
        public void OnPauseButtonClick()
        {
            webCamTextureToMatHelper.Pause();
        }
        public void OnStopButtonClick()
        {
            webCamTextureToMatHelper.Stop();
        }
        public void OnChangeCameraButtonClick()
        {
            webCamTextureToMatHelper.requestedIsFrontFacing = !webCamTextureToMatHelper.IsFrontFacing();
        }
        public void OnRequestedResolutionDropdownValueChanged(int result)
        {
            if ((int)requestedResolution != result)
            {
                requestedResolution = (ResolutionPreset)result;

                int width, height;
                Dimensions(requestedResolution, out width, out height);

                webCamTextureToMatHelper.Initialize(width, height);
            }
        }
        public void OnRequestedFPSDropdownValueChanged(int result)
        {
            string[] enumNames = Enum.GetNames(typeof(FPSPreset));
            int value = (int)System.Enum.Parse(typeof(FPSPreset), enumNames[result], true);

            if ((int)requestedFPS != value)
            {
                requestedFPS = (FPSPreset)value;

                webCamTextureToMatHelper.requestedFPS = (int)requestedFPS;
            }
        }
        public void OnRotate90DegreeToggleValueChanged()
        {
            if (rotate90DegreeToggle.isOn != webCamTextureToMatHelper.rotate90Degree)
            {
                webCamTextureToMatHelper.rotate90Degree = rotate90DegreeToggle.isOn;
            }
        }
        public void OnFlipVerticalToggleValueChanged()
        {
            if (flipVerticalToggle.isOn != webCamTextureToMatHelper.flipVertical)
            {
                webCamTextureToMatHelper.flipVertical = flipVerticalToggle.isOn;
            }


        }
        public void OnFlipHorizontalToggleValueChanged()
        {
            if (flipHorizontalToggle.isOn != webCamTextureToMatHelper.flipHorizontal)
            {
                webCamTextureToMatHelper.flipHorizontal = flipHorizontalToggle.isOn;
            }
        }
        public enum FPSPreset : int
        {
            _0 = 0,
            _1 = 1,
            _5 = 5,
            _10 = 10,
            _15 = 15,
            _30 = 30,
            _60 = 60,
        }
        public enum ResolutionPreset : byte
        {
            _50x50 = 0,
            _640x480,
            _480x640,
            _1280x720,
            _1920x1080,
            _9999x9999,
        }
        private void Dimensions(ResolutionPreset preset, out int width, out int height)
        {
            switch (preset)
            {
                case ResolutionPreset._50x50:
                    width = 50;
                    height = 50;
                    break;
                case ResolutionPreset._640x480:
                    width = 640;
                    height = 480;
                    break;
                case ResolutionPreset._480x640:
                    width = 480;
                    height = 640;
                    break;
                case ResolutionPreset._1280x720:
                    width = 1280;
                    height = 720;
                    break;
                case ResolutionPreset._1920x1080:
                    width = 1920;
                    height = 1080;
                    break;
                case ResolutionPreset._9999x9999:
                    width = 9999;
                    height = 9999;
                    break;
                default:
                    width = height = 0;
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    CUSTOM CODE  ///////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////


        protected virtual void Run()
        {
            //if true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.
            Utils.setDebugMode(true);


            ////////////////////// LOAD NAMES ////////////////////////////
            if (!string.IsNullOrEmpty(classes))
            {
                classNames = readClassNames(classes_filepath);
            }

            //////////////////////// LOAD MODEL ////////////////////////////
            if (string.IsNullOrEmpty(model_filepath))
            {
                Debug.LogError(model_filepath + " is not loaded. Please see \"StreamingAssets/dnn/setup_dnn_module.pdf\". ");
            }
            else
            {
                ////////////////////////// Initialize network ////////////////////////
                net = Dnn.readNet(model_filepath, config_filepath);
                outBlobNames = getOutputsNames(net);
                outBlobTypes = getOutputsTypes(net);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
                                    // Avoids the front camera low light issue that occurs in only some Android devices (e.g. Google Pixel, Pixel2).
                                    webCamTextureToMatHelper.avoidAndroidFrontCameraLowLightIssue = true;
#endif
            webCamTextureToMatHelper.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            if (webCamTextureToMatHelper.IsPlaying() && webCamTextureToMatHelper.DidUpdateThisFrame())
            {

                Mat img = webCamTextureToMatHelper.GetMat();

                if (net == null)
                {
                    Debug.Log("model file is not loaded.");
                }
                else
                {

                    Imgproc.cvtColor(img, bgrMat, Imgproc.COLOR_RGBA2BGR);
                    Size inpSize = new Size(inpWidth > 0 ? inpWidth : bgrMat.cols(),
                                       inpHeight > 0 ? inpHeight : bgrMat.rows());
                    Mat blob = Dnn.blobFromImage(bgrMat, scale, inpSize, mean, swapRB, false);

                    // Run the model
                    net.setInput(blob);
                    List<Mat> outs = new List<Mat>();
                    net.forward(outs, outBlobNames);

                    try
                    {
                        postprocess(img, outs, net, Dnn.DNN_BACKEND_OPENCV);
                    }
                    catch (Exception e)
                    {
                        print(e);
                    }

                    ///////// Dispose ///////
                    for (int i = 0; i < outs.Count; i++)
                    {
                        outs[i].Dispose();
                    }
                    blob.Dispose();
                }

                Utils.fastMatToTexture2D(img, texture);
                inputImage.texture = texture;


            }
        }

        protected virtual List<string> readClassNames(string filename)
        {
            List<string> classNames = new List<string>();

            System.IO.StreamReader cReader = null;
            try
            {
                cReader = new System.IO.StreamReader(filename, System.Text.Encoding.Default);

                while (cReader.Peek() >= 0)
                {
                    string name = cReader.ReadLine();
                    classNames.Add(name);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                return null;
            }
            finally
            {
                if (cReader != null)
                    cReader.Close();
            }

            return classNames;
        }

        protected virtual void postprocess(Mat frame, List<Mat> outs, Net net, int backend = Dnn.DNN_BACKEND_OPENCV)
        {
            MatOfInt outLayers = net.getUnconnectedOutLayers();
            string outLayerType = outBlobTypes[0];

            List<int> classIdsList = new List<int>();
            List<float> confidencesList = new List<float>();
            List<Rect2d> boxesList = new List<Rect2d>();



            ////////////////////////////////////////////////////////////////////
            /////////////////////   FOR Mobile Net SSD /////////////////////////
            ////////////////////////////////////////////////////////////////////

            if (outLayerType == "DetectionOutput")
            {
                // Network produces output blob with a shape 1x1xNx7 where N is a number of
                // detections and an every detection is a vector of values
                // [batchId, classId, confidence, left, top, right, bottom]

                if (outs.Count == 1)
                {
                    outs[0] = outs[0].reshape(1, (int)outs[0].total() / 7);

                    //Debug.Log ("outs[i].ToString() " + outs [0].ToString ());

                    float[] data = new float[7];
                    for (int i = 0; i < outs[0].rows(); i++)
                    {
                        outs[0].get(i, 0, data);

                        float confidence = data[2];
                        if (confidence > confThreshold)
                        {
                            int class_id = (int)(data[1]);

                            float left = data[3] * frame.cols();
                            float top = data[4] * frame.rows();
                            float right = data[5] * frame.cols();
                            float bottom = data[6] * frame.rows();
                            float width = right - left + 1f;
                            float height = bottom - top + 1f;

                            classIdsList.Add((int)(class_id) - 1); // Skip 0th background class id.
                            confidencesList.Add((float)confidence);
                            boxesList.Add(new Rect2d(left, top, width, height));
                        }
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////
            /////////////////////   FOR YOLO V4 ////////////////////////////////
            ////////////////////////////////////////////////////////////////////

            else if (outLayerType == "Region")
            {
                for (int i = 0; i < outs.Count; ++i)
                {
                    // Network produces output blob with a shape NxC where N is a number of
                    // detected objects and C is a number of classes + 4 where the first 4
                    // numbers are [center_x, center_y, width, height]

                    //Debug.Log ("outs[i].ToString() "+outs[i].ToString());

                    float[] positionData = new float[5];
                    float[] confidenceData = new float[outs[i].cols() - 5];
                    for (int p = 0; p < outs[i].rows(); p++)
                    {
                        outs[i].get(p, 0, positionData);
                        outs[i].get(p, 5, confidenceData);

                        int maxIdx = confidenceData.Select((val, idx) => new { V = val, I = idx }).Aggregate((max, working) => (max.V > working.V) ? max : working).I;
                        float confidence = confidenceData[maxIdx];
                        if (confidence > confThreshold)
                        {
                            float centerX = positionData[0] * frame.cols();
                            float centerY = positionData[1] * frame.rows();
                            float width = positionData[2] * frame.cols();
                            float height = positionData[3] * frame.rows();
                            float left = centerX - width / 2;
                            float top = centerY - height / 2;

                            classIdsList.Add(maxIdx);
                            confidencesList.Add((float)confidence);
                            boxesList.Add(new Rect2d(left, top, width, height));
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Unknown output layer type: " + outLayerType);
            }

            ////////////////////////////////////////////////////////////////////
            /////////////////////   FOR YOLO V4 ////////////////////////////////
            ////////////////////////////////////////////////////////////////////

            // NMS is used inside Region layer only on DNN_BACKEND_OPENCV for another backends we need NMS in sample
            // or NMS is required if number of outputs > 1
            if (outLayers.total() > 1 || (outLayerType == "Region" && backend != Dnn.DNN_BACKEND_OPENCV))
            {
                Dictionary<int, List<int>> class2indices = new Dictionary<int, List<int>>();
                for (int i = 0; i < classIdsList.Count; i++)
                {
                    if (confidencesList[i] >= confThreshold)
                    {
                        if (!class2indices.ContainsKey(classIdsList[i]))
                            class2indices.Add(classIdsList[i], new List<int>());

                        class2indices[classIdsList[i]].Add(i);
                    }
                }

                List<Rect2d> nmsBoxesList = new List<Rect2d>();
                List<float> nmsConfidencesList = new List<float>();
                List<int> nmsClassIdsList = new List<int>();
                foreach (int key in class2indices.Keys)
                {
                    List<Rect2d> localBoxesList = new List<Rect2d>();
                    List<float> localConfidencesList = new List<float>();
                    List<int> classIndicesList = class2indices[key];
                    for (int i = 0; i < classIndicesList.Count; i++)
                    {
                        localBoxesList.Add(boxesList[classIndicesList[i]]);
                        localConfidencesList.Add(confidencesList[classIndicesList[i]]);
                    }

                    using (MatOfRect2d localBoxes = new MatOfRect2d(localBoxesList.ToArray()))
                    using (MatOfFloat localConfidences = new MatOfFloat(localConfidencesList.ToArray()))
                    using (MatOfInt nmsIndices = new MatOfInt())
                    {
                        Dnn.NMSBoxes(localBoxes, localConfidences, confThreshold, nmsThreshold, nmsIndices);
                        for (int i = 0; i < nmsIndices.total(); i++)
                        {
                            int idx = (int)nmsIndices.get(i, 0)[0];
                            nmsBoxesList.Add(localBoxesList[idx]);
                            nmsConfidencesList.Add(localConfidencesList[idx]);
                            nmsClassIdsList.Add(key);
                        }
                    }
                }

                boxesList = nmsBoxesList;
                classIdsList = nmsClassIdsList;
                confidencesList = nmsConfidencesList;
            }



            for (int idx = 0; idx < boxesList.Count; ++idx)
            {
                Rect2d box = boxesList[idx];
                drawPred(classIdsList[idx], confidencesList[idx], box.x, box.y,
                    box.x + box.width, box.y + box.height, frame);

            }
            print(classIdsList[0]);
           // print(classIdsList.Count);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////// DRAW BOXES BELOW //////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual void drawPred(int classId, float conf, double left, double top, double right, double bottom, Mat frame)
        {
            Scalar clrGreen = new Scalar(43, 255, 89, 255);
            Scalar clrRed = new Scalar(255, 93, 76, 255);
            Scalar clrBlue = new Scalar(40, 193, 224, 255);
            Scalar clrBlack = new Scalar(0, 0, 0, 255);

            int person = 0;
            int[] animal = { 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            int[] transport = { 1, 2, 3, 4, 5, 6, 7, 8, 32, 33, 38, 39 };
            int[] food = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 };
            int[] tech = { 62, 63, 64, 65, 66, 67 };
            int[] weapons = { 36, 40, 41, 44, 45, 78 };
            int[] infrastructure = { 11, 12, 13, 14 };
            int[] furniture = { 15, 58, 59, 60, 61 };
            int[] personalItems = { 26, 27, 28, 29, 79, 80, 81 };

            // helper function for drawing bounding boxes
            void drawBox(Scalar color, string boxLabel)
            {
                Imgproc.rectangle(frame, new Point(left, top), new Point(right, bottom), color, 2);

                string label = conf.ToString();
                if (classNames != null && classNames.Count != 0)
                {
                    label = boxLabel;
                }

                int[] baseLine = new int[1];
                Size labelSize = Imgproc.getTextSize(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

                top = Mathf.Max((float)top, (float)labelSize.height);
                Imgproc.rectangle(frame, new Point(left, top - labelSize.height),
                    new Point(left + labelSize.width, top + baseLine[0]), Scalar.all(255), Core.FILLED);
                Imgproc.putText(frame, label, new Point(left, top), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, new Scalar(0, 0, 0, 255));
            }

            // if person detected and confidence is greater than .45
            if(detectPeople && classId == person && conf > .45 && classId < (int)classNames.Count)
            {
                drawBox(clrGreen, "person detected... conf: " + conf);
            // check if found object is an animal
            } else if (detectAnimals && animal.Contains(classId))
            {
                // draw red box if so
                drawBox(clrRed, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is transportation method
            else if (detectTransport && transport.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlack, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is food
            else if (detectFood && food.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlack, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is technology
            else if (detectTech && tech.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlue, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is a weapon
            else if (detectWeapons && weapons.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlack, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is public infrastructure
            else if (detectInfrastructure && infrastructure.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlack, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is furniture
            else if (detectFurniture && furniture.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlack, classNames[classId] + " detected... conf: " + conf);
            }
            // check if found object is a personal item
            else if (detectPersonalItems && personalItems.Contains(classId))
            {
                // draw black box if so
                drawBox(clrBlack, classNames[classId] + " detected... conf: " + conf);
            }

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////// DRAW BOXES ABOVE //////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual List<string> getOutputsNames(Net net)
        {
            List<string> names = new List<string>();


            MatOfInt outLayers = net.getUnconnectedOutLayers();
            for (int i = 0; i < outLayers.total(); ++i)
            {
                names.Add(net.getLayer(new DictValue((int)outLayers.get(i, 0)[0])).get_name());
            }
            outLayers.Dispose();

            return names;
        }

        protected virtual List<string> getOutputsTypes(Net net)
        {
            List<string> types = new List<string>();


            MatOfInt outLayers = net.getUnconnectedOutLayers();
            for (int i = 0; i < outLayers.total(); ++i)
            {
                types.Add(net.getLayer(new DictValue((int)outLayers.get(i, 0)[0])).get_type());
            }
            outLayers.Dispose();

            return types;
        }



    }
}

#endif