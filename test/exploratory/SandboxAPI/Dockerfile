#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["test/exploratory/SandboxAPI/SandboxAPI.csproj", "test/exploratory/SandboxAPI/"]
RUN dotnet restore "test/exploratory/SandboxAPI/SandboxAPI.csproj"
COPY . .
WORKDIR "/src/test/exploratory/SandboxAPI"
RUN dotnet build "SandboxAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SandboxAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SandboxAPI.dll"]