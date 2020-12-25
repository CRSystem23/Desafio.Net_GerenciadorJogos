FROM mysql:5.7.22
EXPOSE 3306
COPY ./GerenciadorJogos.WebApi/db/migrations/ /home/database/
COPY ./GerenciadorJogos.WebApi/db/dataset/ /home/database/
COPY ./GerenciadorJogos.WebApi/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh

