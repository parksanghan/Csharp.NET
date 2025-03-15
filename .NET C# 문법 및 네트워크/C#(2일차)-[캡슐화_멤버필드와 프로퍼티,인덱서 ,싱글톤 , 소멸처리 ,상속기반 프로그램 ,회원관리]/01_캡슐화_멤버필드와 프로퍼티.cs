using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//클래스 정의시 첫번째
//맴버 필드 구성
//1) 맴버 필드 구성
//2) property(속성) 정의
//-> 클래스 내부에서나 클래스 외부에서는 반드시 프로퍼티를 사용할 것
   
namespace _1005
{
    //기본적인 프로퍼티
    internal class _01_Man
    {
        private string name;
        private int hp;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
    }

    //_01_Man과 동일한 코드
    //내부적으로 맴버필드가 자동 생성, 관련 프로퍼티코드가 자동 생성
    internal class _02_Man
    {
        private string Name { get; set; }
        private int Hp      { get; set; }       
    }


    //이름은 외부에서 수정 불가!**********************************
    //1) set 생성 X
    //2) set 속성을 은닉! (좀 더 바람직)
    internal class _03_Man
    {
        private string name;
        private int hp;

        public string Name
        {
            get { return name; }
            private set { name = value; }   //외부에서 접근 불가
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
    }

    //위 코드를 간결하게
    internal class _04_Man
    {
        public string Name { get; private set; }
        public int Hp      { get; set; }
    }


    //hp의 범위를 0~100으로 제한**********************************
    //set 설정 : 더 이상 간결하게 처리는 불가능!
    //           (맴버필드 생략, 프로퍼티만 구현)
    internal class _05_Man
    {
        private string name;
        private int hp;

        public string Name
        {
            get { return name; }
            private set { name = value; }   //외부에서 접근 불가
        }
        public int HP
        {
            get { return hp; }
            set 
            { 
                if( value >=0 && value <=100)
                    hp = value; 
            }
        }
    }

    
    //hp의 값이 처리가 안되었을 경우 예외 발생 *******************
    //이전 코드는 외부에서 hp 변경 여부 확인 불가능
    internal class _06_Man
    {
        private string name;
        private int hp;

        public string Name
        {
            get { return name; }
            private set { name = value; }   //외부에서 접근 불가
        }
        public int HP
        {
            get { return hp; }
            set
            {
                if (value >= 0 && value <= 100)
                    hp = value;
                else
                    throw new Exception("범위를 벗어남 : " + value);
            }
        }
    }
}
