#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LDST.Api/LDST.Api.csproj", "LDST.Api/"]
COPY ["LDST.Application/LDST.Application.csproj", "LDST.Application/"]
COPY ["LDST.Domain/LDST.Domain.csproj", "LDST.Domain/"]
COPY ["LDST.Infrastructure/LDST.Infrastructure.csproj", "LDST.Infrastructure/"]
RUN dotnet restore "LDST.Api/LDST.Api.csproj"
COPY . .
WORKDIR "/src/LDST.Api"
RUN dotnet build "LDST.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LDST.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LDST.Api.dll"]