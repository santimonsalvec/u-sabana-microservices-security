FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app/
COPY /MS.Security.Net9.SecurityAPI/. /app/
RUN dotnet restore MS.Security.Net9.SecurityAPI.csproj && dotnet build MS.Security.Net9.SecurityAPI.csproj
EXPOSE 5073
CMD ["dotnet", "run", "MS.Security.Net9.SecurityAPI.csproj"]
ENTRYPOINT ["dotnet", "./bin/Debug/net9.0/MS.Security.Net9.SecurityAPI.dll"]