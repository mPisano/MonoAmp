Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler.Classes
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class HSPI
		Implements IPlugInAPI
		Public Property ActionAdvancedMode As Boolean Implements IPlugInAPI.ActionAdvancedMode
			Get
				Return Main.oPlugin.ActionAdvancedMode
			End Get
			Set(ByVal value As Boolean)
				Main.oPlugin.ActionAdvancedMode = value
			End Set
		End Property

		Public ReadOnly Property ActionName(ByVal ActionNumber As Integer) As String Implements IPlugInAPI.ActionName
			Get
				Return Main.oPlugin(ActionNumber)
			End Get
		End Property

		Public Property Condition(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Boolean Implements IPlugInAPI.Condition
			Get
				Return Main.oPlugin(TrigInfo)
			End Get
			Set(ByVal value As Boolean)
				Main.oPlugin(TrigInfo) = value
			End Set
		End Property

		Public ReadOnly Property HasConditions(ByVal TriggerNumber As Integer) As Boolean Implements IPlugInAPI.HasConditions
			Get
				Return Main.oPlugin(TriggerNumber)
			End Get
		End Property

		Public ReadOnly Property HasTriggers As Boolean Implements IPlugInAPI.HasTriggers
			Get
				Return Main.oPlugin.HasTriggers
			End Get
		End Property

		Public ReadOnly Property HSCOMPort As Boolean Implements IPlugInAPI.HSCOMPort
			Get
				Return False
			End Get
		End Property

		Public ReadOnly Property Name As String Implements IPlugInAPI.Name
			Get
				Return Main.oPlugin.name()
			End Get
		End Property

		Public ReadOnly Property SubTriggerCount(ByVal TriggerNumber As Integer) As Integer Implements IPlugInAPI.SubTriggerCount
			Get
				Return Main.oPlugin(TriggerNumber)
			End Get
		End Property

		Public ReadOnly Property SubTriggerName(ByVal TriggerNumber As Integer, ByVal SubTriggerNumber As Integer) As String Implements IPlugInAPI.SubTriggerName
			Get
				Return Main.oPlugin(TriggerNumber, SubTriggerNumber)
			End Get
		End Property

		Public ReadOnly Property TriggerConfigured(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Boolean Implements IPlugInAPI.TriggerConfigured
			Get
				Return Main.oPlugin(TrigInfo)
			End Get
		End Property

		Public ReadOnly Property TriggerCount As Integer Implements IPlugInAPI.TriggerCount
			Get
				Return Main.oPlugin.TriggerCount()
			End Get
		End Property

		Public ReadOnly Property TriggerName(ByVal TriggerNumber As Integer) As String Implements IPlugInAPI.TriggerName
			Get
				Return Main.oPlugin(TriggerNumber)
			End Get
		End Property

		Public Sub New()
			MyBase.New()
		End Sub

		Public Function AccessLevel() As Integer Implements IPlugInAPI.AccessLevel
			Return Main.oPlugin.AccessLevel()
		End Function

		Public Function ActionBuildUI(ByVal sUnique As String, ByVal ActInfo As IPlugInAPI.strTrigActInfo) As String Implements IPlugInAPI.ActionBuildUI
			Return Main.oPlugin.ActionBuildUI(sUnique, ActInfo)
		End Function

		Public Function ActionConfigured(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean Implements IPlugInAPI.ActionConfigured
			Return Main.oPlugin.ActionConfigured(ActInfo)
		End Function

		Public Function ActionCount() As Integer Implements IPlugInAPI.ActionCount
			Return Main.oPlugin.ActionCount()
		End Function

		Public Function ActionFormatUI(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As String Implements IPlugInAPI.ActionFormatUI
			Return Main.oPlugin.ActionFormatUI(ActInfo)
		End Function

		Public Function ActionProcessPostUI(ByVal PostData As NameValueCollection, ByVal TrigInfoIN As IPlugInAPI.strTrigActInfo) As IPlugInAPI.strMultiReturn Implements IPlugInAPI.ActionProcessPostUI
			Return Main.oPlugin.ActionProcessPostUI(PostData, TrigInfoIN)
		End Function

		Public Function ActionReferencesDevice(ByVal ActInfo As IPlugInAPI.strTrigActInfo, ByVal dvRef As Integer) As Boolean Implements IPlugInAPI.ActionReferencesDevice
			Return Main.oPlugin.ActionReferencesDevice(ActInfo, dvRef)
		End Function

		Public Sub AdjustCoolSetpoint(ByVal tName As String, ByVal posNegAdjustment As String)
			Try
				Main.oPlugin.AdjustCoolSetpoint(tName, CShort(Conversions.ToInteger(posNegAdjustment)))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "AdjustCoolSetpoint(", tName, ",", posNegAdjustment, ") - ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub AdjustHeatSetpoint(ByVal tName As String, ByVal posNegAdjustment As String)
			Try
				Main.oPlugin.AdjustHeatSetpoint(tName, CShort(Conversions.ToInteger(posNegAdjustment)))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "AdjustHeatSetpoint(", tName, ",", posNegAdjustment, ") - ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function Capabilities() As Integer Implements IPlugInAPI.Capabilities
			Return 20
		End Function

		Public Function ConfigDevice(ByVal ref As Integer, ByVal user As String, ByVal userRights As Integer, ByVal newDevice As Boolean) As String Implements IPlugInAPI.ConfigDevice
			Return Main.oPlugin.ConfigDevice(ref, user, userRights, newDevice)
		End Function

		Public Function ConfigDevicePost(ByVal ref As Integer, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As Enums.ConfigDevicePostReturn Implements IPlugInAPI.ConfigDevicePost
			Return Main.oPlugin.ConfigDevicePost(ref, data, user, userRights)
		End Function

		Public Function GenPage(ByVal link As String) As String Implements IPlugInAPI.GenPage
			Return Nothing
		End Function

		Public Function GetCoolOnMinutes(ByVal hName As String) As Integer
			Dim [integer] As Integer = -1
			Try
				[integer] = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetHvacPedByName(hName, "HSREF_COOL", "Cool"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetCoolOnMinutes(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [integer]
		End Function

		Public Function GetCoolSetpoint(ByVal tName As String) As Integer
			Dim [integer] As Integer = -1
			Try
				[integer] = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_COOL", "Cool"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetCoolSetpoint(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [integer]
		End Function

		Public Function GetExtTemp(ByVal tName As String) As Integer
			Dim num As Integer = -1
			Try
				num = If(Not Conversions.ToBoolean(RuntimeHelpers.GetObjectValue(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)("ExtSensor"))), -1, Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_EXTTEMP", "Ext.Temp")))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetExtTemp(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Public Function GetFan(ByVal tName As String) As String
			Dim item As String = "ERROR"
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_FAN", "Fan")))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetFan(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return item
		End Function

		Public Function GetHeatOnMinutes(ByVal hName As String) As Integer
			Dim [integer] As Integer = -1
			Try
				[integer] = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetHvacPedByName(hName, "HSREF_HEAT", "Heat"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetHeatOnMinutes(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [integer]
		End Function

		Public Function GetHeatSetpoint(ByVal tName As String) As Integer
			Dim [integer] As Integer = -1
			Try
				[integer] = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HEAT", "Heat"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetHeatSetpoint(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [integer]
		End Function

		Public Function GetHold(ByVal tName As String) As String
			Dim item As String = "ERROR"
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HOLD", "Hold")))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetHold(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return item
		End Function

		Public Function GetHumidity(ByVal tName As String) As Integer
			Dim num As Integer = -1
			Try
				num = If(Not Conversions.ToBoolean(RuntimeHelpers.GetObjectValue(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)("Humidistat"))), -1, Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HUMIDITY", "Humidity")))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetHumidity(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Public Function GetLastCoolReset(ByVal hName As String) As DateTime
			Dim [date] As DateTime = New DateTime()
			Try
				[date] = Conversions.ToDate(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetHvacPedByName(hName, "HSREF_COOL", "CoolResetTime"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetLastCoolReset(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [date]
		End Function

		Public Function GetLastHeatReset(ByVal hName As String) As DateTime
			Dim [date] As DateTime = New DateTime()
			Try
				[date] = Conversions.ToDate(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetHvacPedByName(hName, "HSREF_HEAT", "HeatResetTime"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetLastHeatReset(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [date]
		End Function

		Public Function GetLastMaintReset(ByVal hName As String) As DateTime
			Dim [date] As DateTime = New DateTime()
			Try
				[date] = Conversions.ToDate(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetHvacPedByName(hName, "HSREF_MAINT", "MaintenanceResetTime"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetLastHeatReset(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [date]
		End Function

		Public Function GetMaintRemainingMinutes(ByVal hName As String) As Integer
			Dim [integer] As Integer = -1
			Try
				[integer] = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetHvacPedByName(hName, "HSREF_MAINT", "MaintenanceInterval"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetMaintRemainingMinutes(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [integer]
		End Function

		Public Function GetMasterProgram() As String
			Dim str As String = "ERROR"
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				str = Conversions.ToString(plugExtraDataGet.GetNamed("Program"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetMasterProgram() ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function GetMode(ByVal tName As String) As String
			Dim item As String = "ERROR"
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_MODE", "Mode")))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetMode(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return item
		End Function

		Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String Implements IPlugInAPI.GetPagePlugin
			Return Main.oPlugin.GetPagePlugin(pageName, user, userRights, queryString)
		End Function

		Public Function GetProgram(ByVal tName As String) As String
			Dim tstatPedByName As String = "ERROR"
			Try
				tstatPedByName = HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_PROGRAM", "Program")
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetProgram(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return tstatPedByName
		End Function

		Public Function GetTemp(ByVal tName As String) As Integer
			Dim [integer] As Integer = -1
			Try
				[integer] = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_TEMP", "Temp"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("GetTemp(", tName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return [integer]
		End Function

		Public Function HandleAction(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean Implements IPlugInAPI.HandleAction
			Return Main.oPlugin.HandleAction(ActInfo)
		End Function

		Public Sub HSEvent(ByVal EventType As Enums.HSEvent, ByVal parms As Object()) Implements IPlugInAPI.HSEvent
			Main.oPlugin.HSEvent(EventType, parms)
		End Sub

		Public Function InitIO(ByVal port As String) As String Implements IPlugInAPI.InitIO
			Return Main.oPlugin.InitIO(port)
		End Function

		Public Function InstanceFriendlyName() As String Implements IPlugInAPI.InstanceFriendlyName
			Return Main.oPlugin.InstanceFriendlyName()
		End Function

		Public Function InterfaceStatus() As IPlugInAPI.strInterfaceStatus Implements IPlugInAPI.InterfaceStatus
			Return Main.oPlugin.InterfaceStatus()
		End Function

		Public Function PagePut(ByVal data As String) As String Implements IPlugInAPI.PagePut
			Return Nothing
		End Function

		Public Function PluginFunction(ByVal proc As String, ByVal parms As Object()) As Object Implements IPlugInAPI.PluginFunction
			Dim obj As Object
			Dim flag As Boolean = False
			Try
				Dim method As MethodInfo = Me.[GetType]().GetMethod(proc)
				If (method IsNot Nothing) Then
					obj = method.Invoke(Me, parms)
					flag = True
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Method ", proc, " does not exist in this plug-in."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Error in PluginProc: ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return Nothing
			End If
			flag = False
			Return obj
		End Function

		Public Function PluginPropertyGet(ByVal proc As String, ByVal parms As Object()) As Object Implements IPlugInAPI.PluginPropertyGet
			Dim value As Object
			Dim flag As Boolean = False
			Try
				Dim [property] As PropertyInfo = Me.[GetType]().GetProperty(proc)
				If ([property] IsNot Nothing) Then
					value = [property].GetValue(Me, parms)
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Property ", proc, " does not exist in this plug-in."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
					value = Nothing
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Error in PluginPropertyGet: ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
				flag = True
			End Try
			If (Not flag) Then
				Return value
			End If
			flag = False
			Return Nothing
		End Function

		Public Sub PluginPropertySet(ByVal proc As String, ByVal value As Object) Implements IPlugInAPI.PluginPropertySet
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("No properties can be set externally.  Ignoring request for property ", proc), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
		End Sub

		Public Sub PollAllStats()
			Try
				Main.oPlugin.PollAllStats()
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("PollAllStats: - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function PollDevice(ByVal dvRef As Integer) As IPlugInAPI.PollResultInfo Implements IPlugInAPI.PollDevice
			Return Main.oPlugin.PollDevice(dvRef)
		End Function

		Public Sub PollStat(ByVal tName As String)
			Try
				Main.oPlugin.PollStat(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("PollStat(", tName, ") problem polling : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub PollStatByNum(ByVal statNum As String)
			Try
				Main.oPlugin.PollStatByNum(statNum)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("PollStat(num=", statNum, ") problem polling : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function PostBackProc(ByVal pageName As String, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As String Implements IPlugInAPI.PostBackProc
			Return Main.oPlugin.postBackProc(pageName, data, user, userRights)
		End Function

		Public Function RaisesGenericCallbacks() As Boolean Implements IPlugInAPI.RaisesGenericCallbacks
			Return False
		End Function

		Public Sub ResetCoolTime(ByVal hName As String)
			Try
				HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByName(hName, "HSREF_COOL", "Cool", 0, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("ResetHeatTime(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub ResetHeatCoolTime(ByVal hName As String)
			Me.ResetCoolTime(hName)
			Me.ResetHeatTime(hName)
		End Sub

		Public Sub ResetHeatTime(ByVal hName As String)
			Try
				HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByName(hName, "HSREF_HEAT", "Heat", 0, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("ResetHeatTime(", hName, ") ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub ResetMaintTime(ByVal hName As String)
			Try
				HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByName(hName, "HSREF_MAINT", "MaintenanceInterval", 0, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("ResetMaintTime(", hName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function Search(ByVal SearchString As String, ByVal RegEx As Boolean) As SearchReturn() Implements IPlugInAPI.Search
			Dim searchReturnArray As SearchReturn() = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Search : SearchString=", SearchString, " RegEx=", Conversions.ToString(RegEx)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
			Return searchReturnArray
		End Function

		Public Sub SetCoolSetpoint(ByVal tName As String, ByVal setPoint As String)
			Try
				Main.oPlugin.SetCoolSetpoint(tName, CShort(Conversions.ToInteger(setPoint)))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "SetCoolSetpoint(", tName, ",", setPoint, ") - ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetFanAuto(ByVal tName As String)
			Try
				Main.oPlugin.SetFanAuto(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetFanAuto(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetFanOn(ByVal tName As String)
			Try
				Main.oPlugin.SetFanOn(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetFanOn(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetFanToggle(ByVal tName As String)
			Try
				Main.oPlugin.SetFanToggle(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetFanToggle(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetHeatSetpoint(ByVal tName As String, ByVal setPoint As String)
			Try
				Main.oPlugin.SetHeatSetpoint(tName, CShort(Conversions.ToInteger(setPoint)))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "SetHeatSetpoint(", tName, ",", setPoint, ") - ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetHold(ByVal tName As String)
			Try
				Main.oPlugin.SetHold(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetHold(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetHoldToggle(ByVal tName As String)
			Try
				Main.oPlugin.SetHoldToggle(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetHoldToggle(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetIOMulti(ByVal colSend As List(Of CAPI.CAPIControl)) Implements IPlugInAPI.SetIOMulti
			Main.oPlugin.SetIOMulti(colSend)
		End Sub

		Public Sub SetMasterProgram(ByVal pName As String)
			Try
				Main.oPlugin.SetMasterProgram(pName, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetMasterProgram(", pName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetMode(ByVal tName As String, ByVal mode As String)
			Try
				Main.oPlugin.SetModeByString(tName, mode)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "SetMode(", tName, ",", mode, ") - ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetModeNext(ByVal tName As String)
			Try
				Main.oPlugin.SetModeNext(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetModeNext(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetModePrev(ByVal tName As String)
			Try
				Main.oPlugin.SetModePrev(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetModePrev(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetProgram(ByVal tName As String, ByVal pName As String)
			Try
				Main.oPlugin.SetProgram(tName, pName, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "SetProgram(", tName, ",", pName, ") - ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetProgramParams(ByVal ParmString As String)
			Try
				Main.oPlugin.SetProgramParams(ParmString)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetProgram(", ParmString, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetRun(ByVal tName As String)
			Try
				Main.oPlugin.SetRun(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetRun(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetTempDown(ByVal tName As String)
			Try
				Main.oPlugin.SetTempDown(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTempDown(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetTempUp(ByVal tName As String)
			Try
				Main.oPlugin.SetTempUp(tName)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTempUp(", tName, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub SetTStatSetpoint(ByVal params As String)
			Try
				Main.oPlugin.SetTStatSetpoint(params)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTStatSetpoint(", params, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Sub ShutdownIO() Implements IPlugInAPI.ShutdownIO
			Main.oPlugin.ShutdownIO()
		End Sub

		Public Sub SpeakIn(ByVal device As Integer, ByVal txt As String, ByVal w As Boolean, ByVal host As String) Implements IPlugInAPI.SpeakIn
			Dim str() As String = { "SpeakIn : device=", Conversions.ToString(device), " txt=", txt, " w=", Conversions.ToString(w), " host=", host }
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
		End Sub

		Public Function SupportsAddDevice() As Boolean Implements IPlugInAPI.SupportsAddDevice
			Return False
		End Function

		Public Function SupportsConfigDevice() As Boolean Implements IPlugInAPI.SupportsConfigDevice
			Return True
		End Function

		Public Function SupportsConfigDeviceAll() As Boolean Implements IPlugInAPI.SupportsConfigDeviceAll
			Return False
		End Function

		Public Function SupportsMultipleInstances() As Boolean Implements IPlugInAPI.SupportsMultipleInstances
			Return False
		End Function

		Public Function SupportsMultipleInstancesSingleEXE() As Boolean Implements IPlugInAPI.SupportsMultipleInstancesSingleEXE
			Return False
		End Function

		Public Function TriggerBuildUI(ByVal sUnique As String, ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As String Implements IPlugInAPI.TriggerBuildUI
			Return Main.oPlugin.TriggerBuildUI(sUnique, TrigInfo)
		End Function

		Public Function TriggerFormatUI(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As String Implements IPlugInAPI.TriggerFormatUI
			Return Main.oPlugin.TriggerFormatUI(TrigInfo)
		End Function

		Public Function TriggerProcessPostUI(ByVal PostData As NameValueCollection, ByVal TrigInfoIn As IPlugInAPI.strTrigActInfo) As IPlugInAPI.strMultiReturn Implements IPlugInAPI.TriggerProcessPostUI
			Return Main.oPlugin.TriggerProcessPostUI(PostData, TrigInfoIn)
		End Function

		Public Function TriggerReferencesDevice(ByVal TrigInfo As IPlugInAPI.strTrigActInfo, ByVal dvRef As Integer) As Boolean Implements IPlugInAPI.TriggerReferencesDevice
			Return Main.oPlugin.TriggerReferencesDevice(TrigInfo, dvRef)
		End Function

		Public Function TriggerTrue(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Boolean Implements IPlugInAPI.TriggerTrue
			Return Main.oPlugin.TriggerTrue(TrigInfo)
		End Function

		Public Sub TStatCallBack(ByVal ParmString As String)
			Dim flag As Boolean = False
			Try
				Dim strArrays As String() = Strings.Split(ParmString, " ", -1, CompareMethod.Binary)
				If (Information.UBound(strArrays, 1) >= 1) Then
					Dim str As String = String.Join(" ", strArrays, 0, Information.UBound(strArrays, 1))
					Dim str1 As String = strArrays(Information.UBound(strArrays, 1))
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Simulate button press ", str1, " on thermostat ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					If (Operators.CompareString(str1, "tempdn", False) = 0) Then
						str1 = "tempdown"
					End If
					If (Operators.CompareString(str1, "prog", False) = 0) Then
						str1 = "program"
					End If
					If (Operators.CompareString(str1, "prog-heat", False) = 0) Then
						str1 = "program-heat"
					End If
					If (Operators.CompareString(str1, "prog-cool", False) = 0) Then
						str1 = "program-cool"
					End If
					Dim lower As String = str1.ToLower()
					If (Operators.CompareString(lower, "tempup", False) = 0) Then
						Main.oPlugin.SetTempUp(str)
					ElseIf (Operators.CompareString(lower, "tempdown", False) = 0) Then
						Main.oPlugin.SetTempDown(str)
					ElseIf (Operators.CompareString(lower, "mode-next", False) = 0) Then
						Main.oPlugin.SetModeNext(str)
					ElseIf (Operators.CompareString(lower, "mode-prev", False) <> 0) Then
						If (Operators.CompareString(lower, "off", False) <> 0) Then
							If (Operators.CompareString(lower, "heat", False) <> 0) Then
								If (Operators.CompareString(lower, "cool", False) <> 0) Then
									If (Operators.CompareString(lower, "auto", False) <> 0) Then
										If (Operators.CompareString(lower, "program", False) <> 0) Then
											If (Operators.CompareString(lower, "program-heat", False) <> 0) Then
												If (Operators.CompareString(lower, "program-cool", False) <> 0) Then
													If (Operators.CompareString(lower, "fan-on", False) = 0) Then
														Main.oPlugin.SetFanOn(str)
														flag = True
													ElseIf (Operators.CompareString(lower, "fan-auto", False) = 0) Then
														Main.oPlugin.SetFanAuto(str)
														flag = True
													ElseIf (Operators.CompareString(lower, "fan-toggle", False) = 0) Then
														Main.oPlugin.SetFanToggle(str)
														flag = True
													ElseIf (Operators.CompareString(lower, "hold-run", False) = 0) Then
														Main.oPlugin.SetRun(str)
														flag = True
													ElseIf (Operators.CompareString(lower, "hold-hold", False) <> 0) Then
														If (Operators.CompareString(lower, "hold-toggle", False) = 0) Then
															Main.oPlugin.SetHoldToggle(str)
														End If
														flag = True
													Else
														Main.oPlugin.SetHold(str)
														flag = True
													End If
												End If
											End If
										End If
									End If
								End If
							End If
						End If
						If (Not flag) Then
							If (Not flag) Then
								If (Not flag) Then
									If (Not flag) Then
										If (Not flag) Then
											If (Not flag) Then
												Main.oPlugin.SetModeByString(str, str1.ToLower())
											End If
										End If
									End If
								End If
							End If
						End If
					Else
						Main.oPlugin.SetModePrev(str)
					End If
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log("TStatCallBack: Too few arguments", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("TStatCallBack(", ParmString, ") - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			flag = False
		End Sub
	End Class
End Namespace