FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WhatYouHaveLost/WhatYouHaveLost.csproj", "WhatYouHaveLost/"]
RUN dotnet restore "WhatYouHaveLost/WhatYouHaveLost.csproj"
COPY . .
WORKDIR "/src/WhatYouHaveLost"
RUN dotnet build "WhatYouHaveLost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatYouHaveLost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "whatYouHaveLost.dll"]
