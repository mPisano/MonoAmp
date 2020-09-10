Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Text
Imports System.Web

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class web_status
		Inherits PageBuilderAndMenu.clsPageBuilder
		Private ExternalRelayNumber As String

		Public Sub New(ByVal pagename As String)
			MyBase.New(pagename)
			Me.ExternalRelayNumber = ""
		End Sub

		Private Function BuildButton(ByVal Name As String, ByVal ButtonText As String, Optional ByVal SubmitForm As Boolean = False, Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_status::BuildButton(System.String,System.String,System.Boolean,System.Boolean)
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

		Private Function BuildContent() As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_status::BuildContent()
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
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_status::BuildDropList(System.String,System.String[],System.String,System.String,System.Boolean,System.Boolean)
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

		Private Function BuildTextBox(ByVal TextBoxId As String, Optional ByVal TextBoxDefault As String = "", Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_status::BuildTextBox(System.String,System.String,System.Boolean)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String BuildTextBox(System.String,System.String,System.Boolean)
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

		Private Function BuildTextDiv(ByVal TextId As String, ByVal TextValue As String, Optional ByVal Rebuilding As Boolean = False) As String
			Dim str As String = Nothing
			If (Not Rebuilding) Then
				Dim strArrays() As String = { "<div id='", TextId, "_div'>", TextValue, "</div>" }
				str = String.Concat(strArrays)
			Else
				Me.divToUpdate.Add(String.Concat(TextId, "_div"), TextValue)
			End If
			Return str
		End Function

		Private Function BuildTstatHvac(ByVal tstat As Collection, ByVal idx As Integer) As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim str As String = String.Concat("TSTAT_", Conversions.ToString(idx))
			Dim cAPIStatu As CAPI.CAPIStatus = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetStatus(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), CAPI.CAPIStatus)
			Dim str1 As String = Conversions.ToString(tstat("Name"))
			Dim deviceStatus As String = Me.getDeviceStatus(tstat, "HSREF_TEMP", True, "")
			Dim deviceStatus1 As String = Me.getDeviceStatus(tstat, "HSREF_EXTTEMP", True, "N/A")
			Dim deviceStatus2 As String = Me.getDeviceStatus(tstat, "HSREF_HUMIDITY", True, "N/A")
			Dim str2 As String = Me.getDeviceStatus(tstat, "HSREF_HEAT", True, "")
			Dim deviceStatus3 As String = Me.getDeviceStatus(tstat, "HSREF_COOL", True, "")
			Dim str3 As String = Me.getDeviceStatus(tstat, "HSREF_MODE", False, "")
			Dim deviceStatus4 As String = Me.getDeviceStatus(tstat, "HSREF_FAN", False, "")
			Dim str4 As String = Me.getDeviceStatus(tstat, "HSREF_HOLD", False, "")
			Dim deviceStatus5 As String = Me.getDeviceStatus(tstat, "HSREF_PROGRAM", False, "")
			Dim str5 As String = ""
			Dim deviceStatus6 As String = ""
			Dim str6 As String = ""
			Dim deviceStatus7 As String = ""
			Dim str7 As String = ""
			Dim deviceStatus8 As String = ""
			Dim str8 As String = ""
			Dim str9 As String = ""
			Dim str10 As String = ""
			Dim hvac As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetHvac(str1)
			If (hvac IsNot Nothing) Then
				str5 = Conversions.ToString(hvac("Name"))
				deviceStatus6 = Me.getDeviceStatus(hvac, "HSREF_MODE", False, "")
				str6 = Me.getDeviceStatus(hvac, "HSREF_FAN", False, "")
				deviceStatus7 = Me.getDeviceStatus(hvac, "HSREF_HEAT", False, "")
				str7 = Me.getDeviceStatus(hvac, "HSREF_COOL", False, "")
				deviceStatus8 = Me.getDeviceStatus(hvac, "HSREF_MAINT", False, "")
				str8 = Me.BuildButton(String.Concat(str, "_RESETHEAT"), "Reset", False, False)
				str9 = Me.BuildButton(String.Concat(str, "_RESETCOOL"), "Reset", False, False)
				str10 = Me.BuildButton(String.Concat(str, "_RESETMAINT"), "Reset", False, False)
			End If
			stringBuilder.Append(String.Concat("<table><tr><td>Master Program:</td><td>", Me.BuildDropList(String.Concat(str, "_MASTER"), HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetUniqueProgramNames(), cAPIStatu.Status, Nothing, False, False), "</td></tr></table>" & VbCrLf & ""))
			stringBuilder.Append("<table border='1'>" & VbCrLf & "")
			stringBuilder.Append("  <tr>" & VbCrLf & "")
			stringBuilder.Append("    <th class='tablecolumn' colspan='9'>Thermostat</td><th class='tablecolumn' colspan='6'>HVAC</td>" & VbCrLf & "")
			stringBuilder.Append("  </tr>" & VbCrLf & "")
			stringBuilder.Append("  <tr>" & VbCrLf & "")
			stringBuilder.Append("    <td colspan='5'></td><td colspan='4'>")
			stringBuilder.Append(String.Concat("<table><tr><td>Program:</td><td>", Me.BuildDropList(String.Concat(str, "_PROGRAM"), HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetUniqueProgramNames(str1), deviceStatus5, Nothing, False, False), "</td></tr></table>"))
			stringBuilder.Append("    </td><td colspan='6'></td>")
			stringBuilder.Append("  </tr>" & VbCrLf & "")
			stringBuilder.Append("  <tr>" & VbCrLf & "")
			stringBuilder.Append("    <td>Name</td><td>Temp</td><td>Ext.Temp</td><td>Humidity</td><td>Run/Hold</td><td>Mode</td><td>Fan</td><td>Heat SP</td><td>Cool SP</td><td>Name</td><td>Mode</td><td>Fan</td><td>Heat Time</td><td>Cool Time</td><td>Maint Time</td>" & VbCrLf & "")
			stringBuilder.Append("  </tr>" & VbCrLf & "")
			stringBuilder.Append("  <tr>" & VbCrLf & "")
			stringBuilder.Append(String.Concat("    <td>", str1, "</td>"))
			stringBuilder.Append(String.Concat("    <td>", deviceStatus, "</td>"))
			stringBuilder.Append(String.Concat("    <td>", deviceStatus1, "</td>"))
			stringBuilder.Append(String.Concat("    <td>", deviceStatus2, "</td>"))
			stringBuilder.Append(String.Concat("    <td>", Me.BuildDropList(String.Concat(str, "_HOLD"), HSPI_INSTEON_THERMOSTAT.utils.HoldOptsStatus, str4, Nothing, False, False), "</td>"))
			If (Not HSPI_INSTEON_THERMOSTAT.utils.isSmarthomeDEVCAT(tstat)) Then
				stringBuilder.Append(String.Concat("    <td>", Me.BuildDropList(String.Concat(str, "_MODE"), HSPI_INSTEON_THERMOSTAT.utils.ModeOptsStatusVenstar, str3, Nothing, False, False), "</td>"))
			Else
				stringBuilder.Append(String.Concat("    <td>", Me.BuildDropList(String.Concat(str, "_MODE"), HSPI_INSTEON_THERMOSTAT.utils.ModeOptsStatusSmarthome, str3, Nothing, False, False), "</td>"))
			End If
			stringBuilder.Append(String.Concat("    <td>", Me.BuildDropList(String.Concat(str, "_FAN"), HSPI_INSTEON_THERMOSTAT.utils.FanOptsStatus, deviceStatus4, Nothing, False, False), "</td>"))
			stringBuilder.Append(String.Concat("    <td>", Me.BuildDropList(String.Concat(str, "_HEATSP"), HSPI_INSTEON_THERMOSTAT.utils.myConfig.BuildHeatSetPointRange(), str2, Nothing, False, False), "</td>"))
			stringBuilder.Append(String.Concat("    <td>", Me.BuildDropList(String.Concat(str, "_COOLSP"), HSPI_INSTEON_THERMOSTAT.utils.myConfig.BuildCoolSetPointRange(), deviceStatus3, Nothing, False, False), "</td>"))
			stringBuilder.Append(String.Concat("    <td>", str5, "</td>"))
			stringBuilder.Append(String.Concat("    <td>", deviceStatus6, "</td>"))
			stringBuilder.Append(String.Concat("    <td>", str6, "</td>"))
			Dim strArrays() As String = { "    <td>  <table><tr><td>", deviceStatus7, "</td></tr><tr><td>", str8, "</td></tr></table>  </td>" }
			stringBuilder.Append(String.Concat(strArrays))
			strArrays = New String() { "    <td>  <table><tr><td>", str7, "</td></tr><tr><td>", str9, "</td></tr></table>  </td>" }
			stringBuilder.Append(String.Concat(strArrays))
			strArrays = New String() { "    <td>  <table><tr><td>", deviceStatus8, "</td></tr><tr><td>", str10, "</td></tr></table>  </td>" & VbCrLf & "" }
			stringBuilder.Append(String.Concat(strArrays))
			stringBuilder.Append("  </tr>" & VbCrLf & "")
			stringBuilder.Append("</table>" & VbCrLf & "")
			Return stringBuilder.ToString()
		End Function

		Private Function getDeviceStatus(ByVal tstatHvac As Collection, ByVal devString As String, ByVal devAsValue As Boolean, Optional ByVal retVal As String = "") As String
			Dim str As String
			Try
				Dim cAPIStatu As CAPI.CAPIStatus = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetStatus(Conversions.ToInteger(tstatHvac(devString))), CAPI.CAPIStatus)
				str = If(Not devAsValue, cAPIStatu.Status, Conversions.ToString(cAPIStatu.Value))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = retVal
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String
			Dim str As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim str1 As String = ""
			Try
				Me.reset()
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
				Me.set_RefreshIntervalMilliSeconds(HSPI_INSTEON_THERMOSTAT.utils.myConfig.ControlPageRefresh * 1000)
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
				Dim str2 As String = String.Concat("WSGPP: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str2, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str2
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Overrides Function postBackProc(ByVal page As String, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As String
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim chrArray As Char()
			Try
				If (userRights <= 1) Then
					Throw New System.Exception("Guests are not authorized to access this feature")
				End If
				Dim nameValueCollection As System.Collections.Specialized.NameValueCollection = HttpUtility.ParseQueryString(data)
				Dim item As String = nameValueCollection("id")
				If (Operators.CompareString(item, "timer", False) <> 0) Then
					Me.pageCommands.Add("stoptimer", "")
					If (item.StartsWith("wsButton_TSTAT")) Then
						chrArray = New Char() { Strings.ChrW(95) }
						Dim [integer] As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
						chrArray = New Char() { Strings.ChrW(95) }
						Dim str As String = item.Split(chrArray)(3)
						Dim tstatByNum As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum([integer])
						Dim str1 As String = String.Concat("TabTstat_", Conversions.ToString([integer]), "_div")
						If (tstatByNum Is Nothing) Then
							Me.divToUpdate.Add("errormessage", String.Concat("Unable to locate TSTAT by index ", Conversions.ToString([integer])))
						Else
							Dim str2 As String = str
							If (Operators.CompareString(str2, "RESETHEAT", False) = 0) Then
								HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByName(Conversions.ToString(tstatByNum("HVACUnit")), "HSREF_HEAT", "Heat", 0, True)
								Me.divToUpdate.Add(str1, Me.BuildTstatHvac(tstatByNum, [integer]))
							ElseIf (Operators.CompareString(str2, "RESETCOOL", False) = 0) Then
								HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByName(Conversions.ToString(tstatByNum("HVACUnit")), "HSREF_COOL", "Cool", 0, True)
								Me.divToUpdate.Add(str1, Me.BuildTstatHvac(tstatByNum, [integer]))
							ElseIf (Operators.CompareString(str2, "RESETMAINT", False) <> 0) Then
								Me.divToUpdate.Add("errormessage", String.Concat("Unknown TSTAT button function: ", str))
							Else
								HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByName(Conversions.ToString(tstatByNum("HVACUnit")), "HSREF_MAINT", "MaintenanceInterval", 0, True)
								Me.divToUpdate.Add(str1, Me.BuildTstatHvac(tstatByNum, [integer]))
							End If
						End If
					ElseIf (Not item.StartsWith("wsDropList_TSTAT")) Then
						Me.divToUpdate.Add("errormessage", String.Concat("web_status - can't understand postBack=", data))
					Else
						Dim str3 As String = item.Substring(item.IndexOf("_") + 1)
						chrArray = New Char() { Strings.ChrW(95) }
						Dim num As Integer = Conversions.ToInteger(item.Split(chrArray)(2))
						chrArray = New Char() { Strings.ChrW(95) }
						Dim str4 As String = item.Split(chrArray)(3)
						Dim collections As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(num)
						Dim str5 As String = Conversions.ToString(collections("Name"))
						String.Concat("TabTstat_", Conversions.ToString(num), "_div")
						If (collections Is Nothing) Then
							Me.divToUpdate.Add("errormessage", String.Concat("Unable to locate TSTAT by index ", Conversions.ToString(num)))
						Else
							Dim str6 As String = str4
							If (Operators.CompareString(str6, "MASTER", False) = 0) Then
								Dim item1 As String = nameValueCollection(str3)
								Dim str7 As String = Main.oPlugin.SetMasterProgram(item1, True)
								If (str7 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str7)
								End If
							ElseIf (Operators.CompareString(str6, "PROGRAM", False) = 0) Then
								Dim item2 As String = nameValueCollection(str3)
								Dim str8 As String = Main.oPlugin.SetProgram(str5, item2, True)
								If (str8 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str8)
								End If
							ElseIf (Operators.CompareString(str6, "MODE", False) = 0) Then
								Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.gModeOptsByName(nameValueCollection(str3))
								Dim str9 As String = Main.oPlugin.SetMode(str5, num1)
								If (str9 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str9)
								End If
							ElseIf (Operators.CompareString(str6, "FAN", False) = 0) Then
								Dim num2 As Integer = HSPI_INSTEON_THERMOSTAT.utils.gFanOptsByName(nameValueCollection(str3))
								Dim str10 As String = Nothing
								Select Case num2
									Case 0
										str10 = Main.oPlugin.SetFanAuto(str5)
										Exit Select
									Case 1
										str10 = Main.oPlugin.SetFanOn(str5)
										Exit Select
									Case 2
										str10 = Main.oPlugin.SetFanToggle(str5)
										Exit Select
								End Select
								If (str10 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str10)
								End If
							ElseIf (Operators.CompareString(str6, "HEATSP", False) = 0) Then
								Dim str11 As String = Main.oPlugin.SetHeatSetpoint(str5, CShort(Conversions.ToInteger(nameValueCollection(str3))))
								If (str11 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str11)
								End If
							ElseIf (Operators.CompareString(str6, "COOLSP", False) = 0) Then
								Dim str12 As String = Main.oPlugin.SetCoolSetpoint(str5, CShort(Conversions.ToInteger(nameValueCollection(str3))))
								If (str12 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str12)
								End If
							ElseIf (Operators.CompareString(str6, "HOLD", False) <> 0) Then
								Me.divToUpdate.Add("errormessage", String.Concat("Unknown TSTAT droplist function: ", str4))
							Else
								Dim item3 As Integer = HSPI_INSTEON_THERMOSTAT.utils.gHoldOptsByName(nameValueCollection(str3))
								Dim str13 As String = Nothing
								Select Case item3
									Case 0
										str13 = Main.oPlugin.SetRun(str5)
										Exit Select
									Case 1
										str13 = Main.oPlugin.SetHold(str5)
										Exit Select
									Case 2
										str13 = Main.oPlugin.SetHoldToggle(str5)
										Exit Select
								End Select
								If (str13 IsNot Nothing) Then
									Me.divToUpdate.Add("errormessage", str13)
								End If
							End If
						End If
					End If
					Me.pageCommands.Add("starttimer", "")
				Else
					Dim num3 As Integer = 1
					Try
						enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
						While enumerator.MoveNext()
							Dim current As Collection = enumerator.Current
							Dim str14 As String = String.Concat("TabTstat_", Conversions.ToString(num3), "_div")
							Me.divToUpdate.Add(str14, Me.BuildTstatHvac(current, num3))
							num3 = num3 + 1
						End While
					Finally
						(DirectCast(enumerator, IDisposable)).Dispose()
					End Try
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Dim str15 As String = String.Concat("WSPBP: ", exception.Message)
				Me.divToUpdate.Add("errormessage", str15)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str15, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return MyBase.postBackProc(page, data, user, userRights)
		End Function
	End Class
End Namespace