using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureService
{
    internal class AzureControl
    {
        static string subscriptionKey = Environment.GetEnvironmentVariable("COMPUTER_VISION_SUBSCRIPTION_KEY"); // API key
        static string endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT"); // 엔드포인트

        ComputerVisionClient client = null;
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        
        Database db = Database.Singleton;

        #region 싱글톤
        public static AzureControl singleton { get; private set; } = null;

        static AzureControl()
        {
            singleton = new AzureControl();
        }
        private AzureControl()
        {

        }
        #endregion

        #region 접속처리
        public bool AzureConnect()
        {
            try
            {
                // 객체 생성
                client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
                { Endpoint = endpoint };

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public bool Database_Connect()
        {
            bool b = db.Connect();

            return b;
        }

        public bool Database_Close()
        {
            bool b = db.Close();

            return b;
        }
        #endregion

        #region  이미지 분석
        public async Task<string> AnalyzeImage(string image)
        {
            string message = string.Empty;
            string summary = string.Empty;
            string tags = string.Empty;

            string message_dummy = string.Empty;
            string tags_dummy = string.Empty;

            try
            {

                Console.WriteLine("이미지 분석 요청 확인");

                // --------------------------- API코드 -----------------------------------------------------------------------
                // Creating a list that defines the features to be extracted from the image.
                List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
                 {
                 VisualFeatureTypes.Description,
                 VisualFeatureTypes.Tags
                 };

                Console.WriteLine($"[이미지 분석] {Path.GetFileName(image)}...");

                // Analyze the image 
                using (var imageStream = new FileStream(image, FileMode.Open))
                {
                    await semaphoreSlim.WaitAsync();

                    ImageAnalysis results = await client.AnalyzeImageInStreamAsync(imageStream, features);
                    Console.WriteLine();

                    // Summarizes the image content.
                    foreach (var caption in results.Description.Captions)
                    {
                        message += summary = ($"{caption.Text}") + Environment.NewLine + ($"{caption.Confidence} %") + Environment.NewLine + Environment.NewLine;
                        message_dummy += ($"{Change_Voca(caption.Text)}") + Environment.NewLine + ($"{caption.Confidence} %") + Environment.NewLine + Environment.NewLine;
                    }
                    summary += Environment.NewLine + Environment.NewLine + message_dummy;

                    Console.WriteLine();


                    // Image tags and their confidence score
                    foreach (var tag in results.Tags)
                    {
                        tags += ($"{tag.Name.PadRight(15)} \t {tag.Confidence:F5} %") + Environment.NewLine;
                        tags_dummy += ($"{Change_Voca(tag.Name).PadRight(15)} \t {tag.Confidence:F5} %") + Environment.NewLine;
                    }
                    message += tags;
                    message += Environment.NewLine + message_dummy;
                    message += tags_dummy;

                    tags += Environment.NewLine + tags_dummy;

                    // --------------------------------------------------------------------------------------------------------

                    db.Picture_Insert(Path.GetFileName(image), summary, tags);

                    return message;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
        #endregion

        #region 글자 추출
        public async Task<string> ExtractTextImage(string Image)
        {
            string message = string.Empty;
            string summary = string.Empty;
            string tags = string.Empty;
            string papago = string.Empty;
            string dummy_papago = string.Empty;
            try
            {
                using (var imageStream = new FileStream(Image, FileMode.Open))
                {
                    // --------------------------- API코드 -----------------------------------------------------------------------
                    // Read text from image
                    BatchReadFileInStreamHeaders textHeaders = await client.BatchReadFileInStreamAsync(imageStream);
                    // After the request, get the operation location (operation ID)
                    string operationLocation = textHeaders.OperationLocation;
                    // Retrieve the URI where the recognized text will be stored from the Operation-Location header. 
                    // We only need the ID and not the full image
                    const int numberOfCharsInOperationId = 36;
                    string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);
                    // Extract the text 
                    // Delay is between iterations and tries a maximum of 10 times.

                    int i = 0;
                    int maxRetries = 10;
                    ReadOperationResult results;
                    do
                    {
                        results = await client.GetReadOperationResultAsync(operationId);
                        Console.WriteLine("Server status: {0}, waiting {1} seconds...", results.Status, i);
                        await Task.Delay(1000);
                    }
                    while ((results.Status == TextOperationStatusCodes.Running || results.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries);

                    // 찾은 텍스트 표시
                    var recognitionResults = results.RecognitionResults;
                    foreach (TextRecognitionResult result in recognitionResults)
                    {
                        foreach (Line line in result.Lines)
                        {
                            message += line.Text + Environment.NewLine;
                            papago += line.Text;
                            dummy_papago += Change_Voca(papago) + Environment.NewLine;
                        }
                    }

                    // -------------------------------------------------------------------------------------------------------------


                    message += Environment.NewLine + Environment.NewLine + dummy_papago;
                    db.Picture_Insert(Path.GetFileName(Image), message, string.Empty);

                    return message;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }

        }
        #endregion

        #region Select
        public string Select(string name)
        {
            string messages = db.Picture_Select(name);

            return messages;
        }

        public string SelectAll()
        {
            string pic = db.Picture_SelectAll();

            return pic;
        }
        #endregion

        #region 파일 저장
        public bool SaveImageToFile(string filename, byte[] imageBuffer)
        {
            try
            {
                // 주어진 이미지 파일의 이름으로 파일을 하나 만든다.
                FileStream writeFileStream = new FileStream(@"C:\Picture\" + filename, FileMode.Create, FileAccess.Write);

                // 이 파일에 바이너리를 넣기 위해 BinaryWriter 객체 생성     보조객체
                BinaryWriter picWriter = new BinaryWriter(writeFileStream);

                // 바이트 배열로 받은 이미지를 파일에 쓴다.
                picWriter.Write(imageBuffer);

                // 파일스트림을 닫는다.
                writeFileStream.Close();

                // 업로드 성공
                return true;
            }
            catch (Exception)
            {
                // 업로드 실패
                return false;
            }

        }
        #endregion

        // ------ 파파고 API 내부 함수
        #region 파파고 함수
        private string Change_Voca(string msg)
        {
            try
            {
                //받은 단어 와 언어 합쳐서 번역한 단어     받은 단어 + 받은 단어 언어  + 원하는 단어
                string json_string2 = OpenAPI.Translate(msg);

                string second = OpenAPI.Parse(json_string2);

                return second;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        #endregion
    }

}