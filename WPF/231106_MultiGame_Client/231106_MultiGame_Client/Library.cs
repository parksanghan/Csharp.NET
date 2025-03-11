using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace _231106_MultiGame_Client
{
    internal static class Library
    {
        public static float MakeDirection(Vector2 centerP, Vector2 targetP)
        {
            // 두 점 간의 벡터 계산
            Vector2 directionVector = targetP - centerP;

            // Atan2 함수를 사용하여 각도 계산 (라디안 값을 도로 변환)
            float angleInRadians = (float)Math.Atan2(directionVector.Y, directionVector.X);
            float angleInDegrees = MathHelper.ToDegrees(angleInRadians);

            // 결과가 음수인 경우 양수 각도로 변환
            if (angleInDegrees < 0)
            {
                angleInDegrees += 360;
            }

            return angleInDegrees;
        }
        public static void DrawRotatedImage(Graphics g, Image image, float angle, Vector2 position, float scale)
        {
            // 이미지의 중심을 기준으로 회전하도록 조정
            float centerX = position.X + image.Width / 2;
            float centerY = position.Y + image.Height / 2;

            // 회전 변환을 적용하기 위한 매트릭스 생성
            Matrix matrix = new Matrix();
            matrix.Translate(-centerX, -centerY, MatrixOrder.Append); // 이미지의 중심을 기준으로 이동
            matrix.Rotate(angle, MatrixOrder.Append); // 주어진 각도로 회전
            matrix.Scale(scale, scale, MatrixOrder.Append); // 주어진 크기로 확대 또는 축소
            matrix.Translate(centerX, centerY, MatrixOrder.Append); // 다시 이미지의 중심으로 이동

            // 매트릭스를 그래픽스 객체에 적용
            g.Transform = matrix;

            // 이미지를 변환된 위치에 그림
            g.DrawImage(image, position.X, position.Y);

            // 그래픽스 객체의 변환을 초기화하여 다음 그리기 작업에 영향을 주지 않도록 함
            g.ResetTransform();
        }
    }
    public static class MathHelper
    {
        public const float Pi = (float)Math.PI;

        public static float ToDegrees(float radians)
        {
            return radians * 180.0f / Pi;
        }
        public static float ToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180.0);
        }
        public static float GetAngleByVectorToVector(Vector2 v1, Vector2 v2)
        {
            // 각도 계산
            float angleRad = (float)Math.Atan2(v2.Y - v1.Y, v2.X - v1.X);

            // 라디안을 0~2*pi 범위로 변환
            float angleDeg = (float)(angleRad * (180.0 / Math.PI) + 360) % 360;

            return angleDeg;
        }
        public static Vector2 GetVectorByAngle(float angleDegrees)
        {
            float angleRad = ToRadians(angleDegrees);

            // 크기가 1인 벡터 계산
            float x = (float)Math.Cos(angleRad);
            float y = (float)Math.Sin(angleRad);

            return new Vector2(x, y);
        }
    }
}
