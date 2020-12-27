FROM nginx
EXPOSE 51137
LABEL version="1.0.0" description="Controle jogos - site" maintainer="Cristiano"
RUN cd / && mkdir Arquivos && chmod 777 -R Arquivos/
COPY ./Arquivos_Front/ /usr/share/nginx/html/
WORKDIR /usr/share/nginx/html/
ENTRYPOINT ["/usr/sbin/nginx"]
CMD ["-g", "daemon off;"]
