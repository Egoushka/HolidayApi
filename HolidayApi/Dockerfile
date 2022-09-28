FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HolidayApi/HolidayApi.csproj", "HolidayApi/"]
RUN dotnet restore "HolidayApi/HolidayApi.csproj"
COPY . .
WORKDIR "/src/HolidayApi"
RUN dotnet build "HolidayApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HolidayApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HolidayApi.dll"]
