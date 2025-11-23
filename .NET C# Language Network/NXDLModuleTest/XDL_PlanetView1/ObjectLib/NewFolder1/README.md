핵심 교육주제
NXDL 모듈 이해
교육내용
NXDL 모듈 사용법 이해

NXDL 모듈을 사용하여 어떻게 Map을 구현하나요?

○NXDL,NXPlanet,NXDLgr dll 을 참조하여  필요한 모듈을 참조하여 해당 컨트롤을 가져온다.
○NXPlanet 내부에서 XDLConfiguration를 참조하여 PBI,PBP 파일을 읽어와 타일형태로 Map을 로드한다.
○컨트롤 제어 핸들러 객체를 등록하여 생성한 NXPlanet 객체를 통해  윈도우 이벤트를 외부에서 받아서 사용한다.
○이벤트 제어 후 내부적으로 Rendering-OnRender -OrthoRendering -OnOrthoRender  순서로 이벤트가 동작하며 재랜더링에 필요한 객체는 OnOrthoRender에 이벤트로 등록하여 사용한다.
○NXPlanetView 내부 함수를 사용하여 구면체에 필요한 각도 측정 거리측정을 이벤트로 등록하여 사용한다.