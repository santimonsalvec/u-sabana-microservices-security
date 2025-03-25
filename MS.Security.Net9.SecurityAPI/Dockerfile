FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /App/
COPY /MS.Security.Net9.SecurityAPI/. /App/
RUN dotnet restore MS.Security.Net9.SecurityAPI.csproj && dotnet build MS.Security.Net9.SecurityAPI.csproj
CMD ["dotnet", "run", "MS.Security.Net9.SecurityAPI.csproj"]
#ENTRYPOINT ["dotnet", "./bin/Debug/net9.0/MS.Security.Net9.SecurityAPI.dll"]