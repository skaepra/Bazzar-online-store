# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملف المشروع مع توجيهه للمجلد الصحيح BackEnd
COPY ["BackEnd/Online-Store/Online-Store.csproj", "Online-Store/"]
RUN dotnet restore "Online-Store/Online-Store.csproj"

# نسخ باقي الملفات
COPY BackEnd/ .
WORKDIR "/src/Online-Store"
RUN dotnet build "Online-Store.csproj" -c Release -o /app/build

# مرحلة النشر
FROM build AS publish
RUN dotnet publish "Online-Store.csproj" -c Release -o /app/publish /p:UseAppHost=false

# مرحلة التشغيل النهائية
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Online-Store.dll"]