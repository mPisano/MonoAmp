Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Text
Imports System.Web

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class web_config
		Inherits PageBuilderAndMenu.clsPageBuilder
		Private timerUpdateTstats As Boolean

		Public Sub New(ByVal pagename As String)
			MyBase.New(pagename)
			Me.timerUpdateTstats = False
		End Sub

		Private Function BuildButton(ByVal Name As String, ByVal ButtonText As String, Optional ByVal SubmitForm As Boolean = False, Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_config::BuildButton(System.String,System.String,System.Boolean,System.Boolean)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String BuildButton(System.String,System.String,System.Boolean,System.Boolean)
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Private Function BuildCheckBox(ByVal Name As String, ByVal Label As String, ByVal Checked As Boolean, Optional ByVal AutoPostBack As Boolean = True, Optional ByVal SubmitForm As Boolean = False, Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_config::BuildCheckBox(System.String,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String BuildCheckBox(System.String,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean)
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Private Function BuildContent() As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_config::BuildContent()
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String BuildContent()
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Private Function BuildDropList(ByVal Name As String, ByVal items As String(), Optional ByVal SelectedValue As String = "", Optional ByVal HeaderItem As String = Nothing, Optional ByVal SubmitForm As Boolean = False, Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_config::BuildDropList(System.String,System.String[],System.String,System.String,System.Boolean,System.Boolean)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String BuildDropList(System.String,System.String[],System.String,System.String,System.Boolean,System.Boolean)
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Private Function BuildGeneralTable() As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append(" <table border='1'><tr style='vertical-align:top;'><td>")
			stringBuilder.Append(" <table border='0'>")
			stringBuilder.Append("  <tr><th class='tablecolumn'>General</th></tr>")
			stringBuilder.Append(String.Concat("  <tr><td>", Me.BuildCheckBox("LogDebugInfo", "Log Debug Information?", HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogDebugInfoChecked, True, False, False), "</td></tr>"))
			stringBuilder.Append(String.Concat("  <tr><td>Insteon Flags:", Me.BuildTextBox("GeneralInsteonFlags", HSPI_INSTEON_THERMOSTAT.utils.myConfig.InsteonFlags.ToString("X"), 5, False), "</td></tr>"))
			stringBuilder.Append(" </table>")
			stringBuilder.Append(" </td><td>")
			stringBuilder.Append(" <table border='0'>")
			stringBuilder.Append("  <tr><th class='tablecolumn' colspan='3'>Bounds</th></tr>")
			stringBuilder.Append("  <tr><td></td><td>Low</td><td>High</td></tr>")
			Dim strArrays() As String = { "  <tr><td>Temperature:</td><td>", Me.BuildTextBox("BoundsTempLow", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsTempLow), 5, False), "</td><td>", Me.BuildTextBox("BoundsTempHigh", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsTempHigh), 5, False), "</td></tr>" }
			stringBuilder.Append(String.Concat(strArrays))
			strArrays = New String() { "  <tr><td>Heat Setpoint:</td><td>", Me.BuildTextBox("BoundsHeatSetLow", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetLow), 5, False), "</td><td>", Me.BuildTextBox("BoundsHeatSetHigh", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetHigh), 5, False), "</td></tr>" }
			stringBuilder.Append(String.Concat(strArrays))
			strArrays = New String() { "  <tr><td>Cool Setpoint:</td><td>", Me.BuildTextBox("BoundsCoolSetLow", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetLow), 5, False), "</td><td>", Me.BuildTextBox("BoundsCoolSetHigh", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetHigh), 5, False), "</td></tr>" }
			stringBuilder.Append(String.Concat(strArrays))
			strArrays = New String() { "  <tr><td>Humidity:</td><td>", Me.BuildTextBox("BoundsHumidityLow", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHumidityLow), 5, False), "</td><td>", Me.BuildTextBox("BoundsHumidityHigh", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHumidityHigh), 5, False), "</td></tr>" }
			stringBuilder.Append(String.Concat(strArrays))
			stringBuilder.Append(" </table>")
			stringBuilder.Append(" </td><td>")
			stringBuilder.Append(" <table border='0'>")
			stringBuilder.Append("  <tr><th class='tablecolumn'>Control Page</th></tr>")
			stringBuilder.Append(String.Concat("  <tr><td>Auto-Refresh (seconds):", Me.BuildTextBox("ControlAutoRefresh", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.myConfig.ControlPageRefresh), 20, False), "</td></tr>"))
			stringBuilder.Append(" </table>")
			stringBuilder.Append(" </td><td>")
			stringBuilder.Append(" <table border='0'>")
			stringBuilder.Append("  <tr><th class='tablecolumn'>Informational</th></tr>")
			stringBuilder.Append("  <tr><td>Plugin Version=3.0.0.9</td></tr>")
			stringBuilder.Append(String.Concat("  <tr><td>Insteon PLM Address=", HSPI_INSTEON_THERMOSTAT.utils.myInsteon.plmAddress, "</td></tr>"))
			stringBuilder.Append(" </table>")
			stringBuilder.Append(" </td></tr></table>")
			Return stringBuilder.ToString()
		End Function

		Private Function BuildHVACsTable() As String
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append(" <table border='1'>")
			stringBuilder.Append("  <tr><th class='tablecolumn' colspan='4'>HVAC Units</th></tr>")
			stringBuilder.Append("  <tr><th>Name</th><th>Location</th><th>Maintenance Interval (Hours)</th><th>Actions</th></tr>")
			Dim num As Integer = 1
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim str As String = Conversions.ToString(current("Name"))
					Dim str1 As String = Conversions.ToString(current("Location"))
					Dim str2 As String = Conversions.ToString(current("MaintenanceInterval"))
					Dim str3 As String = String.Concat("HVAC_", Conversions.ToString(num))
					Dim strArrays() As String = { "  <tr><td>", str, " </td><td>", Me.BuildTextBox(String.Concat(str3, "_LOC"), str1, 15, False), " </td><td>", Me.BuildTextBox(String.Concat(str3, "_MxHours"), str2, 5, False), " </td><td><table border='0'><tr><td>", Me.BuildButton(String.Concat(str3, "_DELETE"), "Delete", False, False), "</td></tr></table></td></tr>" }
					stringBuilder.Append(String.Concat(strArrays))
					num = num + 1
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			stringBuilder.Append(String.Concat("<tr><td>", Me.BuildTextBox("ADDHVAC", "ADD NEW", 15, False), "</td><td colspan='3'> <- Enter a new name to add another HVAC.  Don't forget to SAVE!</td></tr>"))
			stringBuilder.Append(" </table>")
			Return stringBuilder.ToString()
		End Function

		Private Function BuildProgramsTable() As String
			Dim enumerator As IEnumerator = Nothing
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append(" <table border='1'>")
			stringBuilder.Append("  <tr><th class='tablecolumn' colspan='7'>Programs</th></tr>")
			stringBuilder.Append("  <tr><th>Program Name</th><th>Thermostat</th><th>Heat</th><th>Cool</th><th>Mode</th><th>Fan</th><th>Actions</th></tr>")
			Dim num As Integer = 0
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gPrograms.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = DirectCast(enumerator.Current, Collection)
					Dim str As String = Conversions.ToString(current("Name"))
					Dim str1 As String = Conversions.ToString(current("Thermostat"))
					Dim str2 As String = Conversions.ToString(current("Heat"))
					Dim str3 As String = Conversions.ToString(current("Cool"))
					Dim str4 As String = Conversions.ToString(current("Mode"))
					Dim str5 As String = Conversions.ToString(current("Fan"))
					Dim str6 As String = String.Concat("PROGRAM_", Conversions.ToString(num))
					Dim strArrays() As String = { "  <tr><td>", Me.BuildTextBox(String.Concat(str6, "_NAME"), str, 15, False), " </td><td>", Me.BuildDropList(String.Concat(str6, "_TSTAT"), HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Keys.ToArray(Of String)(), str1, "Default", False, False), " </td><td>", Me.BuildTextBox(String.Concat(str6, "_HEAT"), str2, 5, False), " </td><td>", Me.BuildTextBox(String.Concat(str6, "_COOL"), str3, 5, False), " </td><td>", Me.BuildDropList(String.Concat(str6, "_MODE"), HSPI_INSTEON_THERMOSTAT.utils.ModeOptsConfig, str4, Nothing, False, False), " </td><td>", Me.BuildDropList(String.Concat(str6, "_FAN"), HSPI_INSTEON_THERMOSTAT.utils.FanOptsConfig, str5, Nothing, False, False), " </td><td><table border='0'><tr><td>", Me.BuildButton(String.Concat(str6, "_DELETE"), "Delete", False, False), "</td></tr></table></td></tr>" }
					stringBuilder.Append(String.Concat(strArrays))
					num = num + 1
				End While
			Finally
				If (TypeOf enumerator Is IDisposable) Then
					(TryCast(enumerator, IDisposable)).Dispose()
				End If
			End Try
			stringBuilder.Append(String.Concat("<tr><td>", Me.BuildTextBox("ADDPROGRAM", "ADD NEW", 15, False), "</td><td colspan='6'> <- Enter a new name to add another program.  Don't forget to SAVE!</td></tr>"))
			stringBuilder.Append(" </table>")
			Return stringBuilder.ToString()
		End Function

		Private Function BuildTextBox(ByVal TextBoxId As String, Optional ByVal TextBoxDefault As String = "", Optional ByVal TextBoxSize As Integer = 20, Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_config::BuildTextBox(System.String,System.String,System.Int32,System.Boolean)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String BuildTextBox(System.String,System.String,System.Int32,System.Boolean)
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Private Function BuildThermostatsTable() As String
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append(" <table border='1'>")
			stringBuilder.Append("  <tr><th class='tablecolumn' colspan='10'>Thermostats</th></tr>")
			stringBuilder.Append("  <tr><th>Name</th><th>Location</th><th>Insteon Address</th><th>Has<br/>Humidistat?</th><th>Has<br/>External</br>Sensor?</th><th>Force<br/>Protocol</br>= 2 ?</th><th>HVAC Unit</th><th colspan='4'>Actions</th></tr>")
			Dim num As Integer = 1
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim str As String = Conversions.ToString(current("Name"))
					Dim str1 As String = Conversions.ToString(current("Location"))
					Dim str2 As String = Conversions.ToString(current("InsteonAddressLeft"))
					Dim str3 As String = Conversions.ToString(current("InsteonAddressMid"))
					Dim str4 As String = Conversions.ToString(current("InsteonAddressRight"))
					Dim flag As Boolean = Conversions.ToBoolean(current("Humidistat"))
					Dim flag1 As Boolean = Conversions.ToBoolean(current("ExtSensor"))
					Dim flag2 As Boolean = Conversions.ToBoolean(current("InsteonProtocol2"))
					Dim str5 As String = Conversions.ToString(current("HVACUnit"))
					Dim str6 As String = "???"
					Try
						str6 = Conversions.ToString(current("InsteonDeviceCategory"))
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						ProjectData.ClearProjectError()
					End Try
					Dim [integer] As Integer = -1
					Try
						[integer] = Conversions.ToInteger(current("InsteonFirmware"))
					Catch exception1 As System.Exception
						ProjectData.SetProjectError(exception1)
						ProjectData.ClearProjectError()
					End Try
					Dim integer1 As Integer = -1
					Dim str7 As String = ""
					Try
						integer1 = Conversions.ToInteger(current("InsteonProtocol"))
						str7 = If(Not flag2, Conversions.ToString(integer1), String.Concat("<span style='color:red'>", Conversions.ToString(integer1), "</span>"))
					Catch exception2 As System.Exception
						ProjectData.SetProjectError(exception2)
						ProjectData.ClearProjectError()
					End Try
					Dim str8 As String = String.Concat("TSTAT_", Conversions.ToString(num))
					Dim strArrays() As String = { "  <tr><td><table border='0'><tr><td>", str, "</td></tr><tr><td>[", str6, ",", [integer].ToString("X"), ",", str7, "]</td></tr></table>  </td> </td><td>", Me.BuildTextBox(String.Concat(str8, "_LOC"), str1, 15, False), " </td><td><table border='0'><tr><td>", Me.BuildTextBox(String.Concat(str8, "_AddrL"), str2, 5, False), " </td><td>", Me.BuildTextBox(String.Concat(str8, "_AddrM"), str3, 5, False), " </td><td>", Me.BuildTextBox(String.Concat(str8, "_AddrR"), str4, 5, False), " </td></tr></table></td><td>", Me.BuildCheckBox(String.Concat(str8, "_Humidistat"), "", flag, True, False, False), " </td><td>", Me.BuildCheckBox(String.Concat(str8, "_ExtSensor"), "", flag1, True, False, False), " </td><td>", Me.BuildCheckBox(String.Concat(str8, "_ForceProtocol2"), "", flag2, True, False, False), " </td><td>", Me.BuildDropList(String.Concat(str8, "_HVAC"), HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs.Keys.ToArray(Of String)(), str5, "None", False, False), " </td>" }
					stringBuilder.Append(String.Concat(strArrays))
					stringBuilder.Append(String.Concat(" <td><table border='0'><tr><td>", Me.BuildButton(String.Concat(str8, "_DELETE"), "Delete", False, False)))
					If (HSPI_INSTEON_THERMOSTAT.utils.myInsteon.isTstatRegistered(current)) Then
						stringBuilder.Append(String.Concat(" </td><td>", Me.BuildButton(String.Concat(str8, "_READLINKS"), "Read Links", False, False), " </td><td>", Me.BuildButton(String.Concat(str8, "_UPDATELINKS"), "Update Links", False, False)))
					Else
						stringBuilder.Append(String.Concat(" </td><td>", Me.BuildButton(String.Concat(str8, "_REGISTER"), "Register Thermostat with Insteon Plugin", False, False)))
					End If
					stringBuilder.Append("</td></tr></table></td></tr>")
					num = num + 1
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			stringBuilder.Append(String.Concat("<tr><td>", Me.BuildTextBox("ADDTSTAT", "ADD NEW", 15, False), "</td><td colspan='6'> <- Enter a new name to add another thermostat.  Don't forget to SAVE!</td></tr>"))
			stringBuilder.Append(" </table>")
			Return stringBuilder.ToString()
		End Function

		Private Sub doSave()
			HSPI_INSTEON_THERMOSTAT.utils.myConfig.writeINI()
			HSPI_INSTEON_THERMOSTAT.utils.myConfig.DeletePendingDevices()
			HSPI_INSTEON_THERMOSTAT.utils.myTstat.InitDevices()
		End Sub

		Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String
			Dim str As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim str1 As String = ""
			Try
				Me.reset()
				HSPI_INSTEON_THERMOSTAT.utils.CurrentPage = Me
				Dim nameValueCollection As System.Collections.Specialized.NameValueCollection = Nothing
				If (Operators.CompareString(queryString, "", False) <> 0) Then
					nameValueCollection = HttpUtility.ParseQueryString(queryString)
				End If
				If (Operators.CompareString(HSPI_INSTEON_THERMOSTAT.utils.Instance, "", False) <> 0) Then
					str1 = String.Concat(" - ", HSPI_INSTEON_THERMOSTAT.utils.Instance)
				End If
				stringBuilder.Append(HSPI_INSTEON_THERMOSTAT.utils.hs.GetPageHeader(pageName, String.Concat(pageName.Replace("_", " "), str1), "", "", False, False, False, False, False))
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivStart("pluginpage", ""))
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivStart("errormessage", "class='errormessage'"))
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivEnd())
				Me.set_RefreshIntervalMilliSeconds(12000)
				stringBuilder.Append(Me.AddAjaxHandlerPost("id=timer", pageName))
				If (userRights <= 1) Then
					stringBuilder.Append("<p><b>Guests are not authorized to access this feature</b></p>")
				Else
					stringBuilder.Append(Me.BuildContent())
				End If
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivEnd())
				Me.AddBody(stringBuilder.ToString())
				Me.AddFooter(HSPI_INSTEON_THERMOSTAT.utils.hs.GetPageFooter(False))
				Me.suppressDefaultFooter = True
				str = Me.BuildPage()
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Dim str2 As String = String.Concat("WCGPP: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str2, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str2
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Overrides Function postBackProc(ByVal page As String, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As String
			Dim str As String
			Dim [integer] As Integer
			Dim num As Integer
			Dim integer1 As Integer
			Dim chrArray As Char()
			Dim tstatByNum As Collection
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim enumerator1 As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim flag As Boolean = False
			Dim flag1 As Boolean = False
			Try
				If (userRights <= 1) Then
					Throw New System.Exception("Guests are not authorized to access this feature")
				End If
				Dim nameValueCollection As System.Collections.Specialized.NameValueCollection = HttpUtility.ParseQueryString(data)
				Dim item As String = nameValueCollection("id")
				If (Operators.CompareString(item, "timer", False) = 0) Then
					Me.pageCommands.Add("stoptimer", "")
					If (Me.timerUpdateTstats) Then
						Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
						Me.divToUpdate.Add("errormessage", "Complete!")
						Me.timerUpdateTstats = False
					End If
				ElseIf (item.StartsWith("wcButton_TSTAT")) Then
					chrArray = New Char() { Strings.ChrW(95) }
					Dim num1 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str1 As String = item.Split(chrArray)(3)
					If (Operators.CompareString(str1, "DELETE", False) = 0) Then
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.RemoveTstatByNum(num1)
						Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
						Me.divToUpdate.Add("config_programs", Me.BuildProgramsTable())
					ElseIf (Operators.CompareString(str1, "REGISTER", False) = 0) Then
						Me.doSave()
						HSPI_INSTEON_THERMOSTAT.utils.myInsteon.RegisterTstats()
						Me.timerUpdateTstats = True
						Me.pageCommands.Add("starttimer", "")
						Me.divToUpdate.Add("errormessage", "Registering Thermostats, please wait for config page to refresh...")
					ElseIf (Operators.CompareString(str1, "READLINKS", False) <> 0) Then
						If (Operators.CompareString(str1, "UPDATELINKS", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						Me.doSave()
						Dim insteon As HSPI_INSTEON_THERMOSTAT.Insteon = HSPI_INSTEON_THERMOSTAT.utils.myInsteon
						tstatByNum = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(num1)
						insteon.writeLinks(tstatByNum)
						Me.timerUpdateTstats = True
						Me.pageCommands.Add("starttimer", "")
						Me.divToUpdate.Add("errormessage", "Updating Thermostat Links, please wait for config page to refresh...")
					Else
						Me.doSave()
						Dim insteon1 As HSPI_INSTEON_THERMOSTAT.Insteon = HSPI_INSTEON_THERMOSTAT.utils.myInsteon
						tstatByNum = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(num1)
						insteon1.ReadLinks(tstatByNum)
						Me.timerUpdateTstats = True
						Me.pageCommands.Add("starttimer", "")
						Me.divToUpdate.Add("errormessage", "Reading Thermostat Links, please wait for config page to refresh...")
					End If
				ElseIf (item.StartsWith("wcTextBox_TSTAT")) Then
					Dim str2 As String = item.Substring(item.IndexOf("_") + 1)
					chrArray = New Char() { Strings.ChrW(95) }
					Dim integer2 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str3 As String = item.Split(chrArray)(3)
					If (Operators.CompareString(str3, "LOC", False) = 0) Then
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(integer2, "Location", nameValueCollection(str2))
					ElseIf (Operators.CompareString(str3, "AddrL", False) = 0) Then
						Dim item1 As String = nameValueCollection(str2)
						If (Not HSPI_INSTEON_THERMOSTAT.utils.IsHex(item1)) Then
							Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
							Throw New System.Exception(String.Concat("Invalid Insteon address entered: ", item1))
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(integer2, "InsteonAddressLeft", item1)
					ElseIf (Operators.CompareString(str3, "AddrM", False) <> 0) Then
						If (Operators.CompareString(str3, "AddrR", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						Dim item2 As String = nameValueCollection(str2)
						If (Not HSPI_INSTEON_THERMOSTAT.utils.IsHex(item2)) Then
							Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
							Throw New System.Exception(String.Concat("Invalid Insteon address entered: ", item2))
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(integer2, "InsteonAddressRight", item2)
					Else
						Dim item3 As String = nameValueCollection(str2)
						If (Not HSPI_INSTEON_THERMOSTAT.utils.IsHex(item3)) Then
							Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
							Throw New System.Exception(String.Concat("Invalid Insteon address entered: ", item3))
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(integer2, "InsteonAddressMid", item3)
					End If
				ElseIf (item.StartsWith("wcCheckBox_TSTAT")) Then
					Dim str4 As String = item.Substring(item.IndexOf("_") + 1)
					chrArray = New Char() { Strings.ChrW(95) }
					Dim num2 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str5 As String = item.Split(chrArray)(3)
					If (Operators.CompareString(str5, "Humidistat", False) = 0) Then
						If (Operators.CompareString(nameValueCollection(str4), "unchecked", False) <> 0) Then
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "Humidistat", Conversions.ToString(True))
						Else
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "Humidistat", Conversions.ToString(False))
						End If
					ElseIf (Operators.CompareString(str5, "ExtSensor", False) <> 0) Then
						If (Operators.CompareString(str5, "ForceProtocol2", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						If (Operators.CompareString(nameValueCollection(str4), "unchecked", False) <> 0) Then
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "InsteonProtocol2", Conversions.ToString(True))
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "InsteonProtocol", Conversions.ToString(2))
							Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
						Else
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "InsteonProtocol2", Conversions.ToString(False))
							HSPI_INSTEON_THERMOSTAT.utils.myInsteon.GetProtocol(HSPI_INSTEON_THERMOSTAT.utils.myConfig.NameByNum(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats, num2))
							Me.timerUpdateTstats = True
							Me.pageCommands.Add("starttimer", "")
						End If
					ElseIf (Operators.CompareString(nameValueCollection(str4), "unchecked", False) <> 0) Then
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "ExtSensor", Conversions.ToString(True))
					Else
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(num2, "ExtSensor", Conversions.ToString(False))
					End If
				ElseIf (item.StartsWith("wcDropList_TSTAT")) Then
					Dim str6 As String = item.Substring(item.IndexOf("_") + 1)
					chrArray = New Char() { Strings.ChrW(95) }
					Dim integer3 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str7 As String = item.Split(chrArray)(3)
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatByNum(integer3, "HVACUnit", nameValueCollection(str6))
				ElseIf (item.StartsWith("wcButton_PROGRAM")) Then
					chrArray = New Char() { Strings.ChrW(95) }
					Dim num3 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					If (Operators.CompareString(item.Split(chrArray)(3), "DELETE", False) <> 0) Then
						Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
					End If
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.RemoveProgramByNum(num3)
					Me.divToUpdate.Add("config_programs", Me.BuildProgramsTable())
				ElseIf (item.StartsWith("wcTextBox_PROGRAM")) Then
					Dim str8 As String = item.Substring(item.IndexOf("_") + 1)
					chrArray = New Char() { Strings.ChrW(95) }
					Dim integer4 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str9 As String = item.Split(chrArray)(3)
					If (Operators.CompareString(str9, "NAME", False) = 0) Then
						If (String.IsNullOrEmpty(nameValueCollection(str8)) OrElse String.IsNullOrEmpty(nameValueCollection(str8).Trim())) Then
							Me.divToUpdate.Add("config_programs", Me.BuildProgramsTable())
							Throw New System.Exception("Invalid program name.  Can't use an empty string for the program name.")
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateProgramByNum(integer4, "Name", nameValueCollection(str8).Trim())
					ElseIf (Operators.CompareString(str9, "HEAT", False) <> 0) Then
						If (Operators.CompareString(str9, "COOL", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						Try
							num = Conversions.ToInteger(nameValueCollection(str8))
						Catch exception As System.Exception
							ProjectData.SetProjectError(exception)
							Throw New System.Exception(String.Concat("Invalid program cool setting.  Must be integer numeric format. ", exception.Message))
						End Try
						If (num < HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetLow) Then
							Throw New System.Exception("Invalid program cool setting.  Trying to set cool below low bounds")
						End If
						If (num > HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetHigh) Then
							Throw New System.Exception("Invalid program cool setting.  Trying to set cool above high bounds")
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateProgramByNum(integer4, "Cool", Conversions.ToString(num))
					Else
						Try
							[integer] = Conversions.ToInteger(nameValueCollection(str8))
						Catch exception1 As System.Exception
							ProjectData.SetProjectError(exception1)
							Throw New System.Exception(String.Concat("Invalid program heat setting.  Must be integer numeric format. ", exception1.Message))
						End Try
						If ([integer] < HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetLow) Then
							Throw New System.Exception("Invalid program heat setting.  Trying to set heat below low bounds")
						End If
						If ([integer] > HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetHigh) Then
							Throw New System.Exception("Invalid program heat setting.  Trying to set heat above high bounds")
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateProgramByNum(integer4, "Heat", Conversions.ToString([integer]))
					End If
				ElseIf (item.StartsWith("wcDropList_PROGRAM")) Then
					Dim str10 As String = item.Substring(item.IndexOf("_") + 1)
					chrArray = New Char() { Strings.ChrW(95) }
					Dim num4 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str11 As String = item.Split(chrArray)(3)
					If (Operators.CompareString(str11, "TSTAT", False) = 0) Then
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateProgramByNum(num4, "Thermostat", nameValueCollection(str10))
					ElseIf (Operators.CompareString(str11, "MODE", False) <> 0) Then
						If (Operators.CompareString(str11, "FAN", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateProgramByNum(num4, "Fan", nameValueCollection(str10))
					Else
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateProgramByNum(num4, "Mode", nameValueCollection(str10))
					End If
				ElseIf (item.StartsWith("wcButton_HVAC")) Then
					chrArray = New Char() { Strings.ChrW(95) }
					Dim integer5 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					If (Operators.CompareString(item.Split(chrArray)(3), "DELETE", False) <> 0) Then
						Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
					End If
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.RemoveHvacByNum(integer5)
					Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
					Me.divToUpdate.Add("config_hvac", Me.BuildHVACsTable())
				ElseIf (Not item.StartsWith("wcTextBox_HVAC")) Then
					Dim str12 As String = item
					If (Operators.CompareString(str12, "wcCheckBox_LogDebugInfo", False) = 0) Then
						If (Operators.CompareString(nameValueCollection("LogDebugInfo"), "unchecked", False) <> 0) Then
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogDebugInfoChecked = True
						Else
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogDebugInfoChecked = False
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_GeneralInsteonFlags", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("GeneralInsteonFlags")) Or String.IsNullOrEmpty(nameValueCollection("GeneralInsteonFlags").Trim()))) Then
							If (Not HSPI_INSTEON_THERMOSTAT.utils.IsHex(nameValueCollection("GeneralInsteonFlags").Trim())) Then
								Throw New System.Exception("Invalid Insteon Flags.  Must be HEX numeric format (0-F)")
							End If
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.InsteonFlags = Conversions.ToByte(nameValueCollection("GeneralInsteonFlags").Trim())
							Catch exception2 As System.Exception
								ProjectData.SetProjectError(exception2)
								Throw New System.Exception(String.Concat("Invalid Insteon Flags.  Must be HEX numeric format (0-F)", exception2.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsTempLow", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsTempLow")) Or String.IsNullOrEmpty(nameValueCollection("BoundsTempLow").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsTempLow = Conversions.ToInteger(nameValueCollection("BoundsTempLow"))
							Catch exception3 As System.Exception
								ProjectData.SetProjectError(exception3)
								Throw New System.Exception(String.Concat("Invalid Low Temp.  Must be integer numeric format. ", exception3.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsTempHigh", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsTempHigh")) Or String.IsNullOrEmpty(nameValueCollection("BoundsTempHigh").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsTempHigh = Conversions.ToInteger(nameValueCollection("BoundsTempHigh"))
							Catch exception4 As System.Exception
								ProjectData.SetProjectError(exception4)
								Throw New System.Exception(String.Concat("Invalid High Temp.  Must be integer numeric format. ", exception4.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsHeatSetLow", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsHeatSetLow")) Or String.IsNullOrEmpty(nameValueCollection("BoundsHeatSetLow").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetLow = Conversions.ToInteger(nameValueCollection("BoundsHeatSetLow"))
							Catch exception5 As System.Exception
								ProjectData.SetProjectError(exception5)
								Throw New System.Exception(String.Concat("Invalid Low Heat Setpoint.  Must be integer numeric format. ", exception5.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsHeatSetHigh", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsHeatSetHigh")) Or String.IsNullOrEmpty(nameValueCollection("BoundsHeatSetHigh").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetHigh = Conversions.ToInteger(nameValueCollection("BoundsHeatSetHigh"))
							Catch exception6 As System.Exception
								ProjectData.SetProjectError(exception6)
								Throw New System.Exception(String.Concat("Invalid High Heat Setpoint.  Must be integer numeric format. ", exception6.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsCoolSetLow", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsCoolSetLow")) Or String.IsNullOrEmpty(nameValueCollection("BoundsCoolSetLow").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetLow = Conversions.ToInteger(nameValueCollection("BoundsCoolSetLow"))
							Catch exception7 As System.Exception
								ProjectData.SetProjectError(exception7)
								Throw New System.Exception(String.Concat("Invalid Low Cool Setpoint.  Must be integer numeric format. ", exception7.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsCoolSetHigh", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsCoolSetHigh")) Or String.IsNullOrEmpty(nameValueCollection("BoundsCoolSetHigh").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetHigh = Conversions.ToInteger(nameValueCollection("BoundsCoolSetHigh"))
							Catch exception8 As System.Exception
								ProjectData.SetProjectError(exception8)
								Throw New System.Exception(String.Concat("Invalid High Cool Setpoint.  Must be integer numeric format. ", exception8.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsHumidityLow", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsHumidityLow")) Or String.IsNullOrEmpty(nameValueCollection("BoundsHumidityLow").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHumidityLow = Conversions.ToInteger(nameValueCollection("BoundsHumidityLow"))
							Catch exception9 As System.Exception
								ProjectData.SetProjectError(exception9)
								Throw New System.Exception(String.Concat("Invalid Low Humidity.  Must be integer numeric format. ", exception9.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_BoundsHumidityHigh", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("BoundsHumidityHigh")) Or String.IsNullOrEmpty(nameValueCollection("BoundsHumidityHigh").Trim()))) Then
							Try
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHumidityHigh = Conversions.ToInteger(nameValueCollection("BoundsHumidityHigh"))
							Catch exception10 As System.Exception
								ProjectData.SetProjectError(exception10)
								Throw New System.Exception(String.Concat("Invalid High Humidity.  Must be integer numeric format. ", exception10.Message))
							End Try
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_ControlAutoRefresh", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("ControlAutoRefresh")) Or String.IsNullOrEmpty(nameValueCollection("ControlAutoRefresh").Trim()))) Then
							Try
								integer1 = Conversions.ToInteger(nameValueCollection("ControlAutoRefresh"))
							Catch exception11 As System.Exception
								ProjectData.SetProjectError(exception11)
								Throw New System.Exception(String.Concat("Invalid Control Page Refresh.  Must be integer numeric format and >= 5. ", exception11.Message))
							End Try
							If (integer1 < 5) Then
								Throw New System.Exception("Invalid Control Page Refresh.  Must be >= 5. ")
							End If
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.ControlPageRefresh = integer1
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_ADDTSTAT", False) = 0) Then
						Try
							enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
							While enumerator.MoveNext()
								Dim current As Collection = enumerator.Current
								If (Not Operators.ConditionalCompareObjectEqual(current("Name"), nameValueCollection("ADDTSTAT"), False)) Then
									Continue While
								End If
								str = Nothing
								flag = True
								If (flag) Then
									Exit While
								End If
							End While
						Finally
							(DirectCast(enumerator, IDisposable)).Dispose()
						End Try
						If (Not flag) Then
							If (Not (String.IsNullOrEmpty(nameValueCollection("ADDTSTAT")) Or String.IsNullOrEmpty(nameValueCollection("ADDTSTAT").Trim()))) Then
								Dim collections As Collection = New Collection()
								collections.Add(nameValueCollection("ADDTSTAT").Trim(), "Name", Nothing, Nothing)
								collections.Add("Thermostats", "Location", Nothing, Nothing)
								collections.Add("", "InsteonAddressLeft", Nothing, Nothing)
								collections.Add("", "InsteonAddressMid", Nothing, Nothing)
								collections.Add("", "InsteonAddressRight", Nothing, Nothing)
								collections.Add(False, "Humidistat", Nothing, Nothing)
								collections.Add(False, "ExtSensor", Nothing, Nothing)
								collections.Add(False, "InsteonProtocol2", Nothing, Nothing)
								collections.Add(False, "Registered", Nothing, Nothing)
								collections.Add("", "HVACUnit", Nothing, Nothing)
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.AddTstat(collections)
								Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
								Me.divToUpdate.Add("config_programs", Me.BuildProgramsTable())
							Else
								str = Nothing
								flag = True
							End If
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_ADDPROGRAM", False) = 0) Then
						If (Not (String.IsNullOrEmpty(nameValueCollection("ADDPROGRAM")) Or String.IsNullOrEmpty(nameValueCollection("ADDPROGRAM").Trim()))) Then
							Dim collections1 As Collection = New Collection()
							collections1.Add(nameValueCollection("ADDPROGRAM").Trim(), "Name", Nothing, Nothing)
							collections1.Add("Default", "Thermostat", Nothing, Nothing)
							collections1.Add("", "Heat", Nothing, Nothing)
							collections1.Add("", "Cool", Nothing, Nothing)
							collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(-1), "Mode", Nothing, Nothing)
							collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(-1), "Fan", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.myConfig.AddProgram(collections1)
							Me.divToUpdate.Add("config_programs", Me.BuildProgramsTable())
						Else
							str = Nothing
							flag = True
						End If
					ElseIf (Operators.CompareString(str12, "wcTextBox_ADDHVAC", False) <> 0) Then
						If (Operators.CompareString(str12, "wcButton_Save", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						Me.doSave()
					Else
						Try
							enumerator1 = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs.Values.GetEnumerator()
							While enumerator1.MoveNext()
								Dim current1 As Collection = enumerator1.Current
								If (Not Operators.ConditionalCompareObjectEqual(current1("Name"), nameValueCollection("ADDHVAC"), False)) Then
									Continue While
								End If
								str = Nothing
								flag = True
								If (flag) Then
									Exit While
								End If
							End While
						Finally
							(DirectCast(enumerator1, IDisposable)).Dispose()
						End Try
						If (Not flag) Then
							If (Not (String.IsNullOrEmpty(nameValueCollection("ADDHVAC")) Or String.IsNullOrEmpty(nameValueCollection("ADDHVAC").Trim()))) Then
								Dim collections2 As Collection = New Collection()
								collections2.Add(nameValueCollection("ADDHVAC").Trim(), "Name", Nothing, Nothing)
								collections2.Add("Thermostats", "Location", Nothing, Nothing)
								collections2.Add("0", "MaintenanceInterval", Nothing, Nothing)
								HSPI_INSTEON_THERMOSTAT.utils.myConfig.AddHvac(collections2)
								Me.divToUpdate.Add("config_thermostats", Me.BuildThermostatsTable())
								Me.divToUpdate.Add("config_hvac", Me.BuildHVACsTable())
							Else
								str = Nothing
								flag = True
							End If
						End If
					End If
				Else
					Dim str13 As String = item.Substring(item.IndexOf("_") + 1)
					chrArray = New Char() { Strings.ChrW(95) }
					Dim num5 As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
					chrArray = New Char() { Strings.ChrW(95) }
					Dim str14 As String = item.Split(chrArray)(3)
					If (Operators.CompareString(str14, "LOC", False) <> 0) Then
						If (Operators.CompareString(str14, "MxHours", False) <> 0) Then
							Throw New System.Exception(String.Concat("Unknown value submitted from web config page! ", nameValueCollection("id")))
						End If
						Dim integer6 As Integer = -1
						Try
							integer6 = Conversions.ToInteger(nameValueCollection(str13))
						Catch exception12 As System.Exception
							ProjectData.SetProjectError(exception12)
							Throw New System.Exception(String.Concat("Invalid maintenance hours.  Must be integer numeric format. ", exception12.Message))
						End Try
						If (integer6 < 0) Then
							Throw New System.Exception("Invalid maintenance hours.  Must be >= 0")
						End If
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateHvacByNum(num5, "MaintenanceInterval", Conversions.ToString(integer6))
						Dim hvacByNum As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetHvacByNum(num5)
						If (hvacByNum.Contains("HSREF_MAINT")) Then
							HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByHvac(hvacByNum, "HSREF_MAINT", "MaintenanceInterval", 0, True)
						End If
					Else
						HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateHvacByNum(num5, "Location", nameValueCollection(str13))
					End If
				End If
				If (Not flag) Then
					If (Not flag) Then
						If (Not flag) Then
							If (Not flag) Then
								If (Not flag) Then
									If (Not flag) Then
										If (Not flag) Then
											If (Not flag) Then
												If (Not flag) Then
													If (Not flag) Then
														If (Not flag) Then
															If (Not flag) Then
																If (Not flag) Then
																	If (Not flag) Then
																		If (Not flag) Then
																			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
																			flag1 = True
																		End If
																	End If
																End If
															End If
														End If
													End If
												End If
											End If
										End If
									End If
								End If
							End If
						End If
					End If
				End If
			Catch exception14 As System.Exception
				ProjectData.SetProjectError(exception14)
				Dim exception13 As System.Exception = exception14
				Me.divToUpdate.Add("errormessage", exception13.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(exception13.Message, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
				flag1 = True
			End Try
			If (flag OrElse Not flag1) Then
				flag = False
				Return str
			End If
			flag1 = False
			Return MyBase.postBackProc(page, data, user, userRights)
		End Function
	End Class
End Namespace