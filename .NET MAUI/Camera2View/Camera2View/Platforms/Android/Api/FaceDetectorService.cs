using Xamarin.Google.MLKit.Vision.Face;
using Xamarin.Google.MLKit.Vision.Common;
using System.Linq;
using Android.Gms.Extensions;
using Android.Graphics;

namespace Camera2View.Platforms.Android.Api
{
    public class FaceDetectorService
    {
        private IFaceDetector faceDetector;
        
        public FaceDetectorService()
        {
            var faceOpt =  new FaceDetectorOptions.Builder().
                SetPerformanceMode(FaceDetectorOptions.PerformanceModeFast).Build();
            faceDetector = FaceDetection.GetClient(faceOpt);

            // API 옵션 설정
        }
        public async Task<float?> DetectYawAsync
            (byte[] bytearr , int width , int height, int rot)
        {
            var img = InputImage.FromMediaImage
        }
    }
}
