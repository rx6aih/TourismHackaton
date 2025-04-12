FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tourism.Dotnet.Parser/Tourism.Dotnet.Parser.csproj", "Tourism.Dotnet.Parser/"]
COPY ["Tourism.Dotnet.Parser.DAL/Tourism.Dotnet.Parser.DAL.csproj", "Tourism.Dotnet.Parser.DAL/"]
RUN dotnet restore "Tourism.Dotnet.Parser/Tourism.Dotnet.Parser.csproj"
COPY . .
WORKDIR "/src/Tourism.Dotnet.Parser"
RUN dotnet build "Tourism.Dotnet.Parser.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tourism.Dotnet.Parser.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tourism.Dotnet.Parser.dll"]
