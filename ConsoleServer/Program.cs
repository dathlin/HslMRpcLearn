using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System.Threading;
using HslCommunication.MQTT;

namespace ConsoleServer
{
	class Program
	{
		static void Main( string[] args )
		{
			SiemensS7Net plc = new SiemensS7Net( SiemensPLCS.S1200, "127.0.0.1" ); // 此处拿了本地虚拟的PLC测试
			plc.SetPersistentConnection( ); // 设置了长连接

			MqttServer mqttServer = new MqttServer( );
			mqttServer.RegisterMqttRpcApi( "MainPLC", plc );
			mqttServer.ServerStart( 1883 );

			while (true)
			{
				Thread.Sleep( 1000 ); // 每秒读取一次
				OperateResult<short> read = plc.ReadInt16( "M100" );
				if (read.IsSuccess)
				{
					// 读取成功后，进行业务处理，存入数据库，或是其他的分析
					Console.WriteLine( "读取成功，M100：" + read.Content );
				}
				else
				{
					// 读取失败之后，显示下状态
					Console.WriteLine( "读取PLC失败，原因：" + read.Message );
				}

			}
		}
	}
}
