//Control.cs
using System;
using System.Collections.Generic;

namespace _1010_실습관리프로그램
{
	/// <summary>
	/// 학  생 : 연결리스트 구조를 활용하여 저장
	/// 과목명 : 맵(키, value)구조를 활용하여 저장
	/// 성  적 : 배열 구조를 활용하여 저장​
	/// </summary>
	internal class Control
    {
		LinkedList<Student> students		= new LinkedList<Student>();
		Dictionary<int, Subject> subjects	= new Dictionary<int, Subject>();
		List<Grade> grades					= new List<Grade>();

		#region 생성자와 sample 초기화 처리
		public Control()
        {
			Temp_InitData();
		}

		private void Temp_InitData()
        {
			students.AddFirst(new Student(1000, "홍길동", "컴퓨터"));
			students.AddFirst(new Student(1001, "김길동", "IT"));
			students.AddFirst(new Student(1002, "허길동", "컴퓨터"));

			subjects.Add(101, new Subject(101, "C언어"));
			subjects.Add(102, new Subject(102, "C++언어"));
			subjects.Add(103, new Subject(103, "Java언어​"));
		}
		#endregion

		#region 학생(Student) 관련 메서드
		
		public void Student_Printall()
        {
			Console.WriteLine("학생정보 저장개수 : {0}개\n", students.Count);

			LinkedListNode<Student> cur = students.First;

			while (cur != null)
			{
				Student student = cur.Value;
				student.Print();

				cur = cur.Next;
			}
		}

		//F1기능
		public void Student_Insert()
        {			
			Console.WriteLine("[학생 정보 저장]\n");

			try
			{
				int id		 = WbLib.InputInteger("학생번호");
				string name  = WbLib.InputString("이름");
				string major = WbLib.InputString("학과명");

				Student student = new Student(id, name, major);
				students.AddFirst(student);

				Console.WriteLine("학생 정보 저장 성공");
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.ToString());
            }
		}

		//F2기능
		public void Student_Select()
		{
			Console.WriteLine("[학생 검색]\n");			

			try
			{
				int stunumber = WbLib.InputInteger("학생번호 입력");
				LinkedListNode<Student> node = NumberToNode(stunumber);
				node.Value.Println();
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
		}

		//F3기능
		public void Student_Update()
		{
			Console.WriteLine("[학생 정보 수정]\n");

			try
			{
				int stunumber  = WbLib.InputInteger("학생번호");
				string major   = WbLib.InputString("학과명");

				LinkedListNode<Student> node = NumberToNode(stunumber);
				node.Value.Major = major;

				Console.WriteLine("학과명 변경 성공");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//F4기능
		public void Student_Delete()
		{
			Console.WriteLine("[학생 삭제]\n");

			try
			{
				int stunumber = WbLib.InputInteger("학생번호");
				LinkedListNode<Student> node = NumberToNode(stunumber);
				students.Remove(node);

				Console.WriteLine("학생정보 삭제 성공");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		//순차검색(내부함수)
		private LinkedListNode<Student> NumberToNode(int num)
		{
			LinkedListNode<Student> cur = students.First;

			while (cur != null)
			{
				if (cur.Value.Number == num)
					return cur;
				cur = cur.Next;
			}
			throw new Exception("없다");
		}

		#endregion

		#region 과목(Subject) 관련 메서드
		public void Subject_Printall()
		{
			Console.WriteLine("\n과목 저장개수 : {0}개\n", subjects.Count);

			foreach (KeyValuePair<int, Subject> kv in subjects)
			{
				kv.Value.Print();
			}
		}

		//F5기능
		public void Subject_Insert()
		{
			Console.WriteLine("[과목 정보 저장]\n");

			try
			{
				int subjnumber	= WbLib.InputInteger("과목번호");
				string name		= WbLib.InputString("과목명");

				Subject subject = new Subject(subjnumber, name);
				subjects.Add(subjnumber, subject);

				Console.WriteLine("과목 정보 저장 성공");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		//F6기능
		public void Subject_Delete()
		{
			Console.WriteLine("[과목 삭제]\n");

			try
			{
				int subjnumber = WbLib.InputInteger("과목번호");				
				subjects.Remove(subjnumber);

				Console.WriteLine("과목정보 삭제 성공");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		#endregion

		#region 성적(Grade) 관련 메서드

		//F7) 성적 등록
		public void Grade_Insert()
		{
			Console.WriteLine("[성적 저장]\n");

			try
			{
				int stunum	= WbLib.InputInteger("학생번호");
				int majnum	= WbLib.InputInteger("과목번호");
				int jumsu	= WbLib.InputInteger("점수");

				Grade grade = new Grade(stunum, majnum, jumsu);
				grades.Add(grade);

				Console.WriteLine("성적 저장 성공");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		//F8) 성적 삭제(학생번호와 과목번호를 입력받아 해당 성적 삭제)
		public void Grade_Delete()
		{
			Console.WriteLine("[성적 삭제]\n");

			try
			{
				int stunum	 = WbLib.InputInteger("학생번호 입력");

				//해당 학생의 과목 리스트 출력
				Console.WriteLine("{0}번 학생의 과목번호리스트", stunum);
				StuGradePrintAll(stunum);

				int subnum = WbLib.InputInteger("과목번호 입력");

				Grade grade = grades.Find(obj => obj.StuNumber == stunum && obj.SubNumber == subnum);
				grades.Remove(grade);

				Console.WriteLine("성적 정보 삭제 성공");				

				//삭제후 과목정보 출력
				Grade_PrintAll();

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void StuGradePrintAll(int stunum)
        {
			List<Grade> findgrade = grades.FindAll(obj => obj.StuNumber == stunum);

			foreach (Grade grade in findgrade)
			{
				Console.WriteLine("({0},{1}) \t", grade.SubNumber, subjects[grade.SubNumber].Number );
			}
			Console.WriteLine();
		}


		//F9) 성적 검색(학생번호와 과목번호를 입력받아 검색 결과 출력)
		public void Grade_Select()
		{
			Console.WriteLine("[성적 검색]\n");

			try
			{
				int stunum = WbLib.InputInteger("학생번호 입력");
				int subnum = WbLib.InputInteger("과목번호 입력");

				Grade grade = grades.Find(obj => obj.StuNumber == stunum && obj.SubNumber == subnum);
				grade.Println();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//F10) 성적 검색(학생번호를 입력받아 해당 학생의 모든 과목 성적 출력)
		public void Grade_Select1()
		{
			Console.WriteLine("[학생별 모든 과목 검색]\n");

			try
			{
				int stunum = WbLib.InputInteger("학생번호 입력");
				StuGradePrintAll(stunum);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//F11)성적 수정(학생번호와 과목번호를 입력받아 해당 과목의 성적 수정)
		public void Grade_Update()
		{
			Console.WriteLine("[성적 수정]\n");

			try
			{
				int stunum = WbLib.InputInteger("학생번호 입력");

				//해당 학생의 과목 리스트 출력
				Console.WriteLine("{0}번 학생의 과목번호리스트", stunum);
				StuGradePrintAll(stunum);

				int subnum = WbLib.InputInteger("과목번호 입력");
				int jumsu = WbLib.InputInteger("변경할 점수 입력");

				Grade grade = grades.Find(obj => obj.StuNumber == stunum && obj.SubNumber == subnum);

				grade.Jumsu = jumsu;

				Console.WriteLine("변경되었습니다");

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		//F12) 성적 전체 리스트 출력
		public void Grade_PrintAll()
		{
			Console.WriteLine("저장개수 : {0}개\n", grades.Count);

			for (int i = 0; i < grades.Count; i++)
			{
				Grade grade = grades[i];
				grade.Print();
			}
			Console.WriteLine();
		}


		#endregion
	}
}
