using Xamarin.Google.MLKit.Vision.Face;
using Xamarin.Google.MLKit.Vision.Common;
using System.Linq;
using Android.Gms.Extensions;
using Android.Graphics;
namespace NavigatePage.Platforms.Android.Api
{
    public class FaceDetectionService
    {
        private IFaceDetector faceDetector;
        public FaceDetectionService()
        {
           var opt  =  new FaceDetectorOptions.Builder()
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
                (byteArray, width, height, rotation,InputImage.ImageFormatNv21 );
            var result = await faceDetector.Process(image) as IList<Face>;
            if (result != null && result.Any())
            {
                var face = result.First();
                return face.HeadEulerAngleY;
            }
            return null;
        }
        public static async Task<float?> DetectYawAsync(byte[] data, int width, int height, int rotation)
        {
            var image = InputImage.FromByteArray(data, width, height, rotation, InputImageFormatType.Nv21);
            var options = new FaceDetectorOptions.Builder()
                .SetPerformanceMode(FaceDetectorOptions.PerformanceMode.Accurate)
                .SetLandmarkMode(FaceDetectorOptions.LandmarkMode.All)
                .SetClassificationMode(FaceDetectorOptions.ClassificationMode.All)
                .EnableTracking()
                .Build();

            var detector = FaceDetection.GetClient(options);
            var result = await detector.ProcessAsync(image);
            var face = result.FirstOrDefault();

            return face?.HeadEulerAngleY;
        }
    }

}
