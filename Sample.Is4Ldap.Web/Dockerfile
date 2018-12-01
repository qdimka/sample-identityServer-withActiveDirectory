FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Sample.Is4Ldap.Web/Sample.Is4Ldap.Web.csproj", "Sample.Is4Ldap.Web/"]
RUN dotnet restore "Sample.Is4Ldap.Web/Sample.Is4Ldap.Web.csproj"
COPY . .
WORKDIR "/src/Sample.Is4Ldap.Web"
RUN dotnet build "Sample.Is4Ldap.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sample.Is4Ldap.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sample.Is4Ldap.Web.dll"]