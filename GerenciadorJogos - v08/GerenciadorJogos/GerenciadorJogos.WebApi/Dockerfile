FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY GerenciadorJogos.WebApi/GerenciadorJogos.WebApi.csproj GerenciadorJogos.WebApi/
COPY GerenciadorJogos.Dominio/GerenciadorJogos.Dominio.csproj GerenciadorJogos.Dominio/
COPY GerenciadorJogos.Repositorio/GerenciadorJogos.Repositorio.csproj GerenciadorJogos.Repositorio/
COPY GerenciadorJogos.AcessoDados/GerenciadorJogos.AcessoDados.csproj GerenciadorJogos.AcessoDados/
COPY GerenciadorJogos.Generico/GerenciadorJogos.Generico.csproj GerenciadorJogos.Generico/
RUN dotnet restore GerenciadorJogos.WebApi/GerenciadorJogos.WebApi.csproj
COPY . .
WORKDIR /src/GerenciadorJogos.WebApi
RUN dotnet build GerenciadorJogos.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GerenciadorJogos.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GerenciadorJogos.WebApi.dll"]
