# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.csproj MVCClient/
WORKDIR /src/MVCClient
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/MVCClient
RUN dotnet publish -c Release -o /src/publish

#Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
#look into defining https and http also look into ipv4 and ipv6 bindings
ENV ASPNETCORE_URLS http://*:9002;https://*:9003 
ENTRYPOINT ["dotnet", "MVCClient.dll"]
#COPY --from=build /src/leashApi/entrypoint.sh .
#RUN chmod +x ./entrypoint.sh
#CMD /bin/bash ./entrypoint.sh