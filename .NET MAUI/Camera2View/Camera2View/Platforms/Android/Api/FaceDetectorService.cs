using Xamarin.Google.MLKit.Vision.Face;
using Xamarin.Google.MLKit.Vision.Common;
using System.Linq;
using Android.Gms.Extensions;
using Android.Graphics;
using Android.Media;
using Image = Android.Media.Image;

namespace Camera2View.Platforms.Android.Api
{
    public class FaceDetectorService
    {

        private static FaceDetectorService? _instance;   
        public static FaceDetectorService Instance=>_instance ??= new FaceDetectorService();
        private IFaceDetector faceDetector;
        
        private FaceDetectorService()
        {
            var faceOpt =  new FaceDetectorOptions.Builder().
                SetPerformanceMode(FaceDetectorOptions.PerformanceModeFast).Build();
            faceDetector = FaceDetection.GetClient(faceOpt);

            // API 옵션 설정
        }
        public async Task<float?> DetectYawAsyncFromByte
            (byte[] bytearr , int width , int height, int rot)
        {
            var img = InputImage.FromByteArray(bytearr, width, height, rot, InputImage.ImageFormatNv21);
            
            var result = await faceDetector.Process(img) as IList<Face?>;
            if(result != null&& result.Any())
            {
                var face = result.First();
                return face.HeadEulerAngleY;
            }
            return null;

        }
        public async Task<float?> DetectYawAsyncFromImage
            (Image image ,int rot )
        {
            var img = InputImage.FromMediaImage(image, rot);
            var res= await faceDetector.Process(img)as IList<Face?>;

            if (res != null && res.Any())
            {
                var yaw = res.First().HeadEulerAngleY;
                System.Diagnostics.Debug.WriteLine($"[Yaw] {yaw}");
                return yaw;
            }
            return null;
        }
    }
}
