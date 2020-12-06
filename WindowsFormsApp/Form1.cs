using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.MQTT;

namespace WindowsFormsApp
{
	public partial class Form1 : Form
	{
		public Form1( )
		{
			InitializeComponent( );
			syncClient.SetPersistentConnection( );
		}

		private MqttSyncClient syncClient = new MqttSyncClient( "127.0.0.1", 1883 );

		private void button1_Click( object sender, EventArgs e )
		{
			// 锁定
			OperateResult<string, string> write = syncClient.ReadString( "MainPLC/WriteBool", "{ \"address\": \"M300.1\", \"value\": true}" );
			if (write.IsSuccess)
			{
				MessageBox.Show( "锁定成功" );
			}
			else
			{
				MessageBox.Show( "锁定失败，原因：" + write.Message );
			}
		}

		private void button2_Click( object sender, EventArgs e )
		{
			// 解锁
			OperateResult<string, string> write = syncClient.ReadString( "MainPLC/WriteBool", "{ \"address\": \"M300.1\", \"value\": false}" );
			if (write.IsSuccess)
			{
				MessageBox.Show( "解锁成功" );
			}
			else
			{
				MessageBox.Show( "解锁失败，原因：" + write.Message );
			}
		}
	}
}
