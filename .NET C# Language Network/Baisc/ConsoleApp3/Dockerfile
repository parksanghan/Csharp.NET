# 디버그 컨테이너를 사용자 지정하는 방법과 Visual Studio 이 Dockerfile을 사용하여 더 빠른 디버깅을 위해 이미지를 빌드하는 방법을 알아보려면 https://aka.ms/customizecontainer를 참조하세요.

# 이러한 ARG를 사용하면 VS에서 디버깅할 때 최종 이미지를 만드는 데 사용되는 기본을 교체할 수 있습니다.
ARG LAUNCHING_FROM_VS
# 이렇게 하면 기본 이미지가 최종 이미지로 설정되지만 이를 위해 LAUNCHING_FROM_VS가 정의되어 있어야 합니다.
ARG FINAL_BASE_IMAGE=${LAUNCHING_FROM_VS:+aotdebug}

# 이 스테이지는 VS에서 빠른 모드로 실행할 때 사용됩니다(디버그 구성의 기본값).
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# 이 스테이지는 서비스 프로젝트를 빌드하는 데 사용됩니다.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# 네이티브에 게시할 clang/zlib1g-dev 종속성 설치
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConsoleApp3/ConsoleApp3.csproj", "ConsoleApp3/"]
RUN dotnet restore "./ConsoleApp3/ConsoleApp3.csproj"
COPY . .
WORKDIR "/src/ConsoleApp3"
RUN dotnet build "./ConsoleApp3.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 이 스테이지는 최종 스테이지에 복사할 서비스 프로젝트를 게시하는 데 사용됩니다.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConsoleApp3.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

# 이 스테이지는 일반 모드에서 디버깅을 지원하기 위해 VS에서 시작할 때 최종 스테이지의 기본으로 사용됩니다(디버그 구성을 사용하지 않는 경우 기본값).
FROM base AS aotdebug
USER root
# 네이티브 디버깅을 지원하려면 GDB를 설치합니다.
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    gdb
USER app

# 이 스테이지는 프로덕션에서 사용되거나 VS에서 일반 모드로 실행할 때 사용됩니다(디버그 구성을 사용하지 않는 경우 기본값).
FROM ${FINAL_BASE_IMAGE:-mcr.microsoft.com/dotnet/runtime-deps:8.0} AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./ConsoleApp3"]