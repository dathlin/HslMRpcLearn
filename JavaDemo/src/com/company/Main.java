package com.company;

import HslCommunication.Core.Types.OperateResultExTwo;
import HslCommunication.MQTT.MqttSyncClient;

import java.io.Console;

public class Main {

    public static void main(String[] args) {
	// write your code here
        MqttSyncClient syncClient = new MqttSyncClient( "127.0.0.1", 1883 );
        syncClient.SetPersistentConnection();

        // 锁定
        OperateResultExTwo<String, String> write = syncClient.ReadString( "MainPLC/WriteBool", "{ \"address\": \"M300.1\", \"value\": true}",
                null, null, null );
        if (write.IsSuccess)
        {
            System.out.println( "锁定成功" );
        }
        else
        {
            System.out.println( "锁定失败，原因：" + write.Message );
        }


        syncClient.ConnectClose();
    }
}
