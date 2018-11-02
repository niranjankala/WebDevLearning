FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 54116
EXPOSE 44343

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["WebDev.AspNETCore/WebDev.AspNETCore.csproj", "WebDev.AspNETCore/"]
RUN dotnet restore "WebDev.AspNETCore/WebDev.AspNETCore.csproj"
COPY . .
WORKDIR "/src/WebDev.AspNETCore"
RUN dotnet build "WebDev.AspNETCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebDev.AspNETCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebDev.AspNETCore.dll"]