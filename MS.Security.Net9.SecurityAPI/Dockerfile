FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY /MS.Security.Net9.SecurityAPI/. ./
RUN dotnet restore MS.Security.Net9.SecurityAPI.csproj
RUN dotnet build MS.Security.Net9.SecurityAPI.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /src
COPY --from=build /src/bin/Debug/net9.0/ .
EXPOSE 5073
ENV ASPNETCORE_URLS=http://+:5073
ENTRYPOINT ["dotnet", "MS.Security.Net9.SecurityAPI.dll"]
