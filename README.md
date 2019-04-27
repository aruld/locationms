# locationms
Microservice written in C# that returns location based on IP address

It is used with the blog post series that starts here.

Version 1.0 returns location data, plain and simple.

Version 2.0 returns the location data with a "version:v2" property added to the JSON.


#### Deploy service to local Docker on Windows

```
PS C:\Users\arul\Documents\GitHub\locationms> docker build -t locationms .
Sending build context to Docker daemon  10.88MB
Step 1/8 : FROM microsoft/dotnet:2.1-sdk
2.1-sdk: Pulling from microsoft/dotnet
e79bb959ec00: Pull complete
d4b7902036fe: Pull complete
1b2a72d4e030: Pull complete
d54db43011fd: Pull complete
9d2dc3090b61: Pull complete
0edd4d3a9108: Pull complete
9a6ef98360ee: Pull complete
Digest: sha256:4c5d00cf5f69ea4ec7fbd4fbe58a13bf6e2f73ab596700ab814d82f7f9bbe3ce
Status: Downloaded newer image for microsoft/dotnet:2.1-sdk
 ---> d81b18feaa95
Step 2/8 : WORKDIR /app
 ---> Running in 62dffefd218c
Removing intermediate container 62dffefd218c
 ---> 1153edc920f2
Step 3/8 : COPY *.csproj ./
 ---> 8d52550fcfb9
Step 4/8 : RUN dotnet restore
 ---> Running in a6e1e69b44c2
  Restore completed in 1.97 sec for /app/locationms.csproj.
Removing intermediate container a6e1e69b44c2
 ---> 233934757f30
Step 5/8 : COPY . ./
 ---> 5584ff746050
Step 6/8 : EXPOSE 8080
 ---> Running in 49f2a9282dae
Removing intermediate container 49f2a9282dae
 ---> 2dfe0371917e
Step 7/8 : RUN dotnet publish -c Release -o out
 ---> Running in a6643b296dd5
Microsoft (R) Build Engine version 16.0.450+ga8dc7f1d34 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 635.54 ms for /app/locationms.csproj.
  locationms -> /app/bin/Release/netcoreapp2.1/locationms.dll
  locationms -> /app/out/
Removing intermediate container a6643b296dd5
 ---> 383353781957
Step 8/8 : ENTRYPOINT ["dotnet", "out/locationms.dll"]
 ---> Running in a45b91708a59
Removing intermediate container a45b91708a59
 ---> 8b7fe122ea8a
Successfully built 8b7fe122ea8a
Successfully tagged locationms:latest
SECURITY WARNING: You are building a Docker image from Windows against a non-Windows Docker host. All files and directories added to build context will have '-rwxr-xr-x' permissions. It is recommended to double check and reset permissions for sensitive files and directories.
PS C:\Users\arul\Documents\GitHub\locationms>
```

#### Docker images

```
PS C:\Users\arul\Documents\GitHub\locationms> docker images
REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
locationms          latest              8b7fe122ea8a        11 minutes ago      1.75GB
microsoft/dotnet    2.1-sdk             d81b18feaa95        3 hours ago         1.74GB
openzipkin/zipkin   latest              7ee685d20e4d        2 weeks ago         260MB
consul              1.4.4               df36de474d54        5 weeks ago         107MB
hello-world         latest              fce289e99eb9        3 months ago        1.84kB
consul              1.2.4               56d8bceaba66        4 months ago        116MB
```

#### Run the locationms service in background and port forward to 8080 on localhost:

```
PS C:\Users\arul\Documents\GitHub\locationms> docker run -d -p 8080:8080 locationms
00d060865d0a024616c691295ad2e597583c511c7334beec614eafd547cad76c
```

#### Docker container status:

```
PS C:\Users\arul\Documents\GitHub\locationms> docker ps
CONTAINER ID        IMAGE               COMMAND                  CREATED              STATUS              PORTS                    NAMES
00d060865d0a        locationms          "dotnet out/location…"   About a minute ago   Up About a minute   0.0.0.0:8080->8080/tcp   hopeful_noyce
```

#### Docker container logs:

```
PS C:\Users\arul\Documents\GitHub\locationms> docker logs -f 00d060865d0a
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {224a6eeb-fbe8-408b-a95e-7d1e8f9d2b3e} may be persisted to storage in unencrypted form.
Hosting environment: Production
Content root path: /app
Now listening on: http://[::]:8080
Application started. Press Ctrl+C to shut down.
Your IP is 172.17.0.1
http://ip-api.com/json/172.17.0.1
```

#### Shutdown the service:
```
PS C:\Users\arul> docker stop 00d060865d0a
00d060865d0a
PS C:\Users\arul> docker ps
CONTAINER ID        IMAGE               COMMAND             CREATED             STATUS              PORTS               NAMES
PS C:\Users\arul>
```

#### Push container image to your Docker hub account:

##### Login to your Docker hub user account:

```
PS C:\Users\arul\Documents\GitHub\locationms> docker login
Authenticating with existing credentials...
Login Succeeded
```

##### Build image using Docker hub user account:

```
PS C:\Users\arul\Documents\GitHub\locationms> docker build -t microlithdev/locationms .
Sending build context to Docker daemon   11.9MB
Step 1/8 : FROM microsoft/dotnet:2.1-sdk
 ---> d81b18feaa95
Step 2/8 : WORKDIR /app
 ---> Using cache
 ---> 1153edc920f2
Step 3/8 : COPY *.csproj ./
 ---> Using cache
 ---> 8d52550fcfb9
Step 4/8 : RUN dotnet restore
 ---> Using cache
 ---> 233934757f30
Step 5/8 : COPY . ./
 ---> 79b0b1cbda8e
Step 6/8 : EXPOSE 8080
 ---> Running in ed9a3eb2ae2d
Removing intermediate container ed9a3eb2ae2d
 ---> a16a3d9066f4
Step 7/8 : RUN dotnet publish -c Release -o out
 ---> Running in 00ed5b26ceb7
Microsoft (R) Build Engine version 16.0.450+ga8dc7f1d34 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 679.02 ms for /app/locationms.csproj.
  locationms -> /app/bin/Release/netcoreapp2.1/locationms.dll
  locationms -> /app/out/
Removing intermediate container 00ed5b26ceb7
 ---> b3c99a88cdbf
Step 8/8 : ENTRYPOINT ["dotnet", "out/locationms.dll"]
 ---> Running in 679ebaf8cf40
Removing intermediate container 679ebaf8cf40
 ---> 5e038ac304af
Successfully built 5e038ac304af
Successfully tagged microlithdev/locationms:latest
SECURITY WARNING: You are building a Docker image from Windows against a non-Windows Docker host. All files and directories added to build context will have '-rwxr-xr-x' permissions. It is recommended to double check and reset permissions for sensitive files and directories.
```

##### Push container to Docker hub:
```
PS C:\Users\arul\Documents\GitHub\locationms> docker push microlithdev/locationms
The push refers to repository [docker.io/microlithdev/locationms]
8f1aad4b61d4: Pushed
37397a130cc4: Pushed
ee476e4f9837: Pushed
b05f99151ddb: Pushed
3e0189ca1901: Pushed
e198e51e6103: Mounted from microsoft/dotnet
d3e592d10806: Mounted from microsoft/dotnet
2b89ed90d016: Mounted from microsoft/dotnet
b17cc31e431b: Mounted from microsoft/dotnet
12cb127eee44: Mounted from microsoft/dotnet
604829a174eb: Mounted from microsoft/dotnet
fbb641a8b943: Mounted from microsoft/dotnet
latest: digest: sha256:757e072cea163b5bb1bca038f25977f6df64606ab3e618e0c09a3ab20739c351 size: 2845
PS C:\Users\arul\Documents\GitHub\locationms>
```

