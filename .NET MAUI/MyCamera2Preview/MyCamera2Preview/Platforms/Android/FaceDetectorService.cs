using Xamarin.Google.MLKit.Vision.Face;
using Xamarin.Google.MLKit.Vision.Common;
using System.Linq;
using Android.Gms.Extensions;
using Android.Graphics;
using Android.Media;
using Image = Android.Media.Image;
namespace MyCamera2Preview.Platforms.Android
{
    public class FaceDetectorService
    {
        private static FaceDetectorService? _instance;
        public static FaceDetectorService Instance => _instance ??= new FaceDetectorService();   
        private IFaceDetector faceDetector;
        public FaceDetectorService() {
            var opt  =  new FaceDetectorOptions.Builder().
                SetPerformanceMode(FaceDetectorOptions.PerformanceModeFast).Build ();
            faceDetector = FaceDetection.GetClient(opt);
        }
        public async Task<float?> DetectYawAsyncFromByte
          (byte[] bytearr, int width, int height, int rot)
        {

            if (faceDetector == null)
            {
                System.Diagnostics.Debug.WriteLine("❗ faceDetector is null - 아직 초기화되지 않음");
                return null;
            }
            var img = InputImage.FromByteArray(bytearr, width, height, rot, InputImage.ImageFormatNv21);

            var result = await faceDetector.Process(img) as IList<Face?>;
            if (result != null && result.Any())
            {
                var face = result.First();
                return face.HeadEulerAngleY;
            }
            return null;

        }
        public async Task<float?> DetectYawAsyncFromImage
            (Image image, int rot)
        {

            if (faceDetector == null)
            {
                System.Diagnostics.Debug.WriteLine("❗ faceDetector is null - 아직 초기화되지 않음");
                return null;
            }
            var img = InputImage.FromMediaImage(image, rot);
            var res = await faceDetector.Process(img) as IList<Face?>;

            if (res != null && res.Any())
            {
                var yaw = res.First().HeadEulerAngleY;
                System.Diagnostics.Debug.WriteLine($"[Yaw] {yaw}");
                return yaw;
            }
            return null;
        }
        public async Task<float?> DetectYawAsyncFromImage
           (InputImage image)
        {

            if (faceDetector == null)
            {
                System.Diagnostics.Debug.WriteLine("❗ faceDetector is null - 아직 초기화되지 않음");
                return null;
            }

            try
            {
                var res = await faceDetector.Process(image) as IList<Face?>;

                if (res == null || !res.Any())
                {
                    System.Diagnostics.Debug.WriteLine("❗ 얼굴을 감지하지 못했습니다.");
                    return null;
                }

                var yaw = res.First()?.HeadEulerAngleY;
                System.Diagnostics.Debug.WriteLine($"✅ 얼굴 감지 성공 - Yaw: {yaw}");
                return yaw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ 예외 발생: {ex.Message}");
                return null;
            }
        }
    }
}
