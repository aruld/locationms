FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and build everything else
COPY . ./
EXPOSE 8080
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/locationms.dll"]