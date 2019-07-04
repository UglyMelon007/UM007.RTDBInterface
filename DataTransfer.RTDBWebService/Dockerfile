FROM microsoft/dotnet:2.2.0-aspnetcore-runtime
WORKDIR /app
RUN sed -i "s@http://deb.debian.org@http://mirrors.aliyun.com@g" /etc/apt/sources.list
RUN  apt-get clean && apt-get update && apt-get install vim -y
COPY . .
EXPOSE 80
ENTRYPOINT ["dotnet", "DataTransfer.RTDBWebService.dll"]