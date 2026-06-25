# Graph Drawer

WPF `Canvas`에 그래프를 그리는 예제입니다.

## 실행

```powershell
dotnet run --project .\WpfComObjectSample.csproj
```

## 구조

- `MainWindow.xaml`: 그래프 화면과 조작 UI
- `MainWindow.xaml.cs`: UI 이벤트 처리
- `Graphing/GraphCalculator.cs`: 함수값 계산
- `Graphing/GraphRenderer.cs`: 좌표축, 격자, 그래프 렌더링
- `Graphing/GraphOptions.cs`: 그래프 옵션 모델
- `Graphing/GraphFunction.cs`: 지원 함수 목록
