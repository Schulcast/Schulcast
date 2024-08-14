FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /server
COPY . .
RUN apt update && \
	apt install -y procps && \
	apt install -y curl && \
	apt install unzip && \
	curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
ENTRYPOINT dotnet watch --no-hot-reload
EXPOSE 80