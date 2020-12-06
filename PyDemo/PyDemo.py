from HslCommunication import MqttSyncClient

if __name__ == "__main__":
	mqtt = MqttSyncClient( "127.0.0.1", 1883 )
	mqtt.SetPersistentConnection()

	write = mqtt.ReadString('MainPLC/WriteBool', '{ "address": "M300.1", "value": true}')
	if write.IsSuccess == True:
		print('lock success')
	else:
		print('lock failed:' + write.Message)


	read = mqtt.ReadString('MainPLC/ReadBool', '{ "address": "M300.1"}')
	if read.IsSuccess == True:
		print('Value:' + read.Content2)
	else:
		print('Read failed: ' + read.Message)
	
	mqtt.ConnectClose()