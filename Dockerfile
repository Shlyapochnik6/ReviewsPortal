FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["ReviewsPortal.Web/ReviewsPortal.Web.csproj", "ReviewsPortal.Web/"]
COPY ["ReviewsPortal.Domain/ReviewsPortal.Domain.csproj", "ReviewsPortal.Domain/"]
COPY ["ReviewsPortal.Persistence/ReviewsPortal.Persistence.csproj", "ReviewsPortal.Persistence/"]
COPY ["ReviewsPortal.Application/ReviewsPortal.Application.csproj", "ReviewsPortal.Application/"]
RUN dotnet restore "ReviewsPortal.Web/ReviewsPortal.Web.csproj"
RUN dotnet restore "ReviewsPortal.Application/ReviewsPortal.Application.csproj"
RUN dotnet restore "ReviewsPortal.Domain/ReviewsPortal.Domain.csproj"
RUN dotnet restore "ReviewsPortal.Persistence/ReviewsPortal.Persistence.csproj"
COPY . .
WORKDIR "/src/ReviewsPortal.Web"
RUN dotnet build "ReviewsPortal.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReviewsPortal.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReviewsPortal.Web.dll"]