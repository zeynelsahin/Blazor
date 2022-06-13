FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazingTrails.Client/BlazingTrails.Client.csproj", "BlazingTrails.Client/"]
RUN dotnet restore "BlazingTrails.Client/BlazingTrails.Client.csproj"
COPY . .
WORKDIR "/src/BlazingTrails.Client"
RUN dotnet build "BlazingTrails.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazingTrails.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazingTrails.Client.dll"]
