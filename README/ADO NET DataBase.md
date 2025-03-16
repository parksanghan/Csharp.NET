# ADO.NET DataBase

![Untitled](ADO%20NET%20DataBase%205a9b694c9235472a9a11f7a64cc72991/Untitled.png)

**연결형 데이터 베이스** 

**Adapter는 비연결형 데이터  전송**

**xml 은 dataset 을 통해서 문서화를 할수 있다.** 

**dataset은 데이터 소스와 관계없이 프로그램의 논리적 데이터를 구성하여 원하는 작업처리를 담당.**

**원본 db는 원본에 대해 직접적인 접근은 할수 없지만 DBMS 에서 알아서 원본에 대해서  관리를 해준다.**

**Database는 사용자의 요구에 즉각적으로 응답하는 실시간 접근성을 제공하고 삽입, 삭제, 변경 등의 작업으
로 유효한 데이터의 변화를 갖게 됩니다. 또한 여러 사용자가 동시에 원하는 데이터에 접근 가능하며 사용자
가 직접 data의 물리적 주소에 직접 접근하는 것을 막고 간접적으로 원하는 데이터를 참조할 수 있게 해 줍
니다. 이러한 특징을 갖는 Database는 "여러 사용자가 동시에 사용할 수 있게 통합 관리하는 데이터의 집합"
이라고 정의할 수 있습니다.**

**물리스키마 [파일]     | 외부 스키마 [계정 기반] → 로그인과 연결된 DB만 볼수있다.**

**▷참조 무결성 (Referntial Integrity)
데이터 베이스에 서로 다른 테이블을 정의할 때 다른 테이블의 주요 키를 자신의 테이블의 외래 키(Foreign
Key)로 설정하여 관계를 정의할 수 있습니다. 이처럼 관계를 정의하면 다른 테이블의 데이터의 주요 키 값이
없는 데이터를 추가하는 것을 방지할 수 있습니다**

**데이터 베이스를 이용한 쿼리문 c#윈폼**

![Untitled](ADO%20NET%20DataBase%205a9b694c9235472a9a11f7a64cc72991/Untitled%201.png)

**프로시저의 데이터베이스** 

**저장프로시저** 

- **반환이 없는 프로시저함수**

```sql
ALTER PROCEDURE dbo.RemoveSaleByPID
(
@PID int
)
AS
delete from Sale where PID = @PID
RETURN
```

- **결과에 대한 반환이 있는 프로시저함수**

```sql
ALTER PROCEDURE dbo.RemoveProduct
(
@PNAME varchar(50),
@Result int OUTPUT
)
AS
declare @PID int
Exec FindPIDByName @PNAME,@PID OUTPUT
if @PID = -1 
begin
set @Result = 0
end
else
begin
Exec RemoveSaleByPID @PID
delete from Product where PID = @PID
set @Result = 1
end
RETURN
```

**output 은 값이 나갈수도 값이 들어올수도 있다** 

**프로시저 기반 DB 실습** 

**연결형 데이터 베이스    → 선택지 쿼리문 방식 ,**  

**비연결형 데이터베이스**

**DB 에 이미지를 저장하는데 경로를 저장** 

**화면에다가 이미지를 띄운다거나  , 영상을 재생** 

**데이터 테이블은 열의 스키마**  

**데이터 테이블 클래스에는 메모리상의 테이블이다. ADO.net에서는 논리적 DB 를 XML 형태로 저장하고 로딩할 수 있게 기술을 지원한다.**

**메타데이터 .Net 자기의 어셈블리에 대한 코드 정보를 갖고 있는데 xml 도 동일한 형식이다.** 

**xml → 요소의 이름을 바꿔도 문제가 발생하지 않는다** 

**xml → 에는 스키마 가 제공되는데  요소의 키워드와 설정 정보 ,데이터 관계를 알수 있다.**

**정의된 문서와 xml로 구성된 문서** 

**데이터테이블 직접접근하도록 쓰지않는다** 

**원본테이블 숨김** 

**data view 를 하나만들어서 간접적으로 테이블에 접근하도록 인터페이스를 만든다 .**

**원본은 바뀌는데 동기화된 테이블은 바뀌지 않는다** 

**view 의 성질 ?** 

**view는 실제로 존재ㅏ는게 아닌 저장할때 view에 저장된게 아닌 view 를 통해 저장하는데 이때 view는 쓰기 전용 view이다 .**

**view 사용목적 필터링 용도** 

**가상의 메모리 DB** 

**오후 →**  

**Adapter     → 실제 물리적 DB 에 잇는걸 가져와서 메모리 DB 에 넣어주는게  fill**

**수정 된 메모리 DB 에 있는걸 원본에 동기화**

**→ 비연결 쓰는이유 원본과는 다른 데이터 베이스가 생김**

**일반적  데이터 베이스 는 연결형이나 ,  실제론 연결형 , 비연결형  혼용해서 사용** 

**fiill 메서드 업데이트 메서드** 

**fill 메서드가 호출되면** 

**fiil(){**

**dbopen ()**   

**datatable 생성 <= 데이터 스키마**

**reader <= 데이터 채우기수행** 

**}**

**fill 메서드 후에** 

**SqlDataDataper에 검색, 추가, 변경, 삭제에 사용할 SqlCommand를 초기에 설정한 후에 데이터 소스의 데이터를 Fill 메서드를 이용해 DataSet을 채우고 이 후에 작업은 DataSet으로 데이터를 관리하다가 데이터 소스를 변경할 필요가 있을 때 Update 메서드를 이용하여 데이터 소스에 반영시키는 것이 일반적인 사용**

**XML .NET** 

**⇒ XML 을 다루는 API  ?  XML 송수신을 통해 XML Reader를 통해 파싱과정과 XML Writer 변환  ⇒ 스키마 ? ⇒ 메타 데이터** 

**XML 을 데이터를 파싱할 수 있지만 XML 메타 데이터도 파싱처리 할 수 있다.**

**DTD : 유효성 검사를 할 수 있는 문서 구조 인데  ⇒ 데이터 표준화를 위해 구조화 정의** 

 ****

**스키마 선언 :  xml의 네임스페이스** 

**목요일에 시험 아마 데이터 베이스 sql  쿼리를 날리는방식일듯…?** 

# 연결형 데이터 베이스

## 데이터 베이스 연결

```csharp
private SqlConnection scon= null;

        const string constr =
@"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial 
Catalog=WB38;User ID =aaa;Password=1234;";
public SqlConnection SCon {  get { return scon; } }

```

```csharp
public bool Connect()

        {
                scon = new SqlConnection();

                scon.ConnectionString = constr;

                scon.Open();

                return true;
   }
```

## 쿼리문 방식

```csharp
		           string qury =

                    string.Format
("insert into SALGRADE  values({0}, {1}, {2});", id, r, h);
								SqlCommand cmd = scon.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text; //기본값

                cmd.CommandText = qury;
                //방법2
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = scon;
                cmd2.CommandType = System.Data.CommandType.Text; //기본값
                cmd2.CommandText = qury;
                //방법3
                SqlCommand cmd3 = new SqlCommand(qury, scon);
                //cmd2.CommandType은 생략
                if (cmd3.ExecuteNonQuery() == 1)
```

## 파라미터 쿼리 방식

```csharp
private SqlCommand MakeInsertCommand()

        {
            string sql = "insert into Account(accid, name, balance) values(@Accid, @Name, @Balance)"
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@Accid", SqlDbType.Int, 4, "accid");

            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20, "name");

            cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");
            return cmd;
					  or cmd.ExecuteNonQuery();
        }
```

## Select 쿼리로 읽어 오는방식

```csharp
string qury = string.Format("select * from SALGRADE;");

                SqlCommand cmd3 = new SqlCommand(qury, scon);
                SqlDataReader reader = cmd3.ExecuteReader();
                List<string> grades = new List<string>();
                while (reader.Read())   //0...N RowData를 순차적 접근
                {
                    string temp = string.Empty;
                    //1방법 : 컬럼명
                    //temp += reader["GRADE"] + "\t";
                    //temp += reader["LOSAL"] + "\t";
                    //temp += reader["HISAL"];
                    //grades.Add(temp);
                    //2방법 : 인덱스
                    temp += reader[0] + "\t";
                    temp += reader[1] + "\t";
                    temp += reader[2];
                    grades.Add(temp);
                }
```

# 비연결형 데이터 베이스

/

## Adapter 사용

```csharp
private DataSet ds = new DataSet("WB38");
SqlConnection conn = new SqlConnection(constr);

            SqlDataAdapter ad = new SqlDataAdapter();

            

            string sql = "select * from Account";

            ad.SelectCommand = new SqlCommand(sql, conn);

            ad.FillSchema(ds, SchemaType.Mapped, "Account");

            ad.Fill(ds, "Account");

            ad.Dispose();

            conn.Dispose();

            Table_Schema_Print(ds.Tables["Account"], listBox1);

            dataGridView1.DataSource = ds.Tables["Account"];

```

## Table insert

 

```csharp
string name = textBox1.Text;

                int balance = int.Parse(textBox2.Text);

                DateTime dt = monthCalendar1.SelectionStart;

                DataRow dr = ds.Tables[0].NewRow(); //Account

                dr["AccID"] = 1234;

                dr["Name"] = name;

                dr["Balance"] = balance;

								ds.Tables[0].Rows.Add(dr);
```

## Adapter insert

```csharp
SqlConnection conn = new SqlConnection(constr);

                SqlDataAdapter ad = new SqlDataAdapter();

                string sql = "insert into Account(accid, name, balance) values(@Accid, @Name, @Balance)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add("@Accid", SqlDbType.Int,4, "accid");

                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20, "name");

                cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");

                ad.InsertCommand = cmd;              
                //ad.FillSchema(ds, SchemaType.Mapped, "AccountIO");

                ad.Update(ds, "Account");
                ad.Dispose();
                conn.Dispose();
```

```csharp
const string constr =

                 @"Data Source=DESKTOP-0I86BTV\SQLEXPRESS;Initial Catalog=WB38;User ID =aaa;Password=1234;";

        private SqlConnection conn = new SqlConnection(constr);

        private SqlDataAdapter ad = new SqlDataAdapter();

        private DataSet ds = new DataSet("WB38");

        public  DataTable Account_Table { get { return ds.Tables["Account"]; } }

public WbDB()

        {            

            ad.SelectCommand = MakeSelectCommand();

            ad.InsertCommand = MakeInsertCommand();

            ad.DeleteCommand = MakeDeleteCommand();

            ad.UpdateCommand = MakeUpdateCommand();

        }
```

# 쿼리문

```csharp
				public void Update_AccountTable()  // 어뎁터 업데이트 

        {

            try

            {

                ad.Update(ds, "Account");

                MessageBox.Show("Update성공");

            }

				public void Fill_AccountTable()

        {

            try

            {

                ad.FillSchema(ds, SchemaType.Mapped, "Account");

                ad.Fill(ds, "Account");

            }

            catch(Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        public void Update_AccountTable()

        {

            try

            {

                ad.Update(ds, "Account");

                MessageBox.Show("Update성공");

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

    

        public bool Account_Insert(int accid, string name, int balance)

        {

            try

            {

                DataRow dr = ds.Tables["Account"].NewRow(); //Account

                dr["AccID"] = accid;

                dr["Name"] = name;

                dr["Balance"] = balance;

                ds.Tables[0].Rows.Add(dr);

                return true;

            }

            catch(Exception e)

            {

                MessageBox.Show(e.Message);

                return false;

            }

        }

        public bool Account_Delete(int accid)

        {

            try

            {

                DataRow dr = ds.Tables["Account"].Rows.Find(accid); //********

                ds.Tables["Account"].Rows.Remove(dr);

                return true;

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

                return false;

            }

        }

    

        public bool Account_Update(int accid, int money)

        {

            try

            {

                DataRow dr = ds.Tables["Account"].Rows.Find(accid); //********

                dr["Balance"] = money;               

                return true;

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

                return false;

            }

        }
```