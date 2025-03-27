FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore MS.Security.Net9.SecurityAPI.csproj
RUN dotnet publish MS.Security.Net9.SecurityAPI.csproj -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /src
COPY --from=build /src/out .
EXPOSE 5073
ENV ASPNETCORE_URLS=http://+:5073
RUN ls -l
ENTRYPOINT ["dotnet", "MS.Security.Net9.SecurityAPI.dll"]
