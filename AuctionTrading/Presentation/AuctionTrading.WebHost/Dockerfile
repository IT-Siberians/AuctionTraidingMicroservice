#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/AuctionTrading.WebHost/AuctionTrading.WebHost.csproj", "Presentation/AuctionTrading.WebHost/"]
COPY ["Application/AuctionTrading.Application.Services/AuctionTrading.Application.Services.csproj", "Application/AuctionTrading.Application.Services/"]
COPY ["Common/AuctionTrading.Common/AuctionTrading.Common.csproj", "Common/AuctionTrading.Common/"]
COPY ["Domain/AuctionTrading.Domain.Repositories.Abstractions/AuctionTrading.Domain.Repositories.Abstractions.csproj", "Domain/AuctionTrading.Domain.Repositories.Abstractions/"]
COPY ["Domain/AuctionTrading.Domain/AuctionTrading.Domain.csproj", "Domain/AuctionTrading.Domain/"]
COPY ["Domain/AuctionTrading.ValueObjects/AuctionTrading.Domain.ValueObjects.csproj", "Domain/AuctionTrading.ValueObjects/"]
COPY ["Application/AuctionTrading.Application.Models/AuctionTrading.Application.Models.csproj", "Application/AuctionTrading.Application.Models/"]
COPY ["Infrastructure/AuctionTrading.Infrastructure.EntityFramework/AuctionTrading.Infrastructure.EntityFramework.csproj", "Infrastructure/AuctionTrading.Infrastructure.EntityFramework/"]
COPY ["Infrastructure/Repositories.Implementations/AuctionTrading.Infrastructure.Repositories.Implementations.csproj", "Infrastructure/Repositories.Implementations/"]
COPY ["Domain/AuctionTrading.Domain.Services/AuctionTrading.Domain.Services.csproj", "Domain/AuctionTrading.Domain.Services/"]
RUN dotnet restore "./Presentation/AuctionTrading.WebHost/AuctionTrading.WebHost.csproj"
COPY . .
WORKDIR "/src/Presentation/AuctionTrading.WebHost"
RUN dotnet build "./AuctionTrading.WebHost.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuctionTrading.WebHost.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionTrading.WebHost.dll"]