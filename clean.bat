
t1=error
t2=event
echo Deleting and recreating topics
call %~dp0\bin\windows\kafka-topics.bat --zookeeper localhost:2181 
--delete --topic %t1%
call %~dp0\bin\windows\kafka-topics.bat --zookeeper localhost:2181 
--delete --topic %t2%
