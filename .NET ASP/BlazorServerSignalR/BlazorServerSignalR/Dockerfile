# 1) 런타임
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# 안전: 비루트 사용자 (aspnet 이미지에 기본 존재)
USER app
# HTTP 기본 8080 (aspnet 기본), 필요 시 8081 노출
EXPOSE 8080
EXPOSE 8081

# 2) 빌드
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorServerSignalR/BlazorServerSignalR.csproj", "BlazorServerSignalR/"]
RUN dotnet restore "BlazorServerSignalR/BlazorServerSignalR.csproj"
COPY . .
WORKDIR "/src/BlazorServerSignalR"
RUN dotnet build "BlazorServerSignalR.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3) 퍼블리시
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorServerSignalR.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4) 최종
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorServerSignalR.dll"]
