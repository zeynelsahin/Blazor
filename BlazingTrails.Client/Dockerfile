FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazingTrails.Client1/BlazingTrails.Client1.csproj", "BlazingTrails.Client1/"]
RUN dotnet restore "BlazingTrails.Client1/BlazingTrails.Client1.csproj"
COPY . .
WORKDIR "/src/BlazingTrails.Client1"
RUN dotnet build "BlazingTrails.Client1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazingTrails.Client1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazingTrails.Client1.dll"]
