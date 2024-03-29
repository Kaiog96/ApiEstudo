FROM mysql:5.7.22
EXPOSE 3306
COPY ./ApiEstudo/db/migrations/ /home/database/
COPY ./ApiEstudo/db/dataset/ /home/database/
COPY ./ApiEstudo/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh