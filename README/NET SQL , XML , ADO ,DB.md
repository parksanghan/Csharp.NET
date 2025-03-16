# .NET SQL , XML , ADO ,DB

emp 회사원

데이터 저장관리 

![Untitled](NET%20SQL%20,%20XML%20,%20ADO%20,DB%209b656ad932a849638ca43b419b715c93/Untitled.png)

DBMS 데이터 관리

MYSQL - 알고리즘 제공해줌 

DBMS 개발과정 

- DB 생성
- SQL 계정 생성 및 권한부여
- - 데이틉ㄹ생성

SQL 기능에 따른 분류

- 데이터 정의어 DDL : 테이블이나 구조를  생성하는데 사용하며 CREATE(생성) , ALTER(수정) ,DROP(삭제) 문 (정의)
- 데이터 조작어 DML : 테이블에 데이터를 검색 , 삽입 ,수정 삭제하는데 사용되며 SELECT , INSERT (갱신), DELETE , UPLOAD( ⇒ 여기서 SELECT문은 특별히 질의어라 부른다

데이터 베이스에서의 null 허용 = > 

데이터 베이스 제 1 정규화 

## SELECT 기본 사용법

select  쿼리분

```sql
SELECT       // 검색결과 
FROM      // 대상 => 어디에서   EX ) 어떠한 테이블 명 
[WHERE]  // 조건 > 연산자를 사용 !
[GROUP BY]   // 테이블 내에 있는 로우 데이터 남녀 나누기 구분 로우 데이터
[HAVING]     // GROUP BT 종속문장 : 그룹 내 조건 
[ORDER BY]   // 정렬 
```

⇒ 대괄호는 생략이 가능하다.

⇒ 순서는 바뀔

![Untitled](NET%20SQL%20,%20XML%20,%20ADO%20,DB%209b656ad932a849638ca43b419b715c93/Untitled%201.png)

```sql
SELECT from* EMP // 전체
Select empno , ename , job from emp;
Select enmae, empno ,from emp  ~ row (14)
Select count(*) from emp ;  //함수 
Select count(comm) from emp; // 
```

2개의 테이블을 연결하는건 join  → 데이터 정규화에 사용 

카타시안 프로덕트  2개의 테이블 합치는 것이 join

조건없이 join → 결과는 col : 7개 

[equal]

부모 pk = 자식 pk 

결과 : col🕖

row : 기준 테이블의 row 의 개수 * 목적

[non equal]

= 를 사용하지 않고 다른 연산자 활용 

[outter]

null 을 포함한 조인 처리 

# 데이터 정규화

- 제 1정규화

![Untitled](NET%20SQL%20,%20XML%20,%20ADO%20,DB%209b656ad932a849638ca43b419b715c93/Untitled%202.png)

- 제 2정규화

![Untitled](NET%20SQL%20,%20XML%20,%20ADO%20,DB%209b656ad932a849638ca43b419b715c93/Untitled%203.png)

- Customer 테이블을 Orders 테이블과 조건 없이 연결해보자. Customer와 Orders 테이블의 합
체 결과 튜플의 개수는 고객이 다섯 명이고 주문이 열 개이므로 5×10 해서 50이 된다

```sql
SELECT *
FROM Customer, Orders
```

![Untitled](NET%20SQL%20,%20XML%20,%20ADO%20,DB%209b656ad932a849638ca43b419b715c93/Untitled%204.png)

dept col : 3 row : 4

emp col : 8 ROw : 14

```sql
select * from dept, emp;  
```

→ col 11 , ROW : 56

left outrt 는 무조건 출력함 

subquery 문 → 쿼리문 안에 쿼리문 

### 부속질의

```sql

```

가장 비싼 도서의 이름

```sql
select price from book max(price)
```

```sql
select book name 
from book
where price = (select max(price)from book;

```

### 집합 연산 (2개 이상의 테이블을 합칠때 )(유사한 정보를합침)  —  join 이랑 다름

⇒ row 데이터를 합칠때 

- 합칩합 UNION  합친것
- 차집합 EXCEPT  뺀것
- 교집합 INTERESECT  공통된 것만

select sal

 from

emp

select ename, sal , comm,( sal+12)+isnull(comm, 0)'연봉' from emp;