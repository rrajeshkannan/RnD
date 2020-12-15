#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ReactWebApplication/ReactWebApplication.csproj", "ReactWebApplication/"]
RUN dotnet restore "ReactWebApplication/ReactWebApplication.csproj"
COPY . .
WORKDIR "/src/ReactWebApplication"
RUN dotnet build "ReactWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReactWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReactWebApplication.dll"]