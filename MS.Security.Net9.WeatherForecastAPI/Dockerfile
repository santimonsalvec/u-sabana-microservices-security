FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY /MS.Security.Net9.WeatherForecastAPI/. ./
RUN dotnet restore MS.Security.Net9.WeatherForecastAPI.csproj
RUN dotnet build MS.Security.Net9.WeatherForecastAPI.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /src
COPY --from=build /src/bin/Debug/net9.0/ .
EXPOSE 5194
ENV ASPNETCORE_URLS=http://+:5194
ENTRYPOINT ["dotnet", "MS.Security.Net9.WeatherForecastAPI.dll"]
