Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler
Imports System
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Web

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class web_help
		Inherits PageBuilderAndMenu.clsPageBuilder
		Public Sub New(ByVal pagename As String)
			MyBase.New(pagename)
		End Sub

		Public Function BuildContent() As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim strArrays As String() = File.ReadAllLines(String.Concat(HSPI_INSTEON_THERMOSTAT.utils.hs.GetAppPath(), HSPI_INSTEON_THERMOSTAT.utils.sHelpFile))
			Dim num As Integer = 0
			Do
				stringBuilder.Append(strArrays(num))
				num = num + 1
			Loop While num < CInt(strArrays.Length)
			Return stringBuilder.ToString()
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
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivStart("help_content_div", ""))
				stringBuilder.Append(Me.BuildContent())
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivEnd())
				stringBuilder.Append(PageBuilderAndMenu.clsPageBuilder.DivEnd())
				stringBuilder.Append("<HR/>")
				Me.AddFooter(HSPI_INSTEON_THERMOSTAT.utils.hs.GetPageFooter(False))
				Me.suppressDefaultFooter = True
				Me.AddBody(stringBuilder.ToString())
				str = Me.BuildPage()
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("error - ", Information.Err().Description)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Overrides Function postBackProc(ByVal page As String, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As String
			Try
				Dim item As String = HttpUtility.ParseQueryString(data)("id")
				If (Operators.CompareString(item, "timer", False) <> 0) Then
					Me.pageCommands.Add("stoptimer", "")
					If (Not item.StartsWith("wsButton_ArmArea")) Then
						If (Not item.StartsWith("wsButton_DisarmArea")) Then
							Me.divToUpdate.Add("errormessage", String.Concat("web_help - can't understand postBack=", data))
						End If
					End If
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Dim str As String = String.Concat("Error processing web help postBackProc - ", exception.Message)
				Me.divToUpdate.Add("errormessage", str)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return MyBase.postBackProc(page, data, user, userRights)
		End Function
	End Class
End Namespace