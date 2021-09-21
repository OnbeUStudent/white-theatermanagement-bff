#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 82

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["fake/Providers.Fake/Providers.Fake.csproj", "fake/Providers.Fake/"]
COPY ["tobenuget/Common/Common.csproj", "tobenuget/Common/"]
RUN dotnet restore "fake/Providers.Fake/Providers.Fake.csproj"
COPY . .
WORKDIR "/src/fake/Providers.Fake"
RUN dotnet build "Providers.Fake.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Providers.Fake.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Providers.Fake.dll"]
