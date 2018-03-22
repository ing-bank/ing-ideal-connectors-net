using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ING.iDealSample.Custom
{
    [ToolboxData("<{0}:GroupedDropDownList runat=server></{0}:GroupedDropDownList>")]
    public class GroupedDropDownList : DropDownList
    {
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.Items.Count > 0)
            {
                bool selected = false;
                bool optGroupStarted = false;
                string lastOptionGroup = string.Empty;
                for (int i = 0; i < this.Items.Count; i++)
                {
                    var item = this.Items[i];
                    if (item.Enabled)
                    {
                        if (item.Text.Contains('|'))
                        {
                            if (lastOptionGroup != item.Text.Split('|')[1])
                            {
                                if (optGroupStarted)
                                {
                                    writer.WriteEndTag("optgroup");
                                }
                                lastOptionGroup = item.Text.Split('|')[1];
                                writer.WriteBeginTag("optgroup");
                                writer.WriteAttribute("label", lastOptionGroup);
                                writer.Write('>');
                                writer.WriteLine();
                                optGroupStarted = true;
                            }
                            writer.WriteBeginTag("option");
                            if (item.Selected)
                            {
                                if (selected)
                                {
                                    this.VerifyMultiSelect();
                                }
                                selected = true;
                                writer.WriteAttribute("selected", "selected");
                            }
                            writer.WriteAttribute("value", item.Value, true);
                            if (item.Attributes.Count > 0)
                            {
                                item.Attributes.Render(writer);
                            }
                            if (this.Page != null)
                            {
                                this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, item.Value);
                            }
                            writer.Write('>');
                            HttpUtility.HtmlEncode(item.Text.Split('|')[0], writer);
                            writer.WriteEndTag("option");
                            writer.WriteLine();
                        }
                        else
                        {
                            writer.WriteBeginTag("option");
                            if (item.Selected)
                            {
                                if (selected)
                                {
                                    this.VerifyMultiSelect();
                                }
                                selected = true;
                                writer.WriteAttribute("selected", "selected");
                            }
                            writer.WriteAttribute("value", item.Value, true);
                            if (item.Attributes.Count > 0)
                            {
                                item.Attributes.Render(writer);
                            }
                            if (this.Page != null)
                            {
                                this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, item.Value);
                            }
                            writer.Write('>');
                            HttpUtility.HtmlEncode(item.Text, writer);
                            writer.WriteEndTag("option");
                            writer.WriteLine();
                        }
                    }
                }
                if (optGroupStarted)
                {
                    writer.WriteEndTag("optgroup");
                }

            }

        }
    }
}


