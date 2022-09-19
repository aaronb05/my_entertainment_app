#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["my_entertainment_app/my_entertainment_app.csproj", "my_entertainment_app/"]
RUN dotnet restore "my_entertainment_app/my_entertainment_app.csproj"
COPY . .
WORKDIR "/src/my_entertainment_app"
RUN dotnet build "my_entertainment_app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "my_entertainment_app.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "my_entertainment_app.dll"]