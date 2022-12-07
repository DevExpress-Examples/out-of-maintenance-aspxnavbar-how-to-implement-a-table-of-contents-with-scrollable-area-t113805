Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Xml
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If String.IsNullOrEmpty(Request.QueryString.ToString()) Then
			Response.Redirect(Request.RawUrl & "?product=aspnet")
		End If

		Dim document = GetDataSource()
		PopulateControls(document)
	End Sub

	Private Function GetDataSource() As XmlDocument
		Dim doc = New XmlDocument()
		doc.Load(Server.MapPath("~/App_Data/Data.xml"))
		Return doc
	End Function

	Private Sub PopulateControls(ByVal doc As XmlDocument)
		Dim root = doc.SelectSingleNode("items")
		'add groups
		Dim nodes = root.SelectNodes("group")
		For Each group As XmlNode In nodes
			Dim name = group.Attributes("Name").Value
			Dim key = group.Attributes("Key").Value
			AddNavBarItemGroup(name, Nothing, key, True)

			'add group items
			Dim groupItems = group.SelectNodes("item")
			For Each groupContent As XmlNode In groupItems
				Dim contentName = groupContent.Attributes("Name").Value
				Dim description = groupContent.InnerText
				AddNavBarItemGroup(contentName, description, key, False, name)
			Next groupContent
		Next group
	End Sub
	Protected Sub AddNavBarItemGroup(ByVal text As String, ByVal description As String, ByVal key As String, Optional ByVal isGroup As Boolean = False, Optional ByVal groupName As String = Nothing)
		If isGroup Then
			Dim navItem = New NavBarGroup()
			navItem.Name = text
			navItem.Text = text
			navItem.NavigateUrl = "?product=" & key
			If key = Request.QueryString("product") Then
				ItemsSelector.JSProperties("cpActiveGroup") = text
			End If
			ItemsSelector.Groups.Add(navItem)
		Else
			Dim group = ItemsSelector.Groups.FindByName(groupName)
			Dim item = New NavBarItem()
			item.Text = text
			group.Items.Add(item)

			If key = Request.QueryString("product") Then
				AddContentSectionItem(text, description)
			End If
		End If
	End Sub

	Protected Sub AddContentSectionItem(ByVal text As String, ByVal description As String)
		Dim div = New HtmlGenericControl("div")
		div.Attributes("class") = "section"
		contentContainer.Controls.Add(div)

		'header
		Dim header = New HtmlGenericControl("h3")
		header.InnerText = text
		div.Controls.Add(header)

		'content
		Dim content = New HtmlGenericControl("div")
		content.InnerHtml = description
		div.Controls.Add(content)
	End Sub
End Class