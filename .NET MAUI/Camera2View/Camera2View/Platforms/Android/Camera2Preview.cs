using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace MauiApp1.Platforms.Android
{
    public class Camera2Preview:TextureView , TextureView.ISurfaceTextureListener
    {
        private readonly Context context;
        private CameraDevice? cameraDevice;
        private CameraCaptureSession? captureSession;
        private ImageReader? imageReader;
        private string? cameraId;
        private HandlerThread? backgroundThread;
        private Handler backgroundHandler;
        
        public Camera2Preview(Context conttext):base(conttext)  
        {
            this.context = conttext;
            SurfaceTextureListener = this;

        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            System.Diagnostics.Debug.WriteLine("🟢 SurfaceTexture available - StartCamera()");
            StartCamera();
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            return true; // true: 우리가 직접 SurfaceTexture를 관리하지 않겠다
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            // 해상도 변경 대응 필요시 처리
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            // 프레임이 업데이트 될 때 호출됨 (선택적으로 활용 가능)
        }
        public void StartCamera()
        {
            StartBackgroundThread();

            var cameraManager = (CameraManager)context.GetSystemService(Context.CameraService)!;
            cameraId = cameraManager.GetCameraIdList().First(id =>
            {
                // 카메라 가져옴
                var characteristics = cameraManager.GetCameraCharacteristics(id);
                var facing = (Integer)characteristics.Get(CameraCharacteristics.LensFacing);
                return facing.IntValue() == (int)LensFacing.Front; // 또는 Rear
            });

            cameraManager.OpenCamera(cameraId, new CameraStateCallback(this), backgroundHandler);
        }
        private void StartBackgroundThread()
        {
            backgroundThread = new HandlerThread("CameraBackground");
            backgroundThread.Start();
            backgroundHandler = new Handler(backgroundThread.Looper!);
        }
        private class CameraStateCallback : CameraDevice.StateCallback
        {
            private readonly Camera2Preview preview;

            public CameraStateCallback(Camera2Preview preview) => this.preview = preview;

            public override void OnOpened(CameraDevice camera)
            {
                preview.cameraDevice = camera;
                preview.CreateCameraPreviewSession();
            }

            public override void OnDisconnected(CameraDevice camera)
            {
                camera.Close();
                preview.cameraDevice = null;
            }

            public override void OnError(CameraDevice camera, [GeneratedEnum] CameraError error)
            {
                camera.Close();
                preview.cameraDevice = null;
            }
        }

        private void CreateCameraPreviewSession()
        {
            var texture = SurfaceTexture!;
            texture.SetDefaultBufferSize(640, 480); // 프리뷰 해상도

            var surface = new Surface(texture);

            imageReader = ImageReader.NewInstance(640, 480, ImageFormatType.Yuv420888, 2);
            imageReader.SetOnImageAvailableListener(new ImageAvailableListener(), backgroundHandler);

            var surfaces = new List<Surface> { surface, imageReader.Surface! };

            var requestBuilder = cameraDevice.CreateCaptureRequest(CameraTemplate.Preview)!;
            requestBuilder.AddTarget(surface);
            requestBuilder.AddTarget(imageReader.Surface!);

            cameraDevice.CreateCaptureSession(surfaces, new SessionStateCallback(this, requestBuilder), backgroundHandler);
        }

      

        private class SessionStateCallback : CameraCaptureSession.StateCallback
        {
            private readonly Camera2Preview preview;
            private readonly CaptureRequest.Builder requestBuilder;

            public SessionStateCallback(Camera2Preview preview, CaptureRequest.Builder builder)
            {
                this.preview = preview;
                requestBuilder = builder;
            }

            public override void OnConfigured(CameraCaptureSession session)
            {
                preview.captureSession = session;
                var request = requestBuilder.Build();
                session.SetRepeatingRequest(request, null, preview.backgroundHandler);
                // 이때 부터 ImageReaderOnImageavilable 이 게속호출
            }

            public override void OnConfigureFailed(CameraCaptureSession session)
            {
                // 실패 처리
            }
        }

        private class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
        {
            public void OnImageAvailable(ImageReader reader)
            {
                using var image = reader.AcquireLatestImage();
                if (image == null) return;

                // ML Kit InputImage로 변환 시작
                var planes = image.GetPlanes();
                var buffer = planes[0].Buffer;
                var data = new byte[buffer.Remaining()];
                buffer.Get(data);

                int width = image.Width;
                int height = image.Height;

                // 여기서 ML Kit 처리 시작
                System.Diagnostics.Debug.WriteLine($"[프레임 수신] {data.Length} bytes, {width}x{height}");

                image.Close();
            }
        }
    }
}
