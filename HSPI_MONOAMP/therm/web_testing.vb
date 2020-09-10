Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler
Imports System
Imports System.Collections.Specialized
Imports System.Text
Imports System.Web

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class web_testing
		Inherits PageBuilderAndMenu.clsPageBuilder
		Private myTstatNum As Integer

		Private extRelayNum As Integer

		Public Sub New(ByVal pagename As String)
			MyBase.New(pagename)
			Me.myTstatNum = 1
			Me.extRelayNum = 1
		End Sub

		Private Function BuildButton(ByVal Name As String, ByVal ButtonText As String, Optional ByVal SubmitForm As Boolean = False, Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_testing::BuildButton(System.String,System.String,System.Boolean,System.Boolean)
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
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_testing::BuildCheckBox(System.String,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean)
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
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append(" <table border='1'><tr style='vertical-align:top;'><td>")
			stringBuilder.Append(" <table border='0'>")
			stringBuilder.Append("  <tr><th class='tablecolumn'>Test Controls</th></tr>")
			stringBuilder.Append("  <tr>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("LogDeviceList", "Log All HS Devices", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("LogThermostats", "Log Thermostats", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("LogPrograms", "Log Programs", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("LogHVACs", "Log HVACs", False, False), "</td>"))
			stringBuilder.Append("  </tr>")
			stringBuilder.Append("  <tr><td colspan='7'><HR/></td></tr>")
			stringBuilder.Append("  <tr>")
			stringBuilder.Append(String.Concat("    <td>Choose Thermostat number", Me.BuildTextBox("ChooseThermostat", Conversions.ToString(Me.myTstatNum), False), "</td>"))
			stringBuilder.Append("  </tr>")
			stringBuilder.Append("  <tr><td colspan='7'><HR/></td></tr>")
			stringBuilder.Append("  <tr>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("PollMode", "Poll Mode Tstat", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("PollHum", "Poll Humidity Tstat", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("PollTemp", "Poll Temp Tstat", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("PollSet", "Poll Set Tstat", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append("  </tr>")
			stringBuilder.Append("  <tr><td>&nbsp;</td></tr>")
			stringBuilder.Append("  <tr>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("ClearLinksOnTstat", "Clear Links Tstat", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("RemoveFromPLMTstat", "Remove from PLM Tstat", False, False), "</td>"))
			stringBuilder.Append("    <td>&nbsp;</td>")
			stringBuilder.Append(String.Concat("    <td>", Me.BuildButton("UnregisterTstat", "Unregister Tstat", False, False), "</td>"))
			stringBuilder.Append("  </tr>")
			stringBuilder.Append(" </table>")
			stringBuilder.Append(" <hr/>")
			stringBuilder.Append(" <table border='0'>")
			stringBuilder.Append("  <tr><td><div id='message'> ... watch here for status updates ...</div></td></tr></td></tr><tr><td>&nbsp;</td></tr>")
			stringBuilder.Append(" </table>")
			stringBuilder.Append(" </table>")
			Return stringBuilder.ToString()
		End Function

		Private Function BuildTextBox(ByVal TextBoxId As String, Optional ByVal TextBoxDefault As String = "", Optional ByVal Rebuilding As Boolean = False) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.web_testing::BuildTextBox(System.String,System.String,System.Boolean)
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
				Dim str2 As String = String.Concat("WTGPP: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str2, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str2
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Overrides Function postBackProc(ByVal page As String, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As String
			Dim nameValueCollection As System.Collections.Specialized.NameValueCollection = HttpUtility.ParseQueryString(data)
			Try
				If (userRights <= 1) Then
					Throw New System.Exception("Guests are not authorized to access this feature")
				End If
				Dim item As String = nameValueCollection("id")
				If (Operators.CompareString(item, "wcTextBox_ChooseThermostat", False) = 0) Then
					Dim obj As Object = Nothing
					Try
						obj = nameValueCollection("ChooseThermostat")
						Me.myTstatNum = Conversions.ToInteger(obj)
					Catch exception1 As System.Exception
						ProjectData.SetProjectError(exception1)
						Dim exception As System.Exception = exception1
						Me.divToUpdate.Add("errormessage", Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Bad thermostat index: ", obj), " : "), exception.Message)))
						ProjectData.ClearProjectError()
					End Try
				ElseIf (Operators.CompareString(item, "wcButton_ClearLinksOnTstat", False) = 0) Then
					Dim insteon As HSPI_INSTEON_THERMOSTAT.Insteon = HSPI_INSTEON_THERMOSTAT.utils.myInsteon
					Dim tstatByNum As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum)
					insteon.clearLinks(tstatByNum)
				ElseIf (Operators.CompareString(item, "wcButton_RemoveFromPLMTstat", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.RemoveFromPLM(HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum))
				ElseIf (Operators.CompareString(item, "wcButton_UnregisterTstat", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UnregisterTstat(HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum))
				ElseIf (Operators.CompareString(item, "wcButton_PollMode", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum), True, False, False, False)
				ElseIf (Operators.CompareString(item, "wcButton_PollHum", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum), False, False, False, True)
				ElseIf (Operators.CompareString(item, "wcButton_PollTemp", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum), False, True, False, False)
				ElseIf (Operators.CompareString(item, "wcButton_PollSet", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatByNum(Me.myTstatNum), False, False, True, False)
				ElseIf (Operators.CompareString(item, "wcButton_LogThermostats", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogThermostats(64)
				ElseIf (Operators.CompareString(item, "wcButton_LogPrograms", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogPrograms(64)
				ElseIf (Operators.CompareString(item, "wcButton_LogHVACs", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogHVACs(64)
				ElseIf (Operators.CompareString(item, "wcButton_LogDeviceList", False) <> 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Unknown value submitted from web testing page! ", nameValueCollection("id")), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					HSPI_INSTEON_THERMOSTAT.utils.LogDeviceList(True, True, True)
				End If
			Catch exception2 As System.Exception
				ProjectData.SetProjectError(exception2)
				Dim str As String = String.Concat("WTPBP: ", exception2.Message)
				Me.divToUpdate.Add("errormessage", str)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return MyBase.postBackProc(page, data, user, userRights)
		End Function
	End Class
End Namespace