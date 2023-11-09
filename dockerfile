FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Define as variáveis de ambiente
ENV InstrumentationKey a473ef5d-6a99-4c01-87da-5a4e84ebab7c
ENV JwtSecret 1234545688885555
ENV CONNECTION Server=tcp:sv-techchallenges.database.windows.net,1433;Integrated Security=true;Initial Catalog=db_noticias;User Id=techchallenge;Password=Fiapchallenge64;Trusted_Connection=false;TrustServerCertificate=true;


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
