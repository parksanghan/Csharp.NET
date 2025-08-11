# 디버그 컨테이너 커스터마이즈: https://aka.ms/customizecontainer

# 빠른(디버그) 모드용 베이스
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER $APP_UID   # ← APP_UID가 없으면 주석 유지
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 빌드 스테이지
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ⚠️ 여기 한 줄이 핵심: 정확한 파일명만!
COPY ["AspNetCoreBlazorEmpty/AspNetCoreBlazorEmpty.csproj", "AspNetCoreBlazorEmpty/"]
RUN dotnet restore "AspNetCoreBlazorEmpty/AspNetCoreBlazorEmpty.csproj"

# 나머지 소스 복사 후 빌드
COPY . .
WORKDIR "/src/AspNetCoreBlazorEmpty"
RUN dotnet build "AspNetCoreBlazorEmpty.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 게시 스테이지
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AspNetCoreBlazorEmpty.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 최종 이미지
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreBlazorEmpty.dll"]
 
