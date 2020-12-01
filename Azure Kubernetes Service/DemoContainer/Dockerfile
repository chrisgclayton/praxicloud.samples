FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Default Configuration to Release if not provided
ARG BUILD_CONFIGURATION=Release

WORKDIR /app

# Copy Solution Directory
COPY . .
RUN dotnet restore ./src/DemoContainer.Solution.sln

# copy and publish app and libraries
WORKDIR /app/
COPY /. ./
WORKDIR /app
RUN dotnet publish -c Release -o out ./src/praxicloud.samples.democontainer/praxicloud.samples.democontainer.csproj

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "praxicloud.samples.democontainer.dll"]

EXPOSE 10080/tcp
