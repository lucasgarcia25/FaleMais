# FaleMais
Pré-requisitos

Antes de iniciar, verifique se você tem instalado:

Node.js (>= 16.x)

Angular CLI (>= 16.x) → npm install -g @angular/cli

Dotnet SDK (>= 8.0)


Descompacte o projeto e, no terminal (cmd), navegue até a pasta principal (FaleMais) onde está a pasta descompactada.

Backend
Na Pasta raiz onde foi descompactado no terminal rode:

cd FaleMais.Api/FaleMais.API
dotnet restore

Frontend
Na Pasta raiz onde foi descompactado no terminal rode:
cd fale-mais
npm install
npm install -g @angular/cli

O backend pode ser executado de 2 formas:

 Modo Mock (sem banco de dados)
 Modo SQLite SQLite como banco local. O arquivo falemais.db será criado automaticamente.

 Para alternar entre eles basta alterar o arquivo de configuração do projeto (appsettings) no item provider para "Mock" ou "SQLite"

apos isso, execute o back end:

Na Pasta raiz onde foi descompactado no terminal rode:

cd FaleMais.Api/FaleMais.API
dotnet run 

Executando o Frontend

Na Pasta raiz onde foi descompactado no terminal rode:

cd fale-mais
ng serve

Acesse: http://localhost:4200


Para executar os testes:

Na Pasta raiz onde foi descompactado no terminal rode:

cd FaleMais.Api/FaleMais.Tests
dotnet test
