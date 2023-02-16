FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ascender/ascender.csproj", "ascender/"]
RUN dotnet restore "ascender/ascender.csproj"
COPY . .
WORKDIR "/src/ascender"
RUN dotnet build "ascender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ascender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ascender.dll"]
