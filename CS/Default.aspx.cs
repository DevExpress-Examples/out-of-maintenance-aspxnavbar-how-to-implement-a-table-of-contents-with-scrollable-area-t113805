using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (string.IsNullOrEmpty(Request.QueryString.ToString()))
            Response.Redirect(Request.RawUrl + "?product=aspnet");

        var document = GetDataSource();
        PopulateControls(document);        
    }

    private XmlDocument GetDataSource() {
        var doc = new XmlDocument();
        doc.Load(Server.MapPath("~/App_Data/Data.xml"));
        return doc;
    }

    private void PopulateControls(XmlDocument doc) {
        var root = doc.SelectSingleNode("items");
        //add groups
        var nodes = root.SelectNodes("group");
        foreach (XmlNode group in nodes) {
            var name = group.Attributes["Name"].Value;
            var key = group.Attributes["Key"].Value;
            AddNavBarItemGroup(name, null, key, true);

            //add group items
            var groupItems = group.SelectNodes("item");
            foreach (XmlNode groupContent in groupItems) {
                var contentName = groupContent.Attributes["Name"].Value;
                var description = groupContent.InnerText;
                AddNavBarItemGroup(contentName, description, key, false, name);
            }
        }
    }
    protected void AddNavBarItemGroup(string text, string description, string key, bool isGroup = false, string groupName = null) {
        if (isGroup) {
            var navItem = new NavBarGroup();
            navItem.Name = text;
            navItem.Text = text;
            navItem.NavigateUrl = "?product=" + key;
            if (key == Request.QueryString["product"])
                ItemsSelector.JSProperties["cpActiveGroup"] = text;
            ItemsSelector.Groups.Add(navItem);
        } else {
            var group = ItemsSelector.Groups.FindByName(groupName);
            var item = new NavBarItem();
            item.Text = text;
            group.Items.Add(item);

            if (key == Request.QueryString["product"])
                AddContentSectionItem(text, description);
        }
    }

    protected void AddContentSectionItem(string text, string description) {
        var div = new HtmlGenericControl("div");
        div.Attributes["class"] = "section";
        contentContainer.Controls.Add(div);

        //header
        var header = new HtmlGenericControl("h3");
        header.InnerText = text;
        div.Controls.Add(header);

        //content
        var content = new HtmlGenericControl("div");
        content.InnerHtml = description;
        div.Controls.Add(content);
    }
}