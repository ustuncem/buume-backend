FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BUUME.Api/BUUME.Api.csproj", "src/BUUME.Api/"]
COPY ["src/BUUME.BUUME/BUUME.Application.csproj", "src/BUUME.Application/"]
COPY ["src/BUUME.Domain/BUUME.Domain.csproj", "src/BUUME.Domain/"]
COPY ["src/BUUME.Infrastructure/BUUME.Infrastructure.csproj", "src/BUUME.Infrastructure/"]
COPY ["src/BUUME.SharedKernel/BUUME.SharedKernel.csproj", "src/BUUME.SharedKernel/"]
RUN dotnet restore "./src/BUUME.Api/./BUUME.Api.csproj"
COPY . .
WORKDIR "/src/src/BUUME.Api"
RUN dotnet build "./BUUME.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BUUME.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=5254,7232
ENTRYPOINT ["dotnet", "BUUME.Api.dll"]