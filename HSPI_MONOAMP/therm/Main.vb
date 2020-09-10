Imports HomeSeerAPI
Imports HSCF.Communication.Scs.Client
Imports HSCF.Communication.Scs.Communication
Imports HSCF.Communication.Scs.Communication.EndPoints.Tcp
Imports HSCF.Communication.ScsServices.Client
Imports HSPI_INSTEON_THERMOSTAT.My
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices
Imports System.Threading

Namespace HSPI_INSTEON_THERMOSTAT
	<StandardModule>
	Friend NotInheritable Class Main
		<AccessedThroughProperty("client")>
		Private Shared _client As IScsServiceClient(Of IHSApplication)

		<AccessedThroughProperty("clientCallback")>
		Private Shared _clientCallback As IScsServiceClient(Of IAppCallbackAPI)

		Private Shared host As IHSApplication

		Private Shared gAppAPI As HSPI

		Public Shared oPlugin As plugin

		Friend Shared colTrigs_Sync As SortedList

		Friend Shared colTrigs As SortedList

		Friend Shared colActs_Sync As SortedList

		Friend Shared colActs As SortedList

		Public Shared Property client As IScsServiceClient(Of IHSApplication)
			Get
				Return Main._client
			End Get
			Set(ByVal value As IScsServiceClient(Of IHSApplication))
				Dim eventHandler As System.EventHandler = New System.EventHandler(AddressOf Main.client_Disconnected)
				If (Main._client IsNot Nothing) Then
					RemoveHandler Main._client.Disconnected,  eventHandler
				End If
				Main._client = value
				If (Main._client IsNot Nothing) Then
					AddHandler Main._client.Disconnected,  eventHandler
				End If
			End Set
		End Property

		Private Shared Property clientCallback As IScsServiceClient(Of IAppCallbackAPI)
			Get
				Return Main._clientCallback
			End Get
			Set(ByVal value As IScsServiceClient(Of IAppCallbackAPI))
				Main._clientCallback = value
			End Set
		End Property

		Shared Sub New()
			Main.oPlugin = New plugin()
		End Sub

		Private Shared Sub client_Disconnected(ByVal sender As Object, ByVal e As EventArgs)
			Console.WriteLine("Insteon Thermostat Disconnected from server - client")
		End Sub

		<STAThread>
		Public Shared Sub Main()
			Dim enumerator As IEnumerator(Of String) = Nothing
			Dim flag As Boolean = False
			Console.WriteLine("Insteon Thermostat Staring up..." & VbCrLf & "")
			Dim str As String = "127.0.0.1"
			Using commandLineArgs As ReadOnlyCollection(Of String) = MyProject.Application.CommandLineArgs
				enumerator = commandLineArgs.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As String = enumerator.Current
					Dim strArrays() As String = { "=" }
					Dim strArrays1 As String() = current.Split(strArrays, StringSplitOptions.None)
					Dim lower As String = strArrays1(0).ToLower()
					If (Operators.CompareString(lower, "server", False) <> 0) Then
						If (Operators.CompareString(lower, "instance", False) <> 0) Then
							Continue While
						End If
						Try
							HSPI_INSTEON_THERMOSTAT.utils.Instance = strArrays1(1)
						Catch exception As System.Exception
							ProjectData.SetProjectError(exception)
							HSPI_INSTEON_THERMOSTAT.utils.Instance = ""
							ProjectData.ClearProjectError()
						End Try
					Else
						str = strArrays1(1)
					End If
				End While
			End Using
			Main.gAppAPI = New HSPI()
			Console.WriteLine(String.Concat("Insteon Thermostat Connecting to server at ", str, "..."))
			Main.client = ScsServiceClientBuilder.CreateClient(Of IHSApplication)(New ScsTcpEndPoint(str, 10400), Main.gAppAPI)
			Main.clientCallback = ScsServiceClientBuilder.CreateClient(Of IAppCallbackAPI)(New ScsTcpEndPoint(str, 10400), Main.gAppAPI)
			Dim num As Integer = 1
			While True
				Try
					Console.Title = String.Concat("Insteon Thermostat Connecting attempt #", num.ToString())
					Main.client.Connect()
					Main.clientCallback.Connect()
					Main.host = Main.client.ServiceProxy
					Dim aPIVersion As Double = Main.host.APIVersion
					HSPI_INSTEON_THERMOSTAT.utils.callback = Main.clientCallback.ServiceProxy
					aPIVersion = HSPI_INSTEON_THERMOSTAT.utils.callback.APIVersion
					Exit While
				Catch exception2 As System.Exception
					ProjectData.SetProjectError(exception2)
					Dim exception1 As System.Exception = exception2
					Console.WriteLine(String.Concat("Insteon Thermostat Cannot connect attempt ", num.ToString(), ": ", exception1.Message))
					If (exception1.Message.ToLower().Contains("timeout occured.") Or exception1.Message.ToLower().Contains("timeout occurred.")) Then
						num = num + 1
						If (num < 3) Then
							ProjectData.ClearProjectError()
							Continue While
						End If
					End If
					If (Main.client IsNot Nothing) Then
						Main.client.Dispose()
						Main.client = Nothing
					End If
					If (Main.clientCallback IsNot Nothing) Then
						Main.clientCallback.Dispose()
						Main.clientCallback = Nothing
					End If
					Main.wait(4)
					ProjectData.ClearProjectError()
					flag = True
				End Try
				If (flag) Then
					Exit While
				End If
			End While
			If (Not flag) Then
				Try
					HSPI_INSTEON_THERMOSTAT.utils.callback = HSPI_INSTEON_THERMOSTAT.utils.callback
					HSPI_INSTEON_THERMOSTAT.utils.hs = Main.host
					Main.host.Connect("Insteon Thermostat", "")
					Console.WriteLine("Insteon Thermostat Connected, waiting to be initialized...")
					Do
						Thread.Sleep(30)
					Loop While Main.client.CommunicationState = CommunicationStates.Connected And Not HSPI_INSTEON_THERMOSTAT.utils.bShutDown
					If (HSPI_INSTEON_THERMOSTAT.utils.bShutDown) Then
						Console.WriteLine("Insteon Thermostat Shutting down plug-in")
					Else
						Main.oPlugin.ShutdownIO()
						Console.WriteLine("Insteon Thermostat Connection lost, exiting")
					End If
					Main.client.Disconnect()
					Main.clientCallback.Disconnect()
					Main.wait(2)
					ProjectData.EndApp()
				Catch exception3 As System.Exception
					ProjectData.SetProjectError(exception3)
					Console.WriteLine(String.Concat("Insteon Thermostat Cannot connect(2): ", exception3.Message))
					Main.wait(2)
					ProjectData.EndApp()
					ProjectData.ClearProjectError()
				End Try
			End If
			flag = False
		End Sub

		Private Shared Sub wait(ByVal secs As Integer)
			Thread.Sleep(secs * 1000)
		End Sub
	End Class
End Namespace