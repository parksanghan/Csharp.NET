using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Reflection;
using System.Resources;

namespace OmokGame
{	
	/// <summary>
	/// 좌표 하나에 대한 정보
	/// </summary>
	enum JumInfo 
	{
		
		Empty,		// 비었다
		Black,		// 검은 돌이 놓여있다
		White,		// 흰 돌이 놓여있다
		Boundary	// 테두리이다.
	}
	
	/// <summary>
	/// 결과값을 의미
	/// </summary>
	enum Result 
	{
		Win,		// 승리
		SamSam,		// 삼삼
		Nothing,	// 아무 결과도 생기지 않는 점에 놓음
		Occupied,	// 이미 놓여진 돌에 놓았을때 
		Problem		// 웹 서비스에 문제가 있을때	
	}

	/// <summary>
	/// 이 폼은 오목판과 메뉴가 있는 폼으로, 가장 주된 폼이다.
	/// </summary>
	/// <remarks>
	/// 오목판, 검은돌, 흰돌 이미지는 각각 Bitmap 클래스 멤버를 이용한다. 
	/// 그리고, 화면에 관련된 각종 멤버들이 있다.
	/// </remarks>
	public class OmokForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용자의 돌을 저장하는 비트맵 클래스
		/// </summary>
		Bitmap UserBitmap;
		/// <summary>
		/// 컴퓨터의 돌을 저장하는 비트맵 클래스
		/// </summary>
		Bitmap ComBitmap;
		/// <summary>
		/// 오목판 이미지를 저장하는 배트맵 클래스
		/// </summary>
		Bitmap BoardBitmap;		
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;

		/// <summary>
		/// 바둑판 위쪽 외곽의 픽셀을 나타내는 상수
		/// </summary>
		const int TopMarginPixel = 3;  
		/// <summary>
		/// 바둑판 왼쪽 외곽의 픽셀을 나타내는 상수
		/// </summary>
		const int LeftMarginPixel = 4;
		/// <summary>
		/// 돌 하나가 오목판 위에 차지하는 픽셀이 가로 19 세로 19임을 나타내는 상수
		/// </summary>
		const int JumArea = 19;

		/// <summary>
		/// 승리한 후 마우스를 캡쳐하기 위해 쓰이는 변수
		/// </summary>
		bool bMouseCapture;

		/// <summary>
		/// 먼저 하는 돌이 사용자인지인지 컴퓨터인지 저장하는 변수
		/// </summary>
		int nKindOfStartStone;


		/// <summary>
		/// 승리한 후라는 것을 나타내주는 필드
		/// 이 변수는 승리한 후 물리기를 하면 다시 바둑판이 활성화되게끔 하기 위해 필요하다.
		/// </summary>		
		bool bWinAfter;

		/// <summary>
		/// 승리한 후 물리기를 하면 다시 마우스를 정상으로 되돌려 놓아야 하므로, 이 변수를 보고 마우스 캡쳐를 푼다.		
		/// </summary>		
		bool bWinMulligi;

		/// <summary>
		/// 사람이 이겼다고 알리는 변수
		/// </summary>
		bool bUserWin;
		/// <summary>
		/// 컴퓨터가 이겼다고 알리는 변수
		/// </summary>
		bool bComWin;
		/// <summary>
		/// 새 게임을 하기위해 쓰이는 메뉴 아이템
		/// </summary>
		private System.Windows.Forms.MenuItem menuItemNew;
		/// <summary>
		/// 물리기를 위한 메뉴 아이템
		/// </summary>
		private System.Windows.Forms.MenuItem menuItemMulligi;
		/// <summary>
		/// 현재 오목판을 수치로 보기위한 메뉴 아이템
		/// </summary>
		private System.Windows.Forms.MenuItem menuCurrentOmokPanInfo;
		/// <summary>
		/// 오목게임을 끝내기 위한 메뉴 아이템
		/// </summary>
		private System.Windows.Forms.MenuItem menuItemQuit;
		/// <summary>
		/// 정보를 표시하는 메뉴 아이템
		/// </summary>
		private System.Windows.Forms.MenuItem menuItemInfo;
		private System.Windows.Forms.MenuItem menuItem2;

		/// <summary>
		/// 현재 두는 오목에 대한 모든 데이터가 들어가게 된다.
		/// 그리고, 이 필드를 통해서 오목 웹 서비스와 통신한다.
		/// </summary>
		OmokData omokData;

		/// <summary>
		/// 생성자
		/// </summary>
		public OmokForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		
			try 
			{
				BoardBitmap = new Bitmap(@"board.GIF");			
				ClientSize = BoardBitmap.Size;

				omokData = new OmokData();

				// 시작 폼을 보여준다.
				StartForm sf = new StartForm();

				// 누가 먼저 하는지 알 수 있다.
				if(sf.ShowDialog() == DialogResult.Yes) 
				{
					nKindOfStartStone = (int)JumInfo.Black;
					UserBitmap = new Bitmap(@"black.GIF");
					UserBitmap.MakeTransparent(Color.Cyan);
					ComBitmap = new Bitmap(@"white.GIF");
					ComBitmap.MakeTransparent(Color.Cyan);
				}
				else 
				{
					nKindOfStartStone = (int)JumInfo.White;
					UserBitmap = new Bitmap(@"white.GIF");
					UserBitmap.MakeTransparent(Color.Cyan);
					ComBitmap = new Bitmap(@"black.GIF");
					ComBitmap.MakeTransparent(Color.Cyan);
				}

				omokData = new OmokData();

				if(nKindOfStartStone == (int)JumInfo.White) 
				{	//시작이 컴이면
					Point point = new Point(10,10);		//컴의 처음 둘 자리를 지정하고
					omokData.WriteJum(point, nKindOfStartStone); //컴(검은돌)이 둔다 
					TurnStone(ref nKindOfStartStone);	//그리고 다시 사용자돌로 바꿔준다. 
				}
			} 
			catch(ArgumentException e) 
			{
				MessageBox.Show(e.ToString(), "이미지 파일의 경로를 수정하세요");
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemNew = new System.Windows.Forms.MenuItem();
			this.menuItemMulligi = new System.Windows.Forms.MenuItem();
			this.menuCurrentOmokPanInfo = new System.Windows.Forms.MenuItem();
			this.menuItemQuit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemInfo = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemNew,
																					  this.menuItemMulligi,
																					  this.menuCurrentOmokPanInfo,
																					  this.menuItemQuit});
			this.menuItem1.Text = "게임";
			// 
			// menuItemNew
			// 
			this.menuItemNew.Index = 0;
			this.menuItemNew.Text = "새게임";
			this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
			// 
			// menuItemMulligi
			// 
			this.menuItemMulligi.Index = 1;
			this.menuItemMulligi.Text = "한수 물리기";
			this.menuItemMulligi.Click += new System.EventHandler(this.menuItemMulligi_Click);
			// 
			// menuCurrentOmokPanInfo
			// 
			this.menuCurrentOmokPanInfo.Index = 2;
			this.menuCurrentOmokPanInfo.Text = "현재 오목판의 수치화";
			this.menuCurrentOmokPanInfo.Click += new System.EventHandler(this.menuItemCurrentOmokPanInfo_Click);
			// 
			// menuItemQuit
			// 
			this.menuItemQuit.Index = 3;
			this.menuItemQuit.Text = "끝내기";
			this.menuItemQuit.Click += new System.EventHandler(this.menuItemQuit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemInfo});
			this.menuItem2.Text = "도움말";
			// 
			// menuItemInfo
			// 
			this.menuItemInfo.Index = 0;
			this.menuItemInfo.Text = "정보";
			this.menuItemInfo.Click += new System.EventHandler(this.menuItemInfo_Click);
			// 
			// OmokForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(404, 369);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "OmokForm";
			this.Text = "오목 웹 서비스를 이용한 인공지능 오목게임";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new OmokForm());
		}

		/// <summary>
		/// 화면에 이미지를 나타내는 메소드
		/// </summary>
		/// <param name="e">이 객체에서 화면과 관련된 Graphics객체를 이용한다</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			// 바둑판을 그림
			Graphics g = e.Graphics;
			if (BoardBitmap != null) 
			{
				g.DrawImage (BoardBitmap, ClientRectangle);
			}	
			// 바둑돌을 그림
			for(int i=1; i<=19; i++)
			{
				for(int j=1; j<=19; j++) 
				{
					//검은돌이 놓여진 점들을 화면에 출력
					if(omokData.GetKindOfJum(i,j) == (int)JumInfo.Black) 
					{
						g.DrawImage(UserBitmap, LeftMarginPixel+((i-1)*JumArea), TopMarginPixel+((j-1)*JumArea), 18, 18);
					}
						//흰돌이 놓여진 점들을 화면에 출력 
					else if(omokData.GetKindOfJum(i,j) == (int)JumInfo.White) 
					{
						g.DrawImage(ComBitmap, LeftMarginPixel+((i-1)*JumArea), TopMarginPixel+((j-1)*JumArea), 18, 18);
					}
				}
			}
			g.Dispose();

		}

		/// <summary>
		/// 오목판에 마우스를 누르면 호출되는 메소드
		/// </summary>
		/// <param name="e">이 인자로 마우스로 클릭한곳의 좌표를 읽어낸다</param>
		protected override void OnMouseDown(MouseEventArgs e) 
		{
			bool bAICapture = false;

			//승리한후 한수 물리면, 마우스캡쳐를 풀고, 돌을 바꾼다.
			if(bWinMulligi == true) 
			{	
				bMouseCapture = false;
				//승리 중에서 컴이 이겼을때 이면 돌을 바꾸어 주어야 된다
				if(bComWin && !bUserWin)	
				{
					TurnStone(ref nKindOfStartStone);
				}
			}

			if(bMouseCapture == false) 
			{
				//마우스로 눌려진 픽셀을 바둑판 19*19표로 저장할 data
				Point ptChakJum = new Point();	
				//픽셀좌표를 19*19로 변환, 19는 바둑판 한 cell의 픽셀 수 
				ptChakJum.X = (e.X - LeftMarginPixel)/JumArea+1; 
				ptChakJum.Y = (e.Y - TopMarginPixel)/JumArea+1;

				//사용자 돌에 대한 조사
				switch((Result)omokData.ManageOfUserJum(ptChakJum, nKindOfStartStone)) 
				{
					case Result.Win :
						bMouseCapture = true;
						bAICapture = true;

						bUserWin = true;
						bComWin = false;	//사람이 승리했음을 나타내는 변수
						bWinAfter = true;

						Invalidate(new Rectangle(LeftMarginPixel+((ptChakJum.X-1)*JumArea), TopMarginPixel+((ptChakJum.Y-1)*JumArea), 18, 18));
						// 5개 점을 선으로 이어서 보여주면 좋은데...
						MessageBox.Show("You win, congratulation!", "*^^*");
						break;

					case Result.SamSam :
						MessageBox.Show("앗, 거기두면 삼삼입니다. \n다시 두세요!!", "-.-;");
						bAICapture = true; //삼삼이니까 다시 두기위해 잠시 AI를 잠금
						break;

					case Result.Nothing :
						TurnStone(ref nKindOfStartStone);	//돌을 두고 컴퓨터돌로 넘겨주고,
						bAICapture = false;		//컴퓨터 AI를 하기위해서
						// 화면 깜박임을 없애기 위해 갱신된 부분만 화면에 다시 뿌려준다.
						Invalidate(new Rectangle(LeftMarginPixel+((ptChakJum.X-1)*JumArea), TopMarginPixel+((ptChakJum.Y-1)*JumArea), 18, 18));
						break;

					case Result.Occupied :
						//있는데 돌을 또 두면 사용자가 다시 두어야 하니까, TRUE로!
						bAICapture = true;	
						break;
					default :
						break;
				}

	
				//컴퓨터가 두는 것에 대한 처리
				if(bAICapture==false) 
				{
					switch((Result)omokData.ManageOfAIJum(nKindOfStartStone)) 
					{
						case Result.Win :
							bMouseCapture = true;

							//컴이 승리 했슴을 나타내는 변수들
							bComWin = true;
							bUserWin = false;
							bWinAfter = true;
							Invalidate(new Rectangle(LeftMarginPixel+((omokData.ptStore[omokData.StackTop-1].X-1)*JumArea), TopMarginPixel+((omokData.ptStore[omokData.StackTop-1].Y-1)*JumArea), 18, 18));
							MessageBox.Show("Com win, You lose", "-_-;");
							break;
						case Result.SamSam :
							break;
						case Result.Nothing :
							TurnStone(ref nKindOfStartStone);
							// 화면 깜박임을 없애기 위해 갱신된 부분만 화면에 다시 뿌려준다.
							Invalidate(new Rectangle(LeftMarginPixel+((omokData.ptStore[omokData.StackTop-1].X-1)*JumArea), TopMarginPixel+((omokData.ptStore[omokData.StackTop-1].Y-1)*JumArea), 18, 18));
							break;
						default :
							break;
					}
				}
			}

			//승리한후 한수 물리면 승리한것은 없던것으로 
			bWinMulligi = false;	
		}

		/// <summary>
		/// 시작 폼을 보여주는 메서드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemNew_Click(object sender, System.EventArgs e)
		{
			bMouseCapture = false;
			//같은 돌을 선택하면 계속해서 대화상자를 다시 띄우기 위한 while루프.
	
			// 시작 폼을 보여준다.
			StartForm sf = new StartForm();
		
			// 누가 먼저 하는지 알 수 있다.
			if(sf.ShowDialog() == DialogResult.Yes) 
			{
				nKindOfStartStone = (int)JumInfo.Black;
				UserBitmap = new Bitmap(@"black.GIF");
				UserBitmap.MakeTransparent(Color.Cyan);
				ComBitmap = new Bitmap(@"white.GIF");
				ComBitmap.MakeTransparent(Color.Cyan);
			}
			else 
			{
				nKindOfStartStone = (int)JumInfo.White;
				UserBitmap = new Bitmap(@"white.GIF");
				UserBitmap.MakeTransparent(Color.Cyan);
				ComBitmap = new Bitmap(@"black.GIF");
				ComBitmap.MakeTransparent(Color.Cyan);
			}
			GameStart();
		}

		/// <summary>
		/// 사용자와 컴의 순서를 바꾸어 주는 함수
		/// </summary>
		/// <param name="turn">현재 돌의 색깔 정보를 나타내는 수가 들어온다.</param>		
		void TurnStone(ref int turn) 
		{
			if(turn == (int)JumInfo.Black)	turn = (int)JumInfo.White;
			else turn = (int)JumInfo.Black;
		}

		/// <summary>
		///  첫 대화상자를 마치면 실행되는 함수로서 바둑판의 초기화등 
		///  오목을 시작할 수 있게끔 하는 함수 
		/// </summary>
		public void GameStart()
		{
			omokData.InitializeOmok();			//바둑판 초기화 즉, 돌들을 싹 제거
			Invalidate();		//OnPaint함수 호출! (게임 도중에 이함수가 불리워지면 다시 그려야 하므로)

			if(nKindOfStartStone==(int)JumInfo.White) 
			{				//시작이 컴이면
				Point point = new Point(10,10);		//컴의 처음 둘 자리를 지정하고
				omokData.WriteJum(point, nKindOfStartStone); //컴(검은돌)이 둔다 
				TurnStone(ref nKindOfStartStone);			//그리고 다시 사용자돌로 바꿔준다. 
			}

		}

		/// <summary>
		/// 프로그램을 종료하는 메소드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemQuit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// 정보 폼을 보여주는 메소드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemInfo_Click(object sender, System.EventArgs e)
		{
			AboutForm f = new AboutForm();
			f.ShowDialog();
		}

		/// <summary>
		/// 한수 물리기를 하는 메소드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemMulligi_Click(object sender, System.EventArgs e)
		{
			if(omokData.StackTop > 0) 
			{
				omokData.StackTop--;
				omokData.nJums[omokData.ptStore[omokData.StackTop].X, omokData.ptStore[omokData.StackTop].Y] = (int)JumInfo.Empty;
				Invalidate(new Rectangle(LeftMarginPixel+((omokData.ptStore[omokData.StackTop].X-1)*JumArea), TopMarginPixel+((omokData.ptStore[omokData.StackTop].Y-1)*JumArea), 18, 18));
			}
			if(omokData.StackTop > 0) 
			{
				omokData.StackTop--;
				omokData.nJums[omokData.ptStore[omokData.StackTop].X, omokData.ptStore[omokData.StackTop].Y] = (int)JumInfo.Empty;
				Invalidate(new Rectangle(LeftMarginPixel+((omokData.ptStore[omokData.StackTop].X-1)*JumArea), TopMarginPixel+((omokData.ptStore[omokData.StackTop].Y-1)*JumArea), 18, 18));
			}

			if(bWinAfter) 
			{	//승리한 후 물리기를 한다면 
				bWinMulligi = true;	
				bWinAfter = false;	//이기기전으로 돌아가야지
			}
		}

		/// <summary>
		/// 오목판의 수치화해서 보여주는 메소드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemCurrentOmokPanInfo_Click(object sender, System.EventArgs e)
		{
			OmokJumsInfoForm opf = new OmokJumsInfoForm();
			string str = "";
			for(int j=0; j<21; j++)
			{
				for(int i=0; i<21; i++)
				{
					str = str.Insert(str.Length, omokData.nJums[i,j].ToString());
				}
			}
			opf.InputOmokPan(str);
			if(opf.ShowDialog() == DialogResult.OK) 
			{
				for(int j=0; j<21; j++) 
				{
					for(int i=0; i<21; i++) 
					{
						omokData.nJums[i,j] = Convert.ToInt32(opf.strOmokPan[i+j*21].ToString());
					}
				}
				Invalidate();
			}

		}

	}
}
