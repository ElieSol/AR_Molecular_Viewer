using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using System.Threading;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.Networking;

[AddComponentMenu("System/VuforiaScanner")]
public class VuforiaScanner : MonoBehaviour
{
    private bool cameraInitialized;
    private BarcodeReader barCodeReader;
    private bool activateQRCodeReader = false;
    public string downloadedFileName;
    private string urlExtracted = "https://files.rcsb.org/download/0000";
    private string url = "";

    public void activateReader()
    {
        if (activateQRCodeReader == false)
        {
            activateQRCodeReader = true; // initialize the qr code reader
            Start(); // call start function to initialize qr code reader
        }
    }

    void Start()
    {
        if (activateQRCodeReader == true)
        {
            barCodeReader = new BarcodeReader();
            StartCoroutine(InitializeCamera());
        }
    }

    IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Vuforia.Image.PIXEL_FORMAT.GRAYSCALE, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }
    void Update()
    {
        if (cameraInitialized) // extract qr code data from caemra is activated
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Vuforia.Image.PIXEL_FORMAT.GRAYSCALE);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                if (data != null)
                {
                    // QRCode detected.
                    Debug.Log("DECODED TEXT FROM QR: " + data.Text);
                    urlExtracted = data.Text;
                    url = data.Text;
                    Match match = Regex.Match(data.Text, @"([0-9][A-Z][0-9]{2})|([0-9][A-Z]{2}[0-9])|[0-9][A-Z]{3}"); //([0-9][A-Z][0-9]{2})|([0-9][A-Z]{2}[0-9])|[0-9][A-Z]{3}
                    downloadedFileName = match.ToString(); // save the file ID from url to downloadedFileName
                    Debug.Log(downloadedFileName);
                    activateQRCodeReader = false;
                    cameraInitialized = false;
                    Start();
                    return;
                }
                else if (Input.GetKeyUp(KeyCode.Return)) // while camera is active press enter or space to stop the qr code code reader
                {
                    activateQRCodeReader = false;
                    cameraInitialized = false;
                }
                else
                {
                    Debug.Log("No QR code detected !");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
    public string getUrlPDB(){
        return url;
    }
}
