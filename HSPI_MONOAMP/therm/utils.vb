Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler.Classes
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Threading

Namespace HSPI_INSTEON_THERMOSTAT
	<StandardModule>
	Friend NotInheritable Class utils
		Public Const IFACE_NAME As String = "Insteon Thermostat"

		Public Const IFACE_VERSION As String = "3.0.0.9"

		Public Const IFACE_ADDRESS As String = "THERMOSTATS"

		Public Shared sHelpFile As String

		Public Const USER_GUEST As Integer = 1

		Public Const USER_ADMIN As Integer = 2

		Public Const USER_LOCAL As Integer = 4

		Public Const USER_NORMAL As Integer = 8

		Public Shared callback As IAppCallbackAPI

		Public Shared hs As IHSApplication

		Public Shared Instance As String

		Public Shared InterfaceVersion As Integer

		Public Shared bInitIOComplete As Boolean

		Public Shared bShutDown As Boolean

		Public Shared CurrentPage As Object

		Public Shared masterProgramRef As Long

		Public Const MASTER_PROGRAM_DEVICE_NAME As String = "Master Program"

		Public Const DEFAULT_LOCATION2_NAME As String = "Thermostats"

		Public Const DEFAULT_LOCATION1_NAME As String = "Insteon"

		Public Shared myConfig As Config

		Public Shared myTstat As Thermostats

		Public Shared myInsteon As Insteon

		Public Const INSTEON_DEVCAT0503 As String = "0503"

		Public Const INSTEON_DEVCAT050E As String = "050E"

		Public Const INSTEON_DEVCAT050B As String = "050B"

		Public Const INSTEON_DEVCAT050A As String = "050A"

		Public Const INSTEON_DEVCAT0511 As String = "0511"

		Public Const INSTEON_DEVCAT0512 As String = "0512"

		Public Shared gModeOpts As SortedDictionary(Of Integer, String)

		Public Shared gModeOptsByName As SortedDictionary(Of String, Integer)

		Public Shared ModeOptsConfig As String()

		Public Shared ModeOptsStatusSmarthome As String()

		Public Shared ModeOptsStatusVenstar As String()

		Public Shared gFanOpts As SortedDictionary(Of Integer, String)

		Public Shared gFanOptsByName As SortedDictionary(Of String, Integer)

		Public Shared FanOptsConfig As String()

		Public Shared FanOptsStatus As String()

		Public Shared FanOptsActions As String()

		Public Shared gHoldOpts As SortedDictionary(Of Integer, String)

		Public Shared gHoldOptsByName As SortedDictionary(Of String, Integer)

		Public Shared HoldOptsConfig As String()

		Public Shared HoldOptsStatus As String()

		Public Shared HoldOptsActions As String()

		Public Const LABEL_STATUS_TIME As String = "@%0@"

		Public Const LABEL_STATUS_TEMP As String = "@%0@"

		Public Const LABEL_STATUS_HUMIDITY As String = "@%0@"

		Public Const LABEL_CONTROL_RESET As String = "Reset"

		Public Const LABEL_CONTROL_DONTSET As String = "Don't Set"

		Public Const LABEL_CONTROL_OFF As String = "Off"

		Public Const LABEL_CONTROL_ON As String = "On"

		Public Const LABEL_CONTROL_TOGGLE As String = "Toggle"

		Public Const LABEL_CONTROL_PLUS As String = "+"

		Public Const LABEL_CONTROL_MINUS As String = "-"

		Public Const LABEL_CONTROL_HEAT As String = "Heat"

		Public Const LABEL_CONTROL_COOL As String = "Cool"

		Public Const LABEL_CONTROL_AUTO As String = "Auto"

		Public Const LABEL_CONTROL_PROG As String = "Prog"

		Public Const LABEL_CONTROL_PROGRAM As String = "Program"

		Public Const LABEL_CONTROL_PROGRAMHEAT As String = "Program-Heat"

		Public Const LABEL_CONTROL_PROGRAMCOOL As String = "Program-Cool"

		Public Const LABEL_CONTROL_RUN As String = "Run"

		Public Const LABEL_CONTROL_HOLD As String = "Hold"

		Public Const LABEL_STATUS_NONE As String = "None"

		Public Const LABEL_STATUS_UNKNOWN As String = "UNKNOWN"

		Public Const LABEL_STATUS_FAHRENHEIT As String = "Fahrenheit"

		Public Const LABEL_STATUS_CELSIUS As String = "Celsius"

		Public Const PED_NONE As String = "None"

		Public Const PED_DEFAULT As String = "Default"

		Public Const PED_VERSION As String = "Version"

		Public Const PED_NAME As String = "Name"

		Public Const PED_PARENT_NAME As String = "ParentName"

		Public Const PED_NUM As String = "Num"

		Public Const PED_ISREGISTERED As String = "Registered"

		Public Const PED_HASEXTSENSOR As String = "ExtSensor"

		Public Const PED_HASHUMIDISTAT As String = "Humidistat"

		Public Const PED_DISPLAY_DEGREES As String = "DisplayDegrees"

		Public Const PED_LOCATION As String = "Location"

		Public Const PED_HSREF As String = "HSREF"

		Public Const PED_MODE_HSREF As String = "HSREF_MODE"

		Public Const PED_FAN_HSREF As String = "HSREF_FAN"

		Public Const PED_COOL_HSREF As String = "HSREF_COOL"

		Public Const PED_HEAT_HSREF As String = "HSREF_HEAT"

		Public Const PED_TEMP_HSREF As String = "HSREF_TEMP"

		Public Const PED_EXTTEMP_HSREF As String = "HSREF_EXTTEMP"

		Public Const PED_HUMIDITY_HSREF As String = "HSREF_HUMIDITY"

		Public Const PED_PROGRAM_HSREF As String = "HSREF_PROGRAM"

		Public Const PED_HOLD_HSREF As String = "HSREF_HOLD"

		Public Const PED_MAINT_HSREF As String = "HSREF_MAINT"

		Public Const PED_HVACUNIT As String = "HVACUnit"

		Public Const PED_HVACCALL As String = "HVACCall"

		Public Const PED_HVACCALL_STARTTIME As String = "HVACCallStartTime"

		Public Const PED_MODE As String = "Mode"

		Public Const PED_FAN As String = "Fan"

		Public Const PED_COOL As String = "Cool"

		Public Const PED_HEAT As String = "Heat"

		Public Const PED_TEMP As String = "Temp"

		Public Const PED_EXTTEMP As String = "Ext.Temp"

		Public Const PED_HUMIDITY As String = "Humidity"

		Public Const PED_PROGRAM As String = "Program"

		Public Const PED_PROGRAM_THERMOSTAT As String = "Thermostat"

		Public Const PED_HOLD As String = "Hold"

		Public Const PED_COOL_RESET As String = "CoolResetTime"

		Public Const PED_HEAT_RESET As String = "HeatResetTime"

		Public Const PED_MAINT_RESET As String = "MaintenanceResetTime"

		Public Const PED_MAINT As String = "MaintenanceInterval"

		Public Const PED_INSTEONADDR_L As String = "InsteonAddressLeft"

		Public Const PED_INSTEONADDR_M As String = "InsteonAddressMid"

		Public Const PED_INSTEONADDR_R As String = "InsteonAddressRight"

		Public Const PED_INSTEONADDR As String = "InsteonAddress"

		Public Const PED_INSTEONPROTOCOL As String = "InsteonProtocol"

		Public Const PED_INSTEONPROTOCOL_FORCE2 As String = "InsteonProtocol2"

		Public Const PED_INSTEONDEVCAT As String = "InsteonDeviceCategory"

		Public Const PED_INSTEONFIRMWARE As String = "InsteonFirmware"

		Public Const PED_INSTEONLINKS As String = "InsteonLinks"

		Public Const TSTAT_MODE_SUFFIX As String = " Mode"

		Public Const TSTAT_TEMP_SUFFIX As String = " Temp"

		Public Const TSTAT_EXTTEMP_SUFFIX As String = " Ext. Temp"

		Public Const TSTAT_FAN_SUFFIX As String = " Fan"

		Public Const TSTAT_COOL_SUFFIX As String = " Cool"

		Public Const TSTAT_HEAT_SUFFIX As String = " Heat"

		Public Const TSTAT_HUMIDITY_SUFFIX As String = " Humidity"

		Public Const TSTAT_PROGRAM_SUFFIX As String = " Program"

		Public Const TSTAT_HOLD_SUFFIX As String = " Hold"

		Public Const HVAC_MODE_SUFFIX As String = " Mode"

		Public Const HVAC_FAN_SUFFIX As String = " Fan"

		Public Const HVAC_COOL_SUFFIX As String = " Cool"

		Public Const HVAC_HEAT_SUFFIX As String = " Heat"

		Public Const HVAC_MAINT_SUFFIX As String = " Maintenance"

		Public Const IMAGE_DIR As String = "/images/INSTEON_THERMOSTAT"

		Public Const IMAGE_DEVICE_TSTAT_SMALL As String = "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat.gif"

		Public Const IMAGE_DEVICE_TSTAT_LARGE As String = "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat_big.jpg"

		Public Const IMAGE_DEVICE_HVAC_SMALL As String = "/images/INSTEON_THERMOSTAT/gas_furnace.png"

		Public Const IMAGE_DEVICE_HVAC_LARGE As String = "/images/INSTEON_THERMOSTAT/gas_furnace_big.png"

		Public Const IMAGE_MODE_OFF As String = "/images/INSTEON_THERMOSTAT/mode-off.png"

		Public Const IMAGE_MODE_HEAT As String = "/images/INSTEON_THERMOSTAT/mode-heat.png"

		Public Const IMAGE_MODE_COOL As String = "/images/INSTEON_THERMOSTAT/mode-cool.png"

		Public Const IMAGE_MODE_AUTO As String = "/images/INSTEON_THERMOSTAT/mode-auto.png"

		Public Const IMAGE_MODE_PROG As String = "/images/INSTEON_THERMOSTAT/mode-prog.png"

		Public Const IMAGE_MODE_PROG_HEAT As String = "/images/INSTEON_THERMOSTAT/mode-prog-heat.png"

		Public Const IMAGE_MODE_PROG_COOL As String = "/images/INSTEON_THERMOSTAT/mode-prog-cool.png"

		Public Const IMAGE_FAN_OFF As String = "/images/INSTEON_THERMOSTAT/fan-off.png"

		Public Const IMAGE_FAN_ON As String = "/images/INSTEON_THERMOSTAT/fan-on.png"

		Public Const IMAGE_TIME As String = "/images/INSTEON_THERMOSTAT/time.png"

		Public Const IMAGE_TEMP As String = "/images/INSTEON_THERMOSTAT/temp.png"

		Public Const IMAGE_TEMPCOOL As String = "/images/INSTEON_THERMOSTAT/temp-cool.png"

		Public Const IMAGE_TEMPHEAT As String = "/images/INSTEON_THERMOSTAT/temp-heat.png"

		Public Const IMAGE_HUMIDITY As String = "/images/INSTEON_THERMOSTAT/humid.png"

		Public Const IMAGE_RUN As String = "/images/INSTEON_THERMOSTAT/run.png"

		Public Const IMAGE_HOLD As String = "/images/INSTEON_THERMOSTAT/hold.png"

		Public Const IMAGE_UNKNOWN As String = "/images/HomeSeer/status/unknown.png"

		Public Const TRIGGER_LABEL As String = "Insteon Thermostat : Special Triggers"

		Public Const TRIGGER_DATA_PROMPT As String = "--Please Select--"

		Public Const TRIGGER_PROGRAM_CHANGED As String = "Program changed"

		Public Const ACTION_CHANGE_MODE As String = "Change Mode"

		Public Const ACTION_CHANGE_FAN As String = "Change Fan"

		Public Const ACTION_CHANGE_HEATSP As String = "Change Heat SetPoint"

		Public Const ACTION_CHANGE_COOLSP As String = "Change Cool SetPoint"

		Public Const ACTION_CHANGE_HOLDRUN As String = "Change Hold/Run"

		Public Const ACTION_SET_PROGRAM As String = "Set Program"

		Public Const ACTION_POLL_THERMOSTAT As String = "Poll Thermostat"

		Public Const ACTION_DATA_POLL_ALL As String = "All"

		Public Const ACTION_DATA_CHANGE_MODE_NEXT As String = "Next Operating Mode"

		Public Const ACTION_DATA_CHANGE_MODE_PREV As String = "Previous Operating Mode"

		Private Shared debugLogHeader As Boolean

		Shared Sub New()
			HSPI_INSTEON_THERMOSTAT.utils.sHelpFile = "/html/help/INSTEON_THERMOSTAT/hspi_insteon_thermostat_help.html"
			HSPI_INSTEON_THERMOSTAT.utils.Instance = ""
			HSPI_INSTEON_THERMOSTAT.utils.bInitIOComplete = False
			HSPI_INSTEON_THERMOSTAT.utils.bShutDown = False
			HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef = CLng(-1)
			HSPI_INSTEON_THERMOSTAT.utils.myConfig = New Config()
			HSPI_INSTEON_THERMOSTAT.utils.myTstat = New Thermostats()
			HSPI_INSTEON_THERMOSTAT.utils.myInsteon = New Insteon()
			HSPI_INSTEON_THERMOSTAT.utils.gModeOpts = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.gModeOptsByName = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.ModeOptsConfig = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.ModeOptsStatusSmarthome = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.ModeOptsStatusVenstar = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.gFanOpts = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.gFanOptsByName = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.FanOptsConfig = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.FanOptsStatus = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.FanOptsActions = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.gHoldOptsByName = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.HoldOptsConfig = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.HoldOptsStatus = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.HoldOptsActions = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.debugLogHeader = False
		End Sub

		Private Shared Sub AppendThisText(ByVal msg As String)
			Dim num As Integer = 5
			For i As Integer = 1 To num
				Try
					File.AppendAllText(HSPI_INSTEON_THERMOSTAT.utils.myConfig.logFileDebug, msg)
					Exit For
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(1)
					ProjectData.ClearProjectError()
				End Try
			Next

		End Sub

		Public Shared Function AsHexString(ByVal values As Byte()) As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim length As Integer = CInt(values.Length) - 1
			Dim num As Integer = 0
			Do
				If (num <= 0) Then
					stringBuilder.Append("[")
				Else
					stringBuilder.Append(" [")
				End If
				Try
					stringBuilder.Append(values(num).ToString("X2"))
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					stringBuilder.Append("??")
					ProjectData.ClearProjectError()
				End Try
				stringBuilder.Append("]")
				num = num + 1
			Loop While num <= length
			Return stringBuilder.ToString()
		End Function

		Public Shared Function DecodeDecryptString(ByRef inString As String) As Object
			Dim str As String = "InsteonThermostatsqaz123"
			Return HSPI_INSTEON_THERMOSTAT.utils.DecodeDecryptString(inString, str)
		End Function

		Public Shared Function DecodeDecryptString(ByRef inString As String, ByRef password As String) As Object
			Dim obj As Object
			If (String.IsNullOrEmpty(inString) Or String.IsNullOrEmpty(password)) Then
				Return Nothing
			End If
			Try
				Dim numArray As Byte() = Convert.FromBase64String(inString)
				Dim str As String = Encoding.ASCII.GetString(numArray)
				obj = HSPI_INSTEON_THERMOSTAT.utils.hs.DecryptString(str, password, "InsteonThermostatsrty456")
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("DecodeDecryptString - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				obj = Nothing
				ProjectData.ClearProjectError()
			End Try
			Return obj
		End Function

		Public Shared Function DeSerializeObject(ByRef bteIn As Byte(), ByRef ObjOut As Object) As Boolean
			Dim flag As Boolean
			If (bteIn Is Nothing) Then
				Return False
			End If
			If (CInt(bteIn.Length) < 1) Then
				Return False
			End If
			If (ObjOut Is Nothing) Then
				Return False
			End If
			Dim binaryFormatter As System.Runtime.Serialization.Formatters.Binary.BinaryFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
			Try
				ObjOut.[GetType]()
				ObjOut = Nothing
				Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream(bteIn)
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(binaryFormatter.Deserialize(memoryStream))
				If (objectValue IsNot Nothing) Then
					objectValue.[GetType]()
					ObjOut = RuntimeHelpers.GetObjectValue(objectValue)
					flag = If(ObjOut IsNot Nothing, True, False)
				Else
					flag = False
				End If
			Catch invalidCastException As System.InvalidCastException
				ProjectData.SetProjectError(invalidCastException)
				flag = False
				ProjectData.ClearProjectError()
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Insteon Thermostat Error: DeSerializing object: ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				flag = False
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Public Shared Function EncryptEncodeString(ByRef inString As String) As Object
			Dim str As String = "InsteonThermostatsqaz123"
			Return HSPI_INSTEON_THERMOSTAT.utils.EncryptEncodeString(inString, str)
		End Function

		Public Shared Function EncryptEncodeString(ByRef inString As String, ByRef password As String) As Object
			Dim base64String As Object
			If (String.IsNullOrEmpty(inString) Or String.IsNullOrEmpty(password)) Then
				Return Nothing
			End If
			Try
				Dim str As String = HSPI_INSTEON_THERMOSTAT.utils.hs.EncryptStringEx(inString, password, "InsteonThermostatsrty456")
				Dim bytes As Byte() = Encoding.ASCII.GetBytes(str)
				base64String = Convert.ToBase64String(bytes, Base64FormattingOptions.None)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("EncryptEncodeString - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				base64String = Nothing
				ProjectData.ClearProjectError()
			End Try
			Return base64String
		End Function

		Public Shared Function GetLicenseModeString() As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Select Case HSPI_INSTEON_THERMOSTAT.utils.hs.PluginLicenseMode("Insteon Thermostat")
				Case Enums.REGISTRATION_MODES.REG_UNKNOWN
					stringBuilder.Append("Unknown registration status")
					Exit Select
				Case Enums.REGISTRATION_MODES.REG_UNREG
					stringBuilder.Append("Unregistered")
					Exit Select
				Case Enums.REGISTRATION_MODES.REG_TRIAL
					stringBuilder.Append("Trial")
					Exit Select
				Case Enums.REGISTRATION_MODES.REG_REGISTERED
					stringBuilder.Append("Registered.  Thank you!")
					Exit Select
				Case Enums.REGISTRATION_MODES.REG_READY_TO_REGISTER
					stringBuilder.Append("Ready to register")
					Exit Select
				Case Else
					stringBuilder.Append("Unknown")
					Exit Select
			End Select
			Return stringBuilder.ToString()
		End Function

		Private Shared Function GetLogLevelString(ByVal Log_Level As HSPI_INSTEON_THERMOSTAT.utils.LogLevel) As Object
			Dim logLevel As HSPI_INSTEON_THERMOSTAT.utils.LogLevel = Log_Level
			If (logLevel = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Always) Then
				Return "ALWAYS"
			End If
			If (logLevel = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug) Then
				Return "DEBUG"
			End If
			If (logLevel = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors) Then
				Return "ERRORS"
			End If
			If (logLevel = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info) Then
				Return "INFO"
			End If
			If (logLevel = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test) Then
				Return "TEST"
			End If
			Return "UNKNOWN"
		End Function

		Public Shared Sub initGlobals()
			HSPI_INSTEON_THERMOSTAT.utils.gModeOpts = New SortedDictionary(Of Integer, String)() From
			{
				{ -1, "Don't Set" },
				{ 0, "Off" },
				{ 1, "Heat" },
				{ 2, "Cool" },
				{ 3, "Auto" },
				{ 5, "Program" },
				{ 6, "Program-Heat" },
				{ 7, "Program-Cool" }
			}
			HSPI_INSTEON_THERMOSTAT.utils.gModeOptsByName = New SortedDictionary(Of String, Integer)() From
			{
				{ "Don't Set", -1 },
				{ "Off", 0 },
				{ "Heat", 1 },
				{ "Cool", 2 },
				{ "Auto", 3 },
				{ "Program", 5 },
				{ "Program-Heat", 6 },
				{ "Program-Cool", 7 }
			}
			Dim item() As String = { HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(-1), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(1), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(2), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(3) }
			HSPI_INSTEON_THERMOSTAT.utils.ModeOptsConfig = item
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(1), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(2), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(3), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(5), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(6), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(7) }
			HSPI_INSTEON_THERMOSTAT.utils.ModeOptsStatusVenstar = item
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(1), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(2), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(3), HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(5) }
			HSPI_INSTEON_THERMOSTAT.utils.ModeOptsStatusSmarthome = item
			HSPI_INSTEON_THERMOSTAT.utils.gFanOpts = New SortedDictionary(Of Integer, String)() From
			{
				{ -1, "Don't Set" },
				{ 0, "Auto" },
				{ 1, "On" },
				{ 2, "Toggle" }
			}
			HSPI_INSTEON_THERMOSTAT.utils.gFanOptsByName = New SortedDictionary(Of String, Integer)() From
			{
				{ "Don't Set", -1 },
				{ "Auto", 0 },
				{ "On", 1 },
				{ "Toggle", 2 }
			}
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(-1), HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(1) }
			HSPI_INSTEON_THERMOSTAT.utils.FanOptsConfig = item
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(1) }
			HSPI_INSTEON_THERMOSTAT.utils.FanOptsStatus = item
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(1), HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(2) }
			HSPI_INSTEON_THERMOSTAT.utils.FanOptsActions = item
			HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts = New SortedDictionary(Of Integer, String)() From
			{
				{ -1, "Don't Set" },
				{ 0, "Run" },
				{ 1, "Hold" },
				{ 2, "Toggle" }
			}
			HSPI_INSTEON_THERMOSTAT.utils.gHoldOptsByName = New SortedDictionary(Of String, Integer)() From
			{
				{ "Don't Set", -1 },
				{ "Run", 0 },
				{ "Hold", 1 },
				{ "Toggle", 2 }
			}
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(-1), HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(1) }
			HSPI_INSTEON_THERMOSTAT.utils.HoldOptsConfig = item
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(1) }
			HSPI_INSTEON_THERMOSTAT.utils.HoldOptsStatus = item
			item = New String() { HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(0), HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(1), HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(2) }
			HSPI_INSTEON_THERMOSTAT.utils.HoldOptsActions = item
		End Sub

		Public Shared Function IsHex(ByVal value As String) As Boolean
			Dim flag As Boolean = False
			Dim flag1 As Boolean = True
			If (String.IsNullOrEmpty(value) OrElse Strings.Len(value.Trim()) <= 0) Then
				flag1 = False
			Else
				Dim num As Integer = Strings.Len(value)
				Dim num1 As Integer = 1
				While num1 <= num
					If (Strings.InStr("0123456789ABCDEF", Strings.UCase(Strings.Mid(value, num1, 1)), CompareMethod.Binary) <> 0) Then
						num1 = num1 + 1
					Else
						flag1 = False
						flag = True
					End If
					If (flag) Then
						Exit While
					End If
				End While
			End If
			flag = False
			Return flag1
		End Function

		Public Shared Function isSmarthomeDEVCAT(ByVal tstat As Collection) As Boolean
			If (HSPI_INSTEON_THERMOSTAT.utils.isSmarthomeWiredDEVCAT(tstat)) Then
				Return True
			End If
			If (HSPI_INSTEON_THERMOSTAT.utils.isSmarthomeWirelessDEVCAT(tstat)) Then
				Return True
			End If
			Return False
		End Function

		Public Shared Function isSmarthomeWiredDEVCAT(ByVal tstat As Collection) As Boolean
			Dim flag As Boolean
			If (Not tstat.Contains("InsteonDeviceCategory")) Then
				Throw New System.Exception("Unable to determine thermostat type, Insteon device category (devcat) is not set yet.")
			End If
			Try
				flag = If(Operators.CompareString(Conversions.ToString(tstat("InsteonDeviceCategory")), "050B", False) <> 0, False, True)
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Throw New System.Exception(String.Concat("Unable to determine thermostat type : ", exception.Message))
			End Try
			Return flag
		End Function

		Public Shared Function isSmarthomeWirelessDEVCAT(ByVal tstat As Collection) As Boolean
			Dim flag As Boolean
			Dim flag1 As Boolean = False
			If (Not tstat.Contains("InsteonDeviceCategory")) Then
				Throw New System.Exception("Unable to determine thermostat type, Insteon device category (devcat) is not set yet.")
			End If
			Try
				Dim str As String = Conversions.ToString(tstat("InsteonDeviceCategory"))
				If (Operators.CompareString(str, "050A", False) <> 0) Then
					If (Operators.CompareString(str, "0511", False) <> 0) Then
						If (Operators.CompareString(str, "0512", False) <> 0) Then
							flag = False
							flag1 = True
						End If
					End If
				End If
				If (Not flag1) Then
					flag = True
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Throw New System.Exception(String.Concat("Unable to determine thermostat type : ", exception.Message))
			End Try
			flag1 = False
			Return flag
		End Function

		Public Shared Function isVenstarDEVCAT(ByVal tstat As Collection) As Boolean
			Dim flag As Boolean
			If (Not tstat.Contains("InsteonDeviceCategory")) Then
				Throw New System.Exception("Unable to determine thermostat type, Insteon device category (devcat) is not set yet.")
			End If
			Try
				Dim str As String = Conversions.ToString(tstat("InsteonDeviceCategory"))
				flag = If(Operators.CompareString(str, "0503", False) = 0 OrElse Operators.CompareString(str, "050E", False) = 0, True, False)
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Throw New System.Exception(String.Concat("Unable to determine thermostat type : ", exception.Message))
			End Try
			Return flag
		End Function

		Public Shared Sub Log(ByVal Message As String, Optional ByVal Log_Level As HSPI_INSTEON_THERMOSTAT.utils.LogLevel = 0)
			Dim now As DateTime
			Dim str As String()
			Dim str1 As String = "Insteon Thermostat"
			Try
				If (Log_Level = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors) Then
					str1 = String.Concat(str1, " Error")
				End If
				If ((HSPI_INSTEON_THERMOSTAT.utils.myConfig.logLvl And CInt(Log_Level) Or -((Log_Level = HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Always) > 0)) > CInt(HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Always)) Then
					HSPI_INSTEON_THERMOSTAT.utils.hs.WriteLog(str1, Message)
				End If
				If ((HSPI_INSTEON_THERMOSTAT.utils.myConfig.logLvl And 64) > 0) Then
					If (Log_Level <> HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors) Then
						str1 = Conversions.ToString(Operators.ConcatenateObject(String.Concat(str1, " ~ "), HSPI_INSTEON_THERMOSTAT.utils.GetLogLevelString(Log_Level)))
					End If
					If (Not HSPI_INSTEON_THERMOSTAT.utils.debugLogHeader) Then
						HSPI_INSTEON_THERMOSTAT.utils.AppendThisText("" & VbCrLf & "" & VbCrLf & "" & VbCrLf & "")
						HSPI_INSTEON_THERMOSTAT.utils.debugLogHeader = True
					End If
					ReDim str(5)
					now = DateAndTime.Now
					str(0) = now.ToString()
					str(1) = " ~ "
					str(2) = str1
					str(3) = " ~ "
					str(4) = Message
					Dim str2 As String = String.Concat(str)
					Console.WriteLine(str2)
					HSPI_INSTEON_THERMOSTAT.utils.AppendThisText(String.Concat(str2, "" & VbCrLf & ""))
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				ReDim str(7)
				now = DateAndTime.Now
				str(0) = now.ToString()
				str(1) = " ~ "
				str(2) = str1
				str(3) = " Failed Log Message="
				str(4) = Message
				str(5) = " Cause="
				str(6) = exception.Message
				Dim str3 As String = String.Concat(str)
				Console.WriteLine(str3)
				HSPI_INSTEON_THERMOSTAT.utils.AppendThisText(str3)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Shared Sub LogCAPIDeviceHeader()
			HSPI_INSTEON_THERMOSTAT.utils.Log("		CAPI [Idx] [Val] [Row] [Col] [Span] [Type] [String] [Label]", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
		End Sub

		Friend Shared Sub LogDevice(ByVal dvRef As Long, Optional ByVal logCAPI As Boolean = True, Optional ByVal logRelationship As Boolean = True, Optional ByVal logPED As Boolean = True)
			Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(CInt(dvRef)), DeviceClass)
			Dim cAPIStatu As CAPI.CAPIStatus = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetStatus(CInt(dvRef)), CAPI.CAPIStatus)
			Dim [interface]() As String = { "[", deviceByRef.get_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", Conversions.ToString(deviceByRef.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs)), "] [", deviceByRef.get_Address(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", deviceByRef.get_Code(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", deviceByRef.get_Location(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", deviceByRef.get_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", deviceByRef.get_Name(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", Conversions.ToString(cAPIStatu.Value), "] [", cAPIStatu.Status, "] [", cAPIStatu.ImageFile, "]" }
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat([interface]), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
			If (logCAPI) Then
				HSPI_INSTEON_THERMOSTAT.utils.LogDeviceCAPI(dvRef)
			End If
			If (logRelationship) Then
				HSPI_INSTEON_THERMOSTAT.utils.LogRelationships(deviceByRef)
			End If
			If (logPED) Then
				HSPI_INSTEON_THERMOSTAT.utils.LogPluginExtraData(deviceByRef)
			End If
		End Sub

		Friend Shared Sub LogDeviceCAPI(ByVal dvRef As Long)
			Dim cAPIControlArray As CAPI.CAPIControl() = HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetControl(CInt(dvRef))
			For i As Integer = 0 To CInt(cAPIControlArray.Length)
				HSPI_INSTEON_THERMOSTAT.utils.LogDeviceCAPISingle(cAPIControlArray(i))
			Next

		End Sub

		Friend Shared Sub LogDeviceCAPISingle(ByVal objCAPIControl As CAPI.CAPIControl)
			Dim str() As String = { "		CAPI  [", Conversions.ToString(objCAPIControl.CCIndex), "] [", Conversions.ToString(objCAPIControl.ControlValue), "] [", Conversions.ToString(objCAPIControl.ControlLoc_Row), "] [", Conversions.ToString(objCAPIControl.ControlLoc_Column), "] [", Conversions.ToString(objCAPIControl.ControlLoc_ColumnSpan), "] [", Conversions.ToString(CInt(objCAPIControl.ControlType)), "] [", objCAPIControl.ControlString, "] [", objCAPIControl.Label, "]" }
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
		End Sub

		Friend Shared Sub LogDeviceHeader()
			HSPI_INSTEON_THERMOSTAT.utils.Log("[Interface] [REF] [Addr] [Code] [Loc] [Loc2] [Name] [CAPIStatusVlue] [CAPIStatusText] [CAPIImageFile]", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
		End Sub

		Friend Shared Sub LogDeviceList(Optional ByVal logCAPI As Boolean = True, Optional ByVal logRelationship As Boolean = True, Optional ByVal logPED As Boolean = True)
			Dim flag As Boolean = False
			Try
				HSPI_INSTEON_THERMOSTAT.utils.Log("------------------------------------------------------------------------------------", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				HSPI_INSTEON_THERMOSTAT.utils.Log("LOG DEVICES FOR Insteon Thermostat", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				HSPI_INSTEON_THERMOSTAT.utils.Log("------------------------------------------------------------------------------------", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(" Master Program Device Ref = ", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				HSPI_INSTEON_THERMOSTAT.utils.Log("------------------------------------------------------------------------------------", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				HSPI_INSTEON_THERMOSTAT.utils.LogDeviceHeader()
				If (logCAPI) Then
					HSPI_INSTEON_THERMOSTAT.utils.LogCAPIDeviceHeader()
				End If
				HSPI_INSTEON_THERMOSTAT.utils.Log("------------------------------------------------------------------------------------", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				Dim deviceEnumerator As clsDeviceEnumeration = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceEnumerator(), clsDeviceEnumeration)
				If (deviceEnumerator IsNot Nothing) Then
					Do
						Dim [next] As DeviceClass = deviceEnumerator.GetNext()
						If ([next] IsNot Nothing) Then
							If (Operators.CompareString([next].get_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs), "Insteon Thermostat", False) <> 0) Then
								Continue Do
							End If
							flag = True
							HSPI_INSTEON_THERMOSTAT.utils.LogDevice(CLng([next].get_Ref(Nothing)), logCAPI, logRelationship, logPED)
						End If
					Loop While Not deviceEnumerator.get_Finished()
					If (Not flag) Then
						HSPI_INSTEON_THERMOSTAT.utils.Log("No devices found!", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					End If
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log("Can't get list of devices from HomeSeer Enumerator!", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Exception getting list of devices!  Ex=", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Shared Sub LogPluginExtraData(ByRef dv As DeviceClass)
			Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = dv.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
			If (plugExtraDataGet Is Nothing) Then
				HSPI_INSTEON_THERMOSTAT.utils.Log("				No Plugin Extra Data (PED) for the device", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
			Else
				Dim namedKeys As String() = plugExtraDataGet.GetNamedKeys()
				Dim num As Integer = 0
				Do
					Dim str As String = namedKeys(num)
					Try
						HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(String.Concat(String.Concat("				PED NAMED: ", str), " = "), plugExtraDataGet.GetNamed(str))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("				PED NAMED: ", str, " = ??? "), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
						ProjectData.ClearProjectError()
					End Try
					num = num + 1
				Loop While num < CInt(namedKeys.Length)
				Dim num1 As Integer = plugExtraDataGet.UnNamedCount() - 1
				For i As Integer = 0 To num1
					Try
						HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(String.Concat(String.Concat("				PED UNNAMED: ", Conversions.ToString(i)), " = "), plugExtraDataGet.GetUnNamed(i))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Catch exception1 As System.Exception
						ProjectData.SetProjectError(exception1)
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("				PED UNNAMED: ", Conversions.ToString(i), " = ???"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
						ProjectData.ClearProjectError()
					End Try
				Next

			End If
		End Sub

		Friend Shared Sub LogRelationships(ByRef dv As DeviceClass)
			Dim num As Integer = 0
			Dim associatedDevices As Integer() = dv.get_AssociatedDevices(HSPI_INSTEON_THERMOSTAT.utils.hs)
			Dim num1 As Integer = 0
			Do
				Dim num2 As Integer = associatedDevices(num1)
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num2), DeviceClass)
				Dim name() As String = { "			RELATED: [", deviceByRef.get_Name(HSPI_INSTEON_THERMOSTAT.utils.hs), "] [", Conversions.ToString(deviceByRef.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs)), "] [", HSPI_INSTEON_THERMOSTAT.utils.RelationshipString(CInt(deviceByRef.get_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs))), "]" }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(name), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				num = num + 1
				num1 = num1 + 1
			Loop While num1 < CInt(associatedDevices.Length)
			If (num = 0) Then
				HSPI_INSTEON_THERMOSTAT.utils.Log("			No Relationships for the device", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
			End If
		End Sub

		Friend Shared Sub myHsWaitSecs(ByVal secs As Integer)
			Try
				HSPI_INSTEON_THERMOSTAT.utils.hs.WaitSecs(CDbl(secs))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Shared Sub myWaitSecs(ByVal secs As Integer)
			Try
				Thread.Sleep(secs * 1000)
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Shared Sub PEDAdd(ByRef PED As PlugExtraData.clsPlugExtraData, ByVal PEDName As String, ByVal PEDValue As Object)
			Dim numArray As Byte() = Nothing
			If (PED Is Nothing) Then
				PED = New PlugExtraData.clsPlugExtraData()
			End If
			HSPI_INSTEON_THERMOSTAT.utils.SerializeObject(PEDValue, numArray)
			If (Not PED.AddNamed(PEDName, numArray)) Then
				PED.RemoveNamed(PEDName)
				PED.AddNamed(PEDName, numArray)
			End If
		End Sub

		Public Shared Function PEDGet(ByRef PED As PlugExtraData.clsPlugExtraData, ByVal PEDName As String) As Object
			Dim objectValue As Object = RuntimeHelpers.GetObjectValue(New Object())
			Dim named As Byte() = DirectCast(PED.GetNamed(PEDName), Byte())
			If (named Is Nothing) Then
				Return Nothing
			End If
			HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(named, objectValue)
			Return objectValue
		End Function

		Public Shared Sub RegisterWebPage(ByVal link As String, Optional ByVal linktext As String = "", Optional ByVal page_title As String = "", Optional ByVal isConfig As Boolean = False)
			Try
				HSPI_INSTEON_THERMOSTAT.utils.hs.RegisterPage(link, "Insteon Thermostat", HSPI_INSTEON_THERMOSTAT.utils.Instance)
				If (Operators.CompareString(linktext, "", False) = 0) Then
					linktext = link
				End If
				linktext = linktext.Replace("_", " ")
				If (Operators.CompareString(page_title, "", False) = 0) Then
					page_title = linktext
				End If
				Dim webPageDesc As HomeSeerAPI.WebPageDesc = New HomeSeerAPI.WebPageDesc() With
				{
					.plugInName = "Insteon Thermostat",
					.link = link,
					.linktext = String.Concat(linktext, HSPI_INSTEON_THERMOSTAT.utils.Instance),
					.page_title = String.Concat(page_title, HSPI_INSTEON_THERMOSTAT.utils.Instance)
				}
				HSPI_INSTEON_THERMOSTAT.utils.callback.RegisterLink(webPageDesc)
				If (isConfig) Then
					HSPI_INSTEON_THERMOSTAT.utils.callback.RegisterConfigLink(webPageDesc)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("RegisterWebPage [", link, "] - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Shared Function RelationshipString(ByVal relType As Integer) As String
			Dim str As String = Nothing
			Select Case relType
				Case 0
					str = "NOT_SET"
					Exit Select
				Case 1
					str = "INDETERMINATE"
					Exit Select
				Case 2
					str = "PARENT_ROOT"
					Exit Select
				Case 3
					str = "STANDALONE"
					Exit Select
				Case 4
					str = "CHILD"
					Exit Select
				Case Else
					str = "UNKNOWN"
					Exit Select
			End Select
			Return str
		End Function

		Public Shared Function SerializeObject(ByRef ObjIn As Object, ByRef bteOut As Byte()) As Boolean
			Dim flag As Boolean
			If (ObjIn Is Nothing) Then
				Return False
			End If
			Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream()
			Dim binaryFormatter As System.Runtime.Serialization.Formatters.Binary.BinaryFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
			Try
				binaryFormatter.Serialize(memoryStream, RuntimeHelpers.GetObjectValue(ObjIn))
				bteOut = New Byte(CInt((memoryStream.Length - CLng(1))) + 1) {}
				bteOut = memoryStream.ToArray()
				flag = True
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Insteon Thermostat Error: Serializing object ", ObjIn.ToString(), " :", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				flag = False
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Public Shared Function TAInfoToString(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Object
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append("TRIG/ACT-INFO: ")
			Try
				stringBuilder.Append(String.Concat(" evRef=", Conversions.ToString(TrigInfo.evRef)))
				stringBuilder.Append(String.Concat(" UID=", Conversions.ToString(TrigInfo.UID)))
				stringBuilder.Append(String.Concat(" TANumber=", Conversions.ToString(TrigInfo.TANumber)))
				stringBuilder.Append(String.Concat(" SubTANumber=", Conversions.ToString(TrigInfo.SubTANumber)))
				stringBuilder.Append(Operators.ConcatenateObject(" DataIn=", Interaction.IIf(TrigInfo.DataIn IsNot Nothing, True, False)))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				stringBuilder.Append(String.Concat(" EX! ", exception.Message))
				ProjectData.ClearProjectError()
			End Try
			Return stringBuilder.ToString()
		End Function

		Public Enum ActNum
			ChangeMode = 1
			ChangeFan = 2
			ChangeHeatSetPoint = 3
			ChangeCoolSetPoint = 4
			ChangeHoldRun = 5
			SetProgram = 6
			PollThermostat = 7
		End Enum

		Public Enum FanOpt
			FanDontSet = -1
			FanAuto = 0
			FanOn = 1
			FanToggle = 2
		End Enum

		Public Enum HoldOpt
			HoldDontSet = -1
			HoldRun = 0
			HoldHold = 1
			HoldToggle = 2
		End Enum

		Public Enum LogLevel
			Always = 0
			Errors = 1
			Info = 2
			Debug = 64
			Test = 128
		End Enum

		Public Enum ModeOpt
			ModeDontSet = -1
			ModeOff = 0
			ModeHeat = 1
			ModeCool = 2
			ModeAuto = 3
			ModeProg = 5
			ModeProgHeat = 6
			ModeProgCool = 7
		End Enum

		Public Enum SubTrigNum
			ProgramChange = 1
		End Enum

		Public Enum TrigNum
			TriggersAndConditions = 1
			TriggersOnly = 2
		End Enum
	End Class
End Namespace