using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace OmokGame
{	
	/// <summary>
	/// 오목판의 정보들을 가지고 있고, 오목 웹 서비스와 통신하는 클래스
	/// </summary>
	public class OmokData
	{	
		/// <summary>
		/// 인공지능 객체를 생성함
		/// </summary>		
		localhost.Service1 AI;

		/// <summary>
		/// 바둑판의 점들을 나타내는 변수, 19*19외곽에 또 하나의 경계선을 만든다.
		/// 자동으로 0으로 초기화된다
		/// </summary>		
		public int[,] nJums = new int[21,21];

		/// <summary>
		/// 물리기를 위해 처음부터 돌을 둔곳을 차례로 저장해두기 위한 변수
		/// </summary>		
		public Point[] ptStore = new Point[441];

		/// <summary>
		/// 위의 배열을 스택으로 이용하기 위해 쓰이는 변수 
		/// </summary>
		public int StackTop;
	
		public OmokData() 
		{
			//
			// TODO: Add constructor logic here
			//
			AI = new localhost.Service1();	// 초기화
			InitializeOmok();
			StackTop = 0;	//스택을 비운다.	
		}

		/// <summary>
		/// 오목 정보를 초기화하는 메소드
		/// </summary>
		public void InitializeOmok() 
		{
			nJums = new int[21,21];
			for(int i=0; i<21; i++) 
			{
				nJums[i,0] = (int)JumInfo.Boundary; //top 경계선 
				nJums[0,i] = (int)JumInfo.Boundary; //left 경계선 
				nJums[20,i] = (int)JumInfo.Boundary; //right 경계선 
				nJums[i,20] = (int)JumInfo.Boundary; //bottom 경계선 
			}
			ptStore = new Point[441];
		}


		/// <summary>
		/// 인자로 받은 좌표에 인자로 받은 돌의 종류를 입력한다
		/// </summary>
		/// <param name="ptChakJum">지금 둔 곳의 좌표</param>
		/// <param name="kindOfStone">지금 둔 돌의 색깔</param>		
		public void WriteJum(Point ptChakJum, int kindOfStone) 
		{
			//ptChackJum에 저장된 위치에 nTurn에 저장된 돌의 색깔을 저장함!
			nJums[ptChakJum.X, ptChakJum.Y] = kindOfStone;
			//저장장소에 두었던 위치를 저장함. 즉 스택에 Push!
			ptStore[StackTop].X = ptChakJum.X;	
			ptStore[StackTop].Y = ptChakJum.Y; 
			StackTop++;	
		}

		/// <summary>
		/// 해당 점이 비었는지 검은돌이 있는지 흰돌이 있는지를 리턴!
		/// </summary>
		/// <param name="i">해당 좌표의 x</param>
		/// <param name="j">해당 좌표의 y</param>
		/// <returns>해당 돌의 색깔</returns>		
		public int GetKindOfJum(int i, int j) 
		{
			return(nJums[i,j]);
		}

		/// <summary>
		/// 사용자 점을 관리하는 함수 
		/// 즉, 사용자가 돌을 놓은 곳으로 인해 어떤 결과가 생기는 지를 알아내는 함수 
		/// </summary>
		/// <param name="ptJum">사용자가 돌을 놓은 곳의 좌표</param>
		/// <param name="nKindOfStone">사용자가 어떤 돌을 놓았는가</param>
		/// <returns>결과값</returns>		
		public int ManageOfUserJum(Point ptJum, int nKindOfStone) 
		{
			//해당점에 이미 돌이 있으면 넘어감
			if(GetKindOfJum(ptJum.X, ptJum.Y)==(int)JumInfo.Empty) 
			{ 
				// [,] 배열을 웹서비스 인자로 보내기전에 string으로 변환해야한다.
				// 왼쪽 맨위부터 가로로 (0,0).. (20,0)으로 표시된다.
				string strJum = "";
				for(int j=0; j<21; j++)
				{
					for(int i=0; i<21; i++)
					{
						strJum = strJum.Insert(strJum.Length, nJums[i,j].ToString());
					}
				}

				try 
				{
					string str = AI.CheckChakJum(strJum, ptJum.X, ptJum.Y, nKindOfStone);
					//그점에 어떤 돌이 있는지의 정보를 저장
					WriteJum(ptJum, nKindOfStone);   
					switch(str) 
					{
						case "Win":
							//사용자가 이겼을 때
							return (int)Result.Win;	
						case "SamSam" : 
							//빈곳으로 설정
							nJums[ptJum.X, ptJum.Y] = (int)JumInfo.Empty;	
							//스택에 저장했던 돌의 좌표를 POP!!
							StackTop--;	
							return (int)Result.SamSam;
						case "Nothing" :
							// 아무것도 아닌 점에 놓음
							return (int)Result.Nothing; 
					}
				} 
				catch(WebException e) 
				{	
					MessageBox.Show(e.Message, "오목 웹 서비스에 다음과 같은 문제가 있습니다");
					return (int)Result.Problem;
				}


			}
			// 이미 놓여진 돌에 놓았을때 
			return (int)Result.Occupied;	
		}


		/// <summary>
		/// 컴퓨터 점을 관리하는 함수 
		/// 즉, 컴퓨터가 놓을 점을 알아내고, 그 점으로 인해 어떤 결과가 생기는 지를 알아내는 함수 
		/// </summary>
		/// <param name="ComStone">돌의 종류</param>
		/// <returns>결과값</returns>		
		public int ManageOfAIJum(int ComStone) 
		{
			// 이 부분이 웹 서비스를 받는 부분이다.
			try 
			{
				// [,] 배열을 웹서비스 인자로 보내기전에 string으로 변환해야한다.
				// 왼쪽 맨위부터 가로로 (0,0).. (20,0)으로 표시된다.
				string strJum = "";
				for(int j=0; j<21; j++)
				{
					for(int i=0; i<21; i++)
					{
						strJum = strJum.Insert(strJum.Length, nJums[i,j].ToString());
					}
				}

				// 이제 웹 메소드를 호출!!
				// 이 웹 메소드의 리턴값은 Point.ToString() 즉, 문자열 "{X= ,Y= }"이다.
				// 이것도 Point 구조체로 변환할 필요가 있다.
				StringBuilder strAIpt = new StringBuilder(AI.GetBestJum(strJum, ComStone));
				strAIpt.Replace("{X=", "");
				strAIpt.Replace("Y=", "");
				strAIpt.Replace("}", "");

				string str = strAIpt.ToString();
				char[] separators = new char[] {','};
				Point pt = new Point();
				string[] sub  = str.Split(separators);
				pt.X = Convert.ToInt32(sub[0]);	// beta2
				pt.Y = Convert.ToInt32(sub[1]);	// beta2
					
				WriteJum(pt, ComStone);	//컴퓨터가 고른 점에다가 컴돌을 입력 

				string strResult = AI.CheckChakJum(strJum, pt.X, pt.Y, ComStone);	// 	웹서비스를 이용하자.
				if(strResult == "Win") return (int)Result.Win;
				else return (int)Result.Nothing;

			} 
			catch(WebException e) 
			{
				MessageBox.Show(e.Message, "오목 웹 서비스에 다음과 같은 문제가 있습니다");
				return (int)Result.Problem;
			}
		}
	}
}
