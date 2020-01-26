@echo off
set t1=error
set t2=event

cd /c %~dp0/kafk

echo %~dp0
echo %t1%
echo %t2%

echo Deleting and recreating topics
call %~dp0\bin\windows\kafka-topics.bat --zookeeper localhost:2181 --delete --topic %t1%
call %~dp0\bin\windows\kafka-topics.bat --zookeeper localhost:2181 --delete --topic %t2%

echo Check working directory. Press any key to continue
pause >nul

start %~dp0\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties

echo Press any key to continue - after zookeeper is running
pause >nul

start %~dp0\bin\windows\kafka-server-start.bat .\config\server.properties

echo Press any key to continue - after kafka is running
pause >nul

echo creating topic
call %~dp0\bin\windows\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic %t1%
call %~dp0\bin\windows\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic %t2%

echo list existing topics
call %~dp0\bin\windows\kafka-topics.bat --list --bootstrap-server localhost:9092

echo Starting producer consumer instances

echo View produced items in the consumer instance

echo You can close this window now
pause >nul