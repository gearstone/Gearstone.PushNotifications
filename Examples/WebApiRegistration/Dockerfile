FROM microsoft/aspnetcore:2.0.6

WORKDIR /app
COPY bin/Release/netcoreapp2.0/publish .

EXPOSE 80

ENTRYPOINT ["dotnet",  "WebApiRegistration.dll"]