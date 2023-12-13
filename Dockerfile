FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish api.sln -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "chatapi.dll"]