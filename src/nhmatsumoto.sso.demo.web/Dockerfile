#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["nhmatsumoto.sso.demo.web/nhmatsumoto.sso.demo.web.csproj", "nhmatsumoto.sso.demo.web/"]
RUN dotnet restore "nhmatsumoto.sso.demo.web/nhmatsumoto.sso.demo.web.csproj"
COPY . .
WORKDIR "/src/nhmatsumoto.sso.demo.web"
RUN dotnet build "nhmatsumoto.sso.demo.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nhmatsumoto.sso.demo.web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nhmatsumoto.sso.demo.web.dll"]