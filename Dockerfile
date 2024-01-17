FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./src/ .

RUN dotnet restore ./API/API.csproj

RUN dotnet build "API/API.csproj" -c Release

FROM build AS publish       
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT [ "dotnet", "API.dll" ]