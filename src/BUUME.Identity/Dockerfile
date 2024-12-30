FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5254
EXPOSE 5254
EXPOSE 7232

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BUUME.Identity/BUUME.Identity.csproj", "src/BUUME.Identity/"]
RUN dotnet restore "src/BUUME.Identity/BUUME.Identity.csproj"
COPY . .
WORKDIR "src/BUUME.Identity"
RUN dotnet build "BUUME.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BUUME.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BUUME.Identity.dll"]
