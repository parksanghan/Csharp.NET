핵심 교육주제
NXDL 모듈 이해

교육 내용
NXDL 모듈 내 Scene 및 Layer 이해 및 실습 

질문
NXDL 모듈을 사용하여 Scene 객체를 생성하고 레이어에 추가하는 방법은 무엇인가요?

답변
 
○ NXDL 내부의 NSCENE 모듈을 사용하기 위해 NXDLscene.dll을 참조 및 빌드하여 환경을 구성한다.

○ NSCENE 모듈을 통해 NXDL PlanetView의 SceneEditor·SceneDisplay 계층을 구성하여 객체 생성과 표시 기능 레이어를 분리하여 구현한다.

○ 각 레이어에서 객체를 생성할 경우 OnObjectCreated 이벤트가 트리거되며, 사용자 정의 이벤트 핸들러를 등록해 생성된 객체의 속성을 설정할 수 있다.

○ SceneEditor에서 객체 생성 후, 생성 이벤트에서 해당 객체 타입(XscPoint, XscPolyLine, XscPolygon, XscCircle, XscSymbol) 으로 형변환하여 속성을 제어한다.

○ 생성된 Scene 객체는 SML 확장자로 저장할 수 있으며, 파일 로드 시 최상위 XScene 객체를 가져와 Display 레이어에 반영한다.

○ DisplayOrder 설정에 따라 객체의 순서를 갱신할 수 있다.

소감 및 건의사항

금일 OJT에서는 NXDL 모듈을 활용하여 Scene 및 Layer 객체를 생성하고 레이어에 추가하는 실습을 진행하였습니다. 향후 NXDL 모듈 기능을 충분히 숙달하여 업무에 원활하게 적용할 수 있도록 하겠습니다. 감사합니다.