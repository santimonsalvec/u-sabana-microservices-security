FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore MS.Security.Net9.CurrencyMarketAPI.csproj
RUN dotnet publish MS.Security.Net9.CurrencyMarketAPI.csproj -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /src
COPY --from=build /src/out .
EXPOSE 5054
ENV ASPNETCORE_URLS=http://+:5054
ENTRYPOINT ["dotnet", "MS.Security.Net9.CurrencyMarketAPI.dll"]
