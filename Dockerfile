# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.csproj devIdentityServer/
WORKDIR /src/devIdentityServer
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/devIdentityServer
RUN dotnet publish -c Release -o /src/publish

#Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
#look into defining https and http also look into ipv4 and ipv6 bindings
ENV ASPNETCORE_URLS http://*:8986;https://*:8987 
ENTRYPOINT ["dotnet", "devIdentityServer.dll"]
#COPY --from=build /src/leashApi/entrypoint.sh .
#RUN chmod +x ./entrypoint.sh
#CMD /bin/bash ./entrypoint.sh