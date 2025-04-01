using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Telecom;
using Android.Views;
using Java.Lang; // ← 이거 추가!
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Google.MLKit.Vision.Common;
using static Android.Views.TextureView;
using Image = Android.Media.Image;
namespace MyCamera2Preview.Platforms.Android
{
    public class Camera2Preview : TextureView, TextureView.ISurfaceTextureListener
    {
      
        private readonly Context context;
        private CameraDevice? cameraDevice;
        private CameraCaptureSession? captureSession;
        private ImageReader? imageReader;
        private string? cameraId;
        private HandlerThread? backgroundThread;
        private Handler backgroundHandler;
        public Camera2Preview(Context conttext) : base(conttext)
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
            //var characteristics = ((CameraManager)context.GetSystemService(Context.CameraService)!)
            //.GetCameraCharacteristics(cameraId);
            //var configMap = (StreamConfigurationMap)characteristics.Get(CameraCharacteristics.ScalerStreamConfigurationMap);

            //// YUV_420_888 포맷에서 지원하는 해상도 목록 가져오기
            //var outputSizes = configMap.GetOutputSizes((int)ImageFormatType.Yuv420888);

            //// 최대 해상도 선택
            //var maxSize = outputSizes.OrderByDescending(s => s.Width * s.Height).First();
            //System.Diagnostics.Debug.WriteLine($"📸 최대 해상도: {maxSize.Width}x{maxSize.Height}");
            var texture = SurfaceTexture!;
            texture.SetDefaultBufferSize(1280, 720);
            //texture.SetDefaultBufferSize(640, 480);
            var surface = new Surface(texture);

            imageReader = ImageReader.NewInstance(1280, 720, ImageFormatType.Yuv420888, 5);
            imageReader.SetOnImageAvailableListener(new ImageAvailableListener(context), backgroundHandler);

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
            private readonly Context context; // **
            private bool isProcessing = false;

            public ImageAvailableListener(Context context )
            {
                this.context = context;

            }

            //public async void OnImageAvailable(ImageReader reader)
            //{
            //    Image? image = null;

            //    try
            //    {
            //        image = reader.AcquireLatestImage();

            //        if (image == null) return;

            //        var windowManager = context.GetSystemService(Context.WindowService) as IWindowManager;
            //        if (windowManager == null) return;

            //        var rotation = windowManager.DefaultDisplay?.Rotation ?? SurfaceOrientation.Rotation0;
            //        int rotationDegrees = rotation switch
            //        {
            //            SurfaceOrientation.Rotation0 => 0,
            //            SurfaceOrientation.Rotation90 => 90,
            //            SurfaceOrientation.Rotation180 => 180,
            //            SurfaceOrientation.Rotation270 => 270,
            //            _ => 0
            //        };
            //        var yaw = await FaceDetectorService.Instance.DetectYawAsyncFromImage(image, rotationDegrees);
            //        // ML Kit InputImage로 변환 시작
            //        if (yaw.HasValue)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"[Yaw] {yaw.Value}도");
            //        }
            //        var planes = image.GetPlanes();
            //        if (planes == null || planes.Length == 0)
            //        {
            //            System.Diagnostics.Debug.WriteLine("❗ image.GetPlanes() 실패");
            //            return;
            //        }
            //        var buffer = planes[0].Buffer;
            //        var data = new byte[buffer.Remaining()];
            //        buffer.Get(data);

            //        int width = image.Width;
            //        int height = image.Height;

            //        // 여기서 ML Kit 처리 시작
            //        System.Diagnostics.Debug.WriteLine($"[프레임 수신] {data.Length} bytes, {width}x{height}");
            //    }
            //    finally
            //    {
            //        image.Close();
            //    }
            //}
            //            public  void OnImageAvailable(ImageReader reader)
            //            {
            //                Image? image = reader.AcquireLatestImage();

            //                try
            //                {

            //                    if (image == null) return;

            //                    var windowManager = context.GetSystemService(Context.WindowService) as IWindowManager;
            //                    if (windowManager == null) return;

            //                    var rotation = windowManager.DefaultDisplay?.Rotation ?? SurfaceOrientation.Rotation0;
            //                    int rotationDegrees = rotation switch
            //                    {
            //                        SurfaceOrientation.Rotation0 => 0,
            //                        SurfaceOrientation.Rotation90 => 90,
            //                        SurfaceOrientation.Rotation180 => 180,
            //                        SurfaceOrientation.Rotation270 => 270,
            //                        _ => 0
            //                    };

            //                    // ML Kit InputImage로 변환 시작

            //                    var planes = image.GetPlanes();
            //                    if (planes == null || planes.Length == 0)
            //                    {
            //                        System.Diagnostics.Debug.WriteLine("❗ image.GetPlanes() 실패");
            //                        return;
            //                    }
            //                    var buffer = planes[0].Buffer;
            //                    var data = new byte[buffer.Remaining()];
            //                    buffer.Get(data);

            //                    int width = image.Width;
            //                    int height = image.Height;

            //                    // 여기서 ML Kit 처리 시작
            //                    var imagedel = InputImage.FromMediaImage(image, rotationDegrees);
            //                    //image.Close(); // 바로 닫음 (최대 buffer 문제 방지)
            //                    System.Diagnostics.Debug.WriteLine($"[프레임 수신] {data.Length} bytes, {width}x{height}");
            //                    _ = Task.Run(async () =>
            //                    {

            //                        //var yaw = await FaceDetectorService.Instance.DetectYawAsyncFromImage(imagedel);
            //                        //if (yaw.HasValue)
            //                        //{
            //                        //    System.Diagnostics.Debug.WriteLine($"[Yaw] {yaw.Value}도");
            //                        //}
            //                        //image.Close(); // 바로 닫음 (최대 buffer 문제 방지)
            //                        try
            //                        {
            //                            var yaw = await FaceDetectorService.Instance.DetectYawAsyncFromImage(imagedel);
            //                            if (yaw.HasValue)
            //                            {
            //                                System.Diagnostics.Debug.WriteLine($"[Yaw] {yaw.Value}도");
            //                            }
            //                        }
            //                        catch (System.Exception ex)
            //                        {
            //                            System.Diagnostics.Debug.WriteLine($"❌ 예외 발생(Task 내부): {ex.Message}");
            //                        }
            //                        finally
            //                        {
            //                            image.Close(); // Task 안에서 닫아야 함!!
            //                        }
            //                    });
            //                }
            //                finally
            //                {
            //                    image.Close();
            //                }
            //                // 현재 상황
            ///*
            //                Image 객체가 이미 다른쓰레드에서  Close된뒤에 바로 
            //                InputImage.FromMediaImage()에 전달됨.
            //                InputImage.FromMediaImage(image, rotation) 호출 전에 image.Close() 를 하지 않아야 함.
            //                Task.Run() 으로 ML Kit을 비동기 처리하더라도, 
            //                내부에서 처리 중인 이미지가 동시에 처리되는 경우 충돌 발생 가능
            // */


            //            }
            private byte[] Yuv420ToNv21(Image image)
            {
                var yPlane = image.GetPlanes()[0];
                var uPlane = image.GetPlanes()[1];
                var vPlane = image.GetPlanes()[2];

                int ySize = yPlane.Buffer.Remaining();
                int uSize = uPlane.Buffer.Remaining();
                int vSize = vPlane.Buffer.Remaining();

                byte[] nv21 = new byte[ySize + uSize + vSize];

                // Y 채널 복사
                yPlane.Buffer.Get(nv21, 0, ySize);

                // VU 채널을 NV21 순서로 복사
                byte[] uBytes = new byte[uSize];
                byte[] vBytes = new byte[vSize];
                uPlane.Buffer.Get(uBytes);
                vPlane.Buffer.Get(vBytes);

                // NV21은 VU 순서여야 함 (VU VU VU...)
                for (int i = 0; i < uSize; i++)
                {
                    nv21[ySize + (i * 2)] = vBytes[i];
                    nv21[ySize + (i * 2) + 1] = uBytes[i];
                }

                return nv21;
            }
            private long lastProcessedTime = 0;
            public void OnImageAvailable1(ImageReader reader)
            {

                long now = JavaSystem.CurrentTimeMillis();
                if (now - lastProcessedTime < 700)
                {
                    reader.AcquireLatestImage()?.Close(); // 프레임 무시 + 리소스 누수 방지
                    return;
                }
                lastProcessedTime = now;
                Image? image = reader.AcquireLatestImage();



                if (image == null) return;

                var windowManager = context.GetSystemService(Context.WindowService) as IWindowManager;
                if (windowManager == null) return;

                var rotation = windowManager.DefaultDisplay?.Rotation ?? SurfaceOrientation.Rotation0;
                int rotationDegrees = rotation switch
                {
                    SurfaceOrientation.Rotation0 => 0,
                    SurfaceOrientation.Rotation90 => 90,
                    SurfaceOrientation.Rotation180 => 180,
                    SurfaceOrientation.Rotation270 => 270,
                    _ => 0
                };

                // ML Kit InputImage로 변환 시작
                var inputImage = InputImage.FromMediaImage(image, rotationDegrees);//
                var planes = image.GetPlanes();
                if (planes == null || planes.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine("❗ image.GetPlanes() 실패");
                    return;
                }
                var buffer = planes[0].Buffer;
                var data = new byte[buffer.Remaining()];
                buffer.Get(data);
                byte[] nv21bytes = Yuv420ToNv21(image);
                int width = image.Width;
                int height = image.Height;
                image.Close();
                // 여기서 ML Kit 처리 시작

                //image.Close(); // 바로 닫음 (최대 buffer 문제 방지)
                System.Diagnostics.Debug.WriteLine($"[프레임 수신] {data.Length} bytes, {width}x{height}");
                _ = Task.Run(async () =>
                {
                    try
                    {
                        //var yaw = await FaceDetectorService.Instance.DetectYawAsyncFromByte(nv21bytes, width, height, rotationDegrees);
                        var yaw = await FaceDetectorService.Instance.DetectYawAsyncFromImage(inputImage);
                        if (yaw.HasValue)
                        {
                            System.Diagnostics.Debug.WriteLine($"{yaw} degree");
                         
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("❗ 얼굴을 감지하지 못했습니다.");
                          
                        }
                    }
                    catch (System.Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ 예외 발생: {ex.Message}");
                
                    }
                    finally
                    {
                       
                    }
                });
            }

            public async void OnImageAvailable(ImageReader reader)
            {
                long now = JavaSystem.CurrentTimeMillis();
                if (now - lastProcessedTime < 700)
                {
                    reader.AcquireLatestImage()?.Close();
                    return;
                }
                lastProcessedTime = now;

                Image? image = reader.AcquireLatestImage();
                if (image == null) return;

                var windowManager = context.GetSystemService(Context.WindowService) as IWindowManager;
                if (windowManager == null) return;

                var rotation = windowManager.DefaultDisplay?.Rotation ?? SurfaceOrientation.Rotation0;
                int rotationDegrees = rotation switch
                {
                    SurfaceOrientation.Rotation0 => 0,
                    SurfaceOrientation.Rotation90 => 90,
                    SurfaceOrientation.Rotation180 => 180,
                    SurfaceOrientation.Rotation270 => 270,
                    _ => 0
                };
                System.Diagnostics.Debug.WriteLine($"[DEBUG] 회전 각도: {rotationDegrees}, 이미지 크기: {image.Width}x{image.Height}");
                var inputImage = InputImage.FromMediaImage(image, 270);

                try
                {
                    var yaw = await FaceDetectorService.Instance.DetectYawAsyncFromImage(inputImage);
                    if (yaw.HasValue)
                    {
                        System.Diagnostics.Debug.WriteLine($"✅ Yaw: {yaw.Value}도");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("❗ 얼굴을 감지하지 못했습니다.");
                    }
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ 예외 발생: {ex.Message}");
                }
                finally
                {
                    image.Close(); // 동기라서 안전하게 닫힘
                }
            }




        }
    }
}
