using Xamarin.Google.MLKit.Vision.Face;
using Xamarin.Google.MLKit.Vision.Common;
using System.Linq;
using Android.Gms.Extensions;
using Android.Graphics;
 
namespace CameraView.Platforms.Android.Api
{
    public class FaceDetectionService
    {
        private IFaceDetector faceDetector;
        public FaceDetectionService()
        {
            var opt = new FaceDetectorOptions.Builder()
                 .SetPerformanceMode
                 (FaceDetectorOptions.PerformanceModeFast)
                 .Build();
            faceDetector =
                Xamarin.Google.MLKit.Vision.Face.FaceDetection.GetClient(opt);
        }
        public async Task<float?> DetectYawAsync
           (byte[] byteArray, int width, int height, int rotation)
        {
            var image = InputImage.FromByteArray
                (byteArray, width, height, rotation, InputImage.ImageFormatNv21);
            var result = await faceDetector.Process(image) as IList<Face>;
            if (result != null && result.Any())
            {
                var face = result.First();
                return face.HeadEulerAngleY;
            }
            return null;
        }
         public static async Task<float?> DetectYawAsync2(byte[] data, int width, int height, int rotation)
        {
            var image = InputImage.FromByteArray(data, width, height, rotation, InputImage.ImageFormatNv21);
            var options = new FaceDetectorOptions.Builder()
    .SetPerformanceMode(FaceDetectorOptions.PerformanceModeFast) // ✅ 이렇게 써야 합니다
    .SetLandmarkMode(FaceDetectorOptions.LandmarkModeAll)
    .SetClassificationMode(FaceDetectorOptions.ClassificationModeAll)
    .EnableTracking()
    .Build();

            var detector = FaceDetection.GetClient(options);
            var result = await detector.Process(image) as IList<Face>;
            var face = result?.FirstOrDefault();

            return face?.HeadEulerAngleY;
        }
    }
}
