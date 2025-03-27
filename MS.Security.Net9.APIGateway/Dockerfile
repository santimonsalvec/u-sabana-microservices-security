FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore MS.Security.Net9.APIGateway.csproj
RUN dotnet publish MS.Security.Net9.APIGateway.csproj -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /src
COPY --from=build /src/out .
EXPOSE 5270
ENTRYPOINT ["dotnet", "MS.Security.Net9.APIGateway.dll"]
