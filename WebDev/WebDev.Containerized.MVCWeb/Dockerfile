FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["WebDev.Containerized.MVCWeb/WebDev.Containerized.MVCWeb.csproj", "WebDev.Containerized.MVCWeb/"]
RUN dotnet restore "WebDev.Containerized.MVCWeb/WebDev.Containerized.MVCWeb.csproj"
COPY . .
WORKDIR "/src/WebDev.Containerized.MVCWeb"
RUN dotnet build "WebDev.Containerized.MVCWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebDev.Containerized.MVCWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebDev.Containerized.MVCWeb.dll"]